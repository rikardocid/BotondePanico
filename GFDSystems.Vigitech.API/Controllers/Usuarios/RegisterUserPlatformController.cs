using GFDSystems.Vigitech.API.Tools.Services;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAO.Models;
using GFDSystems.Vigitech.DAO.Register;
using GFDSystems.Vigitech.DAO.Tools.Email;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserPlatformController : ControllerBase
    {
        private readonly IEmailSender _emailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IUserToolkitRepository _userToolkitRepository;
        private readonly IRegisterRepository _registerRepository;

        public RegisterUserPlatformController(IEmailSender emailService, IViewRenderService viewRenderService, IUserToolkitRepository userToolkitRepository, IRegisterRepository registerRepository)
        {
            _emailService = emailService;
            _viewRenderService = viewRenderService;
            _userToolkitRepository = userToolkitRepository;
            _registerRepository = registerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _registerRepository.GetAllUsers();
            return Ok(response.Value);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserPlatformDAO registerUser)
        {
            var GenerateUser = _userToolkitRepository.GenerateUser(registerUser.RoleName);
            var password = _userToolkitRepository.GenerateUserPaasword();
            if (GenerateUser == null)
            {
                return StatusCode(500, new Response { Status = "Error", Message = "No se pudo generar el usuario, volver a intentar mas tarde" });
            }
            var response = await _registerRepository.RegisterUserPlatform(registerUser, GenerateUser[0], password, GenerateUser[1]);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var html = await _viewRenderService.RenderToStringAsync("Register/EmailTemplateUserPlatform", response.Value);
                await _emailService.SendEmailAsync(response.Value.Email, html, "Email Verification");
                return Ok(new Response { Status = "OK", Message = "Se ha enviado un correo electrónico favor de verificar para poder contitnuar con el registro " });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new Response { Status = "Error", Message = response.Message });
            }
        }
    
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateInformationFromUserDAO update)
        {
            var response = await _registerRepository.UpdateUser(update);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new Response { Status = response.StatusCode.ToString(), Message = response.Message });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new Response { Status = response.StatusCode.ToString(), Message = response.Message });
            }
        }
    }
}

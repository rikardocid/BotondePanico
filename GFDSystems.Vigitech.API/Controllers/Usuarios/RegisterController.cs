using GFDSystems.Vigitech.API.ResponseExamples;
using GFDSystems.Vigitech.API.Tools.Services;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Models;
using GFDSystems.Vigitech.DAO.Register;
using GFDSystems.Vigitech.DAO.Tools.Email;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class RegisterController : ControllerBase
    {
        private readonly IEmailSender _emailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IRegisterRepository _registerRepository;
        private readonly ICustomSystemLog _customSystemLog;
        private readonly IUserToolkitRepository _userToolkitRepository;

        public RegisterController(IEmailSender emailService, IViewRenderService viewRenderService, IRegisterRepository registerRepository, ICustomSystemLog customSystemLog, IUserToolkitRepository userToolkitRepository)
        {
            _emailService = emailService;
            _viewRenderService = viewRenderService;
            _registerRepository = registerRepository;
            _customSystemLog = customSystemLog;
            _userToolkitRepository = userToolkitRepository;
        }


        /// <summary>
        /// Primer paso para el registro del ciudadano desde la app en la cual obtendrá los datos del dispositivo para validar su registro \n
        /// en la cual obtendra la información basica de 
        /// </summary>
        /// <param name="pre"></param>
        /// <returns></returns>
        [HttpPost("PreRegister")]
        [ProducesResponseType(typeof(Response), 200)]
        [SwaggerResponseExample(200, typeof(SuccessResponseFirstRegister))]
        public async Task<IActionResult> PreRegister([FromBody] PreRegisterDAO pre)
        {
            try
            {
                var response = await _registerRepository.FirstRegister(pre);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var html = await _viewRenderService.RenderToStringAsync("Register/Emailtemplate", response.Value);
                    await _emailService.SendEmailAsync(pre.Email, html, "Email Verification");
                    return Ok(new Response { Status = "OK", Message = "Se ha enviado un correo electrónico favor de verificar para poder contitnuar con el registro " });
                }
                else
                {
                    return StatusCode((int)response.StatusCode, new Response { Status = "Error", Message = response.Message });
                }
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog
                {
                    Description = e.ToMessageAndCompleteStacktrace(),
                    DateLog = DateTime.UtcNow.ToLocalTime(),
                    Controller = GetType().Name,
                    Action = UtilitiesAIO.GetCallerMemberName(),
                    Parameter = JsonConvert.SerializeObject(pre)
                };
                _customSystemLog.AddLog(systemLog);
                return StatusCode(500, new Response { Status = "Error", Message = systemLog.Description });
            }
           

        }


        /// <summary>
        /// Segundo paso para la verificación en 2 pasos después de obtener el código que se envió al correo electrónico 
        /// </summary>
        /// <param name="Code">Código que se envió en correo</param>
        /// <param name="DeviceId">Identificador unico del dispositivo</param>
        /// <returns>Dentro de esa respuesta esta API-KEY para enviarla en el login y se active</returns>
        [HttpPost("VerifyRegisterCode/{Code}/{DeviceId}")]
        [ProducesResponseType(typeof(object), 200)]
        [SwaggerResponseExample(200, typeof(SuccessResponseVerifyCode))]
        [ProducesResponseType(typeof(Response), 400)]
        [SwaggerResponseExample(400, typeof(BadResponseVerifyCode))]
        public async Task<IActionResult> VerifyRegisterCode([FromRoute] string Code, [FromRoute] string DeviceId)
        {
            var response = await _registerRepository.VerifyRegisterCode(Code, DeviceId);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new { StatusCode = System.Net.HttpStatusCode.OK, Message = response.Message, apiKey = response.Value });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new Response { Status = "Error", Message = response.Message });
            }
        }

        /// <summary>
        /// Tercer paso para completar el registro con la información completa del ciudadano 
        /// </summary>
        /// <param name="complete"></param>
        /// <returns></returns>
        [HttpPost("CompleteRegister")]
        [ProducesResponseType(typeof(Response), 200)]
        [SwaggerResponseExample(200, typeof(SuccessResponseCompleteRegister))]
        [ProducesResponseType(typeof(Response), 400)]
        [SwaggerResponseExample(400, typeof(BadResponseCompleteRegister))]
        public async Task<IActionResult> CompleteRegister([FromBody] CompleteRegisterDAO complete)
        {
            var response = await _registerRepository.CompleteRegister(complete);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new Response { Status = response.StatusCode.ToString(), Message = response.Message });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new Response { Status = "Error", Message = response.Message });
            }
        }

    }
}

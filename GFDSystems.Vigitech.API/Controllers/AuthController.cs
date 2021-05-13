using GFDSystems.Vigitech.API.Tools.Security;
using GFDSystems.Vigitech.API.Tools.Services;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAO.Auth;
using GFDSystems.Vigitech.DAO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;using System.Net.Mime;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiKey]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthController( IEmailService emailService, IViewRenderService viewRenderService, IAuthenticationRepository authenticationRepository)
        {
            _emailService = emailService;
            _viewRenderService = viewRenderService;
            _authenticationRepository = authenticationRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDAO login)
        {
            var response = await _authenticationRepository.Login(login.UserName, login.Password);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new Response { Status = response.StatusCode.ToString(), Message = response.Message });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new Response { Status = "Error", Message = response.Message });
            }
        }
        //[HttpPost("Login/{IsMobile?}")]
        //public async Task<IActionResult> Login([FromBody] LoginVM login, [FromQuery] bool IsMobile = false)
        //{

        //    return Ok();
        //}
    }
}

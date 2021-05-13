using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.Auth.RoleManager;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers.Manager
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class AppRolesController : ControllerBase
    {
        private readonly IRoleManagerRepository _roleManagerRepository;

        public AppRolesController(IRoleManagerRepository roleManagerRepository)
        {
            _roleManagerRepository = roleManagerRepository;
        }

        // GET: api/AppRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppRole>>> GetRoles()
        {
            return await _roleManagerRepository.GetAll();
        }

        // GET: api/AppRoles/5
        [HttpGet("RoleById/{id}")]
        public async Task<ActionResult<AppRole>> GetAppRoleById(int id)
        {
            var response = await _roleManagerRepository.GetById(id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response.Value);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Message);
            }
        }

        [HttpGet("RoleByName/{RoleName}")]
        public async Task<ActionResult<AppRole>> GetAppRoleByName(string RoleName)
        {
            var response = await _roleManagerRepository.GetByName(RoleName);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response.Value);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Message);
            }
        }

        // PUT: api/AppRoles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/{RoleName}")]
        public async Task<IActionResult> PutAppRole(int id, string RoleName)
        {
            var response = await _roleManagerRepository.Update(id, RoleName);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response);
            }
        }

        // POST: api/AppRoles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{RoleName}")]
        public async Task<IActionResult> PostAppRole([FromRoute] string RoleName)
        {
            var response = await _roleManagerRepository.Create(RoleName);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response);
            }
        }

        
    }
}

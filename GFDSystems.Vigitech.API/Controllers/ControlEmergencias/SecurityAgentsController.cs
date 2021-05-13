using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAL.Responses;

namespace GFDSystems.Vigitech.API.Controllers.ControlEmergencias
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityAgentsController : ControllerBase
    {
        private readonly ISecurityAgentDAO _securityAgentDAO;
        public SecurityAgentsController(ISecurityAgentDAO securityAgentDAO)
        {
            _securityAgentDAO = securityAgentDAO;
        }
        // GET: api/SecurityAgents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentResponse>>> GetsecurityAgents()
        {
            return await _securityAgentDAO.GetAll().ToListAsync();
        }
        // GET: api/SecurityAgents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecurityAgent>> GetSecurityAgent(int id)
        {
            if (await _securityAgentDAO.ExistAsync(id))
            {
                var citizen = await _securityAgentDAO.GetByIdAsync(id);
                return citizen;
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde a un registro" });
            }
        }
        // PUT: api/SecurityAgents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityAgent(int id, SecurityAgent securityAgent)
        {
            if (id != securityAgent.SecurityAgentId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no pertenece al registro que intenta actualizar" });
            }
            try
            {
                await _securityAgentDAO.UpdateAsync(securityAgent);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _securityAgentDAO.ExistAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no pertenece aNingun ciudadano registrado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Verifique que los datos sean correctos" });
                }
            }
        }
        // POST: api/SecurityAgents
        [HttpPost]
        public async Task<ActionResult<SecurityAgent>> PostSecurityAgent(SecurityAgent securityAgent)
        {
            if (ModelState.IsValid)
            {
                await _securityAgentDAO.CreateAsync(securityAgent);
                return Ok(new Response { Status = "OK", Message = "Se ha registrado correctamente" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de Verificar que los campos sean correctos" });
            }
        }
        // DELETE: api/SecurityAgents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SecurityAgent>> DeleteSecurityAgent(int id)
        {
            var securityAgent = await _securityAgentDAO.GetByIdAsync(id);
            if (securityAgent == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El Id que intentamos eliminar no existe, favor de verificar"
                });
            }
            try
            {
                await _securityAgentDAO.DeleteAsync(securityAgent);
                return Ok(new Response { Status = "OK", Message = "Se ha eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "Error inesperado consulte al administrador del sistema" + ex.Message
                });
            }
        }
    }
}
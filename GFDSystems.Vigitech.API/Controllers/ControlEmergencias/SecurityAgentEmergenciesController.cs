using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAO.Interfaces;

namespace GFDSystems.Vigitech.API.Controllers.ControlEmergencias
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityAgentEmergenciesController : ControllerBase
    {
        private readonly ISecurityAgentEmergencyDAO _securityAgentEmergencyDAO;

        public SecurityAgentEmergenciesController(ISecurityAgentEmergencyDAO securityAgentEmergencyDAO)
        {
            _securityAgentEmergencyDAO = securityAgentEmergencyDAO;
        }

        // GET: api/SecurityAgentEmergencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecurityAgentEmergency>>> GetsecurityAgentEmergencies()
        {
            return await _securityAgentEmergencyDAO.GetAll().ToListAsync();
        }
        // GET: api/SecurityAgentEmergencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecurityAgentEmergency>> GetSecurityAgentEmergency(int id)
        {
            var securityAgentEmergency = await _securityAgentEmergencyDAO.GetByIdAsync(id);

            if (securityAgentEmergency == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID que intenta consultar no existe"
                });
            }
            return securityAgentEmergency;
        }

        // PUT: api/SecurityAgentEmergencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurityAgentEmergency(int id, SecurityAgentEmergency securityAgentEmergency)
        {
            if (id != securityAgentEmergency.SecurityAgentEmergencyId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID de la alerta no existe"
                });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityAgentEmergencyDAO.UpdateAsync(securityAgentEmergency);
                    return Ok(new Response { Status = "OK", Message = "Se actualizo correctamente" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _securityAgentEmergencyDAO.ExistAsync(id))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "El ID de la alerta no existe"
                        });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "Favor de verifique los campos"
                        });
                    }
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "El ID de la alerta no existe"
            });
        }

        // POST: api/SecurityAgentEmergencies
        [HttpPost]
        public async Task<ActionResult<SecurityAgentEmergency>> PostSecurityAgentEmergency(SecurityAgentEmergency securityAgentEmergency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityAgentEmergencyDAO.CreateAsync(securityAgentEmergency);
                    return Ok(new Response { Status = "OK", Message = "Se actualizó correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "Error inesperado, contacte al administrador " + ex.Message
                    });
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Favor de verificar los campos"
            });
        }

        // DELETE: api/SecurityAgentEmergencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SecurityAgentEmergency>> DeleteSecurityAgentEmergency(int id)
        {
            var securityAgentEmergency = await _securityAgentEmergencyDAO.GetByIdAsync(id);
            if (securityAgentEmergency == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID que intenta eliminar no existe"
                });
            }
            else
            {
                try
                {
                    await _securityAgentEmergencyDAO.DeleteAsync(securityAgentEmergency);
                    return Ok(new Response { Status = "OK", Message = "Se ha eliminado correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "Error inesperado consulte al administrador " + ex
                    });
                }
            }
        }

  
    }
}

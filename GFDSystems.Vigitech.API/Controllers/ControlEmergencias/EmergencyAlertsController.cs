using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAO.Interfaces;

namespace GFDSystems.Vigitech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyAlertsController : ControllerBase
    {
        private readonly IEmergencyAlertDAO _emergencyAlertDAO;

        public EmergencyAlertsController(IEmergencyAlertDAO emergencyAlert)
        {
            _emergencyAlertDAO = emergencyAlert;
        }

        // GET: api/EmergencyAlerts
        [HttpGet]// join para tener todas las relaciones
        public async Task<ActionResult<IList<EmergencyAlert>>> GetemergencyAlerts()
        {
            return await _emergencyAlertDAO.GetAll().ToListAsync();
        }

        // GET: api/EmergencyAlerts/{id}
        [HttpGet("{id}")]//preguntar por las consultas y sobre la tabla
        public async Task<ActionResult<EmergencyAlert>> GetEmergencyAlert(int id)
        {
            var emergencyAlert = await _emergencyAlertDAO.GetByIdAsync(id);

            if (emergencyAlert == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID que intenta consultar no existe"
                });
            }
            return emergencyAlert;
        }

        // PUT: api/EmergencyAlerts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergencyAlert(int id, EmergencyAlert emergencyAlert)
        {
            if (id != emergencyAlert.EmergencyAlertId)
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
                    await _emergencyAlertDAO.UpdateAsync(emergencyAlert);
                    return Ok(new Response { Status = "OK", Message = "Se actualizo correctamente" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _emergencyAlertDAO.ExistAsync(id))
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

        // POST: api/EmergencyAlerts
        [HttpPost]
        public async Task<ActionResult<EmergencyAlert>> PostEmergencyAlert(EmergencyAlert emergencyAlert)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emergencyAlertDAO.CreateAsync(emergencyAlert);
                    return Ok(new Response { Status = "OK", Message = "Se actualizó correctamente" });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "Error inesperado, contacte al administrador "+ex.Message
                    });
                }
           
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Favor de verificar los campos"
            });
        }
    }
}

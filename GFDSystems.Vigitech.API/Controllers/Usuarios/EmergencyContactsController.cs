using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.Repository;

namespace GFDSystems.Vigitech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactsController : ControllerBase
    {
        private readonly IEmergencyContactDAO _emergencyContactDAO;
        public EmergencyContactsController(IEmergencyContactDAO emergencyContactDAO)
        {
            _emergencyContactDAO = emergencyContactDAO;
        }
        // GET: api/EmergencyContacts/5
        [HttpGet("{id}")]//pido el id del citizen para mostrar sus contactos
        public async Task<ActionResult<IList<EmergencyContact>>> GetemergencyContacts(int id)
        {
            if (!await _emergencyContactDAO.ExistAsync(id))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no contine contactos registrados" });
            }
            //return await _context.emergencyDirectories.ToListAsync();
            return await _emergencyContactDAO.GetAll(id).ToListAsync();
        }
        // PUT: api/EmergencyContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergencyContact(int id, EmergencyContact emergencyContact)
        {
            if (id != emergencyContact.EmergencyContactId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID que intenta actualizar no existe" });
            }
            try
            {
                await _emergencyContactDAO.UpdateAsync(emergencyContact);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado correctamentee" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _emergencyContactDAO.ExistAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID que intenta actualizar no existe" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "favor de verificar los campos" });
                }
            }
        }
        // POST: api/EmergencyContacts
        [HttpPost]
        public async Task<ActionResult<EmergencyContact>> PostEmergencyContact(EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                await _emergencyContactDAO.CreateAsync(emergencyContact);
                return Ok(new Response { Status = "OK", Message = "Se registraron los contactos correctamente" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de revisar los campos" });
        }
    }
}

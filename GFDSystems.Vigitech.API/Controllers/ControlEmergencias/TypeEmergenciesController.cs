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
    public class TypeEmergenciesController : ControllerBase
    {
        private readonly ITypeEmergencyDAO _typeEmergencyDAO;
        public TypeEmergenciesController(ITypeEmergencyDAO typeEmergencyDAO)
        {
            _typeEmergencyDAO = typeEmergencyDAO;
        }
        // GET: api/TypeEmergencies
        [HttpGet]
        public async Task<ActionResult<IList<TypeEmergency>>> GettypeEmergencies()
        {
            return await _typeEmergencyDAO.GetAll().ToListAsync();
        }
        // PUT: api/TypeEmergencies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeEmergency(int id, TypeEmergency typeEmergency)
        {
            if (id != typeEmergency.TypeEmergencyId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID que intenta modificar no corresponde al registro que se intenta actualizar"
                });
            }
            try
            {
                await _typeEmergencyDAO.UpdateAsync(typeEmergency);
                return Ok(new Response { Status = "OK", Message = "Se actualizó correctamente" });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "error",
                    Message = "Error inesperado, consulte al administrador " + ex.Message
                });
            }
        }
        // POST: api/TypeEmergencies
        [HttpPost]
        public async Task<ActionResult<TypeEmergency>> PostTypeEmergency(TypeEmergency typeEmergency)
        {
            if (ModelState.IsValid)
            {
                if (! await _typeEmergencyDAO.ExistNameAsync(typeEmergency.Name))
                {
                    try
                    {
                        await _typeEmergencyDAO.CreateAsync(typeEmergency);
                        return Ok(new Response { Status = "OK", Message = "Se agrego correctamente" });
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El tipo de emergencia ya existe"
                });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Favor de verificar sus datos"
            });
        }
        // DELETE: api/TypeEmergencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeEmergency>> DeleteTypeEmergency(int id)
        {
            var typeEmergency = await _typeEmergencyDAO.GetByIdAsync(id);
            if (typeEmergency == null)
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
                    await _typeEmergencyDAO.DeleteAsync(typeEmergency);
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

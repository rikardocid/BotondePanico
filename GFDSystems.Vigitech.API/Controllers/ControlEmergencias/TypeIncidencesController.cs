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
    public class TypeIncidencesController : ControllerBase
    {
        private readonly ITypeIncidenceDAO _typeIncidenceDAO;

        public TypeIncidencesController(ITypeIncidenceDAO typeIncidenceDAO)
        {
            _typeIncidenceDAO = typeIncidenceDAO;
        }

        // GET: api/TypeIncidences
        [HttpGet]
        public async Task<ActionResult<IList<TypeIncidence>>> GettypeIncidences()
        {
            return await _typeIncidenceDAO.GetAll().ToListAsync();
        }

        // PUT: api/TypeIncidences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeIncidence(int id, TypeIncidence typeIncidence)
        {
            if (id != typeIncidence.TypeIncidenceId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID que intenta modificar no corresponde al registro que se intenta actualizar"
                });
            }
            try
            {
                await _typeIncidenceDAO.CreateAsync(typeIncidence);
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

        // POST: api/TypeIncidences
        [HttpPost]
        public async Task<ActionResult<TypeIncidence>> PostTypeIncidence(TypeIncidence typeIncidence)
        {
            if (ModelState.IsValid)
            {
                if (! await _typeIncidenceDAO.ExistNameAsync(typeIncidence.Name))
                {
                    try
                    {
                        await _typeIncidenceDAO.CreateAsync(typeIncidence);
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

        // DELETE: api/TypeIncidences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeIncidence>> DeleteTypeIncidence(int id)
        {
            var typeIncidence = await _typeIncidenceDAO.GetByIdAsync(id);
            if (typeIncidence == null)
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
                    await _typeIncidenceDAO.DeleteAsync(typeIncidence);
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

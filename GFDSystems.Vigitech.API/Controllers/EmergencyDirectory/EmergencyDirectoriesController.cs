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
    public class EmergencyDirectoriesController : ControllerBase
    {
        private readonly IEmergencyDirectoryDAO _emergencyDirectoryDAO;

        public EmergencyDirectoriesController(IEmergencyDirectoryDAO emergencyDirectoryDAO)
        {
            _emergencyDirectoryDAO = emergencyDirectoryDAO;
        }
        // GET: api/EmergencyDirectories
        [HttpGet]// todos los contactos
        public async Task<ActionResult<IList<EmergencyDirectory>>> GetemergencyDirectories()
        {
            //return await _context.emergencyDirectories.ToListAsync();
            return await _emergencyDirectoryDAO.GetAll().ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<EmergencyDirectory>> PostEmergencyDirectory(EmergencyDirectory emergencyDirectory)
        {
            if (ModelState.IsValid)
            {
                if (!await _emergencyDirectoryDAO.ExistNameAsync(emergencyDirectory.NameArea))
                {
                    try
                    {
                        await this._emergencyDirectoryDAO.CreateAsync(emergencyDirectory);
                        //var resp = await this._emergencyDirectoryDAO.CreateAsync(emergencyDirectory);
                        //return Ok(new Response { Status = "OK", Message = "Se ha guardado correctamente", entidad = resp });
                        return Ok(new Response { Status = "OK", Message = "Se ha guardado correctamente" });
                        
                    }
                    catch (Exception ex)
                    {

                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "Error inesperado consulte al administrador del sistema " + ex.Message
                        });
                    }
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Favor de verificar sus campos"
            });
        }
        //PUT: api/EmergencyDirectories/5 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmergencyDirectory(int id, EmergencyDirectory emergencyDirectory)
        {
            if (id != emergencyDirectory.EmergencyDirectoryId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El ID no corresponde a ningún contacoto de emergencias"
                });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _emergencyDirectoryDAO.UpdateAsync(emergencyDirectory);
                    return Ok(new Response { Status = "OK", Message = "Se actualizo correctamente" });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!await _emergencyDirectoryDAO.ExistNameAsync(emergencyDirectory.NameArea))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "El ID no corresponde a ningún contacoto de emergencias"
                        });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response
                        {
                            Status = "Error",
                            Message = "Error inesperado consulte al administrador " + ex.Message
                        });
                    }
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "Favor de verificar los campos"
            });
        }


        // DELETE: api/EmergencyDirectories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmergencyDirectory>> DeleteEmergencyDirectory(int id)
        {
            //var emergencyDirectory = await _context.emergencyDirectories.FindAsync(id);
            var emergencyDirectory = await _emergencyDirectoryDAO.GetByIdAsync(id);
            if (emergencyDirectory == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "El Id que intentamos eliminar no existe, favor de verificar"
                });
            }
            try
            {
                await _emergencyDirectoryDAO.DeleteAsync(emergencyDirectory);
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
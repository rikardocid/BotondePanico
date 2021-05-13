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
using GFDSystems.Vigitech.DAL.Entities.Usuarios;

namespace GFDSystems.Vigitech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileDeviceRegistrationsController : ControllerBase
    {
        private readonly IMobileDeviceRegistrationTempDAO _mobileDeviceRegistrationTempDAO;

        public MobileDeviceRegistrationsController(IMobileDeviceRegistrationTempDAO mobileDeviceRegistrationTempDAO)
        {
            _mobileDeviceRegistrationTempDAO = mobileDeviceRegistrationTempDAO;
        }

        // GET: api/MobileDeviceRegistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MobileDeviceRegistrationTemp>>> GetAll()
        {
            return await _mobileDeviceRegistrationTempDAO.GetAll().ToListAsync();
        }

        // GET: api/MobileDeviceRegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MobileDeviceRegistrationTemp>> GetMobileDeviceRegistration(int id)
        {
            var mobile = await _mobileDeviceRegistrationTempDAO.GetByIdAsync(id);

            if (mobile == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe un estado con ese  ID" });
            }
            return mobile;
        }

        // PUT: api/MobileDeviceRegistrations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMobileDeviceRegistration(int id, MobileDeviceRegistrationTemp mobileDeviceRegistration)
        {
            if (id != mobileDeviceRegistration.MobileDeviceRegistrationId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde al Registro que se intenta actualizar" });
            }
            try
            {
                await _mobileDeviceRegistrationTempDAO.UpdateAsync(mobileDeviceRegistration);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado el estado correctamente" });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos " + ex.Message });
            }

        }

        // POST: api/MobileDeviceRegistrations
        [HttpPost]
        public async Task<ActionResult<MobileDeviceRegistrationTemp>> PostMobileDeviceRegistration(MobileDeviceRegistrationTemp mobileDeviceRegistration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _mobileDeviceRegistrationTempDAO.CreateAsync(mobileDeviceRegistration);
                    return Ok(new Response { Status = "OK", Message = "Se ha registrado el estado correctamente" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos " });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "no se ha completado la acción "+ex.Message});
            }
               
                
            
        }

        // DELETE: api/MobileDeviceRegistrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MobileDeviceRegistration>> DeleteMobileDeviceRegistration(int id)
        {
            var mobileDeviceRegistration = await _mobileDeviceRegistrationTempDAO.GetByIdAsync(id);
            if (mobileDeviceRegistration == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            await _mobileDeviceRegistrationTempDAO.DeleteAsync(mobileDeviceRegistration);

            return Ok(new Response { Status = "OK", Message = "Se ha eliminado correctamente" });
        }

    
    }
}

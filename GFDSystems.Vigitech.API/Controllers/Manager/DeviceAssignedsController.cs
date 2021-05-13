using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Responses;
using GFDSystems.Vigitech.DAO.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers.Manager
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceAssignedsController : ControllerBase
    {
        private readonly IDeviceAssignedDAO _deviceAssignedDAO;
        public DeviceAssignedsController(IDeviceAssignedDAO deviceAssignedDAO)
        {
            _deviceAssignedDAO = deviceAssignedDAO;
        }
        [HttpGet("GetAll")]
        public IQueryable<DeviceAssignationResponse> GetAll()
        {
            return (IQueryable<DeviceAssignationResponse>)_deviceAssignedDAO.GetAll().ToList();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceAssigned(int id, DeviceAssigned deviceAssigned)
        {
            if (id != deviceAssigned.DeviceAssignedId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay un dispositivo con ese ID" });
            }
            try
            {
                await _deviceAssignedDAO.UpdateAsync(deviceAssigned,id);
                return Ok(new Response { Status = "OK", Message = "El registro se ha actualizado correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _deviceAssignedDAO.ExistAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay un dispositivo con ese ID" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay un dispositivo con ese ID" });
                }
            }
        }
    }
}
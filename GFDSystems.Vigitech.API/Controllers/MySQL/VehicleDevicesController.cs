using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFDSystems.Vigitech.DAL.Entities.MySQL;
using GFDSystems.Vigitech.DAO.MySQL;
using Microsoft.AspNetCore.Mvc;

namespace GFDSystems.Vigitech.API.Controllers.MySQL
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleDevicesController : ControllerBase
    {
        private readonly IVehicleDeviceDAO _vehicleDeviceDAO;
        public VehicleDevicesController(IVehicleDeviceDAO vehicleDeviceDAO)
        {
            _vehicleDeviceDAO = vehicleDeviceDAO;
        }
        [HttpGet("{id}")]
        public Task<IList<ResponceDevice>> GetDevice(string id)
        {
            return _vehicleDeviceDAO.GetByIdAsync(id);
        }
        [HttpGet("GetAll")]
        public IList<VehicleDevice> GetAll()
        {
            return _vehicleDeviceDAO.GetAll().ToList();
        }

        //    [HttpPost]
        //    public async Task<ActionResult<VehicleDevice>> PostEmergencyDirectory(VehicleDevice entityMySQL)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                await this._entityMySQLDAO.CreateAsync(entityMySQL);
        //                return Ok(new Response { Status = "OK", Message = "Se ha guardado correctamente" });
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //                {
        //                    Status = "Error",
        //                    Message = "Error inesperado consulte al administrador del sistema " + ex.Message
        //                });
        //            }
        //        }
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //        {
        //            Status = "Error",
        //            Message = "Favor de verificar sus campos"
        //        });
        //    }
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> PutEmergencyDirectory(int id, VehicleDevice entityMySQL)
        //    {
        //        if (id != entityMySQL.MySQLId)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //            {
        //                Status = "Error",
        //                Message = "El ID no corresponde a ningún contacoto de emergencias"
        //            });
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                await _entityMySQLDAO.UpdateAsync(entityMySQL);
        //                return Ok(new Response { Status = "OK", Message = "Se actualizo correctamente" });
        //            }
        //            catch (DbUpdateConcurrencyException ex)
        //            {

        //                return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //                {
        //                    Status = "Error",
        //                    Message = "El ID no corresponde a ningún contacoto de emergencias " + ex.Message
        //                });
        //            }
        //        }
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //        {
        //            Status = "Error",
        //            Message = "Favor de verificar los campos"
        //        });
        //    }

        //    [HttpDelete("{id}")]
        //    public async Task<ActionResult<VehicleDevice>> DeleteEmergencyDirectory(int id)
        //    {
        //        var entityMySQL = await _entityMySQLDAO.GetByIdAsync(id);
        //        if (entityMySQL == null)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //            {
        //                Status = "Error",
        //                Message = "El Id que intentamos eliminar no existe, favor de verificar"
        //            });
        //        }
        //        try
        //        {
        //            await _entityMySQLDAO.DeleteAsync(entityMySQL);
        //            return Ok(new Response { Status = "OK", Message = "Se ha eliminado correctamente" });
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response
        //            {
        //                Status = "Error",
        //                Message = "Error inesperado consulte al administrador del sistema" + ex.Message
        //            });
        //        }
        //    }
    }
}

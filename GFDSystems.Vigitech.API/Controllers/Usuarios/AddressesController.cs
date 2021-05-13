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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressDAO _addressDAO;

        public AddressesController(IAddressDAO addressDAO)
        {
            _addressDAO = addressDAO;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]//pido el id del citizen para regresar su direccion
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _addressDAO.GetByIdAsync(id);

            if (address == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay alguna dirección registrada" });
            }

            return address;
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay una direccion con ese ID" });
            }
            try
            {
                await _addressDAO.UpdateAsync(address);
                return Ok(new Response { Status = "OK", Message = "La dirección se ha actualizado correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _addressDAO.ExistAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No hay una direccion con ese ID" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de revisar los campos" });
                }
            }
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressDAO.CreateAsync(address);
                return Ok(new Response { Status = "OK", Message = "Se ha guardado correctamente" });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
        }
    }
}
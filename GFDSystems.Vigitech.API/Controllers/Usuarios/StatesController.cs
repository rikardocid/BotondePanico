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
    public class StatesController : ControllerBase
    {
        private readonly IStateDAO _stateDAO;

        public StatesController(IStateDAO stateDAO)
        {
            _stateDAO = stateDAO;
        }

        // GET: api/States/GetActive
        [HttpGet("GetActive")]
        public async Task<ActionResult<IEnumerable<State>>> GetActive()
        {
            return await _stateDAO.GetActive().ToListAsync();
        }
        // GET: api/States/GetInactive
        [HttpGet("GetInactive")]
        public async Task<ActionResult<IEnumerable<State>>> GetInactive()
        {
            return await _stateDAO.GetInctive().ToListAsync();
        }
        // GET: api/States/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<State>>> GetAll()
        {
            return await _stateDAO.GetAll().ToListAsync();
        }

        // GET: api/States/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            var state = await _stateDAO.GetByIdAsync(id);

            if (state == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe un estado con ese  ID" });
            }
            return state;
        }

        // PUT: api/States/update/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(int id, State state)
        {
            if (id != state.StateId)////preguntar si nos van a madar id
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde al Estado que se intenta actualizar" });
            }
            try
            {
                await _stateDAO.UpdateAsync(state);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado el estado correctamente" });
            }
            catch (DbUpdateConcurrencyException ex )
            {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos "+ ex.Message });
            }
            //return NoContent();
        }

        // POST: api/States
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        {
            if (! await _stateDAO.ExistAsync(state.Description))
            {
                if (ModelState.IsValid)
                {
                    await _stateDAO.CreateAsync(state);
                    return Ok(new Response { Status = "OK", Message = "Se ha registrado el estado correctamente" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El estado que intenta registrar ya existe" });
            }
        }


    }
}

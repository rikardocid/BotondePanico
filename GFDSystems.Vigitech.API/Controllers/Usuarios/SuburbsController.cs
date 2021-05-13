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
    public class SuburbsController : ControllerBase
    {
        private readonly ISuburbDAO _suburbDAO;

        public SuburbsController(ISuburbDAO suburb)
        {
            _suburbDAO = suburb;
        }

        // GET: api/Suburb/GetActive/{id}
        [HttpGet("GetActive/{id}")]//para llenar el combo de dirección
        public async Task<ActionResult<IEnumerable<Suburb>>> GetActive() // pido el id del town para retornar las suburb activos pertenecientes al state
        {

            //var consulta = await _context.towns.
            //            Join(_context.suburbs,
            //            tw => tw.TownId,
            //            sb => sb.TownId,
            //            (tw, sb) => new { tw, sb }).
            //            Where(w => w.tw.StateId == id && w.tw.Status == true && w.sb.Status == true).Select(s => s.sb)
            //            .ToListAsync();//consultamos todos los town activos que pertenezcan al mismo state que tambien este activo

            //if (consulta.Count() == 0)//que este activo el state al que pertenece el town que consultamos
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar sus campos" });
            //}
            //return consulta;//await _context.towns.Where(w => w.Status == true).ToListAsync();
            return await _suburbDAO.GetActive().ToArrayAsync();
        }
        [HttpGet("GetCP/{cp}")]//para llenar el combo de dirección
        public async Task<ActionResult<IEnumerable<Suburb>>> GetCP(int cp ) // pido el id del town para retornar las suburb activos pertenecientes al state
        {
            return await _suburbDAO.GetCP(cp).ToArrayAsync();
        }

        // GET: api/Towns/GetAll
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<IEnumerable<Suburb>>> GetAll(int id)//pido el id del town para retornar las colonias que le pertenecen y que esten activos
        {
            return await _suburbDAO.GetAll().ToArrayAsync();
        }
        // GET: api/Towns/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Suburb>> GetState(int id)
        {
            var suburb = await _suburbDAO.GetByIdAsync(id);

            if (suburb == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe alguna colonia con ese ID" });
            }
            return suburb;
        }

        // PUT: api/Towns/update/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(int id, Suburb suburb)
        {
            if (id != suburb.SuburbId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde a la colonia que se intenta actualizar" });
            }
            try
            {
                await _suburbDAO.UpdateAsync(suburb);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado el municipio correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            //return NoContent();
        }

        // POST: api/Suburb
        [HttpPost]
        public async Task<ActionResult<Suburb>> PostsSuburb(Suburb suburb)
        {
            if (! await _suburbDAO.ExistAsync(suburb.Description))
            {
                if (ModelState.IsValid)
                {
                    
                        await _suburbDAO.CreateAsync(suburb);
                        return Ok(new Response { Status = "OK", Message = "Se ha registrado la colonia correctamente" });
                   
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "La colonia que intenta registrar ya existe" });
            }
        }

       
        
    }
}

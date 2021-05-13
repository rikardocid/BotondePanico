using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAO.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownsController : ControllerBase
    {
        private readonly ITownDAO _townDAO;

        public TownsController(ITownDAO townDAO)
        {
            _townDAO = townDAO;
        }

        // GET: api/Towns/GetActive/{id}
        [HttpGet("GetActive/{id}")]//para llenar el combo de dirección
        public async Task<ActionResult<IEnumerable<Town>>> GetActive(int id) // pido el id del state para retornar los town activos pertenecientes al state
        {

            //var consulta = await _context.states.
            //            Join(_context.towns,
            //            st => st.StateId,
            //            tw => tw.StateId,
            //            (st, tw) => new { st, tw }).
            //            Where(w => w.st.StateId == id && w.st.Estatus == true && w.tw.Status == true).Select(s => s.tw)
            //            .ToListAsync();//consultamos todos los town activos que pertenezcan al mismo state que tambien este activo

            //if (consulta.Count() == 0)//que este activo el state al que pertenece el town que consultamos
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar sus campos" });
            //}
            //return consulta;//await _context.towns.Where(w => w.Status == true).ToListAsync();
            return await _townDAO.GetActive().ToListAsync();
        }
  

        // GET: api/Towns/GetAll
        [HttpGet("GetAll/{id}")]
        public async Task<ActionResult<IEnumerable<Town>>> GetAll(int id)//pido el id del state para retornar los municipios que le pertenecen y que esten activos
        {
            return await _townDAO.GetAll().ToListAsync();
        }
        // GET: api/Towns/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Town>> GetSTown(int id)
        {
            var town = await _townDAO.GetByIdAsync(id);

            if (town == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe un municipio con ese ID" });
            }
            return town;
        }

        // PUT: api/Towns/update/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(int id, Town town)
        {
            if (id != town.TownId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde al municipio que se intenta actualizar" });
            }
            try
            {
                await _townDAO.UpdateAsync(town);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado el municipio correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            //return NoContent();
        }

        // POST: api/Towns
        [HttpPost]
        public async Task<ActionResult<Town>> PostTown(Town town)
        {
            if (! await _townDAO.ExistAsync(town.Description))
            {
                if (ModelState.IsValid)
                {
                        await _townDAO.CreateAsync(town);
                        return Ok(new Response { Status = "OK", Message = "Se ha registrado el municipio correctamente" });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de verificar los campos" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El municipio que intenta registrar ya existe" });
            }
        }

        

    }
}

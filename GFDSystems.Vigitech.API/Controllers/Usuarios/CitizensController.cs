using System;
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
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenDAO _citizenDAO;
        public CitizensController(ICitizenDAO citizenDAO)
        {
            _citizenDAO = citizenDAO;
        }
        // GET: api/Citizens
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Citizen>>> Getcitizens()
        //{
        //    return await _context.citizens.ToListAsync();
        //}
        // GET: api/Citizens/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
            if (await _citizenDAO.ExistCizenAsync(id))
            {
                var citizen = await _citizenDAO.GetByIdAsync(id);
                return citizen;
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no corresponde a un ciudadano registrado" });
            }
        }
        // PUT: api/Citizens
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(int id, Citizen citizen)
        {
            if (id != citizen.CitizenId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no pertenece al ciudadano que intenta actualizar" });
            }
            try
            {
                await _citizenDAO.UpdateAsync(citizen);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _citizenDAO.ExistAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID no pertenece aNingun ciudadano registrado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Verifique que los datos sean correctos" });
                }
            }
        }
        // POST: api/Citizens
        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(Citizen citizen)
        {
            //bool exist = ;
            if (await _citizenDAO.ExistCURPAsync(citizen.CURP))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ciudadano ya se encuentra registrado" });
            }
            else
            {
                if (ModelState.IsValid)
                {

                    await _citizenDAO.CreateAsync(citizen);
                        return Ok(new Response { Status = "OK", Message = "Se ha registrado correctamente" });

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Favor de Verificar que los campos sean correctos" });
                }
            }
        }

    }
}

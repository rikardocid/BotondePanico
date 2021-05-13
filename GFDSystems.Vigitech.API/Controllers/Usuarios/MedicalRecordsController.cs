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
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordDAO _medicalRecordDAO;

        public MedicalRecordsController(IMedicalRecordDAO medicalRecordDAO)
        {
            _medicalRecordDAO = medicalRecordDAO;
        }


        // GET: api/MedicalRecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _medicalRecordDAO.GetByIdAsync(id);

            if (medicalRecord == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe un ciudadano registrado con ese ID" });
            }

            return medicalRecord;
        }

        // PUT: api/MedicalRecords/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.MedicalRecordId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "El ID que intenta actualizar no corresponde al ciudadano" });
            }
            try
            {
                await _medicalRecordDAO.UpdateAsync(medicalRecord);
                return Ok(new Response { Status = "OK", Message = "Se ha actualizado correctamente" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _medicalRecordDAO.ExistCitizenAsync(id))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe el Id que intenta actualizar" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No existe el Id que intenta actualizar" });
                }
            }
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord medicalRecord)
        {
            if (await _medicalRecordDAO.ExistCitizenAsync(medicalRecord.CitizenId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Verifique que los datos sean correctos" });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    await _medicalRecordDAO.CreateAsync(medicalRecord);
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

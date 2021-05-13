using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities.Responses;
using GFDSystems.Vigitech.DAO.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GFDSystems.Vigitech.API.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileDAO _profileDAO;

        public ProfilesController(IProfileDAO profileDAO)
        {
            _profileDAO = profileDAO;
        }
        [HttpGet("{id}")]//pido el id del usuario para regresar sus datos
        public  IQueryable<ProfileResponse> GetAddress(int id)
        {
           
            return _profileDAO.GetByIdAsync(id);
        }

    }
}

using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.CustomResponse;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Auth.RoleManager
{
    public class RoleManagerRepository : IRoleManagerRepository
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManagerRepository(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<AppRole>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<StatusResponse<AppRole>> GetById(int Id)
        {
            var response = await _roleManager.FindByIdAsync(Id.ToString());
           if(response == null)
            {
                return new StatusResponse<AppRole>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No se econtro el rol especificado, favor de verificar",
                    Value = null
                };
            }
            else
            {
                return new StatusResponse<AppRole>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Success",
                    Value = response
                };
            }
        }

        public async Task<StatusResponse<AppRole>> GetByName(string RoleName)
        {
            var response = await _roleManager.FindByNameAsync(RoleName);
            if (response == null)
            {
                return new StatusResponse<AppRole>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No se econtro el rol especificado, favor de verificar",
                    Value = null
                };
            }
            else
            {
                return new StatusResponse<AppRole>
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Success",
                    Value = response
                };
            }
        }

        public async Task<StatusResponse> Create(string RoleName)
        {
            var response = await _roleManager.CreateAsync(new AppRole { Name = RoleName });
            if (response.Succeeded)
            {
                return new StatusResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Role creado correctamente"
                };
            }
            else
            {
                return new StatusResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = string.Join(",", UtilitiesAIO.AddErrors(response))
                };
            }
        }

        public async Task<StatusResponse> Update(int Id, string RoleName)
        {
            var role = await _roleManager.FindByIdAsync(Id.ToString());
            if(role != null)
            {
                role.Name = RoleName;
                var response = await _roleManager.UpdateAsync(role);
                if (response.Succeeded)
                {
                    return new StatusResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = "Role actualizado correctamente"
                    };
                }
                else
                {
                    return new StatusResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Message = string.Join(",", UtilitiesAIO.AddErrors(response))
                    };
                }
            }
            else
            {
                return new StatusResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Error al encontrar el rol especificado"
                };
            }
           
        }
    }
}

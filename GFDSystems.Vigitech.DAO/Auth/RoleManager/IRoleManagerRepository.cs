using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.CustomResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Auth.RoleManager
{
    public interface IRoleManagerRepository
    {
        Task<List<AppRole>> GetAll();
        Task<StatusResponse<AppRole>> GetById(int Id);
        Task<StatusResponse<AppRole>> GetByName(string RoleName);
        Task<StatusResponse> Create(string RoleName);
        Task<StatusResponse> Update(int Id, string RoleName);
    }
}

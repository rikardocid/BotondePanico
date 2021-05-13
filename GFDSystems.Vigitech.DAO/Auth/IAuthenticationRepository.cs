using GFDSystems.Vigitech.DAO.CustomResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Auth
{
    public interface IAuthenticationRepository
    {
        Task<StatusResponse> Login(string UserName, string Password);
    }
}

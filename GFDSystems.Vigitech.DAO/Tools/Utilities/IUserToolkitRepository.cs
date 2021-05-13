using GFDSystems.Vigitech.DAO.CustomResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Tools.Utilities
{
    public interface IUserToolkitRepository
    {
        string[] GenerateUser(string TypeUser);
        string GenerateUserPaasword();
    }
}

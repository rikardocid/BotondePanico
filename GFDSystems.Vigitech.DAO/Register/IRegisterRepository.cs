using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.CustomResponse;
using GFDSystems.Vigitech.DAO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Register
{
    public interface IRegisterRepository
    {
        Task<StatusResponse<AppUser>> FirstRegister(PreRegisterDAO preRegisterDAO);
        Task<StatusResponse<string>> VerifyRegisterCode(string Code, string DeviceId);
        Task<StatusResponse<bool>> CompleteRegister(CompleteRegisterDAO completeRegisterDAO);
        Task<StatusResponse<AppUser>> RegisterUserPlatform(RegisterUserPlatformDAO registerUser, string userName, string password, string type);
        Task<StatusResponse<List<RegisterUserPlatformDAO>>> GetAllUsers();
        Task<StatusResponse> UpdateUser(UpdateInformationFromUserDAO update);
    }
}

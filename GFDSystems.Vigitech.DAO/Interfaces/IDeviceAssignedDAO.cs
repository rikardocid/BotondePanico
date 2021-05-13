using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IDeviceAssignedDAO
    {
        IQueryable<DeviceAssignationResponse> GetAll();
        Task<DeviceAssigned> GetByIdAsync(int id);
        Task<DeviceAssigned> CreateAsync(DeviceAssigned deviceAssigned);
        Task<DeviceAssigned> UpdateAsync(DeviceAssigned deviceAssigned, int idAgent);
        Task DeleteAsync(DeviceAssigned deviceAssigned);
        Task<bool> ExistAsync(int id);
    }
}

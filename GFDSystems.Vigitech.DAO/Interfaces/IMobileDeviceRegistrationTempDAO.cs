using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IMobileDeviceRegistrationTempDAO
    {
        IQueryable<MobileDeviceRegistrationTemp> GetAll();

        Task<MobileDeviceRegistrationTemp> GetByIdAsync(int id);

        Task<MobileDeviceRegistrationTemp> CreateAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp);

        Task<MobileDeviceRegistrationTemp> UpdateAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp);

        Task DeleteAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp);

        Task<bool> ExistAsync(int id);
    }
}

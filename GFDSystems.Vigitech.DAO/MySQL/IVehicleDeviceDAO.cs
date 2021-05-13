using GFDSystems.Vigitech.DAL.Entities.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.MySQL
{
    public interface IVehicleDeviceDAO
    {
        IList<VehicleDevice> GetAll();
        Task<IList<ResponceDevice>> GetByIdAsync(string id);
        //Task<VehicleDevice> CreateAsync(VehicleDevice vehicleDevice);
        //Task<VehicleDevice> UpdateAsync(VehicleDevice vehicleDevice);
        //Task DeleteAsync(VehicleDevice vehicleDevice);
        //Task<bool> SaveAllAsync();
    }
}

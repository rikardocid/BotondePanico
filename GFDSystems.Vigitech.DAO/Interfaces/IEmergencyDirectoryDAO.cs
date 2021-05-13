using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IEmergencyDirectoryDAO
    {
        IQueryable<EmergencyDirectory> GetAll();

        Task<EmergencyDirectory> GetByIdAsync(int id);

        Task<EmergencyDirectory> CreateAsync(EmergencyDirectory emergencyDirectory);

        Task<EmergencyDirectory> UpdateAsync(EmergencyDirectory emergencyDirectory);

        Task DeleteAsync(EmergencyDirectory emergencyAlert);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistNameAsync(string name);

        //Task<bool> SaveAllAsync();
    }
}

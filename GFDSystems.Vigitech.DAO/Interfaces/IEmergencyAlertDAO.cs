using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IEmergencyAlertDAO
    {
        IQueryable<EmergencyAlert> GetAll();

        Task<EmergencyAlert> GetByIdAsync(int id);

        Task<EmergencyAlert> CreateAsync(EmergencyAlert emergencyAlert);

        Task<EmergencyAlert> UpdateAsync(EmergencyAlert emergencyAlert);

        Task DeleteAsync(EmergencyAlert emergencyAlert);

        Task<bool> ExistAsync(int id);

        //Task<bool> SaveAllAsync();
    }
}

using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IEmergencyContactDAO
    {
        IQueryable<EmergencyContact> GetAll(int id);

        Task<EmergencyContact> GetByIdAsync(int id);

        Task<EmergencyContact> CreateAsync(EmergencyContact emergencyContact);

        Task<EmergencyContact> UpdateAsync(EmergencyContact emergency);

        Task DeleteAsync(EmergencyContact emergencyContact);

        Task<bool> ExistAsync(int id);

        //Task<bool> SaveAllAsync();
    }
}

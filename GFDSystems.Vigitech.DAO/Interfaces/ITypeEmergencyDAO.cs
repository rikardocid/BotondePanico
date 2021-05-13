using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ITypeEmergencyDAO
    {
        IQueryable<TypeEmergency> GetAll();

        Task<TypeEmergency> GetByIdAsync(int id);

        Task<TypeEmergency> CreateAsync(TypeEmergency typeEmergency);

        Task<TypeEmergency> UpdateAsync(TypeEmergency typeEmergency);

        Task DeleteAsync(TypeEmergency typeEmergency);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistNameAsync(string name);

        //Task<bool> SaveAllAsync();
    }
}

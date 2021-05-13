using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ISuburbDAO
    {
        IQueryable<Suburb> GetAll();
        IQueryable<Suburb> GetActive();
        IQueryable<Suburb> GetInctive();
        IQueryable<Suburb> GetCP(int cp);

        Task<Suburb> GetByIdAsync(int id);

        Task<Suburb> CreateAsync(Suburb suburb);

        Task<Suburb> UpdateAsync(Suburb suburb);

        Task DeleteAsync(Suburb suburb);

        Task<bool> ExistAsync(string name);
    }
}

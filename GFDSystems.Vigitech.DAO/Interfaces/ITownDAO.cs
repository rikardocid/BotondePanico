using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ITownDAO
    {
        IQueryable<Town> GetAll();
        IQueryable<Town> GetActive();
        IQueryable<Town> GetInctive();

        Task<Town> GetByIdAsync(int id);

        Task<Town> CreateAsync(Town town);

        Task<Town> UpdateAsync(Town town);

        Task DeleteAsync(Town town);

        Task<bool> ExistAsync(string name);
    }
}

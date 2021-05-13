using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IStateDAO
    {
        IQueryable<State> GetAll();
        IQueryable<State> GetActive();
        IQueryable<State> GetInctive();

        Task<State> GetByIdAsync(int id);

        Task<State> CreateAsync(State state);

        Task<State> UpdateAsync(State state);

        Task DeleteAsync(State state);

        Task<bool> ExistAsync(string name);
    }
}

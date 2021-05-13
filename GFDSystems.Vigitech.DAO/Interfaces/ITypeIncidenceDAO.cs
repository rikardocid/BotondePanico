using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ITypeIncidenceDAO
    {
        IQueryable<TypeIncidence> GetAll();

        Task<TypeIncidence> GetByIdAsync(int id);

        Task<TypeIncidence> CreateAsync(TypeIncidence typeIncidence);

        Task<TypeIncidence> UpdateAsync(TypeIncidence typeIncidence);

        Task DeleteAsync(TypeIncidence typeIncidence);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistNameAsync(string name);

        //Task<bool> SaveAllAsync();
    }
}

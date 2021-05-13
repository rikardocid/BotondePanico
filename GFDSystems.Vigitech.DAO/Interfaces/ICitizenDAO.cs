using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ICitizenDAO
    {

        Task<Citizen> GetByIdAsync(int id);

        Task<Citizen> CreateAsync(Citizen citizen);

        Task<Citizen> UpdateAsync(Citizen citizen);

        Task DeleteAsync(Citizen citizen);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistCURPAsync(string id);
        Task<bool> ExistCizenAsync(int id);

    }
}

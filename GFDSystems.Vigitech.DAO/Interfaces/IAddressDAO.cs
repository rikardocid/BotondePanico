using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IAddressDAO
    {

        Task<Address> GetByIdAsync(int id);

        Task<Address> CreateAsync(Address address);

        Task<Address> UpdateAsync(Address address);

        Task DeleteAsync(Address address);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistCizenAsync(int id);

    }
}

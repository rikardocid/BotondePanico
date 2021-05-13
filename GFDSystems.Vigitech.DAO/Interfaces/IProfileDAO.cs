using GFDSystems.Vigitech.DAL.Entities.Responses;
using System.Linq;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IProfileDAO
    {

        IQueryable<ProfileResponse> GetByIdAsync(int id);

    }
}

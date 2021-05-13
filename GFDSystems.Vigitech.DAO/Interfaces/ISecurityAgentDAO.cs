using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ISecurityAgentDAO
    {
        IQueryable<AgentResponse> GetAll();

        Task<SecurityAgent> GetByIdAsync(int id);

        Task<SecurityAgent> CreateAsync(SecurityAgent securityAgent);

        Task<SecurityAgent> UpdateAsync(SecurityAgent securityAgent);

        Task DeleteAsync(SecurityAgent securityAgent);

        Task<bool> ExistAsync(int id);
    }
}

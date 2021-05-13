using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface ISecurityAgentEmergencyDAO
    {
        IQueryable<SecurityAgentEmergency> GetAll();

        Task<SecurityAgentEmergency> GetByIdAsync(int id);

        Task<SecurityAgentEmergency> CreateAsync(SecurityAgentEmergency securityAgentEmergency);

        Task<SecurityAgentEmergency> UpdateAsync(SecurityAgentEmergency securityAgentEmergency);

        Task DeleteAsync(SecurityAgentEmergency securityAgentEmergency);

        Task<bool> ExistAsync(int id);
    }
}

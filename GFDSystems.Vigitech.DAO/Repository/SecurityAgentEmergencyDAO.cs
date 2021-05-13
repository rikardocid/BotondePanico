using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Entities.Usuarios;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Helper;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Repository
{
    public class SecurityAgentEmergencyDAO : ISecurityAgentEmergencyDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public SecurityAgentEmergencyDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<SecurityAgentEmergency> GetAll()
        {
            return this.context.securityAgentEmergencies.AsNoTracking();
        }
        public async Task<SecurityAgentEmergency> GetByIdAsync(int id)
        {
            return await this.context.securityAgentEmergencies 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.SecurityAgentId.Equals(id));
        }
        public async Task<SecurityAgentEmergency> CreateAsync(SecurityAgentEmergency securityAgentEmergency)
        {
            await this.context.securityAgentEmergencies.AddAsync(securityAgentEmergency);
            await SaveAllAsync(securityAgentEmergency);
            return securityAgentEmergency;
        }
        public async Task<SecurityAgentEmergency> UpdateAsync(SecurityAgentEmergency securityAgentEmergency)
        {
            this.context.securityAgentEmergencies.Update(securityAgentEmergency);
            await SaveAllAsync(securityAgentEmergency);
            return securityAgentEmergency;
        }
        public async Task DeleteAsync(SecurityAgentEmergency securityAgentEmergency)
        {
            this.context.securityAgentEmergencies.Remove(securityAgentEmergency);
            await SaveAllAsync(securityAgentEmergency);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<SecurityAgentEmergency>().AnyAsync(e => e.SecurityAgentId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(SecurityAgentEmergency securityAgentEmergency)
        {
            try
            {
                return await this.context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(securityAgentEmergency);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
            
        }
    }
}

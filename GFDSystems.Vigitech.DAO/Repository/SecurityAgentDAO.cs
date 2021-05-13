using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAL.Responses;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Repository
{
    public class SecurityAgentDAO : ISecurityAgentDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public SecurityAgentDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<AgentResponse> GetAll()
        {
            return this.context.securityAgents
                .Join(context.appUsers,
                    sa => sa.AspNetUserId,
                    au => au.Id,
                    (sa, au) => new { sa, au })
                .Where(w => w.au.Id == w.sa.AspNetUserId)
                .Select(s => new AgentResponse
                {
                    FullName = s.au.Name + " " + s.au.MiddleName + " " + s.au.LastName,
                    AgentLicence = s.sa.AgentLicence,
                    Rank = s.sa.Rank,
                    Corporation = s.sa.Corporation
                });
        }
        public async Task<SecurityAgent> GetByIdAsync(int id)
        {
            return await this.context.securityAgents
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.SecurityAgentId.Equals(id));
        }
        public async Task<SecurityAgent> CreateAsync(SecurityAgent securityAgent)
        {
            await this.context.securityAgents.AddAsync(securityAgent);
            await SaveAllAsync(securityAgent);
            return securityAgent;
        }
        public async Task<SecurityAgent> UpdateAsync(SecurityAgent securityAgent)
        {
            this.context.securityAgents.Update(securityAgent);
            await SaveAllAsync(securityAgent);
            return securityAgent;
        }
        public async Task DeleteAsync(SecurityAgent securityAgent)
        {
            this.context.securityAgents.Remove(securityAgent);
            await SaveAllAsync(securityAgent);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<SecurityAgent>().AnyAsync(e => e.SecurityAgentId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(SecurityAgent securityAgent)
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
                systemLog.Parameter = JsonConvert.SerializeObject(securityAgent);
                _customSystemLog.AddLog(systemLog);
                return false;
            }

        }
    }
}

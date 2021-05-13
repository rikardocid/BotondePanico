using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
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
    public class EmergencyAlertDAO : IEmergencyAlertDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public EmergencyAlertDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<EmergencyAlert> GetAll()
        {
            return this.context.emergencyAlerts.AsNoTracking();
        }
        public async Task<EmergencyAlert> GetByIdAsync(int id)
        {
            return await this.context.emergencyAlerts 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmergencyAlertId.Equals(id));
        }
        public async Task<EmergencyAlert> CreateAsync(EmergencyAlert emergencyAlert)
        {
            await this.context.emergencyAlerts.AddAsync(emergencyAlert);
            await SaveAllAsync(emergencyAlert);
            return emergencyAlert;
        }
        public async Task<EmergencyAlert> UpdateAsync(EmergencyAlert emergencyAlert)
        {
            this.context.emergencyAlerts.Update(emergencyAlert);
            await SaveAllAsync(emergencyAlert);
            return emergencyAlert;
        }
        public async Task DeleteAsync(EmergencyAlert emergencyAlert)
        {
            this.context.emergencyAlerts.Remove(emergencyAlert);
            await SaveAllAsync(emergencyAlert);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.emergencyAlerts.AnyAsync(e => e.EmergencyAlertId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(EmergencyAlert emergencyAlert)
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
                systemLog.Parameter = JsonConvert.SerializeObject(emergencyAlert);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

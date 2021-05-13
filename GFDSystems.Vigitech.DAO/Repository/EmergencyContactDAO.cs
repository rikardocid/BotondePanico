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
    public class EmergencyContactDAO : IEmergencyContactDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public EmergencyContactDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<EmergencyContact> GetAll(int id)
        {
            return this.context.emergencyContacts.Where(w=>w.CitizenId==id).AsNoTracking();
        }
        public async Task<EmergencyContact> GetByIdAsync(int id)
        {
            return await this.context.emergencyContacts 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmergencyContactId.Equals(id));
        }
        public async Task<EmergencyContact> CreateAsync(EmergencyContact emergencyContact)
        {
            await this.context.emergencyContacts.AddAsync(emergencyContact);
            await SaveAllAsync(emergencyContact);
            return emergencyContact;
        }
        public async Task<EmergencyContact> UpdateAsync(EmergencyContact emergencyContact)
        {
            this.context.emergencyContacts.Update(emergencyContact);
            await SaveAllAsync(emergencyContact);
            return emergencyContact;
        }
        public async Task DeleteAsync(EmergencyContact emergencyContact)
        {
            this.context.emergencyContacts.Remove(emergencyContact);
            await SaveAllAsync(emergencyContact);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.emergencyContacts.AnyAsync(e => e.EmergencyContactId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(EmergencyContact emergencyContact)
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
                systemLog.Parameter = JsonConvert.SerializeObject(emergencyContact);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

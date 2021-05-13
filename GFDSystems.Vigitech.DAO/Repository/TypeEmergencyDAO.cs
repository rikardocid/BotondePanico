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
    public class TypeEmergencyDAO : ITypeEmergencyDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public TypeEmergencyDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<TypeEmergency> GetAll()
        {
            return this.context.typeEmergencies.AsNoTracking();
        }
        public async Task<TypeEmergency> GetByIdAsync(int id)
        {
            return await this.context.typeEmergencies 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TypeEmergencyId.Equals(id));
        }
        public async Task<TypeEmergency> CreateAsync(TypeEmergency typeEmergency)
        {
            await this.context.typeEmergencies.AddAsync(typeEmergency);
            await SaveAllAsync(typeEmergency);
            return typeEmergency;
        }
        public async Task<TypeEmergency> UpdateAsync(TypeEmergency typeEmergency)
        {
            this.context.typeEmergencies.Update(typeEmergency);
            await SaveAllAsync(typeEmergency);
            return typeEmergency;
        }
        public async Task DeleteAsync(TypeEmergency typeEmergency)
        {
            this.context.typeEmergencies.Remove(typeEmergency);
            await SaveAllAsync(typeEmergency);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<TypeEmergency>().AnyAsync(e => e.TypeEmergencyId.Equals(id));
        }
        public async Task<bool> ExistNameAsync(string name)
        {
            return await this.context.typeEmergencies.AnyAsync(e => e.Name == name);
        }
        public async Task<bool> SaveAllAsync(TypeEmergency typeEmergency)
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
                systemLog.Parameter = JsonConvert.SerializeObject(typeEmergency);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
            
        }
    }
}

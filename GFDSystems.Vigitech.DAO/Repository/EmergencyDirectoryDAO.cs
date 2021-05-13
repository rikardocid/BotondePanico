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
    public class EmergencyDirectoryDAO : IEmergencyDirectoryDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public EmergencyDirectoryDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<EmergencyDirectory> GetAll()
        {
            return this.context.emergencyDirectories.AsNoTracking();
        }
        public async Task<EmergencyDirectory> GetByIdAsync(int id)
        {
            return await this.context.emergencyDirectories 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmergencyDirectoryId.Equals(id));
        }
        public async Task<EmergencyDirectory> CreateAsync(EmergencyDirectory emergencyDirectory)
        {
            await this.context.emergencyDirectories.AddAsync(emergencyDirectory);
            await SaveAllAsync( emergencyDirectory);
            return emergencyDirectory;
        }
        public async Task<EmergencyDirectory> UpdateAsync(EmergencyDirectory emergencyDirectory)
        {
            this.context.emergencyDirectories.Update(emergencyDirectory);
            await SaveAllAsync( emergencyDirectory);
            return emergencyDirectory;
        }
        public async Task DeleteAsync(EmergencyDirectory emergencyDirectory)
        {
            this.context.emergencyDirectories.Remove(emergencyDirectory);
            await SaveAllAsync( emergencyDirectory);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.emergencyDirectories.AnyAsync(e => e.EmergencyDirectoryId.Equals(id));
        }
        public async Task<bool> ExistNameAsync(string name)
        {
            return await this.context.emergencyDirectories.AnyAsync(e => e.NameArea.Equals(name));
        }
        public async Task<bool> SaveAllAsync(EmergencyDirectory emergencyDirectory)
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
                systemLog.Parameter = JsonConvert.SerializeObject(emergencyDirectory);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
           
        }
    }
}

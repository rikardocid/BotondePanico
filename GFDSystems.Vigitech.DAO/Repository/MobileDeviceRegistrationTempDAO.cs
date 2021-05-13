using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
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
    public class MobileDeviceRegistrationTempDAO : IMobileDeviceRegistrationTempDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public MobileDeviceRegistrationTempDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<MobileDeviceRegistrationTemp> GetAll()
        {
            return this.context.MobileDeviceRegistrationTemps.AsNoTracking();
        }
        public async Task<MobileDeviceRegistrationTemp> GetByIdAsync(int id)
        {
            return await this.context.MobileDeviceRegistrationTemps 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.MobileDeviceRegistrationId.Equals(id));
        }
        public async Task<MobileDeviceRegistrationTemp> CreateAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp)
        {
            await this.context.MobileDeviceRegistrationTemps.AddAsync(mobileDeviceRegistrationTemp);
            await SaveAllAsync(mobileDeviceRegistrationTemp);
            return mobileDeviceRegistrationTemp;
        }
        public async Task<MobileDeviceRegistrationTemp> UpdateAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp)
        {
            this.context.MobileDeviceRegistrationTemps.Update(mobileDeviceRegistrationTemp);
            await SaveAllAsync(mobileDeviceRegistrationTemp);
            return mobileDeviceRegistrationTemp;
        }
        public async Task DeleteAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp)
        {
            this.context.MobileDeviceRegistrationTemps.Remove(mobileDeviceRegistrationTemp);
            await SaveAllAsync(mobileDeviceRegistrationTemp);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<MobileDeviceRegistrationTemp>().AnyAsync(e => e.DeviceId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(MobileDeviceRegistrationTemp mobileDeviceRegistrationTemp)
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
                systemLog.Parameter = JsonConvert.SerializeObject(mobileDeviceRegistrationTemp);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
            
        }
    }
}

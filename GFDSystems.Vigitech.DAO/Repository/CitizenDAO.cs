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
    public class CitizenDAO : ICitizenDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;
        public CitizenDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<Citizen> GetAll()
        {
            return this.context.citizens.AsNoTracking();
        }
        public async Task<Citizen> GetByIdAsync(int id)
        {
            return await this.context.citizens 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.AspNetUserId.Equals(id));
        }
        public async Task<Citizen> CreateAsync(Citizen citizen)
        {
            await this.context.citizens.AddAsync(citizen);
            await SaveAllAsync(citizen);
            return citizen;
        }
        public async Task<Citizen> UpdateAsync(Citizen citizen)
        {
            this.context.citizens.Update(citizen);
            await SaveAllAsync(citizen);
            return citizen;
        }
        public async Task DeleteAsync(Citizen citizen)
        {
            this.context.citizens.Remove(citizen);
            await SaveAllAsync(citizen);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.citizens.AnyAsync(e => e.CitizenId.Equals(id));
        }
        public async Task<bool> ExistCURPAsync(string curp)
        {
            return await this.context.citizens.AnyAsync(e => e.CURP.Equals(curp));
        }
        public async Task<bool> ExistCizenAsync(int id)
        {
            return await this.context.citizens.AnyAsync(e => e.AspNetUserId.Equals(id));
        }

        public async Task<bool> SaveAllAsync(Citizen citizen)
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
                systemLog.Parameter = JsonConvert.SerializeObject(citizen);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

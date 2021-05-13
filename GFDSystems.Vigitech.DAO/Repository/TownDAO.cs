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
    public class TownDAO : ITownDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public TownDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<Town> GetAll()
        {
            return this.context.towns.AsNoTracking();
        }
        public IQueryable<Town> GetActive()
        {
            return this.context.towns.Where(w=>w.Status==true).AsNoTracking();
        }

        public IQueryable<Town> GetInctive()
        {
            return this.context.towns.Where(w => w.Status == false).AsNoTracking();
        }
        public async Task<Town> GetByIdAsync(int id)
        {
            return await this.context.towns 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TownId.Equals(id));
        }
        public async Task<Town> CreateAsync(Town town)
        {
            await this.context.towns.AddAsync(town);
            await SaveAllAsync(town);
            return town;
        }
        public async Task<Town> UpdateAsync(Town town)
        {
            this.context.towns.Update(town);
            await SaveAllAsync(town);
            return town;
        }
        public async Task DeleteAsync(Town town)
        {
            this.context.towns.Remove(town);
            await SaveAllAsync(town);
        }
        public async Task<bool> ExistAsync(string name)
        {
            return await this.context.Set<Town>().AnyAsync(e => e.Description.Equals(name));
        }
        public async Task<bool> SaveAllAsync(Town town)
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
                systemLog.Parameter = JsonConvert.SerializeObject(town);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

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
    public class SuburbDAO : ISuburbDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public SuburbDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<Suburb> GetAll()
        {
            return this.context.suburbs.AsNoTracking();
        }
        public IQueryable<Suburb> GetActive()
        {
            return this.context.suburbs.Where(w=>w.Status==true).AsNoTracking();
        }
        public IQueryable<Suburb> GetInctive()
        {
            return this.context.suburbs.Where(w => w.Status == false).AsNoTracking();
        }
        public IQueryable<Suburb> GetCP(int cp)
        {
            return this.context.suburbs.Where(w => w.PostalCode==cp).AsNoTracking();
        }
        public async Task<Suburb> GetByIdAsync(int id)
        {
            return await this.context.suburbs 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.SuburbId.Equals(id));
        }
        public async Task<Suburb> CreateAsync(Suburb suburb)
        {
            await this.context.suburbs.AddAsync(suburb);
            await SaveAllAsync(suburb);
            return suburb;
        }
        public async Task<Suburb> UpdateAsync(Suburb suburb)
        {
            this.context.suburbs.Update(suburb);
            await SaveAllAsync(suburb);
            return suburb;
        }
        public async Task DeleteAsync(Suburb suburb)
        {
            this.context.suburbs.Remove(suburb);
            await SaveAllAsync(suburb);
        }
        public async Task<bool> ExistAsync(string name)
        {
            return await this.context.Set<Suburb>().AnyAsync(e => e.Description.Equals(name));
        }
        public async Task<bool> SaveAllAsync(Suburb suburb)
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
                systemLog.Parameter = JsonConvert.SerializeObject(suburb);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

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
    public class StateDAO : IStateDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public StateDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<State> GetAll()
        {
            return this.context.states.AsNoTracking();
        }
        public IQueryable<State> GetActive()
        {
            return this.context.states.Where(w=>w.Estatus==true).AsNoTracking();
        }

        public IQueryable<State> GetInctive()
        {
            return this.context.states.Where(w => w.Estatus == false).AsNoTracking();
        }
        public async Task<State> GetByIdAsync(int id)
        {
            return await this.context.states 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.StateId.Equals(id));
        }
        public async Task<State> CreateAsync(State state)
        {
            await this.context.states.AddAsync(state);
            await SaveAllAsync(state);
            return state;
        }
        public async Task<State> UpdateAsync(State state)
        {
            this.context.states.Update(state);
            await SaveAllAsync(state);
            return state;
        }
        public async Task DeleteAsync(State state)
        {
            this.context.states.Remove(state);
            await SaveAllAsync(state);
        }
        public async Task<bool> ExistAsync(string name)
        {
            return await this.context.Set<State>().AnyAsync(e => e.Description.Equals(name));
        }
        public async Task<bool> SaveAllAsync(State state)
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
                systemLog.Parameter = JsonConvert.SerializeObject(state);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
            
        }
    }
}

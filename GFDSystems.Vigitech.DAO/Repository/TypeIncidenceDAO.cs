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
    public class TypeIncidenceDAO : ITypeIncidenceDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public TypeIncidenceDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }
        public IQueryable<TypeIncidence> GetAll()
        {
            return this.context.typeIncidences.AsNoTracking();
        }
        public async Task<TypeIncidence> GetByIdAsync(int id)
        {
            return await this.context.typeIncidences 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TypeIncidenceId.Equals(id));
        }
        public async Task<TypeIncidence> CreateAsync(TypeIncidence typeIncidence)
        {
            await this.context.typeIncidences.AddAsync(typeIncidence);
            await SaveAllAsync(typeIncidence);
            return typeIncidence;
        }
        public async Task<TypeIncidence> UpdateAsync(TypeIncidence typeIncidence)
        {
            this.context.typeIncidences.Update(typeIncidence);
            await SaveAllAsync(typeIncidence);
            return typeIncidence;
        }
        public async Task DeleteAsync(TypeIncidence typeIncidence)
        {
            this.context.typeIncidences.Remove(typeIncidence);
            await SaveAllAsync(typeIncidence);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.Set<TypeIncidence>().AnyAsync(e => e.TypeIncidenceId.Equals(id));
        }
        public async Task<bool> ExistNameAsync(string name)
        {
            return await this.context.typeIncidences.AnyAsync(e => e.Name == name);
        }
        public async Task<bool> SaveAllAsync(TypeIncidence typeIncidence)
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
                systemLog.Parameter = JsonConvert.SerializeObject(typeIncidence);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}

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
    public class MedicalRecordDAO : IMedicalRecordDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;

        public MedicalRecordDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            this._customSystemLog = customSystemLog;
        }

        public async Task<MedicalRecord> GetByIdAsync(int id)
        {
            return await this.context.medicalRecords 
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.MedicalRecordId.Equals(id));
        }
        public async Task<MedicalRecord> CreateAsync(MedicalRecord medicalRecord)
        {
            await this.context.medicalRecords.AddAsync(medicalRecord);
            await SaveAllAsync(medicalRecord);
            return medicalRecord;
        }
        public async Task<MedicalRecord> UpdateAsync(MedicalRecord medicalRecord)
        {
            this.context.medicalRecords.Update(medicalRecord);
            await SaveAllAsync(medicalRecord);
            return medicalRecord;
        }
        public async Task DeleteAsync(MedicalRecord medicalRecord)
        {
            this.context.medicalRecords.Remove(medicalRecord);
            await SaveAllAsync(medicalRecord);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await this.context.medicalRecords.AnyAsync(e => e.MedicalRecordId.Equals(id));
        }
        public async Task<bool> ExistCitizenAsync(int id)
        {
            return await this.context.medicalRecords.AnyAsync(e => e.MedicalRecordId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(MedicalRecord medicalRecord)
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
                systemLog.Parameter = JsonConvert.SerializeObject(medicalRecord);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
            
        }
    }
}

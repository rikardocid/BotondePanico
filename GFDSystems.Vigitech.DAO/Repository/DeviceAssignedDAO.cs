using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using GFDSystems.Vigitech.DAL.Responses;

namespace GFDSystems.Vigitech.DAO.Repository
{
    public class DevicesAgentsDAO : IDeviceAssignedDAO
    {
        private readonly ApplicationDbContext context;
        private readonly ICustomSystemLog _customSystemLog;
        public DevicesAgentsDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            this.context = context;
            _customSystemLog = customSystemLog;
        }
        public IQueryable<DeviceAssignationResponse> GetAll()
        {
            return context.deviceAssigneds
                .Join(context.statusDevices, da => da.StatusId, sd => sd.StatusDeviceID, (da, sd) => new { da, sd })
                .Join(context.securityAgents, adj => adj.da.SecurityAgentID, sa => sa.SecurityAgentId, (adj, sa) => new { adj, sa })
                .Join(context.appUsers, saj => saj.sa.AspNetUserId, au => au.Id, (saj, au) => new { saj, au })
                .Where(w => w.saj.sa.Status == true)
                .Select(s => new DeviceAssignationResponse
                {
                    DeviceAssignedId = s.saj.adj.da.DeviceAssignedId,
                    CarLicence = s.saj.adj.da.CarLicence,
                    Description = s.saj.adj.da.Description,
                    DateAsignation = s.saj.sa.DateRegister,
                    DescriptionStatusSevice = s.saj.adj.sd.Description,
                    AgentLicence = s.saj.sa.AgentLicence,
                    Rank = s.saj.sa.Rank,
                    Corporation = s.saj.sa.Corporation,
                    FullName = s.au.Name + " " + s.au.MiddleName + " " + s.au.LastName
                });
        }
        public async Task<DeviceAssigned> GetByIdAsync(int id)
        {
            return await context.deviceAssigneds
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.DeviceAssignedId.Equals(id));
        }
        public async Task<DeviceAssigned> CreateAsync(DeviceAssigned deviceAssigned)
        {
            await context.deviceAssigneds.AddAsync(deviceAssigned);
            await SaveAllAsync(deviceAssigned);
            return deviceAssigned;
        }
        public async Task<DeviceAssigned> UpdateAsync(DeviceAssigned deviceAssigned, int idAgent)
        {
            try
            {
                //TODO guardar historial
                HistoryDeviceAssigned history = new HistoryDeviceAssigned()
                {
                    CarLicence = deviceAssigned.CarLicence,
                    Description = deviceAssigned.Description,
                    DateAsignation = deviceAssigned.DateAsignation,
                    StatusId = deviceAssigned.StatusId,
                    SecurityAgentID = deviceAssigned.SecurityAgentID,
                    DeviceAssignedId = deviceAssigned.DeviceAssignedId
                };
                await context.historyDeviceAssigneds.AddAsync(history);
                await context.SaveChangesAsync();
                //////////////
                deviceAssigned.SecurityAgentID = idAgent;
                deviceAssigned.DateAsignation = DateTime.Now;
                context.deviceAssigneds.Update(deviceAssigned);
                await SaveAllAsync(deviceAssigned);
              
            }
            catch (Exception)
            {
                throw;
            }
            return deviceAssigned;
        }
        public async Task DeleteAsync(DeviceAssigned deviceAssigned)
        {
            context.deviceAssigneds.Remove(deviceAssigned);
            await SaveAllAsync(deviceAssigned);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await context.deviceAssigneds.AnyAsync(e => e.DeviceAssignedId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(DeviceAssigned deviceAssigned)
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(deviceAssigned);
                _customSystemLog.AddLog(systemLog);
                return false;
            }
        }
    }
}
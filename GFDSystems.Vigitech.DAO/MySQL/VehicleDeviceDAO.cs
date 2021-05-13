using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.MySQL;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace GFDSystems.Vigitech.DAO.MySQL
{
    public class VehicleDeviceDAO : IVehicleDeviceDAO
    {
        private readonly ICustomSystemLog _customSystemLog;//aun no se agrega log
        private readonly MySQLContext _mySQLContext;

        public VehicleDeviceDAO(MySQLContext mySQLContext, ICustomSystemLog customSystemLog)
        {
            _mySQLContext = mySQLContext;
            _customSystemLog = customSystemLog;
        }
        public IList<VehicleDevice> GetAll()
        {
            var lst = _mySQLContext.vehicleDevices.ToList();
            return lst;
        }
        public async Task<IList<ResponceDevice>> GetByIdAsync(string id)
        {
            var veicleDevice = _mySQLContext.vehicleDevices.
             Join(_mySQLContext.groupInfos,
             vd => vd.GroupID,
             gi => gi.Id,
             (vd, gi) => new { vd, gi }).
             Where(w => w.vd.GroupID == w.gi.Id && w.vd.DeviceID == id).
             Select(s => new ResponceDevice
             {
                 DeviceID= s.vd.DeviceID,
                 CarLicence= s.vd.CarLicence,
                 EconomicNumber=s.vd.EconomicNumber,
                 GroupName=s.gi.GroupName,
                 Remark=s.gi.Remark,
                 Id=s.gi.Id
             }).ToListAsync();

            return await veicleDevice;//_mySQLContext.vehicleDevices.Where(w => w.Id == id).FirstOrDefaultAsync();
        }
        //public async Task<VehicleDevice> CreateAsync(VehicleDevice entityMySQL)
        //{
           
        //    await _mySQLContext.vehicleDevices.AddAsync(entityMySQL);
        //    await SaveAllAsync(entityMySQL);
        //    return entityMySQL;
        //}
        //public async Task<VehicleDevice> UpdateAsync(VehicleDevice entityMySQL)
        //{
        //     _mySQLContext.vehicleDevices.Update(entityMySQL);
        //    await SaveAllAsync(entityMySQL);
        //    return entityMySQL;
        //}
        //public async Task DeleteAsync(VehicleDevice entityMySQL)
        //{
        //    _mySQLContext.vehicleDevices.Remove(entityMySQL);
        //    await SaveAllAsync(entityMySQL);
        //}
        //public async Task<bool> SaveAllAsync(VehicleDevice entityMySQL)
        //{
        //    try
        //    {
        //        return await this._mySQLContext.SaveChangesAsync() > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        SystemLog systemLog = new SystemLog();
        //        systemLog.Description = ex.ToMessageAndCompleteStacktrace();
        //        systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
        //        systemLog.Controller = GetType().Name;
        //        systemLog.Action = UtilitiesAIO.GetCallerMemberName();
        //        systemLog.Parameter = JsonConvert.SerializeObject(entityMySQL);
        //        _customSystemLog.AddLog(systemLog);
        //        return false;
        //    }
        //}
    }
}

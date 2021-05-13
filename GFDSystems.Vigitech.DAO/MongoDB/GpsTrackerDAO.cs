using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.MongoDB;
using GFDSystems.Vigitech.DAL.Entities.Responses;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore.Internal;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GFDSystems.Vigitech.DAO.MongoDB
{
    public class GpsTrackerDAO : IGpsTrackerDAO
    {
        private readonly ICustomSystemLog _customSystemLog;
        private readonly MySQLContext _mySQLContext;
        private readonly ApplicationDbContext _DbContext;
        public GpsTrackerDAO(MySQLContext mySQLContext, ICustomSystemLog customSystemLog, ApplicationDbContext DbContext)
        {
            _customSystemLog = customSystemLog;
            _mySQLContext = mySQLContext;
            _DbContext = DbContext;
        }
        public IList<LocationResponse> GetAll()
        { 
            DateTime fecha = DateTime.Now;
            string day = fecha.ToShortDateString().Substring(0, 2);
            string mont = fecha.ToShortDateString().Substring(3, 2);
            string year = fecha.ToShortDateString().Substring(6, 4);
            var client = new MongoClient("mongodb://192.168.11.201:27018"); //127.0.0.1:27017
            var database = client.GetDatabase("gpsreal_" + year + "" + mont);
            var collectionDate = database.GetCollection<GpsTracker>("gps_" + day);///.Find(d=> true).Filter();
            try
            {
                #region querys
                //var deviceActive = collectionDate.AsQueryable()
                //            .GroupBy(g => g.Tid)
                //            .Select(s => new GpsTracker
                //            {
                //                Id = s.First().Id,
                //                Tid = s.Key,
                //            })
                //            .Count();//Device active
                //var listDevices = collectionDate.AsQueryable()
                //    .OrderByDescending(d => d.Tm)
                //    .Take(deviceActive).ToList();
                //var veicleDevice = _mySQLContext.vehicleDevices.
                //    Join(_mySQLContext.groupInfos,
                //    vd => vd.GroupID,
                //    gi => gi.Id,
                //    (vd, gi) => new { vd, gi }).
                //    Where(w => w.vd.GroupID == w.gi.Id).
                //    Select(s => new
                //    {
                //        s.vd.DeviceID,
                //        s.vd.CarLicence,
                //        s.vd.EconomicNumber,
                //        s.gi.GroupName,
                //        s.gi.Remark,
                //        s.gi.Id
                //    }).ToList();

                //var simpleResponse = listDevices.AsQueryable().Join(veicleDevice.AsQueryable(),
                //    ld => ld.Tid,
                //    vd => vd.DeviceID,
                //    (ld, vd) => new { ld, vd })
                //    .Where(w => w.ld.Tid == w.vd.DeviceID)
                //    .Select(s => new
                //    {
                //        s.vd.DeviceID,
                //        s.vd.CarLicence,
                //        s.vd.EconomicNumber,
                //        s.vd.GroupName,
                //        s.vd.Remark,
                //        s.vd.Id,
                //        s.ld.GeoLocation.Lat,
                //        s.ld.GeoLocation.Long
                //    })
                //    .ToList();
                #endregion
                var simpleResponse = collectionDate.AsQueryable()
                    .OrderByDescending(d => d.Tm)
                    .Take(collectionDate.AsQueryable()
                            .GroupBy(g => g.Tid)
                            .Select(s => new GpsTracker
                            {
                                Id = s.First().Id,
                                Tid = s.Key,
                            })
                            .Count()).ToList()
                     .Join(_mySQLContext.vehicleDevices.
                                Join(_mySQLContext.groupInfos,
                                    vd => vd.GroupID,
                                    gi => gi.Id,
                                    (vd, gi) => new { vd, gi }).
                                Where(w => w.vd.GroupID == w.gi.Id).
                                Select(s => new
                                {
                                    s.vd.DeviceID,
                                    s.vd.CarLicence,
                                    s.vd.EconomicNumber,
                                    s.gi.GroupName,
                                    s.gi.Remark,
                                    s.gi.Id
                                }).ToList(),
                        ld => ld.Tid,
                        vd => vd.DeviceID,
                        (ld, vd) => new { ld, vd })
                    .Where(w => w.ld.Tid == w.vd.DeviceID)
                    .Select(s => new
                    {
                        s.vd.DeviceID,
                        s.vd.CarLicence,
                        s.vd.EconomicNumber,
                        s.vd.GroupName,
                        s.vd.Remark,
                        s.vd.Id,
                        s.ld.GeoLocation.Lat,
                        s.ld.GeoLocation.Long
                    })
                    .ToList();

                List<LocationResponse> locationResponses = new List<LocationResponse>();

                foreach (var item in simpleResponse)
                {
                    locationResponses.Add(new LocationResponse
                    {
                        Id = item.Id,
                        CarLicence = item.CarLicence,
                        EconomicNumber = item.EconomicNumber,
                        GroupName = item.GroupName,
                        Remark = item.Remark,
                        DeviceID = item.DeviceID,
                        Lat = item.Lat,
                        Long = item.Long
                    });
                }
                return locationResponses;
            }
            catch (Exception ex)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(ex.Message);
                _customSystemLog.AddLog(systemLog);
                return null;
            }
        }
        public IList<GpsTrackerNear> GetNear(double lat, double lon)
        {
            DateTime fecha = DateTime.Now;
            string day = fecha.ToShortDateString().Substring(0, 2);
            string mont = fecha.ToShortDateString().Substring(3, 2);
            string year = fecha.ToShortDateString().Substring(6, 4);
            var client = new MongoClient("mongodb://192.168.11.201:27018"); //127.0.0.1:27017
            var database = client.GetDatabase("gpsreal_" + year + "" + mont);
            var collectionDate = database.GetCollection<GpsTracker>("gps_" + day);///.Find(d=> true).Filter();
            try
            {
                var simpleResponseOrder = collectionDate.AsQueryable()
                      .OrderByDescending(d => d.Tm)
                      .Take(collectionDate.AsQueryable()
                              .GroupBy(g => g.Tid)
                              .Select(s => new GpsTracker
                              {
                                  Id = s.First().Id,
                                  Tid = s.Key,
                              })
                              .Count()).ToList()
                       .Join(_mySQLContext.vehicleDevices.
                                  Join(_mySQLContext.groupInfos,
                                      vd => vd.GroupID,
                                      gi => gi.Id,
                                      (vd, gi) => new { vd, gi }).
                                  Where(w => w.vd.GroupID == w.gi.Id).
                                  Select(s => new
                                  {
                                      s.vd.DeviceID,
                                      s.vd.CarLicence,
                                      s.vd.EconomicNumber,
                                      s.gi.GroupName,
                                      s.gi.Remark,
                                      s.gi.Id
                                  }).ToList(),
                          ld => ld.Tid,
                          vd => vd.DeviceID,
                          (ld, vd) => new { ld, vd })
                      .Where(w => w.ld.Tid == w.vd.DeviceID)
                      .Select(s => new
                      {
                          s.vd.DeviceID,
                          s.vd.CarLicence,
                          s.vd.EconomicNumber,
                          s.vd.GroupName,
                          s.vd.Remark,
                          s.vd.Id,
                          s.ld.GeoLocation.Lat,
                          s.ld.GeoLocation.Long
                      })
                      .ToList();

                List<GpsTrackerNear> devineNear = new List<GpsTrackerNear>();

                foreach (var item in simpleResponseOrder)
                {
                    devineNear.Add(new GpsTrackerNear
                    {
                        Id = item.Id,
                        CarLicence = item.CarLicence,
                        EconomicNumber = item.EconomicNumber,
                        GroupName = item.GroupName,
                        Remark = item.Remark,
                        DeviceID = item.DeviceID,
                        Lat = item.Lat,
                        Long = item.Long,
                        Distance = DistanceGeo(item.Lat, item.Long, lat, lon)
                    });
                }

                return devineNear.OrderBy(o => o.Distance).ToList();
            }
            catch (Exception ex)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(ex.Message);
                _customSystemLog.AddLog(systemLog);
                return null;
            }
        }
        public IList<GpsTracker> GetByIdAsync(string id)
        {
            DateTime fecha = DateTime.Now;
            string day = fecha.ToShortDateString().Substring(0, 2);
            string mont = fecha.ToShortDateString().Substring(3, 2);
            string year = fecha.ToShortDateString().Substring(6, 4);
            var client = new MongoClient("mongodb://192.168.11.201:27018"); //127.0.0.1:27017
            var database = client.GetDatabase("gpsreal_" + year + "" + mont);
            var collectionDate = database.GetCollection<GpsTracker>("gps_" + day);///.Find(d=> true).Filter();
            return collectionDate.AsQueryable()
                .Where(w => w.Tid == id)
                .ToList();
        }
        public double DistanceGeo(double latDevice, double lonDevice, double latEmergency, double lonEmergency)
        {
            /*
              √(𝑥1 − 𝑥2)2 + (𝑦1 − 𝑦2)2 
             */
            try
            {
                return Math.Sqrt(
                        Math.Pow((latDevice - latEmergency), 2) +
                        Math.Pow((lonDevice - lonEmergency), 2)
                       );
                //double EarthRadius = 6371;
                //double Lat = (Convert.ToDouble(latEmergency) -
                //    Convert.ToDouble(latDevice)) * (Math.PI / 180);
                //double Lon = (Convert.ToDouble(lonEmergency) -
                //    (Convert.ToDouble(lonDevice))) * (Math.PI / 180);
                //double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(Convert.ToDouble(latDevice) *
                //    (Math.PI / 180)) * Math.Cos(Convert.ToDouble(latEmergency) * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
                //double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                //return (EarthRadius * c); // retorna en km
            }
            catch (Exception ex)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(ex.Message);
                _customSystemLog.AddLog(systemLog);
                return 0;
            }
        }
    }
}

using GFDSystems.Vigitech.DAL.Entities.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAL
{
    public class ConectionMongoDB
    {
        DateTime fecha = DateTime.Now;
        
        
        public IList<GpsTracker> ClientMongoDB()
        {///05/03/2021 17:37:20
            DateTime fecha = DateTime.Now;
            string day = fecha.ToShortDateString().Substring(0, 2);//"gps_" + DateTime.Now.Year + "_" + DateTime.Now.Month;
            string mont = fecha.ToShortDateString().Substring(3, 2);
            string year = fecha.ToShortDateString().Substring(6, 4);
            var client = new MongoClient("mongodb://192.168.11.201:27018"); //127.0.0.1:27017
            var database = client.GetDatabase("gpsreal_" + year + "" + mont);
            var collectionDate = database.GetCollection<GpsTracker>("gps_" + day);
            return (IList<GpsTracker>)collectionDate;
        }
    }
}

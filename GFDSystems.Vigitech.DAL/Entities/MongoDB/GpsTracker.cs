using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Serializers;
using GFDSystems.Vigitech.DAO.Tools.MongoDB;

namespace GFDSystems.Vigitech.DAL.Entities.MongoDB
{
    public class GpsTracker
    { 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("alarmid")]
        public string Alarmid { get; set; }
        [BsonElement("tid")]
        public string Tid { get; set; }
        [BsonElement("type")]
        public int TypeAlarm { get; set; }
        [BsonElement("flag")]
        public int Flag { get; set; }
        [BsonElement("pt")]
        public int Pt { get; set; }
        [BsonElement("sta")]
        public int Sta { get; set; }
        [BsonElement("sp")]
        public int Sp { get; set; }
        [BsonElement("d")]
        public int D { get; set; }
        [BsonElement("alt")]
        public int Alt { get; set; }
        [BsonElement("deal")]
        public string Deal { get; set; }
        [BsonElement("dt")]
        public string Dt { get; set; }
        [BsonElement("dm")]
        public string Dm { get; set; }
        [BsonElement("du")]
        public string Du { get; set; }
        [BsonElement("EVTUUID")]
        public string Evtuuid { get; set; }
        [BsonElement("desc")]
        public string desc { get; set; }
        [BsonElement("am")]
        public string Am { get; set; }
        [BsonElement("n")]
        public string N { get; set; }
        [BsonElement("v")]
        public int V { get; set; }
        [BsonElement("tm"), BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Tm { get; set; }
        [BsonElement("stm")]
        public DateTime Stm { get; set; }
        [BsonElement("gps")]
        [BsonSerializer(typeof(ArraySerializer))]
        public GeoLocation GeoLocation { get; set; }
    }
}

using GFDSystems.Vigitech.DAL.Entities.MongoDB;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Tools.MongoDB
{
    public class ArraySerializer : SerializerBase<GeoLocation>
    {
        public override GeoLocation Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartArray();
            var lat = context.Reader.ReadDouble();
            var lon = context.Reader.ReadDouble();
            context.Reader.ReadEndArray();

            return new GeoLocation() { Long = (float)lon, Lat = (float)lat };
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GeoLocation value)
        {
            context.Writer.WriteStartArray();
            context.Writer.WriteDouble(value.Lat);
            context.Writer.WriteDouble(value.Long);
            context.Writer.WriteEndArray();
        }
    }
}

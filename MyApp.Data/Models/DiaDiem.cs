using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace netcorebus.Models
{
    public class DiaDiem
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("id")]
        public string XeId { get; set; }

        [BsonElement("noidi")]
        public string NoiDi { get; set; }

        [BsonElement("noiden")]
        public string NoiDen { get; set; }

        [BsonElement("tuyen")]
        public List<Dictionary<string, List<Tuyen>>> Tuyen { get; set; }
    }
}

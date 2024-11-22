using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace netcorebus.Models
{
    public class Xe
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public List<string> Dsghe { get; set; }
        public string Loaixe { get; set; }
    }
}

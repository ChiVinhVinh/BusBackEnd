using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace netcorebus.Models
{
    public class Xe
    {
        [BsonElement("ghế")]
        public string Ghe { get; set; }
        [BsonElement("trạng thái")]
        public string TrangThai { get; set; }
    }
}

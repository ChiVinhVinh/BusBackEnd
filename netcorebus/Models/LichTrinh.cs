using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace netcorebus.Models
{
    public class LichTrinh
    {
        [BsonId]  
        public ObjectId _id { get; set; }
        public int idLichTrinh { get; set; }
        public int idXe { get; set; }
        public int idTuyen { get; set; }
        public string Loaixe { get; set; }
        [BsonElement("noidi")]
        public string noidi { get; set; }
        [BsonElement("noiden")]
        public string noiden { get; set; }
        [BsonElement("ngaydi")]
        public string ngaydi { get; set; }
        [BsonElement("tuyen")]
        public List<Tuyen> tuyen { get; set; } 
        public List<Xe> dsghe { get; set; } 
        public double price { get; set; }
    }
}

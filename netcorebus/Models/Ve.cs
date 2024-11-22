using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace netcorebus.Models
{
    public class Ve
    {
        public string maghe {  get; set; }
        public string noidi { get; set; }
        public string noiden { get; set; }
        public string ngaydi { get; set; }
        public double price { get; set; }
    }
}

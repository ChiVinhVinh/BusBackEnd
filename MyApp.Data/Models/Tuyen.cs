using MongoDB.Bson.Serialization.Attributes;

namespace netcorebus.Models
{
    
        public class Tuyen
        {
        public string Id { get; set; }
        public int IdTuyen { get; set; }
        public int Idlocation { get; set; }
        public List<TuyenDetail> TuyenDetails { get; set; } 
        public int IdXe { get; set; }
        public DateTime Ngay { get; set; }
    }
   
}

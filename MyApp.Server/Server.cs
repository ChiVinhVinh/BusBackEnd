using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MyApp.Server
{
    public class Server
    {
        private readonly IMongoCollection<Xe> _xeCollection;
        private readonly IMongoCollection<DiaDiem> _diadiemCollection;
        private readonly IMongoCollection<Tuyen> _tuyenCollection;
        public Server (IMongoDatabase database)
        {
            _xeCollection = database.GetCollection<Xe>("Xe");
            _diadiemCollection = database.GetCollection<DiaDiem>("DiaDiem");
            _tuyenCollection = database.GetCollection<Tuyen>("Tuyen");
        }
        public async Task<List<LichTrinh>> GetLichTrinhAsync()
        {
            var aggregationPipeline = new[]
            {  
    
            new BsonDocument("$lookup", new BsonDocument
               {
                { "from", "tuyen" },
                { "localField", "idXe" },
                { "foreignField", "idXe" },
                { "as", "tuyen_info" }
               }),

            // Làm phẳng mảng "tuyen_info" để mỗi document chỉ chứa một tuyến từ "tuyen"
            new BsonDocument("$unwind", "$tuyen_info"),

            // Kết hợp "tuyen_info" với collection "diadiem" dựa trên "idlocation"
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "diadiem" },
                { "localField", "tuyen_info.idlocation" },
                { "foreignField", "idlocation" },
                { "as", "diadiem_info" }
            }),

            // Làm phẳng mảng "diadiem_info" để mỗi document chỉ chứa một địa điểm
            new BsonDocument("$unwind", "$diadiem_info"),

            // Tạo cột "lichtrinh" với thông tin tổng hợp cho mỗi "idXe"
            new BsonDocument("$project", new BsonDocument
            {
                { "_id", 0 },
                { "idXe", 1 },
                { "Loaixe", 1 },
                { "dsghe", 1 },
                { "diadiem", "$diadiem_info.location" },
                { "lichtrinh", new BsonDocument
                    {
                        { "kh", "$tuyen_info.kh" },
                        { "kt", "$tuyen_info.kt" },
                        { "timebd", "$tuyen_info.timebd" },
                        { "timekt", "$tuyen_info.timekt" }
                    }
                }
            })
            };

        }
    }
}

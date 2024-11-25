using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using netcorebus.Models;
using System.Threading.Tasks;


namespace netcorebus.Controllers
{
    [Route("lichtrinh")]
    [ApiController]
    public class lichtrinhController : Controller
    {
        private readonly IMongoCollection<LichTrinh> _lichTrinhCollection;
        public lichtrinhController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Bus");
            _lichTrinhCollection = database.GetCollection<LichTrinh>("lichtrinh");
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateGhe(int id, [FromBody] LichTrinh lichTrinh)
        {
            try
            {
                var filter = Builders<LichTrinh>.Filter.Eq(x => x.idLichTrinh, id);
                var result=await _lichTrinhCollection.Find(filter).FirstOrDefaultAsync();
                if (result == null)
                {
                    await _lichTrinhCollection.InsertOneAsync(lichTrinh);
                }
                else {
                   var update=Builders<LichTrinh>.Update.Set(x=>x.dsghe,lichTrinh.dsghe);
                   await _lichTrinhCollection.UpdateOneAsync(filter,update);
                }
                return StatusCode(201,"Đặt lịch thành công");
            }
            catch (Exception ex) { 
                    return BadRequest(ex.Message);
            }
        }
    }
}

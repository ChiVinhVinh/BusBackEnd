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
        public async Task<IActionResult> UpdateGhe(int id, [FromBody] List<Xe> dsGhe)
        {
            try
            {
                var filter = Builders<LichTrinh>.Filter.Eq(x => x.idLichTrinh, id);
                var update = Builders<LichTrinh>.Update.Set(x => x.dsghe, dsGhe);
                var result = await _lichTrinhCollection.UpdateOneAsync(filter, update);
                if (result.ModifiedCount == 0)
                    return NotFound("Khong tim thay");
                var updateLichTrinh = await _lichTrinhCollection.Find(filter).FirstOrDefaultAsync();
                return Ok(updateLichTrinh);
            }
            catch (Exception ex) { 
                    return BadRequest(ex.Message);
            }
        }
    }
}

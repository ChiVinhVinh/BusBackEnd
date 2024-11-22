using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using netcorebus.Models;
namespace netcorebus.Controllers
{
    [Route("/api")]
    [ApiController]
    public class VeController : Controller
    {
        private readonly IMongoCollection<Ve> _veCollection;
        public VeController(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Bus");
            _veCollection = database.GetCollection<Ve>("Ve");
        }
        [HttpPost]
        public async Task<IActionResult> CreateVe([FromBody] Ve Ve)
        {
            await _veCollection.InsertOneAsync(Ve);
            return Ok(Ve);
        }
        
    }
}

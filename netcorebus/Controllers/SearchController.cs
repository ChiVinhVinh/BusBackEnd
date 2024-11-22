using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using netcorebus.Models;
using System.Threading.Tasks;

[Route("")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IMongoCollection<LichTrinh>_lichTrinhCollection;

    public SearchController(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("Bus");
        _lichTrinhCollection = database.GetCollection<LichTrinh>("lichtrinh");
    }
    [HttpPost]
    public async Task<IActionResult> PostSearch([FromBody] SearchRequest request)
    {
        Console.WriteLine(request.DiemDi);
        Console.WriteLine(request.DiemDen);
        Console.WriteLine(request.NgayDi);
       var filter = Builders<LichTrinh>.Filter.Eq("noidi", request.DiemDi) &
                     Builders<LichTrinh>.Filter.Eq("noiden", request.DiemDen) &
                     Builders<LichTrinh>.Filter.Eq("ngaydi", request.NgayDi);
        var results = await _lichTrinhCollection.Find(filter).ToListAsync();

        return Ok(results);
    }
}

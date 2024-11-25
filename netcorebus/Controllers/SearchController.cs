using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using netcorebus.Models;
using System.Threading.Tasks;
using System.Dynamic;
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
        var filter = Builders<LichTrinh>.Filter.Eq("noidi", request.DiemDi) &
                     Builders<LichTrinh>.Filter.Eq("noiden", request.DiemDen);
        var count1 = 0;
        var count2 = 0;
        var count3 = 0;
        var count4 = 0;
        var allresults = await _lichTrinhCollection.Find(filter).ToListAsync();
        List<LichTrinh> results = new List<LichTrinh>();
        if (allresults.Count > 0)
        {
            results = allresults.Where(x => x.ngaydi == request.NgayDi).ToList();
            var maxId = await _lichTrinhCollection
                        .Find(FilterDefinition<LichTrinh>.Empty)
                        .SortByDescending(x => x.idLichTrinh)
                        .Limit(1)
                        .Project(x => x.idLichTrinh)
                        .FirstOrDefaultAsync();
            var distinctIdTuyens = allresults.Select(x => x.idTuyen).Distinct();
            if (results.Count == 0)
            {
                int id=maxId + 1;
                foreach (var idTuyen in distinctIdTuyens)
                {
                    var template = allresults.FirstOrDefault(x => x.idTuyen == idTuyen);
                    if (template != null)
                    {
                        var newLichTrinh = new LichTrinh
                        {
                            idLichTrinh=id++,
                            idXe = template.idXe,
                            idTuyen = template.idTuyen,
                            noidi = template.noidi,
                            noiden = template.noiden,
                            ngaydi = request.NgayDi, 
                            tuyen = template.tuyen,
                            price = template.price,
                            Loaixe=template.Loaixe,
                            dsghe = template.dsghe.Select(ghe => new Xe
                            {
                                Ghe = ghe.Ghe,
                                TrangThai= "Chưa đặt"
                            }).ToList()
                        };
                        results.Add(newLichTrinh);
                    }
                }
            }
            if (results.Count > 0)
            {
                if (results.Count < distinctIdTuyens.Count())
                {

                    int id = maxId + 1;
                    var exitIdtuyen = results.Select(x => x.idTuyen);
                    var lostIdtuyen=distinctIdTuyens.Except(exitIdtuyen);
                    foreach (var idTuyen in lostIdtuyen)
                    {
                        var template = allresults.FirstOrDefault(x => x.idTuyen == idTuyen);
                        if (template != null)
                        {
                            var newLichTrinh = new LichTrinh
                            {
                                idLichTrinh = id++,
                                idXe = template.idXe,
                                idTuyen = template.idTuyen,
                                noidi = template.noidi,
                                noiden = template.noiden,
                                ngaydi = request.NgayDi,
                                tuyen = template.tuyen,
                                price = template.price,
                                Loaixe = template.Loaixe,
                                dsghe = template.dsghe.Select(ghe => new Xe
                                {
                                    Ghe = ghe.Ghe,
                                    TrangThai = "Chưa đặt"
                                }).ToList()
                            };
                            results.Add(newLichTrinh);
                        }
                    }
                }
                else
                {
                    results.ForEach(x =>
                    {
                        var time = x.tuyen[0].timebd;
                        var timeParts = time.Split(":");
                        var hour = int.Parse(timeParts[0]);
                        if (hour >= 0 && hour < 6)
                            count1++;
                        else if (hour >= 6 && hour < 12)
                            count2++;
                        else if (hour >= 12 && hour < 18)
                            count3++;
                        else if (hour >= 18 && hour < 24)
                            count4++;
                    });
                }
               
            }
        }
        Console.WriteLine(results.Count);
        Console.WriteLine(count1);
        Console.WriteLine(count2);
        Console.WriteLine(count3);
        Console.WriteLine(count4);
        return Ok(new
        {
            Trip = results,
            TimeCounts = new
            {
                EarlyMorning = count1,
                Morning = count2,
                AfterNoon = count3,
                Evening = count4,
            }
        });
    }
}

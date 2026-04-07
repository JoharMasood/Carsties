using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;

public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdate = await DB.Find<Item, string>()
            .Sort(x => x.Descending(x => x.UpdatedAT))
            .Project(x => x.UpdatedAT.ToString())
            .ExecuteFirstAsync();

        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"]
            + "/api/auctions?date" + lastUpdate);    
    }
}
using ShareMarket.WinApp.Models;
using System.Net;

namespace ShareMarket.WinApp.Services;

public class GrowwService
{
    readonly static HttpClient Client = new() { BaseAddress = new Uri("https://groww.in/") };
    public async static Task<GrowwStockModel?> GetStockPrice(string code)
    {
        var response = await Client.GetAsync($"v1/api/stocks_data/v1/accord_points/exchange/NSE/segment/CASH/latest_prices_ohlc/{code}");
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsAsync<GrowwStockModel>();
        }
        return null;
    }
}
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Extensions;
using ShareMarket.Core.Models.Results;
using ShareMarket.Core.Models.Services.Groww;
using System;
using System.Net;

namespace ShareMarket.Core.Services;

public class GrowwService
{
    readonly static HttpClient Client = new() { BaseAddress = new Uri("https://groww.in/") };
    public async static Task<Result<GrowwStockModel?>> GetLTPPrice(string code)
    {
        Result<GrowwStockModel?> result = new(null);
        try
        {
            var response = await Client.GetAsync($"v1/api/stocks_data/v1/accord_points/exchange/NSE/segment/CASH/latest_prices_ohlc/{code}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result.ResultObject = await response.Content.ReadAsAsync<GrowwStockModel>();
            }
            else
            {
                result.AddError($"API Error for Code :{code}, {await response.Content.ReadAsStringAsync()}");
                return result;
            }
        }
        catch (Exception ex)
        {
            result.AddError($"Exception at SyncFundamentalEquityPandit: {ex.Message}");
            return result;
        }
        return result;
    }
}
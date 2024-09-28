using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Models;
using ShareMarket.WinApp.Store;
using ShareMarket.WinApp.Utilities;
using System.Globalization;
using System.Net;

namespace ShareMarket.WinApp.Services;

public class GrowwService
{
    readonly static HttpClient Client = new() { BaseAddress = new Uri("https://groww.in/") };
    public async static Task<GrowwStockModel?> GetStockPrice(string code)
    {
        try
        {
            var response = await Client.GetAsync($"v1/api/stocks_data/v1/accord_points/exchange/NSE/segment/CASH/latest_prices_ohlc/PFIZER");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<GrowwStockModel>();
            }
        }
        catch (Exception ex)
        {
        }
       
        return null;
    }

    public async static Task<bool>  SyncPriceEqityPandit(EquityStock stock)
    {
        var url = $"https://www.equitypandit.com/historical-data/{stock.Code}";
        var db = new ShareMarketContext();
        List<EquityPriceHistory> histories = [];

        try
        {
            DateOnly startDate;
            var history_Old = await db.EquityPriceHistories.Where(x => x.Code == stock.Code)
                                    .OrderByDescending(o => o.Date).FirstOrDefaultAsync();
            startDate = history_Old?.Date ?? new DateOnly(2024, 1, 1);

            var GrowwStockModel=new GrowwStockModel();
            HttpClient Client1 = new();
           
            var response = await Client1.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
               
                var pageContents = response.Content.ReadAsStringAsync().Result;
                doc.LoadHtml(pageContents);
                var table =doc.GetElementbyId("testTable2");
                if (table == null) 
                {
                    EquityHistorySyncLog obj = new EquityHistorySyncLog
                    {
                        Name = stock.Name,
                        Code = stock.Code,
                        Provider = url,
                        ErrorMessage = "testTable2 not found in response string",
                        CreatedOn = DateTime.Now
                    };
                    db.Add(obj);
                    db.SaveChanges();
                    return false;
                }
                
                var delTable = table.SelectNodes("//tbody")[0];
                var rows = delTable.ChildNodes.ToList().Where(x => x.Name == "tr");
                foreach ( var row in rows )
                {                   
                    var colums = row.ChildNodes;
                    var date = DateOnly.Parse(colums[0].InnerText);
                    if (date < startDate)
                        continue;                    

                    var close =decimal.Parse(colums[1].InnerText);
                    var open = decimal.Parse(colums[2].InnerText);
                    var high = decimal.Parse(colums[3].InnerText);
                    var low  = decimal.Parse(colums[4].InnerText);
                    histories.Add(new EquityPriceHistory
                    {
                        Date = date,
                        Close = close,
                        Open = open,
                        High = high,
                        Low = low,
                        Code = stock.Code,
                        Name = stock.Name,
                        CreatedById = 1,
                        CreatedOn = DateTimeOffset.Now,
                        EquityId = stock.Id
                    });                   
                }
                await Utility.CreateOrUpdateHistory(histories);
            }
            else
            {
                var er = await response.Content.ReadAsStringAsync();
                EquityHistorySyncLog obj = new EquityHistorySyncLog
                {
                    Name = stock.Name,
                    Code = stock.Code,
                    Provider = url,
                    ErrorMessage = $"API Error: {er}",
                    CreatedOn = DateTime.Now
                };
                db.Add(obj);
                db.SaveChanges();
                return false;
            }
        }
        catch (Exception ex)
        {
            EquityHistorySyncLog obj = new EquityHistorySyncLog
            {
                Name = stock.Name,
                Code = stock.Code,
                Provider = url,
                ErrorMessage = $"testTable2 not found in response string, {ex.Message}",
                CreatedOn = DateTime.Now
            };
            db.Add(obj);
            db.SaveChanges();
            return false;
        }
        return true;
    }

    static DateTime? ExtractDateFromMessage(string dateString)
    {
        // Specify the expected format
        string format = "dd MMM yy";
        DateTime date;
        // Attempt to parse the date
        if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Console.WriteLine($"Parsed Date: {date.ToString("yyyy-MM-dd")}");
        }
        else
        {
            Console.WriteLine("Invalid date format.");
        }
        return date;
    }
}
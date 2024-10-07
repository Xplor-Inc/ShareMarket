using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.WinApp.Models;
using ShareMarket.WinApp.Store;
using System.Net;

namespace ShareMarket.WinApp.Services;

public class GrowwService
{
    readonly static HttpClient Client = new() { BaseAddress = new Uri("https://groww.in/") };
    public async static Task<GrowwStockModel?> GetStockPrice(string code)
    {
        try
        {
            var response = await Client.GetAsync($"v1/api/stocks_data/v1/accord_points/exchange/NSE/segment/CASH/latest_prices_ohlc/{code}");
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

    public async static Task<List<EquityPriceHistory>>  SyncPriceEquityPandit(EquityStock stock)
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
                    EquityHistorySyncLog obj = new()
                    {
                        Name = stock.Name,
                        Code = stock.Code,
                        Provider = url,
                        ErrorMessage = "testTable2 not found in response string",
                        CreatedOn = DateTime.Now
                    };
                    db.Add(obj);
                    db.SaveChanges();
                    return histories;
                }
                
                var delTable = table.SelectNodes("//tbody")[0];
                var rows = delTable.ChildNodes.ToList().Where(x => x.Name == "tr");
                foreach ( var row in rows )
                {                   
                    var colums = row.ChildNodes;
                    var date = DateOnly.Parse(colums[0].InnerText);
                    if (date < startDate)
                        continue;                    

                    var close = decimal.Parse(colums[1].InnerText);
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
                //await Utility.CreateOrUpdateHistory(histories);
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
                return histories;
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
            return histories;
        }
        return histories;
    }
    public async static Task<EquityStock> SyncFundamentalEquityPandit(EquityStock stock)
    {
        var url = $"https://www.equitypandit.com/share-price/{stock.Code}";
        var db = new ShareMarketContext();
        List<EquityPriceHistory> histories = [];

        try
        {
            DateOnly startDate;
            var history_Old = await db.EquityPriceHistories.Where(x => x.Code == stock.Code)
                                    .OrderByDescending(o => o.Date).FirstOrDefaultAsync();
            startDate = history_Old?.Date ?? new DateOnly(2024, 1, 1);

            var GrowwStockModel = new GrowwStockModel();
            HttpClient Client1 = new();

            var response = await Client1.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();

                var pageContents = response.Content.ReadAsStringAsync().Result;
                doc.LoadHtml(pageContents);
                var table = doc.GetElementbyId("ratio_section");
                if (table is null) return stock;
                var NSE_TODAYS_HIGH_LOW = doc.GetElementbyId("NSE_TODAYS_HIGH_LOW").InnerText;
                var NSE_OPEN_PRICE = doc.GetElementbyId("NSE_OPEN_PRICE").InnerText;
                var NSE_PRICE = doc.GetElementbyId("NSE_PRICE").InnerText;

                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants(0).Where(n => n.HasClass("tx-uppercase")).ToList();
                foreach (var item in nodes)
                {
                    if (item.InnerText == "EPS")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.EPS = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "P/E")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.PE = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "P/B")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.PD = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "Dividend Yield")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.Dividend = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "Market Cap")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.MarketCap = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "Face Value")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.FaceValue = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "Book Value")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.BookValue = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "Debt/Equity")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.DebtEquity = decimal.Parse(value);
                        continue;
                    }
                    if (item.InnerText == "ROE")
                    {
                        string value = item.NextSibling.NextSibling.FirstChild.InnerText;
                        stock.ROE = decimal.Parse(value);
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            EquityHistorySyncLog obj = new EquityHistorySyncLog
            {
                Name = stock.Name,
                Code = stock.Code,
                Provider = url,
                ErrorMessage = $"Fundamental sync error: , {ex.Message}",
                CreatedOn = DateTime.Now
            };
            db.Add(obj);
            db.SaveChanges();
        }
        return stock;
    }

}
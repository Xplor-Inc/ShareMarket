using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Models;
using ShareMarket.WinApp.Store;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Policy;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

 public async static Task<bool>  SyncPriceEqityPandit(EquityStock stock,DateOnly? dtFromDate=null)
    {
        var url = $"https://www.equitypandit.com/historical-data/{stock.Code}";
        var db = new ShareMarketContext();
        List<EquityPriceHistory> histories = [];

        try
        {

            var GrowwStockModel=new GrowwStockModel();
            HttpClient Client1 = new();
           
            var response = await Client1.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
               
                var pageContents = response.Content.ReadAsStringAsync().Result;
                doc.LoadHtml(pageContents);
                var table =doc.GetElementbyId("testTable2");
                if (table == null) {
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
                    if (date.Year < 2024)
                        continue;
                    if (dtFromDate != null && date <= dtFromDate.Value)
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
           
                //db.EquityPriceHistories.FromSqlRaw("delete from EquityPriceHistories where EquityId=" + stock.Id);
                await db.Database.ExecuteSqlRawAsync("delete from EquityPriceHistories where EquityId=" + stock.Id);
                await db.AddRangeAsync(histories);
                await db.SaveChangesAsync();

                //string SearchArea = pageContents.Substring(pageContents.IndexOf("<tbody>"), pageContents.IndexOf("</tbody>")- pageContents.IndexOf("<tbody>"));
                //var strRecords = SearchArea.Split("<tr>");
                //for(int i = 1; i < strRecords.Length; i++)
                //{
                //    var columns = strRecords[i].Split("<td");
                //    var date = columns[0];
                //    var strDate = columns[1].Substring(1, 11);
                //    var dt= DateTime.Parse(strDate);
                //    if (dt < DateTime.Now.AddMonths(-3))
                //        continue;
                //    string price = columns[2].Substring(columns[2].IndexOf("<b>"), columns[2].IndexOf("</b>") - columns[2].IndexOf("<b>")).Replace("<b>","");
                //    string open = columns[3].Replace(">", "").Replace("</td", "");
                //    string high = columns[4].Replace(">", "").Replace("</td", "");
                //    string low = columns[5].Replace(">", "").Replace("</td", "");
                //    histories.Add(new EquityPriceHistory
                //    {
                //      //  Date = (DateOnly),
                //        Close = decimal.Parse(price),
                //        Open = decimal.Parse(open),
                //        High = decimal.Parse(high),
                //        Low = decimal.Parse(low),
                //        Code = code,
                //       // Name = StockList[CurrentStock].Name,
                //        //CreatedById = 1,
                //        //CreatedOn = DateTimeOffset.Now,
                //        //EquityId = StockList[CurrentStock].Id
                //    });

                //}
            }

        }
        catch (Exception ex)
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
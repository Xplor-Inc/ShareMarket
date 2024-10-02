using HtmlAgilityPack;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Extensions;
using ShareMarket.Core.Models.Results;
using System.Net;

namespace ShareMarket.Core.Services;

public class EquityPanditService
{
    readonly static HttpClient Client = new() { BaseAddress = new Uri("https://www.equitypandit.com") };

    public async static Task<Result<List<EquityPriceHistory>>> SyncPriceEquityPandit(EquityStock stock, DateOnly lastSyncDate)
    {
        var url = $"/historical-data/{stock.Code}";
        Result<List<EquityPriceHistory>> result = new([]);
        try
        {
            var response = await Client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var doc = new HtmlDocument();

                var pageContents = response.Content.ReadAsStringAsync().Result;
                doc.LoadHtml(pageContents);
                var table = doc.GetElementbyId("testTable2");
                if (table == null)
                {
                    result.AddError($"Page content not found at Node testTable2");
                    return result;
                }

                var delTable = table.SelectNodes("//tbody")[0];
                var rows = delTable.ChildNodes.ToList().Where(x => x.Name == "tr");
                foreach (var row in rows)
                {
                    var colums = row.ChildNodes;
                    var date = DateOnly.Parse(colums[0].InnerText);
                    if (date < lastSyncDate)
                        continue;

                    var close = decimal.Parse(colums[1].InnerText);
                    var open = decimal.Parse(colums[2].InnerText);
                    var high = decimal.Parse(colums[3].InnerText);
                    var low = decimal.Parse(colums[4].InnerText);
                    result.ResultObject.Add(new EquityPriceHistory
                    {
                        Date = date,
                        Close = close,
                        Open = open,
                        High = high,
                        Low = low,
                        Code = stock.Code,
                        Name = stock.Name,
                        EquityId = stock.Id
                    });
                }
            }
            else
            {
                result.AddError($"API Error Url :{url}, {await response.Content.ReadAsStringAsync()}");
                return result;
            }
        }
        catch (Exception ex)
        {
            result.AddError($"Exception at SyncPriceEquityPandit: {ex.Message}");
            return result;
        }
        return result;
    }

    public async static Task<Result<EquityStock>> SyncFundamentalEquityPandit(EquityStock stock)
    {
        Result<EquityStock> result = new(stock);
        var url = $"/share-price/{stock.Code}";

        try
        {
            var response = await Client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var doc = new HtmlDocument();

                var pageContents = response.Content.ReadAsStringAsync().Result;
                doc.LoadHtml(pageContents);
                var table = doc.GetElementbyId("ratio_section");
                if (table is null)
                {
                    result.AddError($"Page content not found at Node ratio_section");
                    return result;
                }
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
            else
            {
                result.AddError($"API Error Url :{url}, {await response.Content.ReadAsStringAsync()}");
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
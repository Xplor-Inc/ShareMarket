using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Entities.Schemes;
using ShareMarket.Core.Models.Services.Groww;
using ShareMarket.WinApp.Store;

namespace ShareMarket.WinApp.Utilities;

public class Utility
{
    private static readonly ShareMarketContext DbContext = new();

    public static async Task MigrateData()
    {
        #region Read Equity Stock Data
        if (!await DbContext.EquityStocks.AnyAsync())
        {
            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\AllStocks.json";

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                List<EquityStock>? stocks = JsonConvert.DeserializeObject<List<EquityStock>>(content);
                if (stocks == null) return;
                await DbContext.AddRangeAsync(stocks);
                await DbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Read Schemes Data
        if (!await DbContext.Schemes.AnyAsync())
        {
            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\Schemes.json";

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                List<Scheme>? schemes = JsonConvert.DeserializeObject<List<Scheme>>(content);
                if (schemes == null) return;
                await DbContext.AddRangeAsync(schemes);
                await DbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Read Scheme StockHoldings Data
        if (!await DbContext.SchemeEquityHoldings.AnyAsync())
        {
            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\StockHoldings.json";

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                List<SchemeEquityHolding>? holdings = JsonConvert.DeserializeObject<List<SchemeEquityHolding>>(content);
                if (holdings == null) return;
                await DbContext.AddRangeAsync(holdings);
                await DbContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Read EquityPriceHistory Data
        if (await DbContext.EquityPriceHistories.AnyAsync()) return;
        var equities = await DbContext.EquityStocks.Where(s => s.Id > 0).ToListAsync();
        foreach (var item in equities)
        {
            if (await DbContext.EquityPriceHistories.AnyAsync(x => x.Code == item.Code)) continue;
            //Text = $"Inserting history data {item.Name}";
            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\History\\{item.Code}.json";
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var histories = JsonConvert.DeserializeObject<List<EquityPriceHistory>>(content);
                List<EquityPriceHistory> list = [];
                histories?.ForEach(x =>
                {
                    if (!list.Any(a => a.Code == x.Code && a.Date == x.Date)) list.Add(x);
                });
                list.ForEach(s => { s.Id = 0; s.EquityId = item.Id; });
                await DbContext.AddRangeAsync(list);
                await DbContext.SaveChangesAsync();
            }
        }
        #endregion
    }
    public static async Task CreateOrUpdateHistory(List<EquityPriceHistory> histories)
    {
        foreach (var item in histories)
        {
            var x = await DbContext.EquityPriceHistories.FirstOrDefaultAsync(x => x.Code == item.Code && x.Date == item.Date);
            if (x != null)
            {
                x.Open          = item.Open;
                x.Close         = item.Close;
                x.High          = item.High;
                x.Low           = item.Low;
                x.UpdatedOn     = DateTimeOffset.Now;
                x.UpdatedById   = 1;
                DbContext.Update(x);
                await DbContext.SaveChangesAsync();
            }
            else
            {                
                DbContext.EquityPriceHistories.Add(item);
                await DbContext.SaveChangesAsync();
            }
        }

        //await RSICalculation(14, histories.First().Code);
        //await RSI_X_EMA(14, histories.First().Code);
        //await DMACalculation(histories.First().Code);
    }
    public static async Task RSI_X_EMA(int days, string code)
    {
        if (days != 14)
            throw new Exception($"RSI will be calculated for only 14 days intervel. {days} not allowed! ");
        var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
        if (stock is null) return;
        var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
        if (histories.Count == 0) return;
        for (int i = 0; i < histories.Count; i++)
        {
            if (i < 27) continue;

            if (i == 27)
            {
                var rSI14DMA = histories.Skip(days).Take(days).Average(a => a.RSI);
                histories[i].RSI14EMA = Math.Round(rSI14DMA, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                histories[i].RSI14EMA = Math.Round(((histories[i - 1].RSI14EMA * (days - 1)) + histories[i].RSI) / days, 2, MidpointRounding.AwayFromZero);
                histories[i].RSI14EMADiff = histories[i].RSI14EMA - histories[i - 1].RSI14EMA;
            }
            histories[i].UpdatedOn      = DateTimeOffset.Now;
            histories[i].UpdatedById    = 1;
        }

        var last = histories.Last();
        stock.RSI14EMA      = last.RSI14EMA;
        stock.RSI14EMADiff  = last.RSI14EMADiff;
        stock.UpdatedById   = last.UpdatedById;
        stock.UpdatedOn     = last.UpdatedOn;
        DbContext.EquityPriceHistories.UpdateRange(histories);
        DbContext.EquityStocks.UpdateRange(stock);
        await DbContext.SaveChangesAsync();
    }
    public static async Task RSICalculation(int days, string code)
    {
        var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
        if (stock is null) return;
        var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
        if (histories.Count == 0) return;
        var lastYear = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        for (int i = 0; i < histories.Count; i++)
        {
            if (i == 0) continue;
            var x0 = histories[i - 1];
            var x1 = histories[i];
            var profit = x1.Close - x0.Close;
            var loss = x0.Close - x1.Close;
            if (profit < 0) profit = 0;
            if (loss < 0) loss = 0;
            histories[i].Profit = profit;
            histories[i].Loss = loss;
            if (i > (days - 1))
            {
                if (i == days)
                {
                    var gain14Days = histories.Skip(1).Take(days).Average(s => s.Profit);

                    var loss14Days = histories.Skip(1).Take(days).Average(s => s.Loss);

                    histories[i].Avg14DaysLoss = Math.Round(loss14Days, 2, MidpointRounding.AwayFromZero);
                    histories[i].Avg14DaysProfit = Math.Round(gain14Days, 2, MidpointRounding.AwayFromZero);
                    if (loss14Days > 0)
                        histories[i].RS = Math.Round(gain14Days / loss14Days, 2, MidpointRounding.AwayFromZero);
                    histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
                }
                else
                {
                    var sum0 = Math.Round(((histories[i - 1].Avg14DaysProfit * (days - 1)) + histories[i].Profit) / days, 2, MidpointRounding.AwayFromZero);
                    var loss0 = Math.Round(((histories[i - 1].Avg14DaysLoss * (days - 1)) + histories[i].Loss) / days, 2, MidpointRounding.AwayFromZero);
                    histories[i].Avg14DaysLoss = loss0;
                    histories[i].Avg14DaysProfit = sum0;
                    if (loss0 > 0)
                        histories[i].RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
                    histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
                }
            }
        }
        var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).First();
        var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).First();
        var last = histories.Last();
        stock.RSI           = last.RSI;
        stock.DayHigh       = last.High;
        stock.DayLow        = last.Low;
        stock.YearHigh      = yH.High;
        stock.YearHighOn    = yH.Date;
        stock.YearLow       = yL.Low;
        stock.YearLowOn     = yL.Date;
        stock.LTP           = last.Close;
        stock.IsRaising     = stock.YearLowOn > stock.YearHighOn;

        DbContext.EquityPriceHistories.UpdateRange(histories);
        DbContext.EquityStocks.Update(stock);
        await DbContext.SaveChangesAsync();
    }
    public static async Task DMACalculation(string code)
    {
        var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
        if (stock is null) return;
        var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
        if (histories.Count == 0) return;
        for (int i = 0; i < histories.Count; i++)
        {
            if (i < 4) continue;

            histories[i].DMA5 = histories.Skip(i - 4).Take(5).Average(s => s.Close);
            if (i > 8)
                histories[i].DMA10 = histories.Skip(i - 9).Take(10).Average(s => s.Close);
            
            if (i > 18)
                histories[i].DMA20 = histories.Skip(i - 19).Take(20).Average(s => s.Close);
            
            if (i > 48)
                histories[i].DMA50 = histories.Skip(i - 49).Take(50).Average(s => s.Close);
            
            if (i > 98)
                histories[i].DMA100 = histories.Skip(i - 99).Take(100).Average(s => s.Close);
            
            if (i > 198)
                histories[i].DMA200 = histories.Skip(i - 199).Take(200).Average(s => s.Close); 
        }

        var last = histories.Last();
        stock.DMA5      = last.DMA5;
        stock.DMA10     = last.DMA10;
        stock.DMA20     = last.DMA20;
        stock.DMA50     = last.DMA50;
        stock.DMA100    = last.DMA100;
        stock.DMA200    = last.DMA200;

        DbContext.EquityPriceHistories.UpdateRange(histories);
        DbContext.EquityStocks.UpdateRange(stock);
        await DbContext.SaveChangesAsync();
    }
    public static async Task RSIIncrementalAsync(string code)
    {
        var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
        if (stock is null) return;
        var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
        if (histories.Count == 0) return;
        var last = histories.Last();
        var today = histories.First();
        var lastYear = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);

        var sum0 = Math.Round(((last.Avg14DaysProfit * 13) + today.Profit) / 14, 2, MidpointRounding.AwayFromZero);
        var loss0 = Math.Round(((last.Avg14DaysLoss * 13) + today.Loss) / 14, 2, MidpointRounding.AwayFromZero);
        today.Avg14DaysLoss = loss0;
        today.Avg14DaysProfit = sum0;
        if (loss0 > 0)
            today.RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
        today.RSI = Math.Round(100 - (100 / (1 + today.RS)), 2, MidpointRounding.AwayFromZero);

        var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).First();
        var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).First();

        stock.RSI           = last.RSI;
        stock.DayHigh       = last.High;
        stock.DayLow        = last.Low;
        stock.YearHigh      = yH.High;
        stock.YearHighOn    = yH.Date;
        stock.YearLow       = yL.Low;
        stock.YearLowOn     = yL.Date;
        stock.IsRaising     = stock.YearLowOn > stock.YearHighOn;
        DbContext.EquityPriceHistories.UpdateRange(histories);
        DbContext.EquityStocks.UpdateRange(stock);
        await DbContext.SaveChangesAsync();

    }
    public static async Task Rank(Label  label)
    {
        var holdin = await DbContext.SchemeEquityHoldings.Where(x => x.NatureName == "EQ" && x.Scheme != null && (x.Scheme.GrowwRating == null || x.Scheme.GrowwRating > 2)).AsNoTracking()
                                .Select(s => new { s.SchemeId, s.Code, s.CompanyName }).ToListAsync();

        var stocks = await DbContext.EquityStocks.Where(s => s.IsActive).AsNoTracking().ToListAsync();
        int count = 0;
        foreach (var stock in stocks)
        {
            count++;
            label.Text = $"{count}/{stocks.Count}. Rank By Schemes...{stock.Name}";
            var z = holdin.Count(c => string.Equals(c.Code?.Trim(), stock.Code.Trim()) || string.Equals(c.CompanyName?.Trim(), stock.Name.Trim()));
            stock.RankByGroww = z;
            DbContext.Update(stock);
            await DbContext.SaveChangesAsync();
        }
    }
    public static async Task<List<EquityStock>> Get51020DMA()
    {
        //var equities = await DbContext.EquityStocks.Where(x => x.DMA5 > 0 && x.DMA10 > 0 && x.DMA20 > 0
        //                                    && x.DMA5 > x.LTP && x.DMA10 < x.LTP && x.DMA20 < x.LTP && x.RSI14EMADiff < -1)
        //                                    .OrderByDescending(o => o.RankByGroww).ToListAsync();

        var equities = await DbContext.EquityStocks.Where(x => x.RankByGroww >= 50 && x.RSI14EMADiff < -1 && x.ROE >= 15 && x.PE < 60)
                                           .OrderByDescending(o => o.RankByGroww).ToListAsync();
        return equities;
    }
    public static async Task<List<EquityStock>> Get5_10_20_50DMA()
    {
        var equities = await DbContext.EquityStocks.Where(x => x.DMA5 > 0 && x.DMA10 > 0 && x.DMA20 > 0 && x.DMA50 > 0
                                            && x.DMA5 > x.LTP && x.DMA10 > x.LTP && x.DMA20 < x.LTP && x.DMA50 < x.LTP)
                                            .OrderByDescending(o => o.RankByGroww).ToListAsync();
        return equities;
    }
    public static DateOnly GetLastThursdayOfMonth(int year, int month)
    {
        // Start with the last day of the month
        DateOnly lastDayOfMonth = new(year, month, DateTime.DaysInMonth(year, month));

        // Work backward to find the last Thursday
        while (lastDayOfMonth.DayOfWeek != DayOfWeek.Thursday)
        {
            lastDayOfMonth = lastDayOfMonth.AddDays(-1);
        }

        return lastDayOfMonth;
    }
    public static void CreateExcelFromList(List<TradeBook> people, string filePath)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Data");
        worksheet.Cell(1, 1).Value = "Name";
        worksheet.Cell(1, 2).Value = "Code";
        worksheet.Cell(1, 3).Value = "BuyDate";
        worksheet.Cell(1, 4).Value = "BuyRate";
        worksheet.Cell(1, 5).Value = "LTP";
        worksheet.Cell(1, 6).Value = "SellDate";
        worksheet.Cell(1, 7).Value = "Target";
        worksheet.Cell(1, 8).Value = "High";
        worksheet.Cell(1, 9).Value = "Diff";
        worksheet.Cell(1, 10).Value = "Holding";
        worksheet.Cell(1, 11).Value = "Rank";
        worksheet.Cell(1, 12).Value = "StopLoss";
        worksheet.Cell(1, 13).Value = "SameDay";
        worksheet.Cell(1, 14).Value = "CapitalUsed";

        int row = 2;
        foreach (var person in people)
        {
            worksheet.Cell(row, 1).Value = person.Name;
            worksheet.Cell(row, 2).Value = person.Code;
            worksheet.Cell(row, 3).Value = person.BuyDate.ToShortDateString();
            worksheet.Cell(row, 4).Value = person.BuyRate;
            worksheet.Cell(row, 5).Value = person.LTP;
            worksheet.Cell(row, 6).Value = person.SellDate?.ToShortDateString();
            worksheet.Cell(row, 7).Value = person.Target;
            worksheet.Cell(row, 8).Value = person.High;
            worksheet.Cell(row, 9).Value = person.Diff;
            worksheet.Cell(row, 10).Value = person.Holding;
            worksheet.Cell(row, 11).Value = person.Rank;
            worksheet.Cell(row, 12).Value = person.StopLoss;
            worksheet.Cell(row, 14).Value = person.CapitalUsed;
            row++;
        }

        // Save the workbook to the file path
        workbook.SaveAs(filePath);
    }
}

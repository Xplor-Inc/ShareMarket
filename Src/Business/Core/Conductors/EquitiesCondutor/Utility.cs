using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Interfaces.Conductors;

namespace ShareMarket.Core.Conductors.EquitiesConductor.Utilities;

public class EquityStockConductor(IRepositoryConductor<EquityStock> EquityRepo, IRepositoryConductor<EquityPriceHistory> HistoryRepo)
{
    public async Task CreateOrUpdateHistoryAsync(List<EquityPriceHistory> histories)
    {
        foreach (var item in histories)
        {
            var x = await HistoryRepo.FindAll(x => x.Code == item.Code && x.Date == item.Date).ResultObject.FirstOrDefaultAsync();
            if (x != null)
            {
                x.Open          = item.Open;
                x.Close         = item.Close;
                x.High          = item.High;
                x.Low           = item.Low;
                x.UpdatedOn     = DateTimeOffset.Now;
                x.UpdatedById   = 1;
                await HistoryRepo.UpdateAsync(x, SystemConstant.SystemUserId);
            }
            else
            {
                await HistoryRepo.CreateAsync(item, SystemConstant.SystemUserId);
            }
        }
    }
    //public static async Task RSI_X_EMA(int days, string code)
    //{
    //    if (days != 14)
    //        throw new Exception($"RSI will be calculated for only 14 days intervel. {days} not allowed! ");
    //    var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
    //    if (stock is null) return;
    //    var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
    //    if (histories.Count == 0) return;
    //    for (int i = 0; i < histories.Count; i++)
    //    {
    //        if (i < 27) continue;

    //        if (i == 27)
    //        {
    //            var rSI14DMA = histories.Skip(days).Take(days).Average(a => a.RSI);
    //            histories[i].RSI14EMA = Math.Round(rSI14DMA, 2, MidpointRounding.AwayFromZero);
    //        }
    //        else
    //        {
    //            histories[i].RSI14EMA = Math.Round(((histories[i - 1].RSI14EMA * (days - 1)) + histories[i].RSI) / days, 2, MidpointRounding.AwayFromZero);
    //            histories[i].RSI14EMADiff = histories[i].RSI14EMA - histories[i - 1].RSI14EMA;
    //        }
    //        histories[i].UpdatedOn      = DateTimeOffset.Now;
    //        histories[i].UpdatedById    = 1;
    //    }

    //    var last = histories.Last();
    //    stock.RSI14EMA      = last.RSI14EMA;
    //    stock.RSI14EMADiff  = last.RSI14EMADiff;
    //    stock.UpdatedById   = last.UpdatedById;
    //    stock.UpdatedOn     = last.UpdatedOn;
    //    DbContext.EquityPriceHistories.UpdateRange(histories);
    //    DbContext.EquityStocks.UpdateRange(stock);
    //    await DbContext.SaveChangesAsync();
    //}
    //public static async Task RSICalculation(int days, string code)
    //{
    //    var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
    //    if (stock is null) return;
    //    var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
    //    if (histories.Count == 0) return;
    //    var lastYear = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
    //    for (int i = 0; i < histories.Count; i++)
    //    {
    //        if (i == 0) continue;
    //        var x0 = histories[i - 1];
    //        var x1 = histories[i];
    //        var profit = x1.Close - x0.Close;
    //        var loss = x0.Close - x1.Close;
    //        if (profit < 0) profit = 0;
    //        if (loss < 0) loss = 0;
    //        histories[i].Profit = profit;
    //        histories[i].Loss = loss;
    //        if (i > (days - 1))
    //        {
    //            if (i == days)
    //            {
    //                var gain14Days = histories.Skip(1).Take(days).Average(s => s.Profit);

    //                var loss14Days = histories.Skip(1).Take(days).Average(s => s.Loss);

    //                histories[i].Avg14DaysLoss = Math.Round(loss14Days, 2, MidpointRounding.AwayFromZero);
    //                histories[i].Avg14DaysProfit = Math.Round(gain14Days, 2, MidpointRounding.AwayFromZero);
    //                if (loss14Days > 0)
    //                    histories[i].RS = Math.Round(gain14Days / loss14Days, 2, MidpointRounding.AwayFromZero);
    //                histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
    //            }
    //            else
    //            {
    //                var sum0 = Math.Round(((histories[i - 1].Avg14DaysProfit * (days - 1)) + histories[i].Profit) / days, 2, MidpointRounding.AwayFromZero);
    //                var loss0 = Math.Round(((histories[i - 1].Avg14DaysLoss * (days - 1)) + histories[i].Loss) / days, 2, MidpointRounding.AwayFromZero);
    //                histories[i].Avg14DaysLoss = loss0;
    //                histories[i].Avg14DaysProfit = sum0;
    //                if (loss0 > 0)
    //                    histories[i].RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
    //                histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
    //            }
    //        }
    //    }
    //    var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).First();
    //    var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).First();
    //    var last = histories.Last();
    //    stock.RSI           = last.RSI;
    //    stock.DayHigh       = last.High;
    //    stock.DayLow        = last.Low;
    //    stock.YearHigh      = yH.High;
    //    stock.YearHighOn    = yH.Date;
    //    stock.YearLow       = yL.Low;
    //    stock.YearLowOn     = yL.Date;
    //    stock.LTP           = last.Close;
    //    stock.IsRaising     = stock.YearLowOn > stock.YearHighOn;

    //    DbContext.EquityPriceHistories.UpdateRange(histories);
    //    DbContext.EquityStocks.Update(stock);
    //    await DbContext.SaveChangesAsync();
    //}

    //public static async Task DMACalculation(string code)
    //{
    //    var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
    //    if (stock is null) return;
    //    var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
    //    if (histories.Count == 0) return;
    //    for (int i = 0; i < histories.Count; i++)
    //    {
    //        if (i < 4) continue;

    //        histories[i].DMA5 = histories.Skip(i - 4).Take(5).Average(s => s.Close);
    //        if (i > 8)
    //            histories[i].DMA10 = histories.Skip(i - 9).Take(10).Average(s => s.Close);
            
    //        if (i > 18)
    //            histories[i].DMA20 = histories.Skip(i - 19).Take(20).Average(s => s.Close);
            
    //        if (i > 48)
    //            histories[i].DMA50 = histories.Skip(i - 49).Take(50).Average(s => s.Close);
            
    //        if (i > 98)
    //            histories[i].DMA100 = histories.Skip(i - 99).Take(100).Average(s => s.Close);
            
    //        if (i > 198)
    //            histories[i].DMA200 = histories.Skip(i - 199).Take(200).Average(s => s.Close); 
    //    }

    //    var last = histories.Last();
    //    stock.DMA5      = last.DMA5;
    //    stock.DMA10     = last.DMA10;
    //    stock.DMA20     = last.DMA20;
    //    stock.DMA50     = last.DMA50;
    //    stock.DMA100    = last.DMA100;
    //    stock.DMA200    = last.DMA200;

    //    DbContext.EquityPriceHistories.UpdateRange(histories);
    //    DbContext.EquityStocks.UpdateRange(stock);
    //    await DbContext.SaveChangesAsync();
    //}
    //public static async Task RSIIncrementalAsync(string code)
    //{
    //    var stock = await DbContext.EquityStocks.Where(x => x.IsActive && x.Code == code).FirstOrDefaultAsync();
    //    if (stock is null) return;
    //    var histories = await DbContext.EquityPriceHistories.Where(x => x.Code == code).OrderBy(o => o.Date).ToListAsync();
    //    if (histories.Count == 0) return;
    //    var last = histories.Last();
    //    var today = histories.First();
    //    var lastYear = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);

    //    var sum0 = Math.Round(((last.Avg14DaysProfit * 13) + today.Profit) / 14, 2, MidpointRounding.AwayFromZero);
    //    var loss0 = Math.Round(((last.Avg14DaysLoss * 13) + today.Loss) / 14, 2, MidpointRounding.AwayFromZero);
    //    today.Avg14DaysLoss = loss0;
    //    today.Avg14DaysProfit = sum0;
    //    if (loss0 > 0)
    //        today.RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
    //    today.RSI = Math.Round(100 - (100 / (1 + today.RS)), 2, MidpointRounding.AwayFromZero);

    //    var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).First();
    //    var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).First();

    //    stock.RSI           = last.RSI;
    //    stock.DayHigh       = last.High;
    //    stock.DayLow        = last.Low;
    //    stock.YearHigh      = yH.High;
    //    stock.YearHighOn    = yH.Date;
    //    stock.YearLow       = yL.Low;
    //    stock.YearLowOn     = yL.Date;
    //    stock.IsRaising     = stock.YearLowOn > stock.YearHighOn;
    //    DbContext.EquityPriceHistories.UpdateRange(histories);
    //    DbContext.EquityStocks.UpdateRange(stock);
    //    await DbContext.SaveChangesAsync();

    //}
    //public static async Task Rank(Label  label)
    //{
    //    var holdin = await DbContext.SchemeEquityHoldings.Where(x => x.NatureName == "EQ" && x.Scheme != null && (x.Scheme.GrowwRating == null || x.Scheme.GrowwRating > 2)).AsNoTracking()
    //                            .Select(s => new { s.SchemeId, s.Code, s.CompanyName }).ToListAsync();

    //    var stocks = await DbContext.EquityStocks.Where(s => s.IsActive).AsNoTracking().ToListAsync();
    //    int count = 0;
    //    foreach (var stock in stocks)
    //    {
    //        count++;
    //        label.Text = $"{count}/{stocks.Count}. Rank By Schemes...{stock.Name}";
    //        var z = holdin.Count(c => string.Equals(c.Code?.Trim(), stock.Code.Trim()) || string.Equals(c.CompanyName?.Trim(), stock.Name.Trim()));
    //        stock.RankByGroww = z;
    //        DbContext.Update(stock);
    //        await DbContext.SaveChangesAsync();
    //    }
    //}

    //public static async Task<List<EquityStock>> Get102030DMA()
    //{
    //    var equities = await DbContext.EquityStocks.Where(x => x.DMA5 > 0 && x.DMA10 > 0 && x.DMA20 > 0
    //                                        && x.DMA5 > x.LTP && x.DMA10 < x.LTP && x.DMA20 < x.LTP)
    //                                        .OrderByDescending(o => o.RankByGroww).ToListAsync();
    //    return equities;
    //}
    //public static async Task<List<EquityStock>> Get5_10_20_50DMA()
    //{
    //    var equities = await DbContext.EquityStocks.Where(x => x.DMA5 > 0 && x.DMA10 > 0 && x.DMA20 > 0 && x.DMA50 > 0
    //                                        && x.DMA5 > x.LTP && x.DMA10 > x.LTP && x.DMA20 < x.LTP && x.DMA50 < x.LTP)
    //                                        .OrderByDescending(o => o.RankByGroww).ToListAsync();
    //    return equities;
    //}

    //public static DateOnly GetLastThursdayOfMonth(int year, int month)
    //{
    //    // Start with the last day of the month
    //    DateOnly lastDayOfMonth = new(year, month, DateTime.DaysInMonth(year, month));

    //    // Work backward to find the last Thursday
    //    while (lastDayOfMonth.DayOfWeek != DayOfWeek.Thursday)
    //    {
    //        lastDayOfMonth = lastDayOfMonth.AddDays(-1);
    //    }

    //    return lastDayOfMonth;
    //}
}

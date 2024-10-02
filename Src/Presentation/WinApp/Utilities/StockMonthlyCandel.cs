using ShareMarket.WinApp.Store;

namespace ShareMarket.WinApp.Utilities;

public class StockMonthlyCandel
{
    private ShareMarketContext? DB;

    //public async Task Cal()
    //{
    //    DB = new ShareMarketContext();
    //    var startDate = new DateOnly(2024, 08, 30);
    //    var endDate = new DateOnly(2024, 09, 26);
    //    List<DateOnly> dates = [startDate, endDate];
    //    List<EquityMonthlyHistory> equityMonthlies = [];
    //    var histories = await DB.EquityPriceHistories.Where(x => dates.Contains(x.Date)).AsNoTracking().ToListAsync();
    //    var stocks = await DB.EquityStocks.ToListAsync();
    //    var uniqueCode = histories.Select(s => s.Code).Distinct().ToList();
    //    foreach (var item in uniqueCode)
    //    {
    //        var stock = stocks.FirstOrDefault(x => x.Code == item);
    //        if (stock is null) continue;
    //        var h1 = histories.FindAll(x => x.Code == item).OrderBy(o => o.Date).ToList();
    //        if (h1.Count == 2)
    //        {
    //            var monthEnd    = h1.First(x => x.Date == endDate);
    //            var monthStart  = h1.Last(x => x.Date == startDate);
    //            equityMonthlies.Add(new EquityMonthlyHistory
    //            {
    //                Close       = monthStart.Close,
    //                Open        = monthStart.Open,
    //                Code        = stock.Code,
    //                EquityId    = stock.Id,
    //                CreatedById = 1,
    //                CreatedOn   = DateTimeOffset.Now,
    //                Date        = dates[1],
    //                Name        = stock.Name,
    //                High        = monthStart.Close > monthStart.Open ? 1 : 0,
    //                Low         = stock.RankByGroww
    //            });
    //        }
    //    }
    //    await DB.Database.ExecuteSqlRawAsync("TRUNCATE TABLE EquityMonthlyHistories");
    //    await DB.EquityMonthlyHistories.AddRangeAsync(equityMonthlies);
    //    await DB.SaveChangesAsync();
    //}
}

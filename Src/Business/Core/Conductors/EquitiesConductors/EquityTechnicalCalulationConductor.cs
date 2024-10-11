using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Extensions;
using ShareMarket.Core.Interfaces.Conductors;
using ShareMarket.Core.Interfaces.Conductors.EquitiesConductors;
using ShareMarket.Core.Models.Results;

namespace ShareMarket.Core.Conductors.EquitiesConductors;

public class EquityTechnicalCalulationConductor(IRepositoryConductor<EquityStock> EquityRepo, IRepositoryConductor<EquityPriceHistory> HistoryRepo) : IEquityTechnicalCalulationConductor
{
    public async Task<Result<bool>> CreateOrUpdateHistoryAsync(List<EquityPriceHistory> histories)
    {
        var r = new Result<bool>();
        foreach (var item in histories)
        {
            var x = await HistoryRepo.FindAll(x => x.Code == item.Code && x.Date == item.Date).ResultObject.FirstOrDefaultAsync();
            if (x != null)
            {
                x.Open = item.Open;
                x.Close = item.Close;
                x.High = item.High;
                x.Low = item.Low;
                x.UpdatedOn = DateTimeOffset.Now;
                x.UpdatedById = 1;
                var updateResult = await HistoryRepo.UpdateAsync(x, SystemConstant.SystemUserId);
                r.AddErrors(updateResult.Errors);
            }
            else
            {
                var createResult = await HistoryRepo.CreateAsync(item, SystemConstant.SystemUserId);
                r.AddErrors(createResult.Errors);
            }
        }
        return r;
    }

    public async Task<Result<bool>> RSI_X_EMA(string code)
    {
        var r = new Result<bool>();
        int days = 14;
        var stock = await EquityRepo.FindAll(x => x.IsActive && x.Code == code).ResultObject.FirstOrDefaultAsync();
        if (stock is null) return r;
        var histories = await HistoryRepo.FindAll(x => x.Code == code, orderBy: o => o.OrderBy("Date", "ASC")).ResultObject.ToListAsync();
        if (histories.Count == 0) return r;
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
            histories[i].UpdatedOn = DateTimeOffset.Now;
            histories[i].UpdatedById = 1;
        }

        var last = histories.Last();
        stock.RSI14EMA = last.RSI14EMA;
        stock.RSI14EMADiff = last.RSI14EMADiff;
        stock.UpdatedById = last.UpdatedById;
        stock.UpdatedOn = last.UpdatedOn;
        var historyUpdateResult = await HistoryRepo.UpdateAsync(histories, SystemConstant.SystemUserId);
        var equityUpdateResult = await EquityRepo.UpdateAsync(stock, SystemConstant.SystemUserId);
        r.AddErrors(historyUpdateResult.Errors);
        r.AddErrors(equityUpdateResult.Errors);
        return r;
    }

    public async Task<Result<bool>> RSICalculation(string code)
    {
        var r = new Result<bool>();
        int days = 14;
        var stock = await EquityRepo.FindAll(x => x.IsActive && x.Code == code).ResultObject.FirstOrDefaultAsync();
        if (stock is null) return r;
        var histories = await HistoryRepo.FindAll(x => x.Code == code, orderBy: o => o.OrderBy("Date", "ASC")).ResultObject.ToListAsync();
        if (histories.Count == 0) return r;
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
        var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).FirstOrDefault();
        var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).FirstOrDefault();
        var last = histories.Last();
        stock.RSI = last.RSI;
        stock.DayHigh = last.High;
        stock.DayLow = last.Low;
        stock.LTP = last.Close;
        if (yH is not null)
        {
            stock.YearHigh = yH.High;
            stock.YearHighOn = yH.Date;
        }
        if (yL is not null)
        {
            stock.YearLow = yL.Low;
            stock.YearLowOn = yL.Date;
        }
        if (yH is not null && yL is not null)
            stock.IsRaising = stock.YearLowOn > stock.YearHighOn;

        var historyUpdateResult = await HistoryRepo.UpdateAsync(histories, SystemConstant.SystemUserId);
        var equityUpdateResult = await EquityRepo.UpdateAsync(stock, SystemConstant.SystemUserId);

        r.AddErrors(historyUpdateResult.Errors);
        r.AddErrors(equityUpdateResult.Errors);
        return r;
    }

    public async Task<Result<bool>> DMACalculation(string code)
    {
        var r = new Result<bool>();
        var stock = await EquityRepo.FindAll(x => x.IsActive && x.Code == code).ResultObject.FirstOrDefaultAsync();
        if (stock is null) return r;
        var histories = await HistoryRepo.FindAll(x => x.Code == code, orderBy: o => o.OrderBy("Date", "ASC")).ResultObject.ToListAsync();
        if (histories.Count == 0) return r;
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
        stock.DMA5 = last.DMA5;
        stock.DMA10 = last.DMA10;
        stock.DMA20 = last.DMA20;
        stock.DMA50 = last.DMA50;
        stock.DMA100 = last.DMA100;
        stock.DMA200 = last.DMA200;

        var historyUpdateResult = await HistoryRepo.UpdateAsync(histories, SystemConstant.SystemUserId);
        var equityUpdateResult = await EquityRepo.UpdateAsync(stock, SystemConstant.SystemUserId);

        r.AddErrors(historyUpdateResult.Errors);
        r.AddErrors(equityUpdateResult.Errors);
        return r;
    }

    public async Task<Result<bool>> RSIIncrementalAsync(string code)
    {
        var r = new Result<bool>();
        var stock = await EquityRepo.FindAll(x => x.IsActive && x.Code == code).ResultObject.FirstOrDefaultAsync();
        if (stock is null) return r;
        var histories = await HistoryRepo.FindAll(x => x.Code == code, orderBy: o => o.OrderBy("Date", "ASC"), take: 2).ResultObject.ToListAsync();
        if (histories.Count == 0) return r;
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

        //var yH = histories.Where(x => x.Date >= lastYear).OrderByDescending(O => O.High).First();
        //var yL = histories.Where(x => x.Date >= lastYear).OrderBy(O => O.Low).First();

        stock.RSI = last.RSI;
        stock.DayHigh = last.High;
        stock.DayLow = last.Low;
        //stock.YearHigh      = yH.High;
        //stock.YearHighOn    = yH.Date;
        //stock.YearLow       = yL.Low;
        //stock.YearLowOn     = yL.Date;
        stock.IsRaising = stock.YearLowOn > stock.YearHighOn;
        var historyUpdateResult = await HistoryRepo.UpdateAsync(histories, SystemConstant.SystemUserId);
        var equityUpdateResult = await EquityRepo.UpdateAsync(stock, SystemConstant.SystemUserId);

        r.AddErrors(historyUpdateResult.Errors);
        r.AddErrors(equityUpdateResult.Errors);
        return r;
    }

}

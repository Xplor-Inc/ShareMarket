using Hangfire;
using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Extensions;
using ShareMarket.Core.Interfaces.Conductors;
using ShareMarket.Core.Interfaces.Conductors.EquitiesConductors;
using ShareMarket.Core.Models.Results;
using ShareMarket.Core.Services;

namespace ShareMarket.Core.Conductors.EquitiesConductors;

public class EquityDailyPriceSyncConductor(
    IRepositoryConductor<EquityStock> EquityRepo,
    IEquityTechnicalCalulationConductor TechnicalCalulationConductor,
    IGrowwService GrowwService) : IEquityDailyPriceSyncConductor
{
    public async Task<int> SyncEquityLTPAsync()
    {
        var r = new Result<bool>();
        var equities = await EquityRepo.FindAll(s => s.IsActive && s.RankByGroww >= 0, orderBy: o => o.OrderBy("RankByGroww", "DESC"))
                    .ResultObject.Select(s=>s.Code).ToListAsync();
        var date = DateOnly.FromDateTime(DateTime.Now);

        for (int count = 0; count < equities.Count; count++)
        {
            var equity = equities[count];
            BackgroundJob.Enqueue<EquityDailyPriceSyncConductor>(x => x.SyncLtpByStocksAsync(equity, date));
        }

        return equities.Count;
    }

    public async Task<string> SyncLtpByStocksAsync(string code, DateOnly date)
    {
        var equity = await EquityRepo.FindAll(s => s.IsActive && s.Code == code, orderBy: o => o.OrderBy("RankByGroww", "DESC"))
                    .ResultObject.FirstOrDefaultAsync() ?? throw new Exception($"Equity Stock data not found with code: {code}");

        var ltpPriceResult = await GrowwService.GetLTPPrice(equity.Code);
        if (ltpPriceResult.HasErrors)
            throw new Exception(ltpPriceResult.GetErrors());

        var Ltp = ltpPriceResult.ResultObject;
        if (Ltp != null && Ltp.Ltp == 0)
        {
            throw new Exception($"Equity Stock LTP found as 0 from Groww with code: {code}");
        }
        if (Ltp is null)
            throw new Exception($"Equity Stock LTP found as NULL from Groww with code: {code}");

        equity.LTP = Ltp.Ltp;
        equity.LTPDate = date;
        equity.Change = Ltp.DayChange;
        equity.PChange = Ltp.DayChangePerc;
        equity.DayHigh = Ltp.High;
        equity.DayLow = Ltp.Low;
        equity.UpdatedOn = DateTimeOffset.Now;
        equity.UpdatedById = 1;

        var equityUpdateResult = await EquityRepo.UpdateAsync(equity, SystemConstant.SystemUserId);
        if (equityUpdateResult.HasErrors)
            throw new Exception(equityUpdateResult.GetErrors());

        var history = new EquityPriceHistory
        {
            Close = Ltp.Close,
            Code = equity.Code,
            CreatedById = 1,
            CreatedOn = DateTimeOffset.Now,
            Date = date,
            EquityId = equity.Id,
            Low = Ltp.Low,
            Name = equity.Name,
            High = Ltp.High,
            Open = Ltp.Open,
        };

        var historyCreateResult = await TechnicalCalulationConductor.CreateOrUpdateHistoryAsync([history]);
        if (equityUpdateResult.HasErrors)
            throw new Exception(equityUpdateResult.GetErrors());
        historyCreateResult = await TechnicalCalulationConductor.RSIIncrementalAsync(equity.Code);
        if (equityUpdateResult.HasErrors)
            throw new Exception(equityUpdateResult.GetErrors());
        //historyCreateResult = await TechnicalCalulationConductor.DMACalculation(equity.Code);
        //if (equityUpdateResult.HasErrors)
        //    throw new Exception(equityUpdateResult.GetErrors());
        //historyCreateResult = await TechnicalCalulationConductor.RSI_X_EMA(equity.Code);
        //if (equityUpdateResult.HasErrors)
        //    throw new Exception(equityUpdateResult.GetErrors());

        return $"{code}:{date:dd-MMM-yyyy}:{equity.LTP}";
    }
}

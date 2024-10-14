using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Entities.Tradings;
using ShareMarket.Core.Models.Dtos.Equities;
using ShareMarket.Core.Services;

namespace ShareMarket.WebApp.Components.EquityMarkets;

public partial class EquityMarket
{
    public List<EquityPriceHistoryDto> TodaysTrades { get; set; } = [];
    protected BuyStratergy Stratergy { get; set; }
    protected DateOnly Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));
    protected async override Task OnInitializedAsync()
    {
        await GetDataAsync(BuyStratergy.RSIBelow35);
        IsLoading = false;
        await base.OnInitializedAsync();
    }

    protected async Task LTPUpdate()
    {
        IsLoading = true;
        foreach (var item in TodaysTrades)
        {
            var resp = await GrowwService.GetLTPPrice(item.Code);
            if (!resp.HasErrors && resp.ResultObject != null)
            {
                //item.LTP = resp.ResultObject.Ltp;
                //item.PChange = resp.ResultObject.DayChangePerc;
            }
        }
        IsLoading = false;
    }

    protected async Task GetDataAsync(BuyStratergy buyStratergy)
    {
        IsLoading = true;
        Stratergy = buyStratergy;
        Expression<Func<EquityPriceHistory, bool>> filter = e => e.Equity.IsActive && e.Equity.RankByGroww >= 75 && e.Equity.PE < 60 
                                                            && e.Equity.ROE >= 15 && e.Date == Date;
        if (buyStratergy == BuyStratergy.RSIBelow35)
            filter = filter.AndAlso(e => e.RSI <= 35);
        if (buyStratergy == BuyStratergy.RSI14EMADiffLess1)
            filter = filter.AndAlso(e => e.RSI14EMADiff < -1);

        var equityResult = HistoryRepo.FindAll(filter, includeProperties: "Equity", orderBy: e => e.OrderBy("Equity.RankByGroww", "DESC"));
        var trades = await equityResult.ResultObject.ToListAsync();
        TodaysTrades = Mapper.Map<List<EquityPriceHistoryDto>>(trades);

        var boughtStocks = await TradeRepo.FindAll(x=>x.SellDate == null).ResultObject.Select(x => x.Code).ToListAsync();
        TodaysTrades.ForEach(e => e.BuyAlready = boughtStocks.Contains(e.Code));
        IsLoading = false;
    }
    protected async Task BuyTrade(EquityPriceHistoryDto equity)
    {
        IsLoading = true;
        var tradeTaken = await TradeRepo.FindAll(x => x.Code == equity.Code && !x.SellDate.HasValue).ResultObject.FirstOrDefaultAsync();
        if (tradeTaken is not null)
        {
            await NotificationService.Error($"Error : Trade is already available taken on {tradeTaken.BuyDate:dd-MMM-yyyy} with {tradeTaken.Stratergy}", "Error");
            IsLoading = false;
            equity.BuyAlready = true;
            return;
        }

        int quantity = 1 + (int)(10000 / equity.Close).ToFixed();

        var trade = new VirtualTrade
        {
            LTP         = equity.Equity.LTP,
            Stratergy   = Stratergy,
            BuyDate     = Date,
            BuyRate     = equity.Close,
            Code        = equity.Code,
            Name        = equity.Name,
            Quantity    = quantity,
            BuyValue    = quantity * equity.Close,
            Target      = equity.Close + (equity.Close * 5 / 100),
            StopLoss    = equity.Close - (equity.Close * 7 / 100),
            EquityId    = equity.Equity.Id
        };

        var tradeResult = await TradeRepo.CreateAsync(trade, UserId);
        if (tradeResult.HasErrors)
        {
            await NotificationService.Error($"Error : {tradeResult.GetErrors()}", "Error");
            IsLoading = false;
            return;
        }
        await NotificationService.Success($"Success : Trade added to virual book successfully", "Success");
        IsLoading = false;
        equity.BuyAlready = true;
    }
}

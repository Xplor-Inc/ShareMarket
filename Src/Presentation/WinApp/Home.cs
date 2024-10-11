using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Entities.Tradings;
using ShareMarket.Core.Enumerations;
using ShareMarket.Core.Extensions;
using ShareMarket.Core.Models.Services.Groww;
using ShareMarket.WinApp.Services;
using ShareMarket.WinApp.Store;
using ShareMarket.WinApp.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace ShareMarket.WinApp;

public partial class Home : Form
{
    protected WebBrowser Browser { get; set; } = new();
    protected List<EquityStock> StockList { get; set; } = [];
    protected int CurrentStock { get; set; }
    protected string Url { get; set; } = string.Empty;
    public string TimeRange { get; set; } = "?period1=&period2=";
    public Home()
    {
        if (Screen.PrimaryScreen != null)
        {
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
        }
        InitializeComponent();
    }

    private async void Home_Load(object sender, EventArgs e)
    {
        await Utility.MigrateData();
        Grd102050DMA.AutoGenerateColumns = false;
        Grd102050DMA.Height = Screen.PrimaryScreen?.Bounds.Height - 500 ?? 0;
        Grd102050DMA.DataSource = await Utility.Get51020DMA();        
    }

    private async void BtnGrowwLTP_Click(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();
        var date = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var startTime = DateTime.Now;
        var x1 = MessageBox.Show($"Are you sure to process to data fro {date:dd-MMM}", "Confirm", MessageBoxButtons.OKCancel);
        if (x1 == DialogResult.Cancel) return;
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive && s.RankByGroww >= 0)
                            .OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        for (int count = 0; count < equities.Count; count++)
        {
            var item = equities[count];
            LblStatus.Text = $"{count+1}/{equities.Count}. Fetching Groww for...{item.Name} as Date {date:dd-MMM-yyyy}";

            var r = await GrowwService.GetStockPrice(item);
            if (r != null && r.Ltp == 0)
            {
            }
            if (r != null)
            {
                item.LTP = r.Ltp;
                item.LTPDate = date;
                item.Change = r.DayChange;
                item.PChange = r.DayChangePerc;
                item.DayHigh = r.High;
                item.DayLow = r.Low;
                item.UpdatedOn = DateTimeOffset.Now;
                item.UpdatedById = 1;

                DbContext.EquityStocks.Update(item);
                await DbContext.SaveChangesAsync();
                LblStatus.Text = $"{count + 1}/{equities.Count}. Updating history for...{item.Name} as Date {date:dd-MMM-yyyy}";

                var history = new EquityPriceHistory
                {
                    Close = r.Close,
                    Code = item.Code,
                    CreatedById = 1,
                    CreatedOn = DateTimeOffset.Now,
                    Date = date,
                    EquityId = item.Id,
                    Low = r.Low,
                    Name = item.Name,
                    High = r.High,
                    Open = r.Open,
                };
                await Utility.CreateOrUpdateHistory([history]);
                await Utility.RSIIncrementalAsync(item.Code);
            }
        }
        LblStatus.Text = $"LTP updated by Groww for {equities.Count} at {DateTime.Now:dd-MMM-yyyy, hh:mm} in {(DateTime.Now-startTime).TotalMinutes } mins.";
        MessageBox.Show(LblStatus.Text);
    }

    private async void BtnYahooHistory_Click(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();
        var from = DateTimeOffset.UtcNow.AddYears(-5).ToUnixTimeSeconds();
        var to = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();
        TimeRange = $"?period1={from}&period2={to}";

        CurrentStock = 0;
        var x = await DbContext.EquityHistorySyncLog.Select(s => s.Code).ToListAsync();
        StockList = await DbContext.EquityStocks.Where(s => s.IsActive && x.Contains(s.Code)).AsNoTracking().ToListAsync();
        LblStatus.Text = $"{CurrentStock + 1}/{StockList.Count}. Yahoo... {StockList[CurrentStock].Name}";

        Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
        Browser.Navigate(Url);
        Browser.DocumentCompleted += Browser_DocumentCompleted_HistoricalData;
        if (Screen.PrimaryScreen != null)
        {
            Browser.Width = Screen.PrimaryScreen.Bounds.Width;
            Browser.Height = Screen.PrimaryScreen.Bounds.Height - 100;
            Browser.Margin = new Padding(10, 50, 10, 10);
        }
        Browser.ScriptErrorsSuppressed = true;
        if (!Controls.Contains(Browser))
            Controls.Add(Browser);
    }

    public async void Browser_DocumentCompleted_HistoricalData(object? sender, WebBrowserDocumentCompletedEventArgs e)
    {
        if (sender is not null && string.Equals(e.Url?.ToString(), Url, StringComparison.InvariantCultureIgnoreCase))
        {
            var found = Browser.Document?.GetElementsByTagName("tbody");
            if (found != null && found.Count > 0)
            {
                foreach (HtmlElement item in found)
                {
                    var trs = item.Children;
                    try
                    {
                        List<EquityPriceHistory> histories = [];
                        foreach (HtmlElement td in trs)
                        {
                            var tds = td.Children;
                            if (tds is null || tds.Count != 7) continue;

                            histories.Add(new EquityPriceHistory
                            {
                                Date = DateOnly.Parse(tds[0]?.InnerText?.Replace(",", "") ?? ""),
                                Close = decimal.Parse(tds[4]?.InnerText ?? ""),
                                Open = decimal.Parse(tds[1]?.InnerText ?? ""),
                                High = decimal.Parse(tds[2]?.InnerText ?? ""),
                                Low = decimal.Parse(tds[3]?.InnerText ?? ""),
                                Code = StockList[CurrentStock].Code,
                                Name = StockList[CurrentStock].Name,
                                CreatedById = 1,
                                CreatedOn = DateTimeOffset.Now,
                                EquityId = StockList[CurrentStock].Id
                            });
                        }

                        var DbContext = new ShareMarketContext();
                        await DbContext.AddRangeAsync(histories);
                        await DbContext.SaveChangesAsync();

                        CurrentStock++;
                        if (CurrentStock < StockList.Count)
                        {
                            LblStatus.Text = $"{CurrentStock + 1}/{StockList.Count}. Yahoo... {StockList[CurrentStock].Name}";
                            Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
                            Browser.Navigate(Url);
                        }
                        else
                        {
                            MessageBox.Show($"Data processing finished for Yahoo...");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while reading data", ex.Message);
                    }
                }
            }
        }
    }

    private void BtnSkipYahooError_Click(object sender, EventArgs e)
    {
        CurrentStock++;
        if (CurrentStock < StockList.Count)
        {
            LblStatus.Text = $"{CurrentStock + 1}/{StockList.Count}. Yahoo... {StockList[CurrentStock].Name}";
            Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
            Browser.Navigate(Url);
        }
    }

    private async void EquityPandit_Click(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive).OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        for (int i = 0; i < equities.Count; i++)
        {
            var item = equities[i];
            LblStatus.Text = $"{i + 1}/{equities.Count}. Equity Pandit...{item.Name}";
            var histories = await GrowwService.SyncPriceEquityPandit(item);

            await DbContext.AddRangeAsync(histories);
            await DbContext.SaveChangesAsync();
        }
    }

    private async void BtnFundamentals_Click(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive).OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        for (int i = 0; i < equities.Count; i++)
        {
            var item = equities[i];
            LblStatus.Text = $"{i + 1}/{equities.Count}. Syncing Fundamental from EquityPanditt...{item.Name}";
            item = await GrowwService.SyncFundamentalEquityPandit(item);

            DbContext.EquityStocks.Update(item);
            await DbContext.SaveChangesAsync();            
        }
    }

    private async void BtnCalculation(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive && s.RankByGroww >= 50)
                    .OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        var startTime = DateTime.Now;

        for (int count = 0; count < equities.Count; count++)
        {
            var item = equities[count];

            LblStatus.Text = $"{count + 1}/{equities.Count}. RSICalculation for...{item.Name}";
            await Utility.RSICalculation(item.Code);
            LblStatus.Text = $"{count + 1}/{equities.Count}. RSI_X_EMA for...{item.Name}";
            await Utility.RSI_X_EMA(item.Code);
            LblStatus.Text = $"{count + 1}/{equities.Count}. DMACalculation for...{item.Name}";
            await Utility.DMACalculation(item.Code);
        }
     
        Grd102050DMA.DataSource = await Utility.Get51020DMA();
        LblStatus.Text = $"Calculation updated for {equities.Count} at {DateTime.Now:dd-MMM-yyyy, hh:mm} in {(DateTime.Now - startTime).TotalMinutes:#.##} mins.";
        MessageBox.Show(LblStatus.Text);
    }
 
    private async void BtnTradeBook(object sender, EventArgs e)
    {
        var DbContext = new ShareMarketContext();

        DateOnly startTrading            = new(2024, 1, 1);
        DateOnly            endTrading              = new(2024, 8, 31);
        List<VirtualTrade>  tradesTaken             = [];
        List<VirtualTrade>  CapBooks                = [];
        int                 MaxCapPerTrade          = 25000;
        int                 MaxParallelTrades       = 20;
        int                 RankByGroww             = 50;
        int                 target                  = 5;
        int                 SL                      = 15;
        bool                startSelling            = false;
        string[]            Codes                   = ["AXISBANK", "TCS", "TATAMOTORS"];
        string              stratergy               = "RSI35";
      
        while (startTrading <= endTrading)
        {
            //if (startTrading == endTrading && !startSelling)
            //{
            //    startSelling = true;
            //    endTrading = endTrading.AddMonths(1);
            //}

            LblStatus.Text = $"Trading for Day of - {startTrading:dd-MMM-yyyy}";

            var availabeTrades = await DbContext.EquityPriceHistories.Where(x => x.Equity.RankByGroww >= RankByGroww //&& Codes.Contains(x.Code)
                                                                                && x.RSI <= 35 && x.Equity.ROE >= 15 && x.Equity.LTP < 5000
                                                                                && x.Equity.PE < 60 && x.Date == startTrading)
                                                .Include(e => e.Equity).ToListAsync();

            var codes = availabeTrades.Select(x => x.Code).ToList();
            codes.AddRange(tradesTaken.Select(x => x.Code));

            var sellAble = await DbContext.EquityPriceHistories.Where(x => codes.Contains(x.Code) && x.Date == startTrading)
                                    .OrderBy(o => o.Date).ToListAsync();
            foreach (var trade in tradesTaken.Where(z => z.SellDate is null))
            {

                LblStatus.Text = $"Selling for Month of - {startTrading:dd-MMM-yyyy}";
                if(trade.SellDate is null && startTrading >= trade.BuyDate.AddMonths(1))
                {
                    trade.Target = trade.BuyRate + (trade.BuyRate * (target + 2) / 100);
                    trade.TargetPer = target + 2;

                }
                if (trade.SellDate is null && startTrading >= trade.BuyDate.AddMonths(2))
                {
                    //var sellManually = sellAble.FirstOrDefault(s => s.Code == trade.Code);
                    //if (sellManually is not null)
                    //{
                    //    trade.SellDate = sellManually.Date;
                    //    trade.SellRate = sellManually.Close;
                    //    trade.ReleasedPL = (trade.SellRate - trade.BuyRate) * trade.Quantity;
                    //    trade.SellAction = SellAction.Manuall;
                    //}
                    trade.Target = trade.BuyRate + (trade.BuyRate * (target + 5) / 100);
                    trade.TargetPer = target + 5;
                }

                if (trade.SellDate is null && startTrading >= trade.BuyDate.AddMonths(3))
                {
                    trade.Target = trade.BuyRate + (trade.BuyRate * (target + 7) / 100);
                    trade.TargetPer = target + 7;
                }

                if (trade.SellDate is null && startTrading >= trade.BuyDate.AddMonths(4))
                {
                    trade.Target = trade.BuyRate + (trade.BuyRate * (target + 12) / 100);
                    trade.TargetPer = target + 9;
                }
                var targetHit = sellAble.FirstOrDefault(s => s.Code == trade.Code && s.High >= trade.Target && s.Date == startTrading);
                if (targetHit is not null)
                {
                    trade.SellDate      = targetHit.Date;
                    trade.ReleasedPL    = (trade.Target - trade.BuyRate) * trade.Quantity;
                    trade.SellAction    = SellAction.Target;
                    trade.SellRate      = trade.Target;
                    trade.SellValue     = trade.SellRate * trade.Quantity;
                }

                var SLHit = sellAble.FirstOrDefault(s => s.Code == trade.Code && s.Low <= trade.StopLoss && s.Date == startTrading);
                if (SLHit is not null)
                {
                    //trade.SellDate = SLHit.Date;
                    //trade.SellAction = SellAction.Stoploss;
                    //trade.PL = (trade.SLRate - trade.BuyRate) * trade.Quantity;
                }
                if(targetHit is not null && SLHit is not null)
                {
                    trade.SellAction = SellAction.All;
                }
            }
            if (startSelling)
            {
                startTrading = startTrading.AddDays(1);
                continue;
            }
            foreach (var trade in availabeTrades)
            {
                if (tradesTaken.Count(f => f.SellDate is null) > MaxParallelTrades) continue;
                if (tradesTaken.Any(f => f.SellDate is null && f.Code == trade.Code)) continue;
                int q = (int)(MaxCapPerTrade / trade.Close).ToFixed() + 1;
                MaxCapPerTrade += MaxCapPerTrade / 100;
                var t = new VirtualTrade
                {
                    BuyDate     = trade.Date,
                    BuyRate     = trade.Close,
                    Code        = trade.Code,
                    Name        = trade.Name,
                    Target      = trade.Close + (trade.Close * target / 100),
                    StopLoss      = trade.Close - (trade.Close * SL / 100),
                    LTP         = trade.Equity.LTP,
                    BuyValue    = trade.Close * q,
                    Quantity    = q,
                    TargetPer   = target
                };
                tradesTaken.Add(t);
            }

            startTrading = startTrading.AddDays(1);
        }        

        GrdAnalysis.Height = Screen.PrimaryScreen?.Bounds.Height - 500 ?? 0;
        GrdAnalysis.DataSource = tradesTaken;
        startTrading = new(2022, 10, 1);
        foreach (var item in tradesTaken.Where(x=>x.SellDate is null))
        {
            item.SellDate = DateOnly.FromDateTime(DateTime.Now);
            item.SellRate = item.LTP;
            item.ReleasedPL = (item.LTP - item.BuyRate) * item.Quantity;
            item.SellAction = SellAction.Manuall;
            item.SellValue = item.SellRate * item.Quantity;
        }
        foreach (var trade in tradesTaken)
        {
            if (trade.SellDate.HasValue)
                trade.Holding = trade.SellDate.Value.DayOfYear - trade.BuyDate.DayOfYear;
        }
        while (startTrading < endTrading)
        {
            var buys = tradesTaken.Where(x => x.BuyDate == startTrading);
            var sells = tradesTaken.Where(x => x.SellDate == startTrading);
            var bv = buys.Sum(s => s.BuyValue);
            var sv = sells.Sum(s => s.SellValue);
            if (bv > 0 || sv > 0)
            {
                var z = new VirtualTrade
                {
                    BuyDate = startTrading,
                    BuyValue = bv,
                    SellValue = sv,
                    ReleasedPL = CapBooks.Sum(s => s.BuyValue) - CapBooks.Sum(s => s.SellValue)
                };
                CapBooks.Add(z);
            }
            startTrading = startTrading.AddDays(1);
        }
        string? folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        string name =  $"{DateTime.Now.Ticks}__{MaxCapPerTrade}_{MaxParallelTrades}_{stratergy}_{tradesTaken.Count}.xlsx";
        string filePath = Path.Combine(folder, name);
        ExcelUtlity.CreateExcelFromList(tradesTaken, CapBooks, filePath);
        LblStatus.Text = $"Report is ready";
        ProcessStartInfo processStartInfo = new()
        {
            FileName = filePath,
            UseShellExecute = true
        };

        Process.Start(processStartInfo);
    }
}

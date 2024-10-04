using Microsoft.EntityFrameworkCore;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Models.Services.Groww;
using ShareMarket.WinApp.Services;
using ShareMarket.WinApp.Store;
using ShareMarket.WinApp.Utilities;
using System.Data;

namespace ShareMarket.WinApp;

public partial class Home : Form
{
    protected ShareMarketContext DbContext = new();
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
        var date = DateOnly.FromDateTime(DateTime.Now);
        var startTime = DateTime.Now;
        var x1 = MessageBox.Show($"Are you sure to process to data fro {date:dd-MMM}", "Confirm", MessageBoxButtons.OKCancel);
        if (x1 == DialogResult.Cancel) return;
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive)
                            .OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        for (int count = 0; count < equities.Count; count++)
        {
            var item = equities[count];
            LblStatus.Text = $"{count+1}/{equities.Count}. Fetching Groww for...{item.Name} as Date {date:dd-MMM-yyyy}";

            var r = await GrowwService.GetStockPrice(item.Code);
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
            }
        }
        LblStatus.Text = $"LTP updated by Groww for {equities.Count} at {DateTime.Now:dd-MMM-yyyy, hh:mm} in {(DateTime.Now-startTime).TotalMinutes } mins.";
        MessageBox.Show(LblStatus.Text);
    }

    private async void BtnYahooHistory_Click(object sender, EventArgs e)
    {
        var from = DateTimeOffset.UtcNow.AddDays(-2).ToUnixTimeSeconds();
        var to = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();
        TimeRange = $"?period1={from}&period2={to}";

        CurrentStock = 0;
        StockList = await DbContext.EquityStocks.Where(s => s.IsActive && s.Code != "EQ").AsNoTracking().ToListAsync();
        LblStatus.Text = $"{CurrentStock + 1}/{StockList.Count}. Yahoo... {StockList[CurrentStock].Name}";

        Url = $"https://trackon.in/";
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

                        await Utility.CreateOrUpdateHistory(histories);

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

    private async void BtnRSI_Click(object sender, EventArgs e)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.IsActive).AsNoTracking().ToListAsync();
        int count = 0;
        foreach (var stock in stocks)
        {
            count++;
            LblStatus.Text = $"{count}/{stocks.Count}. RSI 14 Days...{stock.Name}";
            await Utility.RSICalculation(14, stock.Code);
        }
        MessageBox.Show($"Data processing finished for RSI 14 Days...");
    }

    private async void Btn14DMA_Click(object sender, EventArgs e)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.IsActive).AsNoTracking().ToListAsync();
        int count = 0;
        foreach (var stock in stocks)
        {
            count++;
            LblStatus.Text = $"{count}/{stocks.Count}. DMA 5...{stock.Name}";
            await Utility.RSI_X_EMA(14, stock.Code);
        }
        MessageBox.Show($"Data processing finished for DMA 5...");
    }

    private async void EquityPandit_Click(object sender, EventArgs e)
    {
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive).OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        for (int i = 0; i < equities.Count; i++)
        {
            var item = equities[i];
            LblStatus.Text = $"{i + 1}. Equity Pandit...{item.Name}";
            var histories = await GrowwService.SyncPriceEquityPandit(item);

            await Utility.CreateOrUpdateHistory(histories);
            Grd102050DMA.DataSource = await Utility.Get51020DMA();
        }
    }

    private void Grd102050DMA_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
    {
        //x => x.DMA5 > 0 && x.DMA10 > 0 && x.DMA20 > 0
        //                                    && x.DMA5 > x.LTP && x.DMA10 < x.LTP && x.DMA20 < x.LTP).ToListAsync();
        var source = ((DataGridView)sender).DataSource;

        var code = ((List<EquityStock>)source)[e.RowIndex];
        if (code == null) return;

    }

    private async void BtnFundamentals_Click(object sender, EventArgs e)
    {
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
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive)
                    .OrderByDescending(x1 => x1.RankByGroww).AsNoTracking().ToListAsync();
        var startTime = DateTime.Now;

        for (int count = 0; count < equities.Count; count++)
        {
            var item = equities[count];

            LblStatus.Text = $"{count + 1}/{equities.Count}. RSICalculation for...{item.Name}";
            await Utility.RSICalculation(14, item.Code);
            LblStatus.Text = $"{count + 1}/{equities.Count}. RSI_X_EMA for...{item.Name}";
            await Utility.RSI_X_EMA(14, item.Code);
            LblStatus.Text = $"{count + 1}/{equities.Count}. DMACalculation for...{item.Name}";
            await Utility.DMACalculation(item.Code);
            Grd102050DMA.DataSource = await Utility.Get51020DMA();
        }
        LblStatus.Text = $"Calculation updated for {equities.Count} at {DateTime.Now:dd-MMM-yyyy, hh:mm} in {(DateTime.Now - startTime).TotalMinutes:#.##} mins.";
        MessageBox.Show(LblStatus.Text);
    }
    
    private async void BtnTradeBook1(object sender, EventArgs e)
    {
        DateOnly start = new(2024, 1, 1);
        var today = DateOnly.FromDateTime(DateTime.Now);
        List<TradeBook> tradesTaken = [];
        decimal capitalUsed = 0;
        DateOnly traddingTill = new(2024, 8, 31);
        int MaxCap = 10000; // Per Trade
        while (start <= traddingTill)
        {
            LblStatus.Text = $"Trading for Day of - {start:dd-MMM-yyyy}";

            var availabeTrades = await DbContext.EquityPriceHistories.Where(x => x.Equity.RankByGroww >= 25 
                                    && x.RSI14EMADiff < -1 && x.Equity.ROE >= 15 && x.Equity.LTP < 5000 
                                    && x.Equity.PE < 60 && x.Date == start).Include(e => e.Equity).ToListAsync();
            var codes = availabeTrades.Select(x => x.Code).ToList();
            codes.AddRange(tradesTaken.Select(x => x.Code));
            var sellAble = await DbContext.EquityPriceHistories.Where(x => codes.Contains(x.Code) && x.Date >= start)
                                    .OrderBy(o => o.Date).ToListAsync();
            foreach (var trade in tradesTaken.Where(z => !z.SellDate.HasValue))
            {
                LblStatus.Text = $"Selling for Month of - {start:dd-MMM-yyyy}";
                var maxSellDate = trade.BuyDate.AddMonths(1);
                var s = sellAble.FirstOrDefault(s => s.Code == trade.Code && s.High >= trade.Target && s.Date <= maxSellDate && s.Date <= start);
                if (s is not null)
                {
                    trade.SellDate = s.Date;
                    trade.High = s.High;
                    capitalUsed -= trade.BuyRate * trade.Quantity;
                }
            }
            foreach (var trade in availabeTrades)
            {
                if (tradesTaken.Any(f => f.Code == trade.Code && f.SellDate is null)) continue;
                int q = MaxCap / (int)trade.Close + 1;
                capitalUsed += trade.Close * q;
                var t = new TradeBook
                {
                    Low = trade.Low,
                    BuyDate = trade.Date,
                    BuyRate = trade.Close,
                    Code = trade.Code,
                    Diff = trade.RSI14EMADiff,
                    Name = trade.Name,
                    Target = (trade.Close * 5 / 100) + trade.Close,
                    Rank = trade.Equity.RankByGroww,
                    LTP = trade.Equity.LTP,
                    CapitalUsed = capitalUsed,
                    BuyValue = trade.Close * q,
                    Quantity = q
                };
                tradesTaken.Add(t);
            }

            start = start.AddDays(1);
        }

        var endDate = start.AddMonths(1);
        var codes1 = tradesTaken.Where(x => !x.SellDate.HasValue).Select(x => x.Code).ToList();
        var ZZZ1 = await DbContext.EquityPriceHistories.Where(x => codes1.Contains(x.Code) && x.Date >= start && x.Date < endDate)
                                .OrderBy(o => o.Date).ToListAsync();
        start = start.AddDays(1);
        while (start <= endDate)
        {
            LblStatus.Text = $"Selling for Month of - {start:dd-MMM-yyyy}";
            foreach (var trade in tradesTaken.Where(z => !z.SellDate.HasValue))
            {
                var maxSellDate = trade.BuyDate.AddMonths(1);
                var s = ZZZ1.FirstOrDefault(s => s.Code == trade.Code && s.High >= trade.Target && s.Date <= maxSellDate && s.Date <= start);
                if (s is not null)
                {
                    trade.SellDate = s.Date;
                    trade.High = s.High;

                }

            }
            start = start.AddDays(1);
        }

        foreach (var trade in tradesTaken)
        {
            if (trade.SellDate.HasValue)
                trade.Holding = trade.SellDate.Value.DayOfYear - trade.BuyDate.DayOfYear;
            //var d =  trade.BuyDate.DayNumber - trade.SellDate.Value;
            var tradeStart = trade.BuyDate;
            var SL = trade.BuyRate - (trade.BuyRate * 7 / 100);
            var sellDate = trade.SellDate ?? trade.BuyDate.AddMonths(1);
            var qqq = await DbContext.EquityPriceHistories.Where(x => x.Code == trade.Code && x.Date >= trade.BuyDate && x.Date <= sellDate)
                                    .OrderBy(o => o.Date).ToListAsync();
            while (tradeStart <= sellDate)
            {
                if (tradeStart != sellDate && qqq.Any(g => g.Low < SL)) { trade.StopLoss = true; break; }
                tradeStart = tradeStart.AddDays(1);
            }
        }

        GrdAnalysis.Height = Screen.PrimaryScreen?.Bounds.Height - 500 ?? 0;
        GrdAnalysis.DataSource = tradesTaken;
     //   ExcelUtlity.CreateExcelFromList(tradesTaken, "D:\\Projects\\Xplor-Inc\\Jul-X.xlsx");

    }

    private async void BtnTradeBook(object sender, EventArgs e)
    {
        DateOnly start = new(2024, 8, 1);
        var today = DateOnly.FromDateTime(DateTime.Now);
        List<TradeBook> tradesTaken = [];
       // decimal capitalUsed = 0;
        DateOnly traddingTill = new(2024, 8, 31);
        int MaxCap = 10000; // Per Trade
        var SLPer = 7;
        var tar = 5;
        string[] RR = ["HDFCBANK"];
        while (start <= traddingTill)
        {
            LblStatus.Text = $"Trading for Day of - {start:dd-MMM-yyyy}";

            var availabeTrades = await DbContext.EquityPriceHistories.Where(x => x.Equity.RankByGroww >= 50 //&& RR.Contains(x.Code)
                                    && x.RSI14EMADiff < -1 && x.Equity.ROE >= 15 && x.Equity.LTP < 5000
                                    && x.Equity.PE < 60 && x.Date == start).Include(e => e.Equity).ToListAsync();
            var codes = availabeTrades.Select(x => x.Code).ToList();
            codes.AddRange(tradesTaken.Select(x => x.Code));
            var sellAble = await DbContext.EquityPriceHistories.Where(x => codes.Contains(x.Code) && x.Date == start)
                                    .OrderBy(o => o.Date).ToListAsync();
            foreach (var trade in tradesTaken.Where(z => !z.SellDate.HasValue))
            {
                var tradeStart = trade.BuyDate;
                var SL = trade.BuyRate - (trade.BuyRate * SLPer / 100);
                var sellDate = trade.SellDate ?? trade.BuyDate.AddMonths(1);

                if (sellAble.Any(g => g.Code == trade.Code && g.Date == start && g.Low < SL && !trade.StopLoss))
                {
                    trade.SLDate = tradeStart;
                    trade.StopLoss = true;
                    trade.CapitalUsed = (SL-trade.BuyRate) * trade.Quantity;
                    trade.SellRate = SL;
                    break;
                }
                var maxSellDate = trade.BuyDate.AddMonths(1);
                var s = sellAble.FirstOrDefault(s => s.Code == trade.Code && s.High >= trade.Target && s.Date == start);
                if (s is not null && trade.SLDate is null)
                {
                    trade.SellDate = s.Date;
                    trade.High = s.High;
                    trade.CapitalUsed = (trade.Target -trade.BuyRate) * trade.Quantity;
                    trade.SellRate = trade.Target;
                }
            }
            foreach (var trade in availabeTrades)
            {
                if (tradesTaken.Any(f => f.Code == trade.Code && f.SellDate is null )) continue;
                int q = MaxCap / (int)trade.Close + 1;
                //capitalUsed += trade.Close * q;
                var t = new TradeBook
                {
                    Low = trade.Low,
                    BuyDate = trade.Date,
                    BuyRate = trade.Close,
                    Code = trade.Code,
                    Diff = trade.RSI14EMADiff,
                    Name = trade.Name,
                    Target = (trade.Close * tar / 100) + trade.Close,
                    Rank = trade.Equity.RankByGroww,
                    LTP = trade.Equity.LTP,
                   // CapitalUsed = capitalUsed,
                    BuyValue = trade.Close * q,
                    Quantity = q
                };
                tradesTaken.Add(t);
            }

            start = start.AddDays(1);
        }
        var endDate = traddingTill.AddMonths(1);
        var holdings = tradesTaken.Where(x => !x.SellDate.HasValue && !x.SLDate.HasValue).Select(x => x.Code).ToList();
        while (start <= endDate)
        {
            LblStatus.Text = $"Selling for Month of - {start:dd-MMM-yyyy}";
            var ZZZ1 = await DbContext.EquityPriceHistories.Where(x => holdings.Contains(x.Code) && x.Date == start)
                                .OrderBy(o => o.Date).ToListAsync();
            foreach (var trade in tradesTaken.Where(z => !z.SellDate.HasValue && !z.SLDate.HasValue))
            {
                var tradeStart = trade.BuyDate;
                var SL = trade.BuyRate - (trade.BuyRate * SLPer / 100);

                if (ZZZ1.Any(g => g.Code == trade.Code && g.Low < SL && g.Date == start && !trade.StopLoss))
                {
                    trade.SLDate = start;
                    trade.StopLoss = true;
                    trade.CapitalUsed = (SL-trade.BuyRate) * trade.Quantity;
                    trade.SellRate = SL;
                    break;
                }
                var s = ZZZ1.FirstOrDefault(s => s.Code == trade.Code && s.High >= trade.Target && s.Date == start);
                if (s is not null)
                {
                    trade.SellDate = s.Date;
                    trade.SellRate = trade.Target;
                    trade.High = s.High;
                    trade.CapitalUsed =( trade.Target -trade.BuyRate)* trade.Quantity;
                   // capitalUsed -= trade.Target * trade.Quantity;
                }
                //if (!trade.SellDate.HasValue && !trade.StopLoss)
                //{
                //    trade.LTP30Days = ZZZ1.LastOrDefault(x => x.Code == trade.Code)?.Close ?? 0;
                //    trade.SellRate = trade.LTP30Days;
                //    trade.CapitalUsed = trade.Quantity * (trade.LTP30Days - trade.BuyRate);

                //}
            }
            start = start.AddDays(1);
        }

        foreach (var trade in tradesTaken)
        {
            if (trade.SellDate.HasValue)
                trade.Holding = trade.SellDate.Value.DayOfYear - trade.BuyDate.DayOfYear;
        }
        start = new(2024, 1, 1);
        List<TradeBook> capitals = [];
        while (start < traddingTill.AddMonths(1))
        {
            var buys = tradesTaken.Where(x => x.BuyDate == start);
            var sells = tradesTaken.Where(x => x.SellDate == start || x.SLDate == start);
            var bv = buys.Sum(s => s.Quantity * s.BuyRate);
            var sv = sells.Sum(s => s.SellRate * s.Quantity);
            if (bv > 0 || sv > 0)
            {
                var z = new TradeBook
                {
                    BuyDate = start,
                    BuyRate = bv,
                    SellRate = sv,
                };
                z.CapitalUsed = capitals.Sum(s => s.BuyRate) - capitals.Sum(s => s.SellRate);
                capitals.Add(z);
            }
            start = start.AddDays(1);
        }

        GrdAnalysis.Height = Screen.PrimaryScreen?.Bounds.Height - 500 ?? 0;
        GrdAnalysis.DataSource = tradesTaken;
        ExcelUtlity.CreateExcelFromList(tradesTaken, capitals, $"D:\\Projects\\Xplor-Inc\\{DateTime.Now.Ticks}.xlsx");
        LblStatus.Text = $"Report is ready";

    }

}

using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Services;
using ShareMarket.WinApp.Store;
using ShareMarket.WinApp.Utilities;

namespace ShareMarket.WinApp;

public partial class Home : Form
{
    protected ShareMarketContext    DbContext = Program.DbContext;
    protected WebBrowser            Browser      { get; set; } = new();
    protected List<EquityStock>     StockList    { get; set; } = [];
    protected int                   CurrentStock { get; set; }
    protected string                Url          { get; set; } = string.Empty;
    public string                   TimeRange    { get; set; } = "?period1=&period2=";
    public Home()
    {
        if (Screen.PrimaryScreen != null)
        {
            Width   = Screen.PrimaryScreen.Bounds.Width;
            Height  = Screen.PrimaryScreen.Bounds.Height;
        }
        InitializeComponent();
    }

    private async void Home_Load(object sender, EventArgs e)
    {
        await Utility.MigrateData();
    }

    private async void BtnLTP_Click(object sender, EventArgs e)
    {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var x1 = MessageBox.Show($"Are you sure to process to data fro {date:dd-MMM}", "Confirm", MessageBoxButtons.OKCancel);
        if (x1 == DialogResult.Cancel) return;
        var equities = await DbContext.EquityStocks.Where(s => s.IsActive).AsNoTracking().ToListAsync();
        var count = 0;
        foreach (var item in equities)
        {
            count++;
            LblStatus.Text = $"{count}. Groww...{item.Name}";
            var r = await GrowwService.GetStockPrice(item.Code);
            if (r != null && r.Ltp == 0)
            {
            }
            if (r != null)
            {
                item.LTP            = r.Ltp;
                item.Change         = r.DayChange;
                item.PChange        = r.DayChangePerc;
                item.DayHigh        = r.High;
                item.DayLow         = r.Low;
                item.UpdatedOn      = DateTimeOffset.Now;
                item.UpdatedById    = 1;

                DbContext.EquityStocks.Update(item);
                await DbContext.SaveChangesAsync();
                LblStatus.Text = $"{count}. Updating history for...{item.Name} as Date {date:dd-MMM-yyyy}";

                var history = new EquityPriceHistory
                {
                    Close       = r.Close,
                    Code        = item.Code,
                    CreatedById = 1,
                    CreatedOn   = DateTimeOffset.Now,
                    Date        = date,
                    EquityId    = item.Id,
                    Low         = r.Low,
                    Name        = item.Name,
                    High        = r.High,
                    Open        = r.Open,
                };
                await Utility.CreateOrUpdateHistory([history]);
            }
        }
    }

    private async void BtnYahooHistory_Click(object sender, EventArgs e)
    {
        var from = DateTimeOffset.UtcNow.AddDays(-2).ToUnixTimeSeconds();
        var to = DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeSeconds();
        TimeRange = $"?period1={from}&period2={to}";

        CurrentStock = 0;
        StockList = await DbContext.EquityStocks.Where(s => s.IsActive && s.Code != "EQ").AsNoTracking().ToListAsync();
        LblStatus.Text = $"{CurrentStock + 1}/{StockList.Count}. Yahoo... {StockList[CurrentStock].Name}";

        Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
        Browser.Navigate(Url);
        Browser.DocumentCompleted += Browser_DocumentCompleted_HistoricalData;
        if (Screen.PrimaryScreen != null)
        {
            Browser.Width   = Screen.PrimaryScreen.Bounds.Width;
            Browser.Height  = Screen.PrimaryScreen.Bounds.Height - 100;
            Browser.Margin  = new Padding(10, 50, 10, 10);
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
                                Date        = DateOnly.Parse(tds[0]?.InnerText?.Replace(",", "") ?? ""),
                                Close       = decimal.Parse(tds[4]?.InnerText ?? ""),
                                Open        = decimal.Parse(tds[1]?.InnerText ?? ""),
                                High        = decimal.Parse(tds[2]?.InnerText ?? ""),
                                Low         = decimal.Parse(tds[3]?.InnerText ?? ""),
                                Code        = StockList[CurrentStock].Code,
                                Name        = StockList[CurrentStock].Name,
                                CreatedById = 1,
                                CreatedOn   = DateTimeOffset.Now,
                                EquityId    = StockList[CurrentStock].Id
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
            await Utility.RSI_X_DMA(5, stock.Code);
        }
        MessageBox.Show($"Data processing finished for DMA 5...");
    }
}

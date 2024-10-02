using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.NativeInterop;
using Newtonsoft.Json;
using ShareMarket.DesktopApp.Entities;
using ShareMarket.DesktopApp.Models.MutualFunds;
namespace ShareMarket.DesktopApp;

public partial class Home : Form
{
    protected ShareMarketContext DbContext { get; set; } = new();
    protected WebBrowser Browser { get; set; } = new();
    protected List<EquityStock> StockList { get; set; } = [];
    protected int CurrentStock { get; set; }
    protected string Url { get; set; } = string.Empty;
    public string TimeRange { get; set; } = "?period1=1701388800&period2=1727019266";
    public Home()
    {
        InitializeComponent();


    }
    public async Task GetGrowwMF()
    {
        var x = new DirectoryInfo($"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\Holdings\\");
        foreach (var item in x.EnumerateFiles())
        {
            var p1 = File.ReadAllText(item.FullName);
            var mfData = JsonConvert.DeserializeObject<MFHoldingData>(p1);
            Text = $"Update....{item.Name}";
            var scheme = Program.Mapper.Map<MFSchemes>(mfData);
            scheme.Id = long.Parse(scheme.SchemeCode);
            if (await DbContext.MFSchemes.AnyAsync(s => s.Id == scheme.Id)) continue;
            await DbContext.MFSchemes.AddAsync(scheme);
            await DbContext.SaveChangesAsync();

            List<MFStockHolding> holdings = [];
            mfData?.Holdings.ForEach(h =>
            {
                holdings.Add(new MFStockHolding
                {
                    CompanyName = h.CompanyName,
                    CorpusPer = h.CorpusPer,
                    CreatedById = 1,
                    CreatedOn = DateTimeOffset.Now,
                    InstrumentName = h.InstrumentName,
                    MarketCap = h.MarketCap,
                    MarketValue = h.MarketValue,
                    StockSearchId = h.StockSearchId,
                    NatureName = h.NatureName,
                    PortfolioDate = h.PortfolioDate,
                    Rating = h.Rating,
                    RatingMarketCap = h.RatingMarketCap,
                    SchemeCode = h.SchemeCode,
                    SchemeId = scheme.Id,
                    SectorName = h.SectorName,
                });
            });

            DbContext.ChangeTracker.DetectChanges();
            await DbContext.StockHoldings.AddRangeAsync(holdings);
            await DbContext.SaveChangesAsync();
        }
        //var mfContent = File.ReadAllText($"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\Holdings\\1039-nippon-india-i-come-fund-direct-growth.json");
        //await DbContext.MFSchemes.AddAsync(xfdr);
        //await DbContext.SaveChangesAsync();



        //HttpClient client = new()
        //{
        //    BaseAddress = new Uri("https://groww.in")
        //};

        //for (int i = 0; i < list.Count; i++)
        //{
        //    string name = list[i].Id.ToString();
        //    var x = await client.GetAsync($"/v1/api/data/mf/web/v4/scheme/search/{name}");
        //    if (x.IsSuccessStatusCode)
        //    {
        //        try
        //        {
        //            var y = await x.Content.ReadAsStringAsync();
        //            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\Holdings\\{i+750}-{name}.json";
        //            File.WriteAllText(path, y);

        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //    }
        //}

    }
    private async void Home_Load(object sender, EventArgs e)
    {
        var stocks = await DbContext.EquityStocks.OrderBy(o=>o.Name).ToListAsync();
        stocks.ForEach(stock => stock.Id = 0);
        string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\AllStocks.json";
        var content = JsonConvert.SerializeObject(stocks);
        File.WriteAllText(path, content);

        var mFStocks = await DbContext.StockHoldings.OrderBy(o => o.SchemeId).ToListAsync();
        mFStocks.ForEach(stock => stock.Id = 0);
         path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\StockHoldings.json";
        content = JsonConvert.SerializeObject(mFStocks);
        File.WriteAllText(path, content);

        var schemes = await DbContext.MFSchemes.OrderBy(o => o.SchemeName).ToListAsync();
        path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\Schemes.json";
        content = JsonConvert.SerializeObject(schemes);
        File.WriteAllText(path, content);

        DbContext.Database.Migrate();
        if (!await DbContext.EquityStocks.AnyAsync())
            await ReadEquityData();

        if (!await DbContext.DailyHistories.AnyAsync())
            await ReadHistoryData();
        await GetTodayData();
        await RSI5DMA();
        //  CodeMapping();
        //await RSI5DMA();
        //await BottonOutHunting_Calculation();
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
                        List<DailyHistory> histories = [];
                        foreach (HtmlElement td in trs)
                        {
                            var tds = td.Children;
                            if (tds is null || tds.Count != 7) continue;

                            histories.Add(new DailyHistory
                            {
                                Date = DateOnly.Parse(tds[0]?.InnerText?.Replace(",", "") ?? ""),
                                Close = decimal.Parse(tds[4]?.InnerText ?? ""),
                                Open = decimal.Parse(tds[1]?.InnerText ?? ""),
                                High = decimal.Parse(tds[2]?.InnerText ?? ""),
                                Low = decimal.Parse(tds[3]?.InnerText ?? ""),
                                Code = StockList[CurrentStock].Code,
                                Name = StockList[CurrentStock].Name
                            });
                        }
                        histories = histories.OrderBy(o => o.Date).ToList();
                        if (histories.Count > 100)
                        {
                            string path = $"D:\\Projects\\Xplor-Inc\\ShareMarket\\DesktopApp\\JsonData\\History\\{StockList[CurrentStock].Code}.json";
                            var content = JsonConvert.SerializeObject(histories);
                            File.WriteAllText(path, content);

                            await DbContext.AddRangeAsync(histories);
                            await DbContext.SaveChangesAsync();
                            //await RSICalculation(StockList[CurrentStock].Code);
                        }
                        else
                        {
                            await AddIncrementalHistoryData(histories);
                            await CalculateIncrementalRSI(StockList[CurrentStock].Code);
                        }
                        CurrentStock++;
                        if (CurrentStock < StockList.Count)
                        {
                            Text = $"{CurrentStock + 1}. {StockList[CurrentStock].Name}";
                            Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
                            Browser.Navigate(Url);
                        }
                        else
                        {
                            grdRSIData.Visible = dataGridView1.Visible = true;
                            Browser.Visible = false;
                            await GetTodayData();
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
    public async Task ReadHistoryData()
    {
        var stocks = await DbContext.EquityStocks.Where(s => s.Id > 0).ToListAsync();
        foreach (var item in stocks)
        {
            if (await DbContext.DailyHistories.AnyAsync(x => x.Code == item.Code)) continue;
            Text = $"Inserting history data {item.Name}";
            string path = $"C:\\Users\\hoshi\\OneDrive\\Desktop\\ShareMarket\\DesktopApp\\JsonData\\History\\{item.Code}.json";
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
                var histories = JsonConvert.DeserializeObject<List<DailyHistory>>(content);
                List<DailyHistory> list = [];
                histories?.ForEach(x =>
                {
                    if (!list.Any(a => a.Code == x.Code && a.Date == x.Date)) list.Add(x);
                });
                list.ForEach(s => s.Id = 0);
                await DbContext.AddRangeAsync(list);
                await DbContext.SaveChangesAsync();
            }
        }
    }
    public async Task ReadEquityData()
    {
        string path = $"C:\\Users\\hoshi\\OneDrive\\Desktop\\ShareMarket\\DesktopApp\\JsonData\\EquityStocks\\AllEquityStocks.json";

        if (File.Exists(path))
        {
            var content = File.ReadAllText(path);
            List<EquityStock>? stocks = JsonConvert.DeserializeObject<List<EquityStock>>(content);
            if (stocks == null) return;
            await DbContext.AddRangeAsync(stocks);
            await DbContext.SaveChangesAsync();
        }
        //string path = "C:\\Users\\hoshi\\OneDrive\\Desktop\\ShareMarket\\DesktopApp\\JsonData\\EquityStocks\\NIFTYSMALLCAP100.json";
        //  string path = "C:\\Users\\hoshi\\OneDrive\\Desktop\\ShareMarket\\DesktopApp\\JsonData\\EquityStocks\\Nifty200.json";
        // string path = "C:\\Users\\hoshi\\OneDrive\\Desktop\\ShareMarket\\DesktopApp\\JsonData\\EquityStocks\\NIFTYMIDCAP100.json";
        //if (File.Exists(path))
        //{
        //    var dbData = await DbContext.EquityStocks.ToListAsync(); 
        //    var content = File.ReadAllText(path);
        //    IndexData? indexData = JsonConvert.DeserializeObject<IndexData>(content);
        //    if (indexData == null) return;

        //    List<StockModel> stocks = indexData.Data.Where(x => x.Priority == 0).OrderBy(o => o.Meta.CompanyName).ToList();//.OrderBy(o => o.Meta.CompanyName);

        //    stocks = [.. stocks.OrderBy(o => o.Meta.CompanyName)];
        //    List<EquityStock> dbx = [];
        //    foreach (var stock in stocks.Where(x => x.Priority == 0))
        //    {
        //        if(dbData.Any(x=>x.Code == stock.Symbol))
        //        {
        //            continue;
        //        }
        //        dbx.Add(new()
        //        {
        //            Change = stock.Change,
        //            DayHigh = stock.DayHigh,
        //            DayLow = stock.DayLow,
        //            IndexName = indexData.Name,
        //            LastPrice = stock.LastPrice,
        //            LastUpdateTime = stock.LastUpdateTime,
        //            Open = stock.Open,
        //            PChange = stock.PChange,
        //            PerChange30d = stock.PerChange30d,
        //            PerChange365d = stock.PerChange365d,
        //            PreviousClose = stock.PreviousClose,
        //            Code = stock.Symbol,
        //            YearHigh = stock.YearHigh,
        //            YearLow = stock.YearLow,
        //            Name = stock.Meta.CompanyName,
        //            Industry = stock.Meta.Industry,
        //            IsCASec = stock.Meta.IsCASec,
        //            IsDebtSec = stock.Meta.IsDebtSec,
        //            IsFNOSec = stock.Meta.IsFNOSec,
        //            IsSLBSec = stock.Meta.IsSLBSec,
        //            IsETFSec = stock.Meta.IsETFSec,
        //            IsSuspended = stock.Meta.IsSuspended,
        //            IsDelisted = stock.Meta.IsDelisted
        //        });
        //    }
        //    await DbContext.AddRangeAsync(dbx);
        //    await DbContext.SaveChangesAsync();

        //}
    }
    public async Task RSI5DMA()
    {
        var stocks = DbContext.EquityStocks.Where(x=>x.RSI5DMADiff == 0).ToList();
        int a = 0;
        foreach (var stock in stocks)
        {
            a++;
            Text = $"{a+1}/{stocks.Count}. Calculating RSI5DMA for {stock.Name}";
            var history = await DbContext.DailyHistories.Where(x => x.Code == stock.Code).OrderBy(o => o.Date).ToListAsync();
            if (history.Count == 0) continue;
            for (int i = 0; i < history.Count; i++)
            {
                if (i == 0) continue;
                var x0 = history[i - 1];
                var x1 = history[i];
                var profit = x1.Close - x0.Close;
                var loss = x0.Close - x1.Close;
                if (profit < 0) profit = 0;
                if (loss < 0) loss = 0;
                history[i].Profit = profit;
                history[i].Loss = loss;
                if (i > 4)
                {
                    if (i == 5)
                    {
                        var rSI5DMA = (history[i - 5].RSI + history[i - 4].RSI + history[i - 3].RSI +
                            history[i - 2].RSI + history[i - 1].RSI + history[i].RSI) / 5;

                        history[i].RSI5DMA = Math.Round(rSI5DMA, 2, MidpointRounding.AwayFromZero);

                    }
                    else
                    {
                        history[i].RSI5DMA = Math.Round(((history[i - 1].RSI5DMA * 4) + history[i].RSI) / 5, 2, MidpointRounding.AwayFromZero);


                        history[i].RSI5DMADiff = history[i].RSI5DMA - history[i - 1].RSI5DMA;
                    }
                }
            }

            var last = history.Last();
            stock.RSI5DMA = last.RSI5DMA;
            stock.RSI5DMADiff = last.RSI5DMADiff;
            DbContext.DailyHistories.UpdateRange(history);
            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            await GetTodayData();
        }
    }
    public async Task RSI_X_DMA(int days)
    {
        var stocks = DbContext.EquityStocks.Where(x => x.Id > 0).ToList();
        int a = 0;
        foreach (var stock in stocks)
        {
            a++;
            Text = $"{a + 1}/{stocks.Count}. Calculating RSI-{days}-DMA for {stock.Name}";
            var history = await DbContext.DailyHistories.Where(x => x.Code == stock.Code).OrderBy(o => o.Date).ToListAsync();
            if (history.Count == 0) continue;
            for (int i = 0; i < history.Count; i++)
            {
                if (i == 0) continue;
                var x0 = history[i - 1];
                var x1 = history[i];
                var profit = x1.Close - x0.Close;
                var loss = x0.Close - x1.Close;
                if (profit < 0) profit = 0;
                if (loss < 0) loss = 0;
                history[i].Profit = profit;
                history[i].Loss = loss;
                if (i > days-1)
                {
                    if (i == days)
                    {
                        var rSI5DMA = (history[i - 5].RSI + history[i - 4].RSI + history[i - 3].RSI +
                            history[i - 2].RSI + history[i - 1].RSI + history[i].RSI) / 5;

                        history[i].RSI5DMA = Math.Round(rSI5DMA, 2, MidpointRounding.AwayFromZero);

                    }
                    else
                    {
                        history[i].RSI5DMA = Math.Round(((history[i - 1].RSI5DMA * 4) + history[i].RSI) / 5, 2, MidpointRounding.AwayFromZero);


                        history[i].RSI5DMADiff = history[i].RSI5DMA - history[i - 1].RSI5DMA;
                    }
                }
            }

            var last = history.Last();
            stock.RSI5DMA = last.RSI5DMA;
            stock.RSI5DMADiff = last.RSI5DMADiff;
            DbContext.DailyHistories.UpdateRange(history);
            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            await GetTodayData();
        }
    }
    public void CodeMapping()
    {
        var codes = DbContext.EquityStocks.Where(x => x.Code != "EQ" || x.Code == null).ToList();

        foreach (var code in codes)
        {
            Text = $"Update Codes for MFDD....{code.Name}";
            var yy = DbContext.StockHoldings.Where(x => x.CompanyName == code.Name).ToList();
            if (yy.Any())
            {
                yy.ForEach(s => s.Code = code.Code);
                DbContext.UpdateRange(yy);
                DbContext.SaveChanges();
            }
        }
    }
    public async Task CodeMapping1()
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.Code == "EQ").ToListAsync();

        var Xstocks = await DbContext.Stock.ToListAsync();

        int count = 0;
        foreach (var stock in stocks)
        {
            count++;
            Text = $"{count}. Update Codes for....{stock.Name}";

            var x = Xstocks.FirstOrDefault(a => a.Name.ToLower().TrimEnd().Replace("  ", " ") == stock.Name.ToLower().TrimEnd());
            if (x is not null && stock.Code == "EQ")
            {
                stock.Code = x.Code;
            }
        }
        DbContext.UpdateRange(stocks);
        await DbContext.SaveChangesAsync();
    }
    public async Task CreateStcokMaster()
    {
        var m1 = await DbContext.EquityStocks.ToListAsync();

        var me = await DbContext.StockHoldings.Where(d => d.NatureName == "EQ" && d.CompanyName != null).ToListAsync();
        int counter = 0;
        List<EquityStock> ss = [];
        foreach (var x in me)
        {
            counter++;
            Text = $"Counter running at {counter} of {me.Count}";
            if (m1.Any(a => a.Name.ToLower().TrimEnd() == x.CompanyName.ToLower().TrimEnd())
                || ss.Any(a => a.Name.ToLower().TrimEnd() == x.CompanyName.ToLower().TrimEnd())) continue;

            var ex = new EquityStock
            {
                InstrumentName = x.InstrumentName,
                SectorName = x.SectorName,
                Change = 0,
                Code = x.NatureName,
                CreatedById = x.CreatedById,
                CreatedOn = x.CreatedOn,
                DayHigh = 0,
                DayLow = 0,
                DeletedById = x.DeletedById,
                DeletedOn = x.DeletedOn,
                IndexName = string.Empty,
                Industry = string.Empty,
                IsETFSec = false,
                IsFNOSec = false,
                LTP = 0,
                Name = x.CompanyName,
                Open = 0,
                PChange = 0,
                PerChange30d = 0,
                PerChange365d = 0,
                PreviousClose = 0,
                RSI = 0,
                RSI5DMA = 0,
                RSI5DMADiff = 0,
                YearHigh = 0,
                YearLow = 0,
                YearHighOn = DateOnly.MaxValue,
                YearLowOn = DateOnly.MaxValue
            };
            ss.Add(ex);
        }
        await DbContext.AddRangeAsync(ss);
        await DbContext.SaveChangesAsync();
    }
    public async Task RSICalculation(string? code = null)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.Code == code || code == null).ToListAsync();
        int q = 0;
        foreach (var stock in stocks)
        {
            q++;
            Text = $"{q}. Calculating RSI for {stock.Name}";
            var history = await DbContext.DailyHistories.Where(x => x.Code == stock.Code).OrderBy(o => o.Date).ToListAsync();
            if (history.Count == 0) continue;
            for (int i = 0; i < history.Count; i++)
            {
                if (i == 0) continue;
                var x0 = history[i - 1];
                var x1 = history[i];
                var profit = x1.Close - x0.Close;
                var loss = x0.Close - x1.Close;
                if (profit < 0) profit = 0;
                if (loss < 0) loss = 0;
                history[i].Profit = profit;
                history[i].Loss = loss;
                if (i > 13)
                {
                    if (i == 14)
                    {
                        var gain14Days = (history[i - 13].Profit + history[i - 12].Profit + history[i - 11].Profit + history[i - 10].Profit
                            + history[i - 9].Profit + history[i - 8].Profit +
                            history[i - 7].Profit + history[i - 6].Profit + history[i - 5].Profit + history[i - 4].Profit + history[i - 3].Profit +
                            history[i - 2].Profit + history[i - 1].Profit + history[i].Profit) / 14;

                        var loss14Days = (history[i - 13].Loss + history[i - 12].Loss + history[i - 11].Loss + history[i - 10].Loss +
                            history[i - 9].Loss + history[i - 8].Loss +
                            history[i - 7].Loss + history[i - 6].Loss + history[i - 5].Loss + history[i - 4].Loss + history[i - 3].Loss +
                            history[i - 2].Loss + history[i - 1].Loss + history[i].Loss) / 14;
                        history[i].Avg14DaysLoss = Math.Round(loss14Days, 2, MidpointRounding.AwayFromZero);
                        history[i].Avg14DaysProfit = Math.Round(gain14Days, 2, MidpointRounding.AwayFromZero);
                        if (loss14Days > 0)
                            history[i].RS = Math.Round(gain14Days / loss14Days, 2, MidpointRounding.AwayFromZero);
                        history[i].RSI = Math.Round(100 - (100 / (1 + history[i].RS)), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        var sum0 = Math.Round(((history[i - 1].Avg14DaysProfit * 13) + history[i].Profit) / 14, 2, MidpointRounding.AwayFromZero);
                        var loss0 = Math.Round(((history[i - 1].Avg14DaysLoss * 13) + history[i].Loss) / 14, 2, MidpointRounding.AwayFromZero);
                        history[i].Avg14DaysLoss = loss0;
                        history[i].Avg14DaysProfit = sum0;
                        if (loss0 > 0)
                            history[i].RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
                        history[i].RSI = Math.Round(100 - (100 / (1 + history[i].RS)), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }

            var last = history.Last();
            stock.RSI = last.RSI;
            DbContext.DailyHistories.UpdateRange(history);
            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            await GetTodayData();
        }
    }
    public async Task RSICalculation(int days, string? code = null)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.Code == code || code == null).ToListAsync();
        int q = 0;
        foreach (var stock in stocks)
        {
            q++;
            Text = $"{q}. Calculating RSI for {stock.Name}";
            var histories = await DbContext.DailyHistories.Where(x => x.Code == stock.Code).OrderBy(o => o.Date).ToListAsync();
            if (histories.Count == 0) continue;
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
                if (i > days-1)
                {
                    if (i == days)
                    {
                        var gain14Days = histories.Take(days).Average(s=>s.Profit);

                        var loss14Days = histories.Take(days).Average(s => s.Loss);

                        histories[i].Avg14DaysLoss = Math.Round(loss14Days, 2, MidpointRounding.AwayFromZero);
                        histories[i].Avg14DaysProfit = Math.Round(gain14Days, 2, MidpointRounding.AwayFromZero);
                        if (loss14Days > 0)
                            histories[i].RS = Math.Round(gain14Days / loss14Days, 2, MidpointRounding.AwayFromZero);
                        histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        var sum0 = Math.Round(((histories[i - 1].Avg14DaysProfit * (days-1)) + histories[i].Profit) / days, 2, MidpointRounding.AwayFromZero);
                        var loss0 = Math.Round(((histories[i - 1].Avg14DaysLoss * (days - 1)) + histories[i].Loss) / days, 2, MidpointRounding.AwayFromZero);
                        histories[i].Avg14DaysLoss = loss0;
                        histories[i].Avg14DaysProfit = sum0;
                        if (loss0 > 0)
                            histories[i].RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
                        histories[i].RSI = Math.Round(100 - (100 / (1 + histories[i].RS)), 2, MidpointRounding.AwayFromZero);
                    }
                }
            }

            var last = histories.Last();
            stock.RSI = last.RSI;
            DbContext.DailyHistories.UpdateRange(histories);
            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            await GetTodayData();
        }
    }
    public async Task CalculateIncrementalRSI(string? code = null)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.Code == code).ToListAsync();
        foreach (var stock in stocks)
        {
            Text = $"Calculating Incremental RSI for {stock.Name}";
            var history = await DbContext.DailyHistories.Where(x => x.Code == stock.Code).OrderByDescending(o => o.Date).Take(2).ToListAsync();
            if (history.Count != 2) continue;
            var last = history.Last();
            var today = history.First();

            var sum0 = Math.Round(((last.Avg14DaysProfit * 13) + today.Profit) / 14, 2, MidpointRounding.AwayFromZero);
            var loss0 = Math.Round(((last.Avg14DaysLoss * 13) + today.Loss) / 14, 2, MidpointRounding.AwayFromZero);
            today.Avg14DaysLoss = loss0;
            today.Avg14DaysProfit = sum0;
            if (loss0 > 0)
                today.RS = Math.Round(sum0 / loss0, 2, MidpointRounding.AwayFromZero);
            today.RSI = Math.Round(100 - (100 / (1 + today.RS)), 2, MidpointRounding.AwayFromZero);

            stock.RSI = last.RSI;
            stock.UpdatedOn = DateTimeOffset.Now;
            DbContext.DailyHistories.UpdateRange(history);
            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            //await BottonOutHunting_Calculation(stock.Code);
        }
    }
    public async Task BottonOutHunting_Calculation(string? code = null)
    {
        var stocks = await DbContext.EquityStocks.Where(x => x.RSI > 0).ToListAsync();
        var yearBack = DateOnly.FromDateTime(DateTimeOffset.Now.AddYears(-1).AddDays(-1).Date);
        foreach (var stock in stocks)
        {
            Text = $"BottonOutHunting Calculating for {stock.Name}";
            var history = await DbContext.DailyHistories.Where(x => x.Code == stock.Code && x.Date >= yearBack).OrderBy(o => o.Date).ToListAsync();
            if (history.Count == 0) continue;
            for (int i = 0; i < history.Count; i++)
            {
                var low = history.OrderBy(x => x.Low).FirstOrDefault();
                var high = history.OrderByDescending(x => x.High).FirstOrDefault();
                if (low is null || high is null) continue;
                stock.YearLow = low.Low;
                stock.YearLowOn = low.Date;
                stock.YearHigh = high.High;
                stock.YearHighOn = high.Date;
            }

            DbContext.EquityStocks.UpdateRange(stocks);
            await DbContext.SaveChangesAsync();

            await GetTodayData();
        }
    }
    public async Task AddIncrementalHistoryData(List<DailyHistory> histories)
    {
        foreach (var history in histories)
        {
            Text = $"Updating history for {history.Name} for {history.Date:dd-MMM-yyyy}";
            var x = await DbContext.DailyHistories.FirstOrDefaultAsync(x => x.Code == history.Code && x.Date == history.Date);
            if (x != null)
            {
                x.Open = history.Open;
                x.Close = history.Close;
                x.High = history.High;
                x.Low = history.Low;
                x.UpdatedOn = DateTimeOffset.Now;
                x.UpdatedById = 1;
                DbContext.Update(x);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                DbContext.DailyHistories.Add(history);
                await DbContext.SaveChangesAsync();
            }
        }
    }
    public async Task GetTodayData()
    {
        if (Screen.PrimaryScreen != null)
        {
            Browser.Width = Screen.PrimaryScreen.Bounds.Width;
            Browser.Height = Screen.PrimaryScreen.Bounds.Height;
            Controls.Add(Browser);
        }

        var data = await DbContext.EquityStocks.Where(x => x.RSI5DMADiff <= -1).ToListAsync();
        grdRSIData.AutoGenerateColumns = false;
        grdRSIData.DataSource = data;
    }
    private async void GrdRSIData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        var source = ((DataGridView)sender).DataSource;
        var code = ((List<EquityStock>)source)[e.RowIndex].Code;
        var today = DateOnly.FromDateTime(DateTime.Now.AddDays(-30));

        var data = await DbContext.DailyHistories.Where(x => x.Date > today && x.Code == code).ToListAsync();
        dataGridView1.AutoGenerateColumns = false;
        dataGridView1.DataSource = data;
    }
    private async void BtnReferesh_Click(object sender, EventArgs e)
    {
        CurrentStock = 480;
        StockList = await DbContext.EquityStocks.Where(s => s.IsActive && s.IndexName == "").ToListAsync();
        Text = $"{CurrentStock + 1}. {StockList[CurrentStock].Name}";

        Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
        Browser.Navigate(Url);
        Browser.DocumentCompleted += Browser_DocumentCompleted_HistoricalData;
        grdRSIData.Visible = dataGridView1.Visible = false;
        if (Screen.PrimaryScreen != null)
        {
            Browser.Width = Screen.PrimaryScreen.Bounds.Width;
            Browser.Height = Screen.PrimaryScreen.Bounds.Height - 100;
            Browser.Margin = new Padding(10, 50, 10, 10);
        }
        Browser.ScriptErrorsSuppressed = true;
        Controls.Add(Browser);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        CurrentStock++;
        if (CurrentStock < StockList.Count)
        {
            Text = $"{CurrentStock + 1}. {StockList[CurrentStock].Name}";
            Url = $"https://finance.yahoo.com/quote/{StockList[CurrentStock].Code}.NS/history/{TimeRange}";
            Browser.Navigate(Url);
        }
    }
}

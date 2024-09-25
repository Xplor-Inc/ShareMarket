namespace ShareMarket.DesktopApp.Models.Equities;

public class StockModel
{
    public int          Priority        { get; set; }
    public string       Symbol          { get; set; } = default!;
    public decimal      Open            { get; set; }
    public decimal      DayHigh         { get; set; }
    public decimal      DayLow          { get; set; }
    public decimal      LastPrice       { get; set; }
    public decimal      PreviousClose   { get; set; }
    public decimal      Change          { get; set; }
    public decimal      PChange         { get; set; }
    public DateTime     LastUpdateTime  { get; set; }
    public decimal      YearHigh        { get; set; }
    public decimal      YearLow         { get; set; }
    public decimal      PerChange365d   { get; set; }
    public decimal      PerChange30d    { get; set; }
    public StockMeta    Meta            { get; set; } = new StockMeta();
}

namespace ShareMarket.DesktopApp.Entities;

public class DailyHistory : Auditable
{
    public decimal          RSI5DMA         { get; set; }
    public decimal          RSI5DMADiff     { get; set; }
    public decimal  Avg14DaysProfit { get; set; }
    public decimal  Avg14DaysLoss   { get; set; }
    public decimal  Close           { get; set; }
    public string   Code            { get; set; } = default!;
    public DateOnly Date            { get; set; }
    public decimal  High            { get; set; }
    public decimal  Loss            { get; set; }
    public decimal  Low             { get; set; }
    public string   Name            { get; set; } = default!;
    public decimal  Open            { get; set; }
    public decimal  Profit          { get; set; }
    public decimal  RS              { get; set; }
    public decimal  RSI             { get; set; }
}

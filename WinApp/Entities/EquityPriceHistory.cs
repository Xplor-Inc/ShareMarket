namespace ShareMarket.WinApp.Entities;

public class EquityPriceHistory : Auditable
{
    public decimal  Avg14DaysProfit { get; set; }
    public decimal  Avg14DaysLoss   { get; set; }
    public decimal  Close           { get; set; }
    public string   Code            { get; set; } = default!;
    public DateOnly Date            { get; set; }
    public long     EquityId        { get; set; }
    public decimal  High            { get; set; }
    public decimal  Loss            { get; set; }
    public decimal  Low             { get; set; }
    public string   Name            { get; set; } = default!;
    public decimal  Open            { get; set; }
    public decimal  Profit          { get; set; }
    public decimal  RS              { get; set; }
    public decimal  RSI             { get; set; }
    public decimal  RSI14DMA         { get; set; }
    public decimal  RSI14DMADiff     { get; set; }


    public EquityStock Equity       { get; set; } = default!;
}

namespace ShareMarket.DesktopApp.Entities;

public class EquityStock : Auditable
{
    public decimal          Change          { get; set; }
    public string           Code            { get; set; } = default!;
    public decimal          DayHigh         { get; set; }
    public decimal          DayLow          { get; set; }
    public string           IndexName       { get; set; } = default!;
    public string           Industry        { get; set; } = default!;
    public string?          InstrumentName  { get; set; } = default!;
    public bool             IsActive        { get; set; }
    public bool             IsETFSec        { get; set; }
    public bool             IsFNOSec        { get; set; }
    public decimal          LTP             { get; set; }
    public string           Name            { get; set; } = default!;
    public decimal          Open            { get; set; }
    public decimal          PChange         { get; set; }
    public decimal          PerChange365d   { get; set; }
    public decimal          PerChange30d    { get; set; }
    public decimal          PreviousClose   { get; set; }
    public string?          SectorName      { get; set; } = default!;
    public decimal          YearHigh        { get; set; }
    public DateOnly         YearHighOn      { get; set; }
    public decimal          YearLow         { get; set; }
    public DateOnly         YearLowOn       { get; set; }


    #region Stratergy
    public decimal          RSI             { get; set; }
    public decimal          RSI5DMA         { get; set; }
    public decimal          RSI5DMADiff     { get; set; }
    public decimal          RSI10DMA        { get; set; }
    public decimal          RSI10DMADiff    { get; set; }
    public decimal          RSI15DMA        { get; set; }
    public decimal          RSI15DMADiff    { get; set; }

    #endregion
}


public class Stock : Entity
{
    public string Name { get; set; }
    public string? Details { get; set; }
    public int? SectorId { get; set; }
    public string Code { get; set; }
    public decimal MarketCap { get; set; }
    public string SearchText { get; set; }
    public decimal PE { get; set; }
    public decimal Price { get; set; }
    public DateTime? lastDataSync { get; set; }
    public bool SyncStatus { get; set; }
    public string FinologyUrl { get; set; }
    public string ScreenerUrl { get; set; }
}
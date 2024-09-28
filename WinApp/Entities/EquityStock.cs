namespace ShareMarket.WinApp.Entities;

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
    public bool             IsRaising       { get; set; }
    public decimal          LTP             { get; set; }
    public string           Name            { get; set; } = default!;
    public decimal          PChange         { get; set; }
    public int              RankByGroww     { get; set; }
    public string?          SectorName      { get; set; } = default!;
    public int              SharekhanId     { get; set; }
    public decimal          YearHigh        { get; set; }
    public DateOnly         YearHighOn      { get; set; }
    public decimal          YearLow         { get; set; }
    public DateOnly         YearLowOn       { get; set; }


    #region Stratergy
    public decimal          RSI             { get; set; }
    public decimal          RSI14DMA         { get; set; }
    public decimal          RSI14DMADiff     { get; set; }
    #endregion

    public List<EquityPriceHistory>? PriceHistories { get; set; }
}

public class EquityHistorySyncLog : Auditable
{
    public string           Name           { get; set; }
    public string           Code           { get; set; } = default!;
    public string           ErrorMessage   { get; set; }
    public string           Provider       { get; set; }
}
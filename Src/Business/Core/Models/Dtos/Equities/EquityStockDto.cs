namespace ShareMarket.Core.Models.Dtos.Equities;

public class EquityStockDto : AuditableDto
{
    public decimal  Change      { get; set; }
    public string   Code        { get; set; } = default!;
    public bool     BuyAlready  { get; set; }
    public decimal  DayHigh     { get; set; }
    public decimal  DayLow      { get; set; }
    public string   IndexName   { get; set; } = default!;
    public string   Industry    { get; set; } = default!;
    public string?  InstrumentName { get; set; } = default!;
    public bool     IsActive    { get; set; }
    public bool     IsETFSec    { get; set; }
    public bool     IsFNOSec    { get; set; }
    public bool     IsRaising   { get; set; }
    public decimal  LTP         { get; set; }
    public DateOnly LTPDate     { get; set; }
    public string   Name        { get; set; } = default!;
    public decimal  PChange     { get; set; }
    public int      RankByGroww { get; set; }
    public string?  SectorName  { get; set; } = default!;
    public int      SharekhanId { get; set; }
    public decimal  YearHigh    { get; set; }
    public DateOnly YearHighOn  { get; set; }
    public decimal  YearLow     { get; set; }
    public DateOnly YearLowOn   { get; set; }

    #region Fundamental
    public decimal  EPS         { get; set; }
    public decimal  PE          { get; set; }
    public decimal  PD          { get; set; }
    public decimal  Dividend    { get; set; }
    public decimal  MarketCap   { get; set; }
    public decimal  FaceValue   { get; set; }
    public decimal  BookValue   { get; set; }
    public decimal  DebtEquity  { get; set; }
    public decimal  ROE         { get; set; }
    #endregion

    #region Stratergy
    public decimal  RSI         { get; set; }
    public decimal  RSI14EMA    { get; set; }
    public decimal  RSI14EMADiff { get; set; }
    public decimal  DMA5        { get; set; }
    public decimal  DMA10       { get; set; }
    public decimal  DMA20       { get; set; }
    public decimal  DMA50       { get; set; }
    public decimal  DMA100      { get; set; }
    public decimal  DMA200      { get; set; }

    #endregion

    public List<EquityPriceHistoryDto>? PriceHistories { get; set; }
}
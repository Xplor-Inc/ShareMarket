namespace ShareMarket.Core.Models.Services.Groww;

public class GrowwStockModel
{
    public decimal Open             { get; set; }
    public decimal High             { get; set; }
    public decimal Low              { get; set; }
    public decimal Ltp              { get; set; }
    public decimal Close            { get; set; }
    public decimal DayChange        { get; set; }
    public decimal DayChangePerc    { get; set; }
}


public class TradeBook
{
    public decimal      CapitalUsed { get; set; }
    public bool         StopLoss    { get; set; }
    public decimal      LTP30Days   { get; set; }   
    public string       Code        { get; set; } = string.Empty;
    public string       Name        { get; set; } = string.Empty;
    public DateOnly     BuyDate     { get; set; }
    public DateOnly?    SellDate    { get; set; }
    public decimal      SellRate    { get; set; }
    public DateOnly?    SLDate      { get; set; }
    public decimal      BuyRate     { get; set; }
    public int          Quantity    { get; set; }
    public decimal      BuyValue    { get; set; }
    public decimal      Diff        { get; set; }
    public decimal      LTP         { get; set; }
    public decimal      Target      { get; set; }
    public decimal      Holding     { get; set; }
    public decimal      High        { get; set; }
    public decimal      Low         { get; set; }
    public int          Rank        { get; set; }
}
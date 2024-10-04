using ShareMarket.Core.Enumerations;

namespace ShareMarket.Core.Entities.Tradings;

public class VirtualTrade : Auditable
{
    public DateOnly     BuyDate     { get; set; }
    public decimal      BuyRate     { get; set; }
    public decimal      BuyValue    { get; set; }
    public string       Code        { get; set; } = string.Empty;
    public decimal      Holding     { get; set; }
    public decimal      LTP         { get; set; }
    public string       Name        { get; set; } = string.Empty;
    public int          Quantity    { get; set; }
    public DateOnly?    SellDate    { get; set; }
    public decimal      SellRate    { get; set; }
    public decimal      SellValue   { get; set; }
    public DateOnly?    SLDate      { get; set; }
    public decimal      SLRate      { get; set; }
    public SellAction   SellAction  { get; set; }
    public decimal      Target      { get; set; }
}
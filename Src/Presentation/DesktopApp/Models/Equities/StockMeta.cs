namespace ShareMarket.DesktopApp.Models.Equities;

public class StockMeta
{
    public string   CompanyName { get; set; } = default!;
    public string   Industry    { get; set; } = default!;
    public bool     IsFNOSec    { get; set; }
    public bool     IsCASec     { get; set; }
    public bool     IsSLBSec    { get; set; }
    public bool     IsDebtSec   { get; set; }
    public bool     IsSuspended { get; set; }
    public bool     IsETFSec    { get; set; }
    public bool     IsDelisted  { get; set; }

}

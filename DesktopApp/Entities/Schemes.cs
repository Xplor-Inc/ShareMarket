using Newtonsoft.Json;
using ShareMarket.DesktopApp.Models.MutualFunds;

namespace ShareMarket.DesktopApp.Entities;

public class MFSchemes : Auditable
{
    public string? NfoRisk { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaDesc { get; set; }
    public string? MetaRobots { get; set; }
    public string? SchemeCode { get; set; }
    public string? SchemeName { get; set; }
    public string? SearchId { get; set; }
    public int? GrowwRating { get; set; }
    public string? CrisilRating { get; set; }
}

public class MFStockHolding : Auditable
{
    public long SchemeId { get; set; }
    public MFSchemes Scheme { get; set; } 
    public string? SchemeCode { get; set; } = default!;
    public DateTime? PortfolioDate { get; set; }
    public string? CompanyName { get; set; } = default!;
    public string? NatureName { get; set; } = default!;
    public string? SectorName { get; set; } = default!;
    public string? InstrumentName { get; set; } = default!;
    public string? Rating { get; set; } = default!;
    public string? MarketValue { get; set; } = default!;
    public double? CorpusPer { get; set; }
    public string? MarketCap { get; set; } = default!;
    public string? RatingMarketCap { get; set; } = default!;
    public string? StockSearchId { get; set; } = default!;
    public string? Code { get; set; }
}

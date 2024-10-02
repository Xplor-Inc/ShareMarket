using ShareMarket.Core.Entities;

namespace ShareMarket.Core.Entities.Schemes;

public class Scheme : Auditable
{
    public string? MetaTitle { get; set; }
    public string? MetaDesc { get; set; }
    public string? MetaRobots { get; set; }
    public string? SchemeCode { get; set; }
    public string? SchemeName { get; set; }
    public string? SearchId { get; set; }
    public int? GrowwRating { get; set; }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarket.DesktopApp.Models.MutualFunds;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class MFHoldingData
{
    [JsonProperty("nfo_risk")]
    public string? NfoRisk { get; set; }


    [JsonProperty("meta_title")]
    public string? MetaTitle { get; set; }

    [JsonProperty("meta_desc")]
    public string? MetaDesc { get; set; }

    [JsonProperty("meta_robots")]
    public string? MetaRobots { get; set; }


    [JsonProperty("scheme_code")]
    public string? SchemeCode { get; set; }


    [JsonProperty("scheme_name")]
    public string? SchemeName { get; set; }

    [JsonProperty("search_id")]
    public string? SearchId { get; set; }



    [JsonProperty("groww_rating")]
    public string? GrowwRating { get; set; }

    [JsonProperty("crisil_rating")]
    public string? CrisilRating { get; set; }


    [JsonProperty("holdings")]
    public List<Holding> Holdings { get; set; }

}





public class Holding
{
    [JsonProperty("scheme_code")]
    public string? SchemeCode { get; set; }

    [JsonProperty("portfolio_date")]
    public DateTime PortfolioDate { get; set; }

    [JsonProperty("company_name")]
    public string? CompanyName { get; set; }

    [JsonProperty("nature_name")]
    public string? NatureName { get; set; }

    [JsonProperty("sector_name")]
    public string? SectorName { get; set; }

    [JsonProperty("instrument_name")]
    public string? InstrumentName { get; set; }

    [JsonProperty("rating")]
    public string? Rating { get; set; }

    [JsonProperty("market_value")]
    public string? MarketValue { get; set; }

    [JsonProperty("corpus_per")]
    public double? CorpusPer { get; set; }

    [JsonProperty("market_cap")]
    public string? MarketCap { get; set; }

    [JsonProperty("rating_market_cap")]
    public string? RatingMarketCap { get; set; }

    [JsonProperty("stock_search_id")]
    public string? StockSearchId { get; set; }
}


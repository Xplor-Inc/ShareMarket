using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShareMarket.DesktopApp.Models.MutualFunds
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class MFContent
    {
        //[JsonProperty("id")]
        //public string Id { get; set; }

        [JsonProperty("fund_name")]
        public string FundName { get; set; }

        [JsonProperty("search_id")]
        public string SearchId { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("sub_category")]
        public string SubCategory { get; set; }

        [JsonProperty("sub_sub_category")]
        public List<string> SubSubCategory { get; set; }

        [JsonProperty("aum")]
        public double? Aum { get; set; }

        [JsonProperty("available_for_investment")]
        public int AvailableForInvestment { get; set; }

        [JsonProperty("min_sip_investment")]
        public double? MinSipInvestment { get; set; }

        [JsonProperty("sip_allowed")]
        public bool SipAllowed { get; set; }

        [JsonProperty("lumpsum_allowed")]
        public bool LumpsumAllowed { get; set; }

        [JsonProperty("return3y")]
        public double? Return3y { get; set; }

        [JsonProperty("return1y")]
        public double? Return1y { get; set; }

        [JsonProperty("return5y")]
        public double? Return5y { get; set; }

        [JsonProperty("nav")]
        public decimal? Nav { get; set; }

        [JsonProperty("return1d")]
        public double Return1d { get; set; }

        [JsonProperty("min_investment_amount")]
        public double MinInvestmentAmount { get; set; }

        [JsonProperty("groww_rating")]
        public int? GrowwRating { get; set; }

        [JsonProperty("risk_rating")]
        public int RiskRating { get; set; }

        [JsonProperty("scheme_name")]
        public string SchemeName { get; set; }

        [JsonProperty("scheme_type")]
        public string SchemeType { get; set; }

        [JsonProperty("fund_manager")]
        public string FundManager { get; set; }

        [JsonProperty("fund_house")]
        public string FundHouse { get; set; }

        [JsonProperty("scheme_code")]
        public string SchemeCode { get; set; }

        [JsonProperty("launch_date")]
        public DateOnly? LaunchDate { get; set; }

        [JsonProperty("risk")]
        public string Risk { get; set; }

        [JsonProperty("doc_type")]
        public string DocType { get; set; }

        [JsonProperty("registrar_agent")]
        public string RegistrarAgent { get; set; }

        [JsonProperty("doc_required")]
        public bool DocRequired { get; set; }

        [JsonProperty("plan_type")]
        public string PlanType { get; set; }

        [JsonProperty("page_view")]
        public int PageView { get; set; }

        [JsonProperty("direct_fund")]
        public string DirectFund { get; set; }

        [JsonProperty("amc")]
        public string Amc { get; set; }

        [JsonProperty("direct_search_id")]
        public string DirectSearchId { get; set; }

        [JsonProperty("direct_scheme_name")]
        public string DirectSchemeName { get; set; }

        [JsonProperty("term_page_view")]
        public int TermPageView { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }
    }

    public class MFModel
    {
        [JsonProperty("content")]
        public List<MFContent> Content { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }



}

namespace ShareMarket.DesktopApp.Models.Equities;

public class IndexData
{
    public string           Name    { get; set; } = default!;
    public List<StockModel> Data    { get; set; } = [];
}

using Microsoft.EntityFrameworkCore;
using ShareMarket.DesktopApp.Entities;

namespace ShareMarket.DesktopApp;

public class ShareMarketContext : DbContext
{
    public DbSet<Stock> Stock { get; set; }
    public DbSet<MFSchemes> MFSchemes { get; set; }
    public DbSet<MFStockHolding> StockHoldings { get; set; }
    public DbSet<DailyHistory>  DailyHistories  { get; set; }
    public DbSet<EquityStock>   EquityStocks    { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Data Source=XPLOR-INC;Initial Catalog=ShareMarket2;User ID=sa;Password=Saini@123;TrustServerCertificate=True");
}

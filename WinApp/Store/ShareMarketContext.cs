using Microsoft.EntityFrameworkCore;
using ShareMarket.WinApp.Store.EntityConfiguration;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Store.Maps;

namespace ShareMarket.WinApp.Store;

public class ShareMarketContext : DbContext
{
    public DbSet<EquityPriceHistory>    EquityPriceHistories    { get; set; }
    public DbSet<EquityStock>           EquityStocks            { get; set; }
    public DbSet<Scheme>                Schemes                 { get; set; }
    public DbSet<SchemeEquityHolding>   SchemeEquityHoldings    { get; set; }
    public DbSet<EquityHistorySyncLog>  EquityHistorySyncLog    { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMapping(new EquityPriceHistoryMap());
        modelBuilder.AddMapping(new EquityStockMap());
        modelBuilder.AddMapping(new SchemeEquityHoldingMap());
        modelBuilder.AddMapping(new SchemeMap());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=SHARAD;Initial Catalog=ShareMarket;Persist Security Info=True;Integrated Security=true;TrustServerCertificate=True;");
    }
}

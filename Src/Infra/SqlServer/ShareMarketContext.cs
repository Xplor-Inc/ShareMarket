using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Entities.Schemes;
using ShareMarket.Core.Interfaces.SqlServer;
using ShareMarket.SqlServer.Extensions;
using ShareMarket.SqlServer.Maps.Audits;
using ShareMarket.SqlServer.Maps.EquityStocks;
using ShareMarket.SqlServer.Maps.Schemes;
using ShareMarket.SqlServer.Maps.Users;

namespace ShareMarket.SqlServer;
public class ShareMarketContext : DataContext<User>, IShareMarketContext
{
    #region Properties
    public DbSet<EmailAuditLog>         EmailAuditLogs          { get; set; }
    public DbSet<UserLogin>             UserLogins              { get; set; }
    public DbSet<EquityPriceHistory>    EquityPriceHistories    { get; set; }
    public DbSet<EquityStock>           EquityStocks            { get; set; }
    public DbSet<Scheme>                Schemes                 { get; set; }
    public DbSet<SchemeEquityHolding>   SchemeEquityHoldings    { get; set; }
    public DbSet<EquityHistorySyncLog>  EquityHistorySyncLog    { get; set; }
   #endregion

    #region Constructor
    public ShareMarketContext(string connectionString, ILoggerFactory loggerFactory)
        : base(connectionString, loggerFactory)
    {
    }

    public ShareMarketContext(IConnection connection, ILoggerFactory loggerFactory)
        : base(connection, loggerFactory)
    {
    }

    #endregion

    #region IShareMarketContext Implementation
    IQueryable<EmailAuditLog>           IShareMarketContext.EmailAuditLogs         => EmailAuditLogs;
    IQueryable<ChangeLog>               IShareMarketContext.ChangeLogs             => ChangeLogs;
    IQueryable<UserLogin>               IShareMarketContext.UserLogins             => UserLogins;

    #endregion

    public override void ConfigureMappings(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMapping(new EquityPriceHistoryMap());
        modelBuilder.AddMapping(new EquityStockMap());
        modelBuilder.AddMapping(new SchemeEquityHoldingMap());
        modelBuilder.AddMapping(new SchemeMap());
        modelBuilder.AddMapping(new ChangeLogMap());
        modelBuilder.AddMapping(new EmailAuditLogMap());
        modelBuilder.AddMapping(new UserMap());
        modelBuilder.AddMapping(new UserLoginMap());

        base.ConfigureMappings(modelBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
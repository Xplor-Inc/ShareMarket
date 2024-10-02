using ShareMarket.Core.Interfaces.SqlServer;
using ShareMarket.SqlServer.Extensions;
using ShareMarket.SqlServer.Maps.Audits;
using ShareMarket.SqlServer.Maps.Users;

namespace ShareMarket.SqlServer;
public class ShareMarketContext : DataContext<User>, IShareMarketContext
{
    #region Properties
    public DbSet<EmailAuditLog>     EmailAuditLogs      { get; set; }
    public DbSet<UserLogin>         UserLogins          { get; set; }
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
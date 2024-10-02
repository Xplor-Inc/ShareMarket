namespace ShareMarket.Core.Interfaces.SqlServer;
public interface IContext : IDisposable
{
    void                Add<T>(T entity) where T : class;
    void                CreateStructure();
    void                DeleteDatabase();
    void                Delete<T>(T entity) where T : class;
    void                DropStructure();
    long                ExecuteCommand(string commandText);
    IQueryable<T>       Query<T>() where T : class;
    IQueryable<T>       Query<T>(string SPName) where T : class;
    Task<int>           SaveChangesAsync(CancellationToken cancellationToken = default);
    void                Update<T>(T entity) where T : class;
}
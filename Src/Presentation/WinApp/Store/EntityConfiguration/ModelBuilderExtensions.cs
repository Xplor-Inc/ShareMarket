using Microsoft.EntityFrameworkCore;

namespace ShareMarket.WinApp.Store.EntityConfiguration;

public static class ModelBuilderExtensions
{
    public static void AddMapping<TEntity>(this ModelBuilder builder, BaseMap<TEntity> map) where TEntity : class
    {
        builder.Entity<TEntity>(map.Configure);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareMarket.WinApp.Store.EntityConfiguration;
public abstract class BaseMap<TEntity> where TEntity : class
{
    public abstract void Configure(EntityTypeBuilder<TEntity> entity);
}

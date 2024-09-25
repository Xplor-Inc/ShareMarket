using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Store.EntityConfiguration;

namespace ShareMarket.WinApp.Store.Maps;

public class SchemeMap : BaseMap<Scheme>
{
    public override void Configure(EntityTypeBuilder<Scheme> entity)
    {
        entity
            .ToTable("Schemes");

        entity
        .Property(e => e.MetaTitle)
        .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
           .Property(e => e.MetaDesc)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
     
        entity
           .Property(e => e.MetaRobots)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
           .Property(e => e.SchemeCode)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
   
        entity
           .Property(e => e.SchemeName)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
           .Property(e => e.SearchId)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}
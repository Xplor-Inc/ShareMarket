using ShareMarket.Core.Entities.Equities;

namespace ShareMarket.SqlServer.Maps.EquityStocks;

public class EquityStockMap : Map<EquityStock>
{
    public override void Configure(EntityTypeBuilder<EquityStock> entity)
    {
        entity
            .ToTable("EquityStocks");

        entity
           .Property(e => e.Change)
           .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
           .Property(e => e.DayHigh)
           .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.DayLow)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.LTP)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.PChange)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.YearHigh)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.YearLow)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

    }
}
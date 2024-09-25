using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareMarket.WinApp.Entities;
using ShareMarket.WinApp.Store.EntityConfiguration;

namespace ShareMarket.WinApp.Store.Maps;

public class EquityPriceHistoryMap : BaseMap<EquityPriceHistory>
{
    public override void Configure(EntityTypeBuilder<EquityPriceHistory> entity)
    {
        entity
            .ToTable("EquityPriceHistories");

        entity
           .Property(e => e.Code)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
           .Property(e => e.Name)
           .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
           .Property(e => e.Avg14DaysProfit)
           .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
           .Property(e => e.Avg14DaysLoss)
           .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.Close)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.High)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.Loss)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.Low)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.Open)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.Profit)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.RS)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);

        entity
            .Property(e => e.RSI)
            .HasPrecision(StaticConfiguration.DECIMAL_PRECISION, StaticConfiguration.DECIMAL_SCALE_2);
    }
}
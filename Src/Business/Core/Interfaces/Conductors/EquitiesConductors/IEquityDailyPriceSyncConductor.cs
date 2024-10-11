namespace ShareMarket.Core.Interfaces.Conductors.EquitiesConductors;

public interface IEquityDailyPriceSyncConductor
{
    Task<int> SyncEquityLTPAsync();
    Task<string> SyncLtpByStocksAsync(string code, DateOnly date);
}
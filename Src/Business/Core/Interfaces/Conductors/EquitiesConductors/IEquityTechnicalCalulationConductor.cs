using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Models.Results;

namespace ShareMarket.Core.Interfaces.Conductors.EquitiesConductors;

public interface IEquityTechnicalCalulationConductor
{
    Task<Result<bool>> CreateOrUpdateHistoryAsync(List<EquityPriceHistory> histories);
    Task<Result<bool>> DMACalculation(string code);
    Task<Result<bool>> RSICalculation(string code);
    Task<Result<bool>> RSIIncrementalAsync(string code);
    Task<Result<bool>> RSI_X_EMA(string code);
}
using ShareMarket.Core.Models.Results;
using ShareMarket.Core.Models.Services.Groww;

namespace ShareMarket.Core.Services;

public interface IGrowwService
{
    Task<Result<GrowwStockModel?>> GetLTPPrice(string code);
}
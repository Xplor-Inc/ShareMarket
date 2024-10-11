using AutoMapper;
using ShareMarket.Core.Entities.Equities;
using ShareMarket.Core.Entities.Users;
using ShareMarket.Core.Models.Dtos.Equities;
using ShareMarket.Core.Models.Dtos.Users;

namespace ShareMarket.Core.Models.Dtos;
public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<EquityStock, EquityStockDto>().ReverseMap();
    }
}

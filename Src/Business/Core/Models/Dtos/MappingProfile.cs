using AutoMapper;
using ShareMarket.Core.Entities.Users;
using ShareMarket.Core.Models.Dtos.Users;

namespace ShareMarket.Core.Models.Dtos;
public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<User,                 UserDto>().ReverseMap();
    }
}

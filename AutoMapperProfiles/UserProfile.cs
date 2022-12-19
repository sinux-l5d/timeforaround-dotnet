using AutoMapper;
using TimeForARound.Dto;
using TimeForARound.Entities;

namespace TimeForARound.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Server to client
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.RoundsCount, opt => opt.MapFrom(src => src.Rounds.Count))
            .ForMember(dest => dest.RoundsPaid, opt => opt.MapFrom(src => src.Rounds.Count(r => r.AsBeenPaid)));
        CreateMap<User, UserDetailedDto>();
        
        // Client to server
        CreateMap<UserRegisterDto, User>();
    }
}
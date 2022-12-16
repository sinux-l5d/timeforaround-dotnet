using AutoMapper;
using TimeForARound.Dto;
using TimeForARound.Entities;

namespace TimeForARound.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Server to client
        CreateMap<User, UserDto>();
        
        // Client to server
        CreateMap<UserRegisterDto, User>();
    }
}
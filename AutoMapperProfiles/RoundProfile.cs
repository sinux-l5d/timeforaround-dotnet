using AutoMapper;
using TimeForARound.Dto;
using TimeForARound.Entities;

namespace TimeForARound.AutoMapperProfiles;

public class RoundProfile : Profile
{
    public RoundProfile()
    {
        // Server to client
        CreateMap<Round, RoundDto>();

        // Client to server
        CreateMap<RoundNewDto, Round>()
            .ForMember(dest => dest.ReportedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.AsBeenPaid, opt => opt.MapFrom(src => false));
    }
}
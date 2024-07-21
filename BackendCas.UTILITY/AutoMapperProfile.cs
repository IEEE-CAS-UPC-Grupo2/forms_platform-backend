using System.Text;
using AutoMapper;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.UTILITY;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AdministratorDTO, WebAdministrator>()
            .ForMember(dest => dest.Password,
                opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password ?? string.Empty)));

        CreateMap<WebAdministrator, AdministratorDTO>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.Password)));


        CreateMap<PlatformEvent, PlatformEventDTO>()
            .ReverseMap()
            .ForMember(dest => dest.EventDateAndTime, opt => opt.MapFrom(src =>
                src.EventDateAndTime));

        CreateMap<Participation, ParticipationDTO>().ReverseMap();
    }
}
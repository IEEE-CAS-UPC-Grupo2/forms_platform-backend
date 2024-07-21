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
            .ForMember(dest => dest.EventDateAndTime, opt => opt.MapFrom(src =>
                FormatDateTime(src.EventDateAndTime)))
            .ReverseMap()
            .ForMember(dest => dest.EventDateAndTime, opt => opt.MapFrom(src =>
                src.EventDateAndTime));

        CreateMap<Participation, ParticipationDTO>().ReverseMap();
    }

    private string FormatDateTime(string? dateTimeString)
    {
        if (string.IsNullOrEmpty(dateTimeString)) return null;

        if (DateTime.TryParse(dateTimeString, out var parsedDateTime))
            return parsedDateTime.ToString("dd/MM/yyyy HH:mm:ss");

        return dateTimeString;
    }
}
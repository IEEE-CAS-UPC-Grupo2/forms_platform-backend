using System;
using System.Text;
using AutoMapper;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.UTILITY
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AdministratorDTO, AdministratorsCa>()
                       .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Password ?? string.Empty)));

            CreateMap<AdministratorsCa, AdministratorDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.Password)));


            CreateMap<EventsCa, EventsCaDTO>()
                 .ForMember(dest => dest.EventDateAndTime, opt => opt.MapFrom(src =>
                     FormatDateTime(src.EventDateAndTime)))
                 .ReverseMap()
                 .ForMember(dest => dest.EventDateAndTime, opt => opt.MapFrom(src =>
                     src.EventDateAndTime));

            CreateMap<Participation, ParticipantDTO>().ReverseMap();
           
        }

        private string FormatDateTime(string? dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return null;
            }

            if (DateTime.TryParse(dateTimeString, out DateTime parsedDateTime))
            {
                return parsedDateTime.ToString("yyyy/MM/dd HH:mm:ss");
            }

            return dateTimeString;
        }
    }
}

using System;
using AutoMapper;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.UTILITY;

public class AutoMapperProfile : Profile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Administrator, AdministratorDTO>().ReverseMap();

            CreateMap<EventsCa, EventsCaDTO>()
                .ForMember(dest => dest.EventDateTime, opt => opt.MapFrom(src => src.EventDateTime.HasValue ? src.EventDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : null))
                .ReverseMap()
                .ForMember(dest => dest.EventDateTime, opt => opt.MapFrom(src => ParseDateTime(src.EventDateTime)));

            CreateMap<Participant, ParticipantDTO>().ReverseMap();
            CreateMap<Attendance, AttendanceDTO>().ReverseMap();
            CreateMap<Certificate, CertificateDTO>().ReverseMap();
        }

        private DateTime? ParseDateTime(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return null;
            }

            if (DateTime.TryParseExact(dateTimeString, "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDateTime))
            {
                return parsedDateTime;
            }

            return null;
        }
    }
}
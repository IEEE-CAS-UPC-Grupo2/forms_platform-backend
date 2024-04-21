using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.UTILITY
{
    public class AutoMapperProfile  :Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Administrator,AdministratorDTO>().ReverseMap();
            CreateMap<EventsCa,EventsCaDTO>().ReverseMap();
            CreateMap<Participant,ParticipantDTO>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;


namespace BackendCas.BLL.Services;

public class EventsCaService : IEventsCa
{
    private readonly IGenericRepository<EventsCa> _EventRepository;
    private readonly IMapper _mapper;

    public EventsCaService(IGenericRepository<EventsCa> eventrepository, IMapper mapper)
    {
        _EventRepository = eventrepository;
        _mapper = mapper;
    }

    async Task<List<EventsCaDTO>> IEventsCa.List()
    {
        try
        {
            var listaCategorias = await _EventRepository.Find();


            return _mapper.Map<List<EventsCaDTO>>(listaCategorias.ToList());
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<EventsCaDTO> IEventsCa.Create(EventsCaDTO modelo)
    {
        try
        {
            var EventCreate = await _EventRepository.Create(_mapper.Map<EventsCa>(modelo));
            if (EventCreate.IdEvent == 0) throw new TaskCanceledException("The event doesn't create");
            return _mapper.Map<EventsCaDTO>(EventCreate);
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<bool> IEventsCa.Edit(EventsCaDTO modelo)
    {
        try
        {
            var EventModel = _mapper.Map<EventsCa>(modelo);

            var EventFind = await _EventRepository.Obtain(u => u.IdEvent == EventModel.IdEvent);
            if (EventFind == null) throw new TaskCanceledException("The event doesn't exist");
            EventFind.EventTitle = EventModel.EventTitle;
            EventFind.EventDescription = EventModel.EventDescription;
            EventFind.ImageUrl = EventModel.ImageUrl;
            EventFind.Modality = EventModel.Modality;
            EventFind.InstitutionInCharge = EventModel.InstitutionInCharge;
            EventFind.Vacancy = EventModel.Vacancy;
            EventFind.AddressEvent = EventModel.AddressEvent;
            EventFind.Speaker = EventModel.Speaker;
            EventFind.EventDateTime = EventModel.EventDateTime;
            EventFind.IdAdministrator = EventModel.IdAdministrator;


            var answer = await _EventRepository.Edit(EventFind);

            if (!answer) throw new TaskCanceledException("The event doesn't edit");
            return answer;
        }
        catch (Exception)
        {
            throw;
        }
    }


    async Task<bool> IEventsCa.Delete(int id)
    {
        try
        {
            var EventFind = await _EventRepository.Obtain(p => p.IdEvent == id);

            if (EventFind == null) throw new TaskCanceledException("The event doesn't exist");

            var answer = await _EventRepository.Delete(EventFind);
            if (!answer) throw new TaskCanceledException("The event doesn't delete");

            return answer;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class PlatformEventService : IPlatformEventService
{
    private readonly IGenericRepository<PlatformEvent> _eventRepository;
    private readonly IMapper _mapper;

    public PlatformEventService(IGenericRepository<PlatformEvent> eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    async Task<List<WebEventDTO>> IPlatformEventService.List()
    {
        var listEvents = await _eventRepository.Find();


        return _mapper.Map<List<WebEventDTO>>(listEvents.ToList());
    }

    async Task<WebEventDTO> IPlatformEventService.GetById(int id)
    {
        try
        {
            var events = await _eventRepository.Obtain(c => c.IdEvent == id);
            if (events == null) throw new KeyNotFoundException("El evento no existe");

            return _mapper.Map<WebEventDTO>(events);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<WebEventDTO> IPlatformEventService.Create(WebEventDTO modelo)
    {
        var createEvent = await _eventRepository.Create(_mapper.Map<PlatformEvent>(modelo));
        if (createEvent.IdEvent == 0) throw new TaskCanceledException("El evento no pudo ser creado");
        return _mapper.Map<WebEventDTO>(createEvent);
    }

    async Task<bool> IPlatformEventService.Edit(WebEventDTO modelo)
    {
        var eventModel = _mapper.Map<PlatformEvent>(modelo);

        var foundEvent = await _eventRepository.Obtain(u => u.IdEvent == eventModel.IdEvent);
        if (foundEvent == null) throw new TaskCanceledException("El evento no existe");
        foundEvent.EventTitle = eventModel.EventTitle;
        foundEvent.EventDescription = eventModel.EventDescription;
        foundEvent.ImageUrl = eventModel.ImageUrl;
        foundEvent.Modality = eventModel.Modality;
        foundEvent.InstitutionInCharge = eventModel.InstitutionInCharge;
        foundEvent.Vacancy = eventModel.Vacancy;
        foundEvent.Address = eventModel.Address;
        foundEvent.Speaker = eventModel.Speaker;
        foundEvent.EventDateAndTime = eventModel.EventDateAndTime;
        foundEvent.IdAdministrator = eventModel.IdAdministrator;

        var answer = await _eventRepository.Edit(foundEvent);

        if (!answer) throw new TaskCanceledException("El evento no pudo ser editado");
        return answer;
    }

    async Task<bool> IPlatformEventService.Delete(int id)
    {
        var foundEvent = await _eventRepository.Obtain(p => p.IdEvent == id);

        if (foundEvent == null) throw new TaskCanceledException("El evento no existe");

        var answer = await _eventRepository.Delete(foundEvent);
        if (!answer) throw new TaskCanceledException("El evento no pudo ser borrado");

        return answer;
    }
}
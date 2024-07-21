using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class PlatformEventService : IPlatformEventService
{
    private readonly IGenericRepository<PlatformEvent> _EventRepository;
    private readonly IMapper _mapper;

    public PlatformEventService(IGenericRepository<PlatformEvent> eventrepository, IMapper mapper)
    {
        _EventRepository = eventrepository;
        _mapper = mapper;
    }

    async Task<List<WebEventDTO>> IPlatformEventService.List()
    {
        var listaCategorias = await _EventRepository.Find();


        return _mapper.Map<List<WebEventDTO>>(listaCategorias.ToList());
    }

    async Task<WebEventDTO> IPlatformEventService.GetById(int id)
    {
        try
        {
            var events = await _EventRepository.Obtain(c => c.IdEvent == id);
            if (events == null) throw new KeyNotFoundException("The event doesn't exist");

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
        var EventCreate = await _EventRepository.Create(_mapper.Map<PlatformEvent>(modelo));
        if (EventCreate.IdEvent == 0) throw new TaskCanceledException("The event doesn't create");
        return _mapper.Map<WebEventDTO>(EventCreate);
    }

    async Task<bool> IPlatformEventService.Edit(WebEventDTO modelo)
    {
        var EventModel = _mapper.Map<PlatformEvent>(modelo);

        var EventFind = await _EventRepository.Obtain(u => u.IdEvent == EventModel.IdEvent);
        if (EventFind == null) throw new TaskCanceledException("The event doesn't exist");
        EventFind.EventTitle = EventModel.EventTitle;
        EventFind.EventDescription = EventModel.EventDescription;
        EventFind.ImageUrl = EventModel.ImageUrl;
        EventFind.Modality = EventModel.Modality;
        EventFind.InstitutionInCharge = EventModel.InstitutionInCharge;
        EventFind.Vacancy = EventModel.Vacancy;
        EventFind.Address = EventModel.Address;
        EventFind.Speaker = EventModel.Speaker;
        EventFind.EventDateAndTime = EventModel.EventDateAndTime;
        EventFind.IdAdministrator = EventModel.IdAdministrator;


        var answer = await _EventRepository.Edit(EventFind);

        if (!answer) throw new TaskCanceledException("The event doesn't edit");
        return answer;
    }


    async Task<bool> IPlatformEventService.Delete(int id)
    {
        var EventFind = await _EventRepository.Obtain(p => p.IdEvent == id);

        if (EventFind == null) throw new TaskCanceledException("The event doesn't exist");

        var answer = await _EventRepository.Delete(EventFind);
        if (!answer) throw new TaskCanceledException("The event doesn't delete");

        return answer;
    }
}
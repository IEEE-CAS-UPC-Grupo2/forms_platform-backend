using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class ParticipantService : IParticipantService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Participation> _ParticipantRepository;

    public ParticipantService(IGenericRepository<Participation> participantRepository, IMapper mapper)
    {
        _ParticipantRepository = participantRepository;
        _mapper = mapper;
    }

    async Task<List<ParticipantDTO>> IParticipantService.List()
    {
        try
        {
            var listParticipants = await _ParticipantRepository.Find();
            return _mapper.Map<List<ParticipantDTO>>(listParticipants.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<ParticipantDTO> IParticipantService.Create(ParticipantDTO model)
    {
        try
        {
            var participantCreated = await _ParticipantRepository.Create(_mapper.Map<Participation>(model));
            if (participantCreated.IdParticipation == 0)
                throw new TaskCanceledException("The participant doesn't create");

            return _mapper.Map<ParticipantDTO>(participantCreated);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<ParticipantDTO> IParticipantService.GetById(int id)
    {
        try
        {
            var participant = await _ParticipantRepository.Obtain(p => p.IdParticipation == id);
            if (participant == null) throw new TaskCanceledException("The participant doesn't exist");

            return _mapper.Map<ParticipantDTO>(participant);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<bool> IParticipantService.Edit(ParticipantDTO model)
    {
        try
        {
            var participantModel = _mapper.Map<Participation>(model);
            var participantEdited =
                await _ParticipantRepository.Obtain(u => u.IdParticipation == participantModel.IdParticipation);

            participantEdited.Dni = participantModel.Dni;
            participantEdited.Name = participantModel.Name;
            participantEdited.Email = participantModel.Email;
            participantEdited.StudyCenter = participantModel.StudyCenter;
            participantEdited.Career = participantModel.Career;
            participantEdited.IeeeMembershipCode = participantModel.IeeeMembershipCode;

            return await _ParticipantRepository.Edit(participantEdited);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<bool> IParticipantService.Delete(int id)
    {
        try
        {
            var participantDeleted = await _ParticipantRepository.Obtain(u => u.IdParticipation == id);

            if (participantDeleted == null) throw new TaskCanceledException("The participant doesn't exist");

            return await _ParticipantRepository.Delete(participantDeleted);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
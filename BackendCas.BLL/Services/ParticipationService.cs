using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services;

public class ParticipationService : IParticipationService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Participation> _participationRepository;

    public ParticipationService(IGenericRepository<Participation> participationRepository, IMapper mapper)
    {
        _participationRepository = participationRepository;
        _mapper = mapper;
    }

    async Task<List<Participation>> IParticipationService.List()
    {
        try
        {
            var listParticipations = await _participationRepository.Find();
            return _mapper.Map<List<Participation>>(listParticipations.ToList());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<ParticipationDTO> IParticipationService.Create(ParticipationDTO model)
    {
        try
        {
            var foundParticipation = await _participationRepository.Obtain(p =>
                p.IdEvent == model.IdEvent &&
                p.Dni == model.DNI &&
                p.Email == model.Email);

            if (foundParticipation != null)
            {
                throw new TaskCanceledException("Persona ya se encuentra registrada para el evento");
            }
            
            var participationCreated = await _participationRepository.Create(_mapper.Map<Participation>(model));
            if (participationCreated.IdParticipation == 0)
                throw new TaskCanceledException("No se ha podido crear la participación");

            return _mapper.Map<ParticipationDTO>(participationCreated);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<Participation> IParticipationService.GetById(int id)
    {
        try
        {
            var participation = await _participationRepository.Obtain(p => p.IdParticipation == id);
            if (participation == null) throw new TaskCanceledException("La participación no existe");

            return _mapper.Map<Participation>(participation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<bool> IParticipationService.Edit(Participation model)
    {
        try
        {
            var participationModel = _mapper.Map<Participation>(model);
            var participationEdited =
                await _participationRepository.Obtain(u => u.IdParticipation == participationModel.IdParticipation);

            participationEdited.Dni = participationModel.Dni;
            participationEdited.Name = participationModel.Name;
            participationEdited.Email = participationModel.Email;
            participationEdited.StudyCenter = participationModel.StudyCenter;
            participationEdited.Career = participationModel.Career;
            participationEdited.IeeeMembershipCode = participationModel.IeeeMembershipCode;
            participationEdited.HasAttended = participationModel.HasAttended;
            participationEdited.HasCertificate = participationModel.HasCertificate;

            return await _participationRepository.Edit(participationEdited);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<bool> IParticipationService.Delete(int id)
    {
        try
        {
            var participationDeleted = await _participationRepository.Obtain(u => u.IdParticipation == id);

            if (participationDeleted == null) throw new TaskCanceledException("La participación no existe");

            return await _participationRepository.Delete(participationDeleted);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    async Task<bool> IParticipationService.UpdateAttendance(AttendanceDTO model)
    {
        try
        {
            var foundParticipation = await _participationRepository.Obtain(p =>
                p.IdEvent == model.IdEvent &&
                p.Dni == model.DNI &&
                p.Email == model.Email);

            foundParticipation.HasAttended = true;
            
            return await _participationRepository.Edit(foundParticipation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
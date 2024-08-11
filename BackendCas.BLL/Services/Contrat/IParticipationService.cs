using BackendCas.DTO;
using BackendCas.MODEL;

namespace BackendCas.BLL.Services.Contrat;

public interface IParticipationService
{
    Task<List<Participation>> List();

    Task<ParticipationDTO> Create(ParticipationDTO model);

    Task<bool> Edit(Participation model);

    Task<bool> Delete(int id);

    Task<Participation> GetById(int id);

    Task<bool> UpdateAttendance(AttendanceDTO model);
}
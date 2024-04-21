using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IParticipantService
{
    Task<List<ParticipantDTO>> List();
    
    Task<ParticipantDTO> Create(ParticipantDTO model);
    
    Task<bool> Edit(ParticipantDTO model);
    
    Task<bool> Delete(int id);
}
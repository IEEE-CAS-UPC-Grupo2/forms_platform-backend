using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IPlatformEventService
{
    Task<List<PlatformEventDTO>> List();

    Task<PlatformEventDTO> GetById(int id);

    Task<PlatformEventDTO> Create(PlatformEventDTO model);

    Task<bool> Edit(PlatformEventDTO model);

    Task<bool> Delete(int id);
}
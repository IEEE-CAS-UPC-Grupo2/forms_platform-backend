using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IPlatformEventService
{
    Task<List<WebEventDTO>> List();

    Task<WebEventDTO> GetById(int id);

    Task<WebEventDTO> Create(WebEventDTO model);

    Task<bool> Edit(WebEventDTO model);

    Task<bool> Delete(int id);
}
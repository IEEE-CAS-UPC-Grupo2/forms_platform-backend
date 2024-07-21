using BackendCas.DTO;

namespace BackendCas.BLL.Services.Contrat;

public interface IWebAdministratorService
{
    Task<List<AdministratorDTO>> List();

    Task<AdministratorDTO> Create(AdministratorDTO model);

    Task<bool> Edit(AdministratorDTO model);

    Task<bool> Delete(int id);
}
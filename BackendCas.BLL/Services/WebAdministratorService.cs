using System.Text;
using AutoMapper;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;
using BackendCas.UTILITY;

namespace BackendCas.BLL.Services;

public class WebAdministratorService : IWebAdministratorService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<WebAdministrator> _adminRepository;

    public WebAdministratorService(IGenericRepository<WebAdministrator> adminRepository, IMapper mapper)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
    }

    async Task<List<AdministratorDTO>> IWebAdministratorService.List()
    {
        var listAdministrators = await _adminRepository.Find();


        return _mapper.Map<List<AdministratorDTO>>(listAdministrators.ToList());
    }

    async Task<AdministratorDTO> IWebAdministratorService.Create(AdministratorDTO model)
    {
        var adminModel = _mapper.Map<WebAdministrator>(model);
        adminModel.Password = PasswordHelper.HashPassword(model.Password);
        adminModel.Email = model.Email;
        adminModel.Name = model.Name;

        var createdAdmin = await _adminRepository.Create(adminModel);
        if (createdAdmin.IdAdministrator == 0)
            throw new TaskCanceledException("No se pudo crear al administrador");

        return _mapper.Map<AdministratorDTO>(createdAdmin);
    }

    async Task<bool> IWebAdministratorService.Edit(AdministratorDTO model)
    {
        var adminModel = _mapper.Map<WebAdministrator>(model);

        var adminFound =
            await _adminRepository.Obtain(u => u.IdAdministrator == adminModel.IdAdministrator);
        if (adminFound == null) throw new TaskCanceledException("El administrador no existe");
        adminFound.Name = adminModel.Name;
        adminFound.Email = adminModel.Email;
        adminFound.Password = adminModel.Password;

        var answer = await _adminRepository.Edit(adminFound);

        if (!answer) throw new TaskCanceledException("The administrator doesn't edit");
        return answer;
    }

    async Task<bool> IWebAdministratorService.Delete(int id)
    {
        var foundAdmin = await _adminRepository.Obtain(p => p.IdAdministrator == id);

        if (foundAdmin == null) throw new TaskCanceledException("El administrador no existe");

        var answer = await _adminRepository.Delete(foundAdmin);
        if (!answer) throw new TaskCanceledException("El administrador no pudo ser eliminado");

        return answer;
    }
}
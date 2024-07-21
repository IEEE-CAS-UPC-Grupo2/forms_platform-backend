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
    private readonly IGenericRepository<WebAdministrator> _PruebaRepository;

    public WebAdministratorService(IGenericRepository<WebAdministrator> pruebaRepository, IMapper mapper)
    {
        _PruebaRepository = pruebaRepository;
        _mapper = mapper;
    }

    async Task<List<AdministratorDTO>> IWebAdministratorService.List()
    {
        var listaCategorias = await _PruebaRepository.Find();


        return _mapper.Map<List<AdministratorDTO>>(listaCategorias.ToList());
    }

    async Task<AdministratorDTO> IWebAdministratorService.Create(AdministratorDTO modelo)
    {
        var adminModel = _mapper.Map<WebAdministrator>(modelo);
        adminModel.Password = PasswordHelper.HashPassword(modelo.Password);
        adminModel.Email = modelo.Email;
        adminModel.Name = modelo.Name;

        var productoCreado = await _PruebaRepository.Create(adminModel);
        if (productoCreado.IdAdministrator == 0)
            throw new TaskCanceledException("The administrator doesn't create");

        return _mapper.Map<AdministratorDTO>(productoCreado);
    }

    async Task<bool> IWebAdministratorService.Edit(AdministratorDTO modelo)
    {
        var productoModelo = _mapper.Map<WebAdministrator>(modelo);

        var productoEncontraro =
            await _PruebaRepository.Obtain(u => u.IdAdministrator == productoModelo.IdAdministrator);
        if (productoEncontraro == null) throw new TaskCanceledException("The administrator doesn't exist");
        productoEncontraro.Name = productoModelo.Name;
        productoEncontraro.Email = productoModelo.Email;
        productoEncontraro.Password = productoModelo.Password;


        var answer = await _PruebaRepository.Edit(productoEncontraro);

        if (!answer) throw new TaskCanceledException("The administrator doesn't edit");
        return answer;
    }


    async Task<bool> IWebAdministratorService.Delete(int id)
    {
        var productoEncontrado = await _PruebaRepository.Obtain(p => p.IdAdministrator == id);

        if (productoEncontrado == null) throw new TaskCanceledException("The administrator doesn't exist");

        var answer = await _PruebaRepository.Delete(productoEncontrado);
        if (!answer) throw new TaskCanceledException("The administrator doesn't delete");

        return answer;
    }

    private byte[] EncryptPassword(string password)
    {
        // Generate a salt and hash the password
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        // Convert the hash to byte array
        return Encoding.UTF8.GetBytes(hash);
    }
}
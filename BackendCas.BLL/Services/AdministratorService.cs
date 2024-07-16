using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BackendCas.BLL.Services.Contrat;
using BackendCas.DAL.Repositories.Contrat;
using BackendCas.DTO;
using BackendCas.MODEL;
using BackendCas.UTILITY;
using BCrypt.Net;

namespace BackendCas.BLL.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IGenericRepository<AdministratorsCa> _PruebaRepository;
    private readonly IMapper _mapper;

    public AdministratorService(IGenericRepository<AdministratorsCa> pruebaRepository, IMapper mapper)
    {
        _PruebaRepository = pruebaRepository;
        _mapper = mapper;
    }

    async Task<List<AdministratorDTO>> IAdministratorService.List()
    {
        try
        {
            var listaCategorias = await _PruebaRepository.Find();


            return _mapper.Map<List<AdministratorDTO>>(listaCategorias.ToList());
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<AdministratorDTO> IAdministratorService.Create(AdministratorDTO modelo)
    {
        try
        {
            var adminModel = _mapper.Map<AdministratorsCa>(modelo);
            adminModel.Password = PasswordHelper.HashPassword(modelo.Password);
            adminModel.Email = modelo.Email;
            adminModel.Name = modelo.Name;

            var productoCreado = await _PruebaRepository.Create(adminModel);
            if (productoCreado.IdAdministrator == 0)
                throw new TaskCanceledException("The administrator doesn't create");

            return _mapper.Map<AdministratorDTO>(productoCreado);
        }
        catch (Exception)
        {
            throw;
        }
    }

    async Task<bool> IAdministratorService.Edit(AdministratorDTO modelo)
    {
        try
        {
            var productoModelo = _mapper.Map<AdministratorsCa>(modelo);

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
        catch (Exception)
        {
            throw;
        }
    }


    async Task<bool> IAdministratorService.Delete(int id)
    {
        try
        {
            var productoEncontrado = await _PruebaRepository.Obtain(p => p.IdAdministrator == id);

            if (productoEncontrado == null) throw new TaskCanceledException("The administrator doesn't exist");

            var answer = await _PruebaRepository.Delete(productoEncontrado);
            if (!answer) throw new TaskCanceledException("The administrator doesn't delete");

            return answer;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private byte[] EncryptPassword(string password)
    {
        // Generate a salt and hash the password
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        // Convert the hash to byte array
        return Encoding.UTF8.GetBytes(hash);
    }
}
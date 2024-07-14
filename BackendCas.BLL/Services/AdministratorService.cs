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

namespace BackendCas.BLL.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IGenericRepository<Administrator> _PruebaRepository;
    private readonly IMapper _mapper;

    public AdministratorService(IGenericRepository<Administrator> pruebaRepository, IMapper mapper)
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
            var productoCreado = await _PruebaRepository.Create(_mapper.Map<Administrator>(modelo));
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
            var productoModelo = _mapper.Map<Administrator>(modelo);

            var productoEncontraro =
                await _PruebaRepository.Obtain(u => u.IdAdministrator == productoModelo.IdAdministrator);
            if (productoEncontraro == null) throw new TaskCanceledException("The administrator doesn't exist");
            productoEncontraro.NameAdministrator = productoModelo.NameAdministrator;
            productoEncontraro.EmailAdministrator = productoModelo.EmailAdministrator;
            productoEncontraro.PasswordAdministrator = productoModelo.PasswordAdministrator;


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
}
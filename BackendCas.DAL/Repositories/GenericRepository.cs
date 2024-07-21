using System.Linq.Expressions;
using BackendCas.DAL.DBContext;
using BackendCas.DAL.Repositories.Contrat;
using Microsoft.EntityFrameworkCore;

namespace BackendCas.DAL.Repositories;

public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
{
    private readonly BackendCasContext _dbContext;

    public GenericRepository(BackendCasContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<TModelo> Obtain(Expression<Func<TModelo, bool>> filtro)
    {
        var modelo = await _dbContext.Set<TModelo>().FirstOrDefaultAsync(filtro);
        return modelo;
    }

    public async Task<TModelo> Create(TModelo modelo)
    {
        _dbContext.Set<TModelo>().Add(modelo);
        await _dbContext.SaveChangesAsync();
        return modelo;
    }

    public async Task<bool> Edit(TModelo modelo)
    {
        _dbContext.Set<TModelo>().Update(modelo);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(TModelo modelo)
    {
        _dbContext.Set<TModelo>().Remove(modelo);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IQueryable<TModelo>> Find(Expression<Func<TModelo, bool>> filtro = null)
    {
        var queryModelo =
            filtro == null ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);
        return queryModelo;
    }
}
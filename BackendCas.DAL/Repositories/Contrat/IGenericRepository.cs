using System.Linq.Expressions;

namespace BackendCas.DAL.Repositories.Contrat;

public interface IGenericRepository<TModel> where TModel : class
{
    Task<TModel> Obtain(Expression<Func<TModel, bool>> filter);

    Task<TModel> Create(TModel model);

    Task<bool> Edit(TModel model);

    Task<bool> Delete(TModel model);

    Task<IQueryable<TModel>> Find(Expression<Func<TModel, bool>> filter = null);
}
using FastTechKitchen.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace FastTechKitchen.Domain.Interfaces.Infrastructure;

public interface IRepository<T> : IDisposable where T : class, IBaseEntity
{
    IEnumerable<T> GetAll();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(Guid id);
    IQueryable<T> FindBy(Expression<Func<T, bool>> expression);

    void DetachAsync(T entity);

}
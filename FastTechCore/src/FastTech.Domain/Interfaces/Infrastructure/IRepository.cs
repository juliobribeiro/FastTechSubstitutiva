using FastTech.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace FastTech.Domain.Interfaces.Infrastructure;

public interface IRepository<T> : IDisposable where T : class, IBaseEntity
{
    IEnumerable<T> GetAll();
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(Guid id);
    IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
}
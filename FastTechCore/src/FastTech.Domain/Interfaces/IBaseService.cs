using FastTech.Domain.Entities.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;

namespace FastTech.Domain.Interfaces;

public interface IBaseService<T> : IRepository<T> where T : class, IBaseEntity
{
    Task Remove(T entity);
}
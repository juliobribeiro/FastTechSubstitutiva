using FastTechKitchen.Domain.Entities.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;

namespace FastTechKitchen.Domain.Interfaces;

public interface IBaseService<T> : IRepository<T> where T : class, IBaseEntity
{
    Task Remove(T entity);
}
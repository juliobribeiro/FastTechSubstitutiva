using FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Domain.Interfaces.Infrastructure;

public interface IItemCardapioRepository : IRepository<ItemCardapio>
{
    Task<ItemCardapio> GetById(Guid id, bool include = false, bool tracking = false);
}
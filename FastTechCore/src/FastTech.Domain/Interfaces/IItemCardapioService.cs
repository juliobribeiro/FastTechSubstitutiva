using FastTech.Domain.Entities;

namespace FastTech.Domain.Interfaces;

public interface IItemCardapioService : IBaseService<ItemCardapio>
{
    Task<ItemCardapio> GetById(Guid id, bool include, bool tracking);
    Task Remove(Guid id);
}
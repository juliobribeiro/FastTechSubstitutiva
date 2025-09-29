using FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Domain.Interfaces;

public interface IPedidoItemCardapioService : IBaseService<PedidoItemCardapio>
{
    Task<PedidoItemCardapio> GetById(Guid id, bool include, bool tracking);
    Task Remove(Guid id);
}
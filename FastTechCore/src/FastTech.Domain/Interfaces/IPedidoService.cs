using FastTech.Domain.Entities;

namespace FastTech.Domain.Interfaces;

public interface IPedidoService : IBaseService<Pedido>
{
    Task<Pedido> GetById(Guid id, bool include, bool tracking);
    Task Remove(Guid id);
}
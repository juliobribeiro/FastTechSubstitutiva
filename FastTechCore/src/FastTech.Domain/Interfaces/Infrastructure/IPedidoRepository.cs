using FastTech.Domain.Entities;

namespace FastTech.Domain.Interfaces.Infrastructure;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<Pedido> GetById(Guid id, bool include = false, bool tracking = false);
}
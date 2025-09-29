using FastTechKitchen.Domain.Entities;
using System.Linq.Expressions;

namespace FastTechKitchen.Domain.Interfaces.Infrastructure;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<Pedido> GetById(Guid id, bool include = false, bool tracking = false);
    Task<IEnumerable<Pedido>> FindBy(Expression<Func<Pedido, bool>> expression, bool tracking = false);
}
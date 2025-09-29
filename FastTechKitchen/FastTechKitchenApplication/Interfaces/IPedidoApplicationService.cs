using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces.RabbitMQ;
using System.Linq.Expressions;
using EN = FastTechKitchen.Domain.Entities;


namespace FastTechKitchen.Application.Interfaces;

public interface IPedidoApplicationService 
{
    Task<Pedido> GetById(Guid id);
    Task<Pedido> Add(BasicPedido model);
    Task<Pedido> Update(Pedido model);
    Task<IEnumerable<Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression);
}
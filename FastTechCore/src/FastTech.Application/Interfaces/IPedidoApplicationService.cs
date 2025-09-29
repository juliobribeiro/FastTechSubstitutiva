using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces.RabbitMQ;

namespace FastTech.Application.Interfaces;

public interface IPedidoApplicationService : IBaseRabbitMQConsumer
{
    Task<Pedido> GetById(Guid id);
    Task<Pedido> Add(BasicPedido model);
    Task<Pedido> Update(Pedido model);
}
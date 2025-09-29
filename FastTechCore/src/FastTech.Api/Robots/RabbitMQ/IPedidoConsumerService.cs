using FastTech.Application.DataTransferObjects.MessageBrokers;

namespace FastTech.Api.Robots.RabbitMQ
{
    public interface IPedidoConsumerService
    {
        Task<List<Pedido>> GetPedidosAsync();
    }
}

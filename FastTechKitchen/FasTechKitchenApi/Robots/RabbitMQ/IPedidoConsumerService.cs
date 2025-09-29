using FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

namespace FasTechKitchen.Api.Robots.RabbitMQ
{
    public interface IPedidoConsumerService
    {
        Task<List<Pedido>> GetPedidosAsync();
    }
}

using FastTechKitchen.Application.Interfaces.RabbitMQ;
using FastTechKitchen.Application.DataTransferObjects;

namespace FastTechKitchen.Application.Interfaces;

public interface IPedidoItemCardapioApplicationService
{
    Task<PedidoItemCardapio> GetById(Guid id);
    Task<PedidoItemCardapio> Add(BasicPedidoItemCardapio model);
    Task<PedidoItemCardapio> Update(PedidoItemCardapio model);
}
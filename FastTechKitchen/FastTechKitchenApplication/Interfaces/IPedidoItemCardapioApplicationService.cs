using FastTechKitchen.Application.Interfaces.RabbitMQ;
//using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Domain.Entities;

namespace FastTechKitchen.Application.Interfaces;

public interface IPedidoItemCardapioApplicationService
{
    Task<PedidoItemCardapio> GetById(Guid id);
    Task<PedidoItemCardapio> Add(PedidoItemCardapio model);
    Task<PedidoItemCardapio> Update(PedidoItemCardapio model);
}
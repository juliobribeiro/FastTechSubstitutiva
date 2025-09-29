using FastTechKitchen.Application.Interfaces.RabbitMQ;
using FastTechKitchen.Application.DataTransferObjects;

namespace FastTechKitchen.Application.Interfaces;

public interface IItemCardapioApplicationService : IBaseRabbitMQConsumer
{
    Task<ItemCardapio> GetById(Guid id);
    Task<ItemCardapio> Add(BasicItemCardapio model);
    Task<ItemCardapio> Update(ItemCardapio model);
    Task<List<ItemCardapio>> GetAll();
}
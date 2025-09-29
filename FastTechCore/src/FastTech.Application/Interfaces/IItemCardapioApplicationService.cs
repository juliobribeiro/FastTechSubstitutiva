using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces.RabbitMQ;

namespace FastTech.Application.Interfaces;

public interface IItemCardapioApplicationService : IBaseRabbitMQConsumer
{
    Task<ItemCardapio> GetById(Guid id);
    Task<ItemCardapio> Add(BasicItemCardapio model);
    Task<ItemCardapio> Update(ItemCardapio model);
    Task<List<ItemCardapio>> GetAll();
}
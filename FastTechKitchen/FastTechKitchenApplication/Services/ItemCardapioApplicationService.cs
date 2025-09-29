using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Domain.Constants;
using FastTechKitchen.Domain.Interfaces;
using System.Text.Json;
using EN = FastTechKitchen.Domain.Entities;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

namespace FastTechKitchen.Application.Services;

public class ItemCardapioApplicationService(IItemCardapioService ItemCardapioService, IMapper mapper) : Interfaces.IItemCardapioApplicationService
{
    private readonly IItemCardapioService _ItemCardapioService = ItemCardapioService;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ItemCardapio>> GetAll()
    {
        var itemCardapios = _ItemCardapioService.GetAll();
        return _mapper.Map<List<ItemCardapio>>(itemCardapios);
    }


    public async Task<ItemCardapio> Add(BasicItemCardapio model)
    {
        var ItemCardapio = _mapper.Map<EN.ItemCardapio>(model);

        ItemCardapio = await _ItemCardapioService.Add(ItemCardapio);

        return _mapper.Map<ItemCardapio>(ItemCardapio);
    }

    public async Task<ItemCardapio> Update(ItemCardapio model)
    {
        var ItemCardapio = await _ItemCardapioService.GetById(model.Id, include: false, tracking: true);
        if (ItemCardapio == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, ItemCardapio);

        ItemCardapio = await _ItemCardapioService.Update(ItemCardapio);

        return _mapper.Map<ItemCardapio>(ItemCardapio);
    }

    public async Task<ItemCardapio> Add(MSG.BasicItemCardapio model)
    {
        var ItemCardapio = _mapper.Map<EN.ItemCardapio>(model);

        ItemCardapio = await _ItemCardapioService.Add(ItemCardapio);

        return _mapper.Map<ItemCardapio>(ItemCardapio);
    }

    public async Task<ItemCardapio> Update(MSG.ItemCardapio model)
    {
        var ItemCardapio = await _ItemCardapioService.GetById(model.Id, include: false, tracking: true);
        if (ItemCardapio == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, ItemCardapio);

        ItemCardapio = await _ItemCardapioService.Update(ItemCardapio);

        return _mapper.Map<ItemCardapio>(ItemCardapio);
    }

    public async Task<ItemCardapio> GetById(Guid id)
    {
        var ItemCardapio = await _ItemCardapioService.GetById(id, include: false, tracking: false);
        return _mapper.Map<ItemCardapio>(ItemCardapio);
    }

    public async Task Consumer(string message, string rountingKey)
    {
        switch (rountingKey)
        {
            case AppConstants.Routes.RabbitMQ.ItemCardapioInsert:
                var ItemCardapioInsert = JsonSerializer.Deserialize<MSG.BasicItemCardapio>(message);
                await Add(ItemCardapioInsert);
                break;

            case AppConstants.Routes.RabbitMQ.ItemCardapioUpdate:
                var ItemCardapioUpdate = JsonSerializer.Deserialize<MSG.ItemCardapio>(message);
                await Update(ItemCardapioUpdate);
                break;
        }
    }

    public void Dispose()
    {
        _ItemCardapioService.Dispose();

        GC.SuppressFinalize(this);
    }
}

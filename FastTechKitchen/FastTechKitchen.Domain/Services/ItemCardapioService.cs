using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FastTechKitchen.Domain.Services;

public class ItemCardapioService(IItemCardapioRepository ItemCardapioRepository, UserData userData) : BaseService<ItemCardapio>(ItemCardapioRepository, userData), IItemCardapioService
{
    private readonly IItemCardapioRepository _ItemCardapioRepository = ItemCardapioRepository;

    public async Task<ItemCardapio> GetById(Guid id, bool include, bool tracking)
    {
        var entity = await _ItemCardapioRepository.GetById(id, include, tracking);

        if (entity == null)
            throw new ValidationException("O ItemCardapio não existe.");

        return entity;
    }

    public override async Task<ItemCardapio> Add(ItemCardapio entity)
    {
        var ItemCardapio = await _ItemCardapioRepository.GetById(entity.Id);

        if (ItemCardapio != null)
            throw new ValidationException("O ItemCardapio já existe.");

        return await base.Add(entity);
    }

    public override async Task<ItemCardapio> Update(ItemCardapio entity)
    {
        return await base.Update(entity);
    }

    public async Task Remove(Guid id)
    {
        var entity = await _ItemCardapioRepository.GetById(id, false, true);
        if (entity == null)
            throw new Exception("O ItemCardapio não existe.");

        await Remove(entity);
    }
}
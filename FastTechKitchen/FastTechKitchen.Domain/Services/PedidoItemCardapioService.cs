using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FastTechKitchen.Domain.Services;

public class PedidoItemCardapioService(IPedidoItemCardapioRepository pedidoItemCardapioRepository, UserData userData) : BaseService<PedidoItemCardapio>(pedidoItemCardapioRepository, userData), IPedidoItemCardapioService
{
    private readonly IPedidoItemCardapioRepository _pedidoItemCardapioRepository = pedidoItemCardapioRepository;

    public async Task<PedidoItemCardapio> GetById(Guid id, bool include, bool tracking)
    {
        var entity = await _pedidoItemCardapioRepository.GetById(id, include, tracking);

        if (entity == null)
            throw new ValidationException("O pedidoItemCardapio não existe.");

        return entity;
    }

    public override async Task<PedidoItemCardapio> Add(PedidoItemCardapio entity)
    {
        var pedidoItemCardapio = await _pedidoItemCardapioRepository.GetById(entity.Id);

        if (pedidoItemCardapio != null)
            throw new ValidationException("O pedidoItemCardapio já existe.");

        return await base.Add(entity);
    }

    public override async Task<PedidoItemCardapio> Update(PedidoItemCardapio entity)
    {
        return await base.Update(entity);
    }

    public async Task Remove(Guid id)
    {
        var entity = await _pedidoItemCardapioRepository.GetById(id, false, true);
        if (entity == null)
            throw new Exception("O pedidoItemCardapio não existe.");

        await Remove(entity);
    }
}
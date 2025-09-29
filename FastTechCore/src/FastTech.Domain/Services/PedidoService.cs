using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FastTech.Domain.Services;

public class PedidoService(IPedidoRepository PedidoRepository, UserData userData) : BaseService<Pedido>(PedidoRepository, userData), IPedidoService
{
    private readonly IPedidoRepository _PedidoRepository = PedidoRepository;

    public async Task<Pedido> GetById(Guid id, bool include, bool tracking)
    {
        var entity = await _PedidoRepository.GetById(id, include, tracking);

        if (entity == null)
            throw new ValidationException("O Pedido não existe.");

        return entity;
    }

    public override async Task<Pedido> Add(Pedido entity)
    {
        var Pedido = await _PedidoRepository.GetById(entity.Id);

        if (Pedido != null)
            throw new ValidationException("O Pedido já existe.");

        return await base.Add(entity);
    }

    public override async Task<Pedido> Update(Pedido entity)
    {
        return await base.Update(entity);
    }

    public async Task Remove(Guid id)
    {
        var entity = await _PedidoRepository.GetById(id, false, true);
        if (entity == null)
            throw new Exception("O Pedido não existe.");

        await base.Remove(entity);
    }
}
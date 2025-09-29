using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace FastTechKitchen.Domain.Services;

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
        var existing = await _PedidoRepository.GetById(entity.Id);

        if (existing != null)
        {
            // Atualiza os campos necessários
            _repository.DetachAsync(existing); // Desanexa do DbContext

            entity.PrepareToUpdate(_userData.Id); // Se você usa campos como UpdatedAt, UpdatedBy
            return await _repository.Update(entity); // OU use _PedidoRepository.Update
        }

        entity.PrepareToInsert(_userData.Id);
        return await _repository.Add(entity);
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

        await Remove(entity);
    }
}
using AutoMapper;
using FastTech.Application.DataTransferObjects;
using FastTech.Domain.Constants;
using FastTech.Domain.Interfaces;
using System.Text.Json;
using EN = FastTech.Domain.Entities;
using MSG = FastTech.Application.DataTransferObjects.MessageBrokers;

namespace FastTech.Application.Services;

public class PedidoApplicationService(IPedidoService PedidoService, IMapper mapper) : Interfaces.IPedidoApplicationService
{
    private readonly IPedidoService _PedidoService = PedidoService;
    private readonly IMapper _mapper = mapper;

    public async Task<Pedido> Add(BasicPedido model)
    {
        var Pedido = _mapper.Map<EN.Pedido>(model);

        Pedido = await _PedidoService.Add(Pedido);

        return _mapper.Map<Pedido>(Pedido);
    }

    public async Task<Pedido> Update(Pedido model)
    {
        var Pedido = await _PedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);

        Pedido = await _PedidoService.Update(Pedido);

        return _mapper.Map<Pedido>(Pedido);
    }

    public async Task<Pedido> Add(MSG.BasicPedido model)
    {
        var Pedido = _mapper.Map<EN.Pedido>(model);

        Pedido = await _PedidoService.Add(Pedido);

        return _mapper.Map<Pedido>(Pedido);
    }

    public async Task<Pedido> Update(MSG.Pedido model)
    {
        var Pedido = await _PedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);

        Pedido = await _PedidoService.Update(Pedido);

        return _mapper.Map<Pedido>(Pedido);
    }

    public async Task<Pedido> GetById(Guid id)
    {
        var Pedido = await _PedidoService.GetById(id, include: false, tracking: false);
        return _mapper.Map<Pedido>(Pedido);
    }

    public async Task Consumer(string message, string rountingKey)
    {
        switch (rountingKey)
        {
            case AppConstants.Routes.RabbitMQ.PedidoInsert:
                var PedidoInsert = JsonSerializer.Deserialize<MSG.BasicPedido>(message);
                await Add(PedidoInsert);
                break;

            case AppConstants.Routes.RabbitMQ.PedidoUpdate:
                var PedidoUpdate = JsonSerializer.Deserialize<MSG.Pedido>(message);
                await Update(PedidoUpdate);
                break;
        }
    }

    public async Task PublishAsync(string message, string rountingKey)
    {
        switch (rountingKey)
        {
            case AppConstants.Routes.RabbitMQ.PedidoInsert:
                var PedidoInsert = JsonSerializer.Deserialize<MSG.BasicPedido>(message);
                await Add(PedidoInsert);
                break;

            case AppConstants.Routes.RabbitMQ.PedidoUpdate:
                var PedidoUpdate = JsonSerializer.Deserialize<MSG.Pedido>(message);
                await Update(PedidoUpdate);
                break;
        }
    }

    public void Dispose()
    {
        _PedidoService.Dispose();

        GC.SuppressFinalize(this);
    }
}

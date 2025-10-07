using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Domain.Constants;
using FastTechKitchen.Domain.Interfaces;
using System.Text.Json;
using EN = FastTechKitchen.Domain.Entities;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

namespace FastTechKitchen.Application.Services;

public class PedidoItemCardapioApplicationService(IPedidoItemCardapioService pedidoItemCardapioService, IMapper mapper) : Interfaces.IPedidoItemCardapioApplicationService
{
    private readonly IPedidoItemCardapioService _PedidoItemCardapioService = pedidoItemCardapioService;
    private readonly IMapper _mapper = mapper;

    public async Task<PedidoItemCardapio> Add(BasicPedidoItemCardapio model)
    {
        var PedidoItemCardapio = _mapper.Map<Domain.Entities.PedidoItemCardapio>(model);

        PedidoItemCardapio = await _PedidoItemCardapioService.Add(PedidoItemCardapio);

        return _mapper.Map<PedidoItemCardapio>(PedidoItemCardapio);
    }

    public async Task<EN.PedidoItemCardapio> Update(PedidoItemCardapio model)
    {
        var PedidoItemCardapio = await _PedidoItemCardapioService.GetById(model.Id, include: false, tracking: true);
        if (PedidoItemCardapio == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, PedidoItemCardapio);

        PedidoItemCardapio = await _PedidoItemCardapioService.Update(PedidoItemCardapio);

        return _mapper.Map<EN.PedidoItemCardapio>(PedidoItemCardapio);
    }

    public async Task<EN.PedidoItemCardapio> Add(EN.PedidoItemCardapio model)
    {
        var PedidoItemCardapio = _mapper.Map<Domain.Entities.PedidoItemCardapio>(model);

        PedidoItemCardapio = await _PedidoItemCardapioService.Add(PedidoItemCardapio);

        return _mapper.Map<EN.PedidoItemCardapio>(PedidoItemCardapio);
    }

    public async Task<EN.PedidoItemCardapio> Update(EN.PedidoItemCardapio model)
    {
        var PedidoItemCardapio = await _PedidoItemCardapioService.GetById(model.Id, include: false, tracking: true);
        if (PedidoItemCardapio == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, PedidoItemCardapio);

        PedidoItemCardapio = await _PedidoItemCardapioService.Update(PedidoItemCardapio);

        return _mapper.Map<EN.PedidoItemCardapio>(PedidoItemCardapio);
    }

    public async Task<EN.PedidoItemCardapio> GetById(Guid id)
    {
        var PedidoItemCardapio = await _PedidoItemCardapioService.GetById(id, include: false, tracking: false);
        return _mapper.Map<EN.PedidoItemCardapio>(PedidoItemCardapio);
    }



    public void Dispose()
    {
        _PedidoItemCardapioService.Dispose();

        GC.SuppressFinalize(this);
    }

}

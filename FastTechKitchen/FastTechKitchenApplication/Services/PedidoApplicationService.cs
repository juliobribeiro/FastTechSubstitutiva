using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using FastTechKitchen.Domain.Constants;
using FastTechKitchen.Domain.Interfaces;
using System.Linq.Expressions;
using System.Text.Json;
using EN = FastTechKitchen.Domain.Entities;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

namespace FastTechKitchen.Application.Services;

public class PedidoApplicationService : IPedidoApplicationService
{
    private readonly IPedidoService _PedidoService;
    private readonly IPedidoItemCardapioApplicationService _pedidoItemCardapioAppService;
    private readonly IMapper _mapper;

    public PedidoApplicationService(
        IPedidoService pedidoService,
        IPedidoItemCardapioApplicationService pedidoItemCardapioAppService,
        IMapper mapper)
    {
        _PedidoService = pedidoService;
        _pedidoItemCardapioAppService = pedidoItemCardapioAppService;
        _mapper = mapper;
    }

    public async Task<Pedido> Add(BasicPedido model)
    {
        // 1. Mapeia apenas os campos do Pedido
        var pedidoEntity = _mapper.Map<EN.Pedido>(model);

        // 2. Salva; aqui o EF gera o Id
        pedidoEntity = await _PedidoService.Add(pedidoEntity);

        // 3. Agora cria cada item com Id novo e o Id do pedido recém‑gerado
        foreach (var item in model.Itens)
        {
            item.PedidoId = pedidoEntity.Id;
            await _pedidoItemCardapioAppService.Add(item);
        }

        return _mapper.Map<Pedido>(pedidoEntity);
    }


    // Os outros métodos permanecem iguais
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

    public async Task<IEnumerable<Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression)
    {
        var pedidos = _PedidoService.FindBy(expression);
        return _mapper.Map<IEnumerable<Pedido>>(pedidos);
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

    public void Dispose()
    {
        _PedidoService.Dispose();
        GC.SuppressFinalize(this);
    }

}

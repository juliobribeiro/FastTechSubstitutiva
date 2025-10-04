using AutoMapper;
using FastTechKitchen.Application.Interfaces;
using FastTechKitchen.Domain.Interfaces;
using System.Linq.Expressions;
using CTT = FastTech.Contracts.DataTransferObjects;
using DTO = FastTechKitchen.Application.DataTransferObjects;
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

    // O método principal Add agora usa o tipo CTT.BasicPedido para evitar ambiguidade.
    public async Task<DTO.Pedido> Add(CTT.PedidoMessage model)
    {
        
        var itensDoPedido = model.Pedido;

        DTO.Pedido ultimoPedidoSalvo = null;

        foreach (var itemContrato in itensDoPedido)
        {

            var pedidoEntity = _mapper.Map<EN.Pedido>(itemContrato);
            pedidoEntity = await _PedidoService.Add(pedidoEntity);
            var itemDto = _mapper.Map<DTO.BasicPedidoItemCardapio>(itemContrato);
            itemDto.PedidoId = pedidoEntity.Id;

            await _pedidoItemCardapioAppService.Add(itemDto);

            ultimoPedidoSalvo = _mapper.Map<DTO.Pedido>(pedidoEntity);
        }

        if (ultimoPedidoSalvo == null)
        {
            throw new InvalidOperationException("Nenhum item de pedido foi processado.");
        }

        return ultimoPedidoSalvo;
    }

    // Os outros métodos permanecem iguais
    public async Task<DTO.Pedido> Update(DTO.Pedido model)
    {
        var Pedido = await _PedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);
        Pedido = await _PedidoService.Update(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    // Mantenha este se ainda for usado internamente
    public async Task<DTO.Pedido> Add(MSG.BasicPedido model)
    {
        var Pedido = _mapper.Map<EN.Pedido>(model);
        Pedido = await _PedidoService.Add(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public async Task<IEnumerable<DTO.Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression)
    {
        var pedidos = _PedidoService.FindBy(expression);
        return _mapper.Map<IEnumerable<DTO.Pedido>>(pedidos);
    }

    public async Task<DTO.Pedido> Update(MSG.Pedido model)
    {
        var Pedido = await _PedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);
        Pedido = await _PedidoService.Update(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public async Task<DTO.Pedido> GetById(Guid id)
    {
        var Pedido = await _PedidoService.GetById(id, include: false, tracking: false);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public void Dispose()
    {
        _PedidoService.Dispose();
        GC.SuppressFinalize(this);
    }

}
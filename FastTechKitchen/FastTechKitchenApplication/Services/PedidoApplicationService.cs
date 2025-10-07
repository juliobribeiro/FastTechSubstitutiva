using AutoMapper;
using FastTechKitchen.Application.DataTransferObjects;
using FastTechKitchen.Application.Interfaces;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Infraestructure.Data;
using System.Linq.Expressions;
using CTT = FastTech.Contracts.DataTransferObjects;
using DTO = FastTechKitchen.Application.DataTransferObjects;
using EN = FastTechKitchen.Domain.Entities;
using MSG = FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

namespace FastTechKitchen.Application.Services;

public class PedidoApplicationService : IPedidoApplicationService
{
    private readonly IPedidoService _pedidoService;
    private readonly IPedidoItemCardapioApplicationService _pedidoItemCardapioAppService;
    private readonly IMapper _mapper;
    private readonly ApplicationDBContext _dbContext;

    public PedidoApplicationService(
        IPedidoService pedidoService,
        IPedidoItemCardapioApplicationService pedidoItemCardapioAppService,
        IMapper mapper, 
        ApplicationDBContext dbContext)
    {
        _pedidoService = pedidoService;
        _pedidoItemCardapioAppService = pedidoItemCardapioAppService;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    // O método principal Add agora usa o tipo CTT.BasicPedido para evitar ambiguidade.
    public async Task<DTO.Pedido> Add(CTT.PedidoMessage model)
    {

        var itensDoPedido = model.Pedido;

        DTO.Pedido ultimoPedidoSalvo = null;

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        //foreach (var itemContrato in itensDoPedido)
        //{

        //    var pedidoEntity = _mapper.Map<EN.Pedido>(itemContrato);
        //    pedidoEntity = await _PedidoService.Add(pedidoEntity);
        //    var itemDto = _mapper.Map<DTO.BasicPedidoItemCardapio>(itemContrato);
        //    itemDto.PedidoId = pedidoEntity.Id;

        //    await _pedidoItemCardapioAppService.Add(itemDto);

        //    ultimoPedidoSalvo = _mapper.Map<DTO.Pedido>(pedidoEntity);
        //}

        //if (ultimoPedidoSalvo == null)
        //{
        //    throw new InvalidOperationException("Nenhum item de pedido foi processado.");
        //}

        //return ultimoPedidoSalvo;

        try
        {
            // Itera sobre a lista de BasicPedido (Contrato)
            foreach (var basicPedidoContrato in itensDoPedido)
            {
                // 1. Mapeia o BasicPedido (Contrato) para a Entidade Pedido.
                var pedidoEntity = _mapper.Map<EN.Pedido>(basicPedidoContrato);

                // 2. Salva o Pedido na base de dados.
                var newPedido = await _pedidoService.Add(pedidoEntity);

                // 3. Itera sobre os Itens do Pedido.
                // O BasicPedido (Contrato) DEVE ter a propriedade Items (BasicPedidoItemCardapio)
                if (basicPedidoContrato.Items != null)
                {
                    foreach (var itemContrato in basicPedidoContrato.Items)
                    {
                        // 3a. Mapeia o Item Contrato para a Entidade PedidoItemCardapio.
                        var pedidoItemEntity = _mapper.Map<EN.PedidoItemCardapio>(itemContrato);
                        pedidoItemEntity.PedidoId = newPedido.Id; // Associa ao Pedido recém-criado

                        // 3b. Salva o ItemCardapio.
                        // Nota: O método Add deve receber a ENTIDADE, não um DTO.
                        await _pedidoItemCardapioAppService.Add(pedidoItemEntity);
                    }
                }

                // 4. Mapeia a Entidade Pedido salva de volta para o DTO (PedidoDto) para retorno.
                ultimoPedidoSalvo = _mapper.Map<DTO.Pedido>(newPedido);
            }

            // Confirma todas as operações do lote.
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            // Reverte todas as operações em caso de falha.
            await transaction.RollbackAsync();
            throw;
        }

        return ultimoPedidoSalvo;
    }

    

    // Os outros métodos permanecem iguais
    public async Task<DTO.Pedido> Update(DTO.Pedido model)
    {
        var Pedido = await _pedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);
        Pedido = await _pedidoService.Update(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    // Mantenha este se ainda for usado internamente
    public async Task<DTO.Pedido> Add(MSG.BasicPedido model)
    {
        var Pedido = _mapper.Map<EN.Pedido>(model);
        Pedido = await _pedidoService.Add(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public async Task<IEnumerable<DTO.Pedido>> FindBy(Expression<Func<EN.Pedido, bool>> expression)
    {
        var pedidos = _pedidoService.FindBy(expression);
        return _mapper.Map<IEnumerable<DTO.Pedido>>(pedidos);
    }

    public async Task<DTO.Pedido> Update(MSG.Pedido model)
    {
        var Pedido = await _pedidoService.GetById(model.Id, include: false, tracking: true);
        if (Pedido == null)
            throw new Exception("O Item do Cardapio não existe.");

        _mapper.Map(model, Pedido);
        Pedido = await _pedidoService.Update(Pedido);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public async Task<DTO.Pedido> GetById(Guid id)
    {
        var Pedido = await _pedidoService.GetById(id, include: false, tracking: false);
        return _mapper.Map<DTO.Pedido>(Pedido);
    }

    public void Dispose()
    {
        _pedidoService.Dispose();
        GC.SuppressFinalize(this);
    }

}
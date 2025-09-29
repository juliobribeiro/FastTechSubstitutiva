namespace FastTech.Application.DataTransferObjects.MessageBrokers;

public record BasicPedido(
    Guid ItemPedidoId,
    int FormaDeEntrega,
    bool Ativo
);
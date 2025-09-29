namespace FastTech.Application.DataTransferObjects.MessageBrokers;

public record Pedido(
    Guid Id,
    DateTime CreatedAt,
    Guid CreatedBy,
    DateTime? UpdatedAt,
    Guid? UpdatedBy,
    bool Removed,
    DateTime? RemovedAt,
    Guid? RemovedBy,
    Guid ItemPedidoId,
    int FormaDeEntrega,
    bool Ativo
);

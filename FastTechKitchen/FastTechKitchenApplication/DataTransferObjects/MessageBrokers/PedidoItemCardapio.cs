namespace FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

public record PedidoItemCardapio(
    Guid Id,
    DateTime CreatedAt,
    Guid CreatedBy,
    DateTime? UpdatedAt,
    Guid? UpdatedBy,
    bool Removed,
    DateTime? RemovedAt,
    Guid? RemovedBy,
    Guid ItemPedidoId,
    Guid PedidoId
);

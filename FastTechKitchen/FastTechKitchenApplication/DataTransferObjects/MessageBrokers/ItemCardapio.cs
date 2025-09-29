namespace FastTechKitchen.Application.DataTransferObjects.MessageBrokers;

public record ItemCardapio(
    Guid Id,
    DateTime CreatedAt,
    Guid CreatedBy,
    DateTime? UpdatedAt,
    Guid? UpdatedBy,
    bool Removed,
    DateTime? RemovedAt,
    Guid? RemovedBy,
    string Name,
    int DDD,
    string PhoneNumber,
    string Email
);

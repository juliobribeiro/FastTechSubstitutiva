using FastTechKitchen.Domain.Entities.Interfaces;

namespace FastTechKitchen.Domain.Entities;

public abstract class Identifier : IIdentifier
{
    public Guid Id { get; set; }
}
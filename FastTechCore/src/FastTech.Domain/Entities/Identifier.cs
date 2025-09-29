using FastTech.Domain.Entities.Interfaces;

namespace FastTech.Domain.Entities;

public abstract class Identifier : IIdentifier
{
    public Guid Id { get; set; }
}
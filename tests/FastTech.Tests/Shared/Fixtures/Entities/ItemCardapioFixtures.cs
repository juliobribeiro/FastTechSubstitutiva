using Bogus;
using FastTech.Tests.Shared.Fixtures.Utils;
using DTO = FastTech.Application.DataTransferObjects;
using EN = FastTech.Domain.Entities;

namespace FastTech.Tests.Shared.Fixtures.Entities;

public sealed class ItemCardapioFixtures : BaseFixtures<DTO.ItemCardapio>
{
    public static EN.ItemCardapio GenerateItemCardapio()
    {
        var now = DateTime.UtcNow;
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var faker = new Faker<EN.ItemCardapio>("pt_BR")
            .RuleFor(i => i.Id, _ => id)
            .RuleFor(i => i.Nome, f => f.Commerce.ProductName())
            .RuleFor(i => i.Descricao, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Preco, f => f.Random.Double(1, 1000))
            .RuleFor(i => i.Disponivel, f => f.Random.Bool())
            .RuleFor(i => i.CreatedAt, _ => now)
            .RuleFor(i => i.CreatedBy, _ => userId)
            .RuleFor(i => i.UpdatedAt, _ => now)
            .RuleFor(i => i.UpdatedBy, _ => userId)
            .RuleFor(i => i.Removed, _ => false)
            .RuleFor(i => i.RemovedAt, _ => null)
            .RuleFor(i => i.RemovedBy, _ => null);

        return faker.Generate();
    }

    public static EN.ItemCardapio CreateAs_Base()
    {
        var item = GenerateItemCardapio();
        return item;
    }

    public static DTO.BasicItemCardapio CreateAs_BaseDTO()
    {
        var faker = new Faker<DTO.BasicItemCardapio>("pt_BR")
            .RuleFor(i => i.Nome, f => f.Commerce.ProductName())
            .RuleFor(i => i.Descricao, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Preco, f => f.Random.Double(1, 1000))
            .RuleFor(i => i.Disponivel, f => f.Random.Bool());

        return faker.Generate();
    }

    public static DTO.ItemCardapio CreateAs_DTO()
    {
        var faker = new Faker<DTO.ItemCardapio>("pt_BR")
            .RuleFor(i => i.Nome, f => f.Commerce.ProductName())
            .RuleFor(i => i.Descricao, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Preco, f => f.Random.Double(1, 1000))
            .RuleFor(i => i.Disponivel, f => f.Random.Bool());

        var contact = faker.Generate();

        return contact;
    }
}

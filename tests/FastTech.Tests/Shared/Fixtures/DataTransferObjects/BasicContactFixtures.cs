using Bogus;
using FastTech.Application.DataTransferObjects;
using FastTech.Tests.Shared.Fixtures.Utils;

namespace FastTech.Tests.Shared.Fixtures.DataTransferObjects;

public sealed class BasicItemCardapioFixtures : BaseFixtures<BasicItemCardapio>
{

    private static readonly int[] ValidBrazilianDDDs = new[]
    {
        11, 12, 13, 14, 15, 16, 17, 18, 19,
        21, 22, 24,
        27, 28,
        31, 32, 33, 34, 35, 37, 38,
        41, 42, 43, 44, 45, 46,
        47, 48, 49,
        51, 53, 54, 55,
        61,
        62, 63, 64,
        65, 66,
        67,
        68,
        69,
        71, 73, 74, 75, 77,
        79,
        81, 82, 83, 84, 85, 86, 87, 88, 89,
        91, 92, 93, 94, 95, 96, 97, 98, 99
    };

    public BasicItemCardapioFixtures() : base() { }

    public static BasicItemCardapio GenerateUser()
    {
        var faker = Faker
            .RuleFor(u => u.Nome, f => f.Person.FullName)
            .RuleFor(u => u.Descricao, f => f.Lorem.Random.String())
            .RuleFor(u => u.Preco, f => f.Finance.Random.Double())
            .RuleFor(u => u.Disponivel, f => f.System.Random.Bool());

        return faker.Generate();
    }

    public static BasicItemCardapio CreateAs_Base()
    {
        var ItemCardapio = GenerateUser();

        return ItemCardapio;
    }

    public static BasicItemCardapio CreateAs_InvalidName()
    {
        var ItemCardapio = CreateAs_Base();
        ItemCardapio.Nome = string.Empty;

        return ItemCardapio;
    }
}
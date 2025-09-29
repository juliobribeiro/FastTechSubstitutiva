using Bogus;
using FastTech.Domain.Entities;
using FastTech.Domain.Enums;
using FastTech.Tests.Shared.Fixtures.Utils;

namespace FastTech.Tests.Shared.Fixtures.Entities;

public sealed class UserFixtures : BaseFixtures<User>
{
    public static User GenerateUser()
    {
        var faker = new Faker<User>("pt_BR")
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Internet.Password(9))
            .RuleFor(u => u.PermissionLevel, f => f.PickRandom<PermissionLevel>())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("#########"))
            .RuleFor(u => u.DDD, f => f.Random.Int(10, 99));

        return faker.Generate();
    }

    public static User CreateAs_Base()
    {
        var user = GenerateUser();
        user.PrepareToInsert(Guid.NewGuid());

        return user;
    }
}
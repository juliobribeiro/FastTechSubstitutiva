using Bogus;
using FastTech.Application.DataTransferObjects;
using FastTech.Domain.Enums;
using FastTech.Tests.Shared.Fixtures.Utils;

namespace FastTech.Tests.Shared.Fixtures.DataTransferObjects;

public sealed class BasicUserFixtures : BaseFixtures<BasicUser>
{
    public BasicUserFixtures() : base() { }

    public static BasicUser GenerateUser()
    {
        var faker = Faker
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Internet.Password(9) + "#" + 1)
            .RuleFor(u => u.PermissionLevel, f => f.PickRandom<PermissionLevel>())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("#########"))
            .RuleFor(u => u.DDD, f => f.Random.Int(10, 99));

        return faker.Generate();
    }

    public static BasicUser CreateAs_Base()
    {
        var user = GenerateUser();

        return user;
    }

    public static BasicUser CreateAs_InvalidName()
    {
        var user = CreateAs_Base();
        user.Name = string.Empty;

        return user;
    }

    public static BasicUser CreateAs_InvalidEmail()
    {
        var user = CreateAs_Base();
        user.Email = FakerDefault.Random.String2(2, 2);

        return user;
    }

    public static BasicUser CreateAs_InvalidPassword()
    {
        var user = CreateAs_Base();
        user.Password = FakerDefault.Random.String2(2, 2);

        return user;
    }
}
using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;
using FastTech.Domain.Services;
using FastTech.Infrastructure.Data.Repositories;
using FastTech.Tests.Shared.Fixtures.Entities;
using FastTech.Tests.Shared.Fixtures.Utils;

namespace FastTech.Tests.Domain.Services;

public class UserServiceTests : BaseServiceTests
{
    private readonly IUserRepository _repository;
    private readonly IUserService _userService;
    private readonly UserData _userData;

    public UserServiceTests()
    {
        _userData = UserDataFixtures.CreateAs_Base();
        _repository = new UserRepository(_context);
        _userService = new UserService(_repository, _userData);
    }

    public class Insert : UserServiceTests
    {
        [Fact]
        public async Task ShouldInsertUser()
        {
            // Arrange
            var user = UserFixtures.CreateAs_Base();

            // Act
            var result = await _userService.Add(user);
            await SaveChanges();

            // Assert
            Assert.NotNull(result);
        }
    }

    public override void Dispose()
    {
        _context?.Dispose();
        _repository?.Dispose();

        GC.SuppressFinalize(this);
    }
}
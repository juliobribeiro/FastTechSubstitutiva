using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;
using FastTech.Domain.Services;
using FastTech.Tests.Shared.Fixtures.Entities;
using Moq;
using EN = FastTech.Domain.Entities;

public class ItemCardapioServiceUnitTests
{
    private readonly Mock<IItemCardapioRepository> _ItemCardapioRepositoryMock;
    private readonly Mock<EN.UserData> _userDataMock;
    private readonly IItemCardapioService _ItemCardapioService;

    public ItemCardapioServiceUnitTests()
    {
        _ItemCardapioRepositoryMock = new Mock<IItemCardapioRepository>();
        _userDataMock = new Mock<EN.UserData>();

        _ItemCardapioService = new ItemCardapioService(
            _ItemCardapioRepositoryMock.Object,
            _userDataMock.Object
        );
    }

    [Fact]
    public async Task Create_ValidItemCardapio_ShouldCallRepositoryOnce()
    {
        // Arrange
        var ItemCardapio = ItemCardapioFixtures.CreateAs_Base();

        // Mock do repositório de contatos, configurando para adicionar o contato
        _ItemCardapioRepositoryMock.Setup(repo => repo.Add(It.IsAny<ItemCardapio>()))
                              .ReturnsAsync(ItemCardapio);

        // Act
        var result = await _ItemCardapioService.Add(ItemCardapio);

        // Assert
        _ItemCardapioRepositoryMock.Verify(repo => repo.Add(It.IsAny<ItemCardapio>()), Times.Once); // Verifica se o método Add foi chamado uma vez
        Assert.NotNull(result);
        Assert.Equal(ItemCardapio.Nome, result.Nome);
    }

    [Fact]
    public async Task Update_ExistingItemCardapio_ShouldCallRepositoryOnce()
    {
        // Arrange
        var ItemCardapio = ItemCardapioFixtures.CreateAs_Base();
        ItemCardapio.Nome = "Updated Name";

        _ItemCardapioRepositoryMock.Setup(repo => repo.Update(It.IsAny<ItemCardapio>()))
                              .ReturnsAsync(ItemCardapio);

        // Act
        var result = await _ItemCardapioService.Update(ItemCardapio);

        // Assert
        _ItemCardapioRepositoryMock.Verify(repo => repo.Update(It.IsAny<ItemCardapio>()), Times.Once);
        Assert.Equal("Updated Name", result.Nome);
    }

    [Fact]
    public async Task Delete_ValidId_ShouldCallRepositoryOnce()
    {
        // Arrange
        var ItemCardapioId = Guid.NewGuid();
        _ItemCardapioRepositoryMock.Setup(repo => repo.Delete(ItemCardapioId))
                              .Returns(Task.CompletedTask);

        // Act
        await _ItemCardapioService.Delete(ItemCardapioId);

        // Assert
        _ItemCardapioRepositoryMock.Verify(repo => repo.Delete(ItemCardapioId), Times.Once);
    }
}

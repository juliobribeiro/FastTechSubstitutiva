using FastTech.Application.DataTransferObjects;
using FastTech.Tests.Shared.Fixtures.DataTransferObjects;

namespace FastTech.Tests.Domain.Validations;

public class BasicItemCardapioValidationTests : BaseValidationTest
{
    [Fact]
    public void BasicItemCardapio_AllAttributesValid_ShouldHaveRequiredAttribute_ResultValid()
    {
        // Arrange
        var basicItemCardapio = BasicItemCardapioFixtures.CreateAs_Base();

        // Act
        var validationResults = ValidateModel(basicItemCardapio);

        // Assert
        Assert.Empty(validationResults);
    }

    /// <summary>
    /// Data for testing invalid BasicItemCardapio
    /// </summary>
    public static IEnumerable<object[]> GetBasicItemCardapioInvalidData()
    {
        yield return new object[] { BasicItemCardapioFixtures.CreateAs_InvalidName() };
    }

    [Theory]
    [MemberData(nameof(GetBasicItemCardapioInvalidData))]
    public void BasicItemCardapio_ShouldHaveRequiredAttribute_AllResultsInvalid(BasicItemCardapio basicItemCardapio)
    {
        // Act
        var validationResults = ValidateModel(basicItemCardapio);

        // Assert
        Assert.NotEmpty(validationResults);
    }
}
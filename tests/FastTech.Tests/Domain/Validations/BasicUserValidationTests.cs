using FastTech.Application.DataTransferObjects;
using FastTech.Tests.Shared.Fixtures.DataTransferObjects;

namespace FastTech.Tests.Domain.Validations;

public class BasicUserValidationTestsTest : BaseValidationTest
{
    [Fact]
    public void BasicUser_AllAttributesValid_ShouldHaveRequiredAttribute_ResultValid()
    {
        // Arrange
        var basicUser = BasicUserFixtures.CreateAs_Base();

        // Act
        var validationResults = ValidateModel(basicUser);

        // Assert
        Assert.Empty(validationResults);
    }

    /// <summary>
    /// Data for testing invalid BasicUser
    /// </summary>
    public static IEnumerable<object[]> GetBasicUserInvalidData()
    {
        yield return new object[] { BasicUserFixtures.CreateAs_InvalidName() };
        yield return new object[] { BasicUserFixtures.CreateAs_InvalidEmail() };
        yield return new object[] { BasicUserFixtures.CreateAs_InvalidPassword() };
    }

    [Theory]
    [MemberData(nameof(GetBasicUserInvalidData))]
    public void BasicUser_ShouldHaveRequiredAttribute_AllResultsInvalid(BasicUser basicUser)
    {
        // Act
        var validationResults = ValidateModel(basicUser);

        // Assert
        Assert.NotEmpty(validationResults);
    }
}
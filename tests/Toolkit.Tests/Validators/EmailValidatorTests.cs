using FluentAssertions;
using GSalvi.Toolkit.Validators;

namespace GSalvi.Toolkit.Tests.Validators;

public class EmailValidatorTests
{
    [Fact]
    public void IsEmailAddress_ShouldReturnTrue_ForValidSimpleEmail()
    {
        // Arrange
        const string email = "test@example.com";

        // Act
        var result = EmailValidator.IsEmailAddress(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsEmailAddress_ShouldReturnTrue_ForValidEmailWithSubdomainAndPlusTag()
    {
        // Arrange
        const string email = "user.name+tag+sorting@example.co.uk";

        // Act
        var result = EmailValidator.IsEmailAddress(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsEmailAddress_ShouldReturnFalse_ForEmailMissingAtSymbol()
    {
        // Arrange
        const string email = "plainaddress";

        // Act
        var result = EmailValidator.IsEmailAddress(email);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsEmailAddress_ShouldReturnFalse_ForEmailWithMissingDomain()
    {
        // Arrange
        const string email = "user@.com";

        // Act
        var result = EmailValidator.IsEmailAddress(email);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsEmailAddress_ShouldReturnFalse_ForEmptyEmailString()
    {
        // Arrange
        var email = string.Empty;

        // Act
        var result = EmailValidator.IsEmailAddress(email);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsEmailAddress_ShouldThrowArgumentNullException_ForNullInput()
    {
        // Arrange
        string? email = null;

        // Act
        Action act = () => EmailValidator.IsEmailAddress(email!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}

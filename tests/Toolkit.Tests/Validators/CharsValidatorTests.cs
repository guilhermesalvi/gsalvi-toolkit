using FluentAssertions;
using Sozo.Toolkit.Validators;

namespace Sozo.Toolkit.Tests.Validators;

public class CharsValidatorTests
{
    #region HasOnlyAllowedChars Tests

    [Fact]
    public void HasOnlyAllowedChars_ReturnsTrue_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;
        char[] allowedChars = ['a', 'b', 'c'];

        // Act
        var result = CharsValidator.HasOnlyAllowedChars(input, allowedChars);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyAllowedChars_ReturnsTrue_WhenAllCharactersAreAllowed()
    {
        // Arrange
        const string input = "abcabc";
        char[] allowedChars = ['a', 'b', 'c'];

        // Act
        var result = CharsValidator.HasOnlyAllowedChars(input, allowedChars);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyAllowedChars_ReturnsFalse_WhenAnyCharacterIsNotAllowed()
    {
        // Arrange
        const string input = "abcd";
        char[] allowedChars = ['a', 'b', 'c'];

        // Act
        var result = CharsValidator.HasOnlyAllowedChars(input, allowedChars);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasOnlyAllowedChars_ReturnsFalse_WhenAllowedSetIsEmptyAndInputIsNotEmpty()
    {
        // Arrange
        const string input = "a";
        var allowedChars = Array.Empty<char>();

        // Act
        var result = CharsValidator.HasOnlyAllowedChars(input, allowedChars);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasOnlyAllowedChars_IsCaseSensitive()
    {
        // Arrange
        const string input = "a";
        char[] allowedChars = { 'A' };

        // Act
        var result = CharsValidator.HasOnlyAllowedChars(input, allowedChars);

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region HasOnlyDigits Tests

    [Fact]
    public void HasOnlyDigits_ReturnsTrue_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;

        // Act
        var result = CharsValidator.HasOnlyDigits(input);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyDigits_ReturnsTrue_WhenAllCharactersAreDigits()
    {
        // Arrange
        const string input = "1234567890";

        // Act
        var result = CharsValidator.HasOnlyDigits(input);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyDigits_ReturnsFalse_WhenInputContainsLetters()
    {
        // Arrange
        const string input = "1234a56789";

        // Act
        var result = CharsValidator.HasOnlyDigits(input);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasOnlyDigits_ReturnsFalse_WhenInputContainsWhitespace()
    {
        // Arrange
        const string input = "123 456";

        // Act
        var result = CharsValidator.HasOnlyDigits(input);

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region HasOnlyLetters Tests

    [Fact]
    public void HasOnlyLetters_ReturnsTrue_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;

        // Act
        var result = CharsValidator.HasOnlyLetters(input);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyLetters_ReturnsTrue_WhenAllCharactersAreLetters()
    {
        // Arrange
        const string input = "HelloWorld";

        // Act
        var result = CharsValidator.HasOnlyLetters(input);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasOnlyLetters_ReturnsFalse_WhenInputContainsDigits()
    {
        // Arrange
        const string input = "Hello123";

        // Act
        var result = CharsValidator.HasOnlyLetters(input);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void HasOnlyLetters_ReturnsFalse_WhenInputContainsSymbols()
    {
        // Arrange
        const string input = "Hello!";

        // Act
        var result = CharsValidator.HasOnlyLetters(input);

        // Assert
        result.Should().BeFalse();
    }

    #endregion
}

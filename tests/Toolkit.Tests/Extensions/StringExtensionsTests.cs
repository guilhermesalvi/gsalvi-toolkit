using FluentAssertions;
using Sozo.Toolkit.Extensions;

namespace Sozo.Toolkit.Tests.Extensions;

public class StringExtensionsTests
{
    #region RemoveDiacritics Tests

    [Fact]
    public void RemoveDiacritics_ReturnsEmptyString_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input.RemoveDiacritics();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveDiacritics_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;

        // Act
        var result = input.RemoveDiacritics();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveDiacritics_RemovesAccents_FromAccentedText()
    {
        // Arrange
        const string input = "Às ínclitas maçãs, o coração exibia êxtase, paixão e júbilo; já a fênix, à luz do pôr-do-sol, encantava o céu.";
        const string expected = "As inclitas macas, o coracao exibia extase, paixao e jubilo; ja a fenix, a luz do por-do-sol, encantava o ceu.";

        // Act
        var result = input.RemoveDiacritics();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveDiacritics_ReturnsSameString_WhenThereAreNoDiacritics()
    {
        // Arrange
        const string input = "Hello World!";
        const string expected = "Hello World!";

        // Act
        var result = input.RemoveDiacritics();

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region RemoveSpecificChars Tests

    [Fact]
    public void RemoveSpecificChars_ReturnsEmptyString_WhenInputIsNull()
    {
        // Arrange
        string? input = null;
        char[] charsToRemove = ['a', 'e', 'i', 'o', 'u'];

        // Act
        var result = input.RemoveSpecificChars(charsToRemove);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveSpecificChars_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;
        char[] charsToRemove = ['x', 'y'];

        // Act
        var result = input.RemoveSpecificChars(charsToRemove);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveSpecificChars_RemovesOnlySpecifiedCharacters()
    {
        // Arrange
        const string input = "Às ínclitas maçãs";
        char[] charsToRemove = ['a', 'e', 'i', 'o', 'u'];
        const string expected = "s nclts mçs";

        // Act
        var result = input.RemoveSpecificChars(charsToRemove);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveSpecificChars_ReturnsOriginalString_WhenNoCharactersMatchRemovalList()
    {
        // Arrange
        const string input = "Às ínclitas maçãs";
        char[] charsToRemove = ['z', 'x'];
        const string expected = "Às ínclitas maçãs";

        // Act
        var result = input.RemoveSpecificChars(charsToRemove);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region RemoveCharsNotIn Tests

    [Fact]
    public void RemoveCharsNotIn_ReturnsEmptyString_WhenInputIsNull()
    {
        // Arrange
        string? input = null;
        char[] allowedChars = ['a', 'b'];

        // Act
        var result = input.RemoveCharsNotIn(allowedChars);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveCharsNotIn_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;
        char[] allowedChars = ['a', 'b'];

        // Act
        var result = input.RemoveCharsNotIn(allowedChars);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveCharsNotIn_KeepsOnlyAllowedCharacters()
    {
        // Arrange
        const string input = "abcXYZ123";
        var allowedChars = "1234567890".ToCharArray();
        const string expected = "123";

        // Act
        var result = input.RemoveCharsNotIn(allowedChars);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveCharsNotIn_ReturnsEmptyString_WhenNoAllowedCharactersExistInInput()
    {
        // Arrange
        const string input = "abcdef";
        var allowedChars = "1234567890".ToCharArray();
        var expected = string.Empty;

        // Act
        var result = input.RemoveCharsNotIn(allowedChars);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region RemoveDuplicateSpaces Tests

    [Fact]
    public void RemoveDuplicateSpaces_ReturnsEmptyString_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input.RemoveDuplicateSpaces();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveDuplicateSpaces_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;

        // Act
        var result = input.RemoveDuplicateSpaces();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveDuplicateSpaces_DoesNotChangeString_WhenThereAreNoDuplicateSpaces()
    {
        // Arrange
        const string input = "No duplicates";
        const string expected = "No duplicates";

        // Act
        var result = input.RemoveDuplicateSpaces();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveDuplicateSpaces_ReplacesMultipleSpacesWithSingleSpaces()
    {
        // Arrange
        const string input = "This  is   a    test";
        const string expected = "This is a test";

        // Act
        var result = input.RemoveDuplicateSpaces();

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region RemoveNonAlphanumeric Tests

    [Fact]
    public void RemoveNonAlphanumeric_ReturnsEmptyString_WhenInputIsNull()
    {
        // Arrange
        string? input = null;
        var allowed = Array.Empty<char>();

        // Act
        var result = input.RemoveNonAlphanumeric(allowed);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveNonAlphanumeric_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = string.Empty;
        var allowed = Array.Empty<char>();

        // Act
        var result = input.RemoveNonAlphanumeric(allowed);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void RemoveNonAlphanumeric_RemovesPunctuationAndSymbols_WhenNoAllowedCharactersProvided()
    {
        // Arrange
        const string input = "Às ínclitas maçãs, 123!.";
        const string expected = "Àsínclitasmaçãs123";

        // Act
        var result = input.RemoveNonAlphanumeric();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveNonAlphanumeric_KeepsAllowedCharacters_WhenSpecified()
    {
        // Arrange
        const string input = "Hello, World! 123.";
        char[] allowed = [' ', ','];
        const string expected = "Hello, World 123";

        // Act
        var result = input.RemoveNonAlphanumeric(allowed);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RemoveNonAlphanumeric_ReturnsEmptyString_WhenAllCharactersAreNonAlphanumeric_AndNoAllowedProvided()
    {
        // Arrange
        const string input = "!@#$%^&*()";
        var allowed = Array.Empty<char>();
        var expected = string.Empty;

        // Act
        var result = input.RemoveNonAlphanumeric(allowed);

        // Assert
        result.Should().Be(expected);
    }

    #endregion
}

namespace Sozo.Toolkit.Validators;

using System.Collections.Generic;
using System.Linq;

public static class CharsValidator
{
    /// <summary>
    /// Determines if the given string contains only the specified allowed characters.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <param name="chars">The allowed characters.</param>
    /// <returns>True if the string contains only allowed characters; otherwise, false.</returns>
    public static bool HasOnlyAllowedChars(string value, params char[] chars)
    {
        var allowedChars = new HashSet<char>(chars);

        foreach (var c in value)
        {
            if (!allowedChars.Contains(c))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Determines if the given string contains only digits.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>True if the string contains only digits; otherwise, false.</returns>
    public static bool HasOnlyDigits(string value) => value.All(char.IsDigit);

    /// <summary>
    /// Determines if the given string contains only letters.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>True if the string contains only letters; otherwise, false.</returns>
    public static bool HasOnlyLetters(string value) => value.All(char.IsLetter);
}

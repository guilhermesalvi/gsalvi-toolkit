namespace GSalvi.Toolkit.Extensions;

using System.Collections.Generic;
using System.Globalization;
using System.Text;

public static class StringExtensions
{
    /// <summary>
    /// Removes diacritics from a string, converting accented characters into their simple forms.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>The string without diacritics.</returns>
    public static string RemoveDiacritics(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var normalizedString = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalizedString.Length);

        foreach (var c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Removes all specified characters from the string.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <param name="chars">The characters to remove.</param>
    /// <returns>The string with the specified characters removed.</returns>
    public static string RemoveSpecificChars(this string? value, params char[] chars)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var removeSet = new HashSet<char>(chars);
        var sb = new StringBuilder(value.Length);

        foreach (var c in value)
        {
            if (!removeSet.Contains(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Removes characters from the string that are not in the allowed set.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <param name="allowedChars">The characters that are allowed and should be kept.</param>
    /// <returns>The string containing only the allowed characters.</returns>
    public static string RemoveCharsNotIn(this string? value, params char[] allowedChars)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var allowedSet = new HashSet<char>(allowedChars);
        var sb = new StringBuilder(value.Length);

        foreach (var c in value)
        {
            if (allowedSet.Contains(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Removes duplicate spaces from the string, leaving only one space between words.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <returns>The string with duplicate spaces removed.</returns>
    public static string RemoveDuplicateSpaces(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var sb = new StringBuilder(value.Length);

        for (int i = 0; i < value.Length; i++)
        {
            if (i > 0 && char.IsWhiteSpace(value[i]) && char.IsWhiteSpace(value[i - 1]))
            {
                continue;
            }
            sb.Append(value[i]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Removes non-alphanumeric characters from the string, except for those specified as allowed.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <param name="allowed">Additional characters that should be preserved.</param>
    /// <returns>The string with non-alphanumeric characters removed.</returns>
    public static string RemoveNonAlphanumeric(this string? value, params char[] allowed)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var allowedSet = new HashSet<char>(allowed);
        var sb = new StringBuilder(value.Length);

        foreach (var c in value)
        {
            if (char.IsLetterOrDigit(c) || allowedSet.Contains(c))
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}

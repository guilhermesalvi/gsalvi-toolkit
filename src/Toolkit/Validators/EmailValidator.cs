namespace GSalvi.Toolkit.Validators;

using System.Text.RegularExpressions;

public static class EmailValidator
{
    private static readonly Regex EmailRegex = new Regex(
        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Determines whether the specified string is a valid email address.
    /// </summary>
    /// <param name="value">The input string to validate as an email address.</param>
    /// <returns>
    /// True if the input string matches the email address format; otherwise, false.
    /// </returns>
    public static bool IsEmailAddress(string value) => EmailRegex.IsMatch(value);
}

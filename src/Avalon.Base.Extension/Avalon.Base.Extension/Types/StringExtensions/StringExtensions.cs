namespace Avalon.Base.Extension.Types.StringExtensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns the value with an append to the front if the value is shorter 
    /// than the maximum length.
    /// </summary>
    /// <param name="value">
    /// Value to negate.
    /// </param>
    /// <param name="characterToAppend">
    /// Character to append.
    /// </param>
    /// <param name="maxLength">
    /// Maximum length result.
    /// </param>
    /// <returns>
    /// Returns the value with an append to the front if the value is shorter 
    /// than the maximum length.
    /// </returns>
    public static string AppendFront(this string value, 
        string characterToAppend, int maxLength)
    {
        if(value.IsNotNullOrEmpty() && value.Length < maxLength)
        {
            return (characterToAppend + value).AppendFront(
                characterToAppend, maxLength);
        }

        return value;
    }

    /// <summary>
    /// Truncate a string and append and ending character.
    /// </summary>
    /// <param name="value">
    /// Value to truncate.
    /// </param>
    /// <param name="maxLength">
    /// Maximum length.
    /// </param>
    /// <param name="appendCharacter">
    /// Character to append at the end.
    /// </param>
    /// <returns>
    /// String truncated with an append character.
    /// </returns>
    public static string Truncate(this string value, int maxLength, string appendCharacter = "...")
    {
        if (value.IsNotNullOrEmpty() && value.Length > maxLength)
        {
            return value.Substring(0, maxLength) + 
                (appendCharacter.IsNotNullOrEmpty() ? appendCharacter : string.Empty );
        }

        return value;
    }

    /// <summary>
    /// Set a default value if a string is null or empty.
    /// </summary>
    /// <param name="value">
    /// Value to set a default value.
    /// </param>
    /// <param name="defaultValue">
    /// Default value in case the string is null or empty.
    /// </param>
    /// <returns>
    /// Default value if the string is null or empty. Otherwise will return the original string value.
    /// </returns>
    public static string ToDefaultValueIfEmpty(this string value, string defaultValue)
    {
        return value.IsNullOrEmpty() ? defaultValue : value;
    }
}
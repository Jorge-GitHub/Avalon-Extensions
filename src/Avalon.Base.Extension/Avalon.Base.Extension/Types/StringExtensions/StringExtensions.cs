using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    /// <summary>
    /// Split string into chunks of strings.
    /// </summary>
    /// <param name="value">
    /// Value to split.
    /// </param>
    /// <param name="blockSize">
    /// Chunk size.
    /// </param>
    /// <returns>
    /// The value broken into a list of strings.
    /// </returns>
    public static List<string> SplitByBlocks(this string value, int blockSize)
    {
        List<string> strings = new List<string>();
        if (value.IsNotNullOrEmpty() && blockSize > 0)
        {
            int length = value.Length;
            for (int i = 0; i < length; i += blockSize)
            {
                if (blockSize + i > length)
                {
                    blockSize = length - i;
                }
                strings.Add(value.Substring(i, blockSize));
            }
        }

        return strings;
    }

	/// <summary>
	/// Checks if a string is a valid email or not.
	/// </summary>
	/// <param name="email">
	/// Email to check.
	/// </param>
	/// <returns>
	/// Flag that determinate whether a string is a valid email or not.
	/// </returns>
	public static bool IsAValidEmail(this string email)
    {
        if (email.IsNotNullOrEmpty())
        {
			return new EmailAddressAttribute().IsValid(email);
		}
        return false;
    }

	/// <summary>
	/// Checks if a string is not a valid email or not.
	/// </summary>
	/// <param name="email">
	/// Email to check.
	/// </param>
	/// <returns>
	/// Flag that determinate whether a string is not a valid email or not.
	/// </returns>
	public static bool IsNotAValidEmail(this string email)
	{
		return !email.IsAValidEmail();
	}
}
using System.Globalization;
using System.Text.RegularExpressions;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// String extension methods.
/// </summary>
public static class StringBasicExtensions
{
    /// <summary>
    /// Limit the size of the string.
    /// </summary>
    /// <param name="value">
    /// Value to limit.
    /// </param>
    /// <param name="length">
    /// Maximum length desired.
    /// </param>
    /// <returns>
    /// String with a length equal or less than the length desired.
    /// </returns>
    public static string LimitLength(this string value, int length)
    {
        if (!value.IsNullOrEmpty())
        {
            if (value.Length > length)
            {
                return value.Substring(0, length);
            }
        }

        return value;
    }

    /// <summary>
    /// Determinate if the string is an integer or not.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is an integer or not.
    /// </returns>
    public static bool IsAnInteger(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            int result;

            return int.TryParse(value, out result);
        }

        return false;
    }

    /// <summary>
    /// Determinate if the string is a decimal or not.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is a decimal or not.
    /// </returns>
    public static bool IsADecimal(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            decimal result;

            return decimal.TryParse(value, out result);
        }
        return false;
    }

    /// <summary>
    /// Determinate if the string is numeric or not.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is a valid number or not.
    /// </returns>
    public static bool IsNumeric(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        return false;
    }

    /// <summary>
    /// Determinate whether the string is a valid Boolean or not.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is a valid Boolean or not.
    /// </returns>
    public static bool ISABoolean(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            bool result;

            return bool.TryParse(value.ToLower(), out result);
        }

        return false;
    }

    /// <summary>
    /// Convert a null string into an empty string.
    /// </summary>
    /// <param name="value">
    /// value to convert to an empty string if is null.
    /// </param>
    /// <returns>
    /// Empty string if the string is null.
    /// </returns>
    public static string ToEmptyStringIfNull(this string value)
    {
        if (value.IsNotNullOrEmpty())
        {
            return value;
        }

        return string.Empty;
    }

    /// <summary>
    /// Convert a null string into an string.
    /// </summary>
    /// <param name="value">
    /// value to convert to an empty string if is null.
    /// </param>
    /// <returns>
    /// Empty string if the string is null.
    /// </returns>
    public static string ToSafeString(this string value)
    {
        return value.ToEmptyStringIfNull();
    }

    /// <summary>
    /// Reverse the string.
    /// </summary>
    /// <param name="value">
    /// Value to reverse.
    /// </param>
    /// <returns>
    /// The reverse value o the string.
    /// </returns>
    public static string Reverse(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            char[] charValues = value.ToCharArray();
            Array.Reverse(charValues);

            return new string(charValues);
        }

        return value;
    }

    /// <summary>
    /// Determinate whether the string is all upper case.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is all upper case.
    /// </returns>
    public static bool IsAllUpperCase(this string value)
    {
        if (value.IsNullOrEmpty())
        {
            return false;
        }

        return Regex.IsMatch(value, @"^[A-Z]+$");
    }

    /// <summary>
    /// Determinate whether the string is all lower case.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the string is all lower case.
    /// </returns>
    public static bool IsAllLowerCase(this string value)
    {
        if (value.IsNullOrEmpty())
        {
            return false;
        }

        return Regex.IsMatch(value, @"^[a-z]+$");
    }

    /// <summary>
    /// Capitalize the string.
    /// </summary>
    /// <param name="value">
    /// String to capitalize.
    /// </param>
    /// <returns>
    /// Capitalized string.
    /// <para>
    /// For example the string "hello wORLD" will return "Hello World".
    /// </para>
    /// </returns>
    public static string ToTitleCase(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        return value;
    }
    /// <summary>
    /// Count the number of words within the string.
    /// </summary>
    /// <param name="value">
    /// String to check for the number of words within.
    /// </param>
    /// <returns>
    /// Number of words within the string.
    /// </returns>
    public static int WordCount(this string value)
    {
        if (!value.IsNullOrEmpty())
        {
            return value.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        return 0;
    }

    /// <summary>
    /// Indicates whether the specified string is null or an System.String.Empty string.
    /// </summary>
    /// <param name="value">
    /// The string to test.
    /// </param>
    /// <returns>
    /// True if the value parameter is null or an empty string (""); otherwise, false.
    /// </returns>
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Indicates whether the specified string is NOT null or NOT an System.String.Empty string.
    /// </summary>
    /// <param name="value">
    /// The string to test.
    /// </param>
    /// <returns>
    /// True if the value parameter is NOT null or NOT an empty string (""); otherwise, false.
    /// </returns>
    public static bool IsNotNullOrEmpty(this string value)
    {
        return !value.IsNullOrEmpty();
    }

    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in the 
    /// current instance are replaced with another specified string.
    /// </summary>
    /// <param name="value">
    /// String to examine.
    /// </param>
    /// <param name="stringToRemove">
    /// The string to be removed.
    /// </param>
    /// <returns>
    //  A string that is equivalent to the current string except that all instances
    //  of stringToRemove are removed.
    /// </returns>
    public static string Remove(this string value, string stringToRemove)
    {
        return value.Replace(stringToRemove, "");
    }

    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in the 
    /// current instance are replaced with another specified string.
    /// </summary>
    /// <param name="value">
    /// String to examine.
    /// </param>
    /// <param name="stringToRemove">
    /// The string to be removed.
    /// </param>
    /// <param name="IgnoreCase">
    /// Flag that determinate whether to be case insensitive or not.
    /// </param>
    /// <returns>
    //  A string that is equivalent to the current string except that all instances
    //  of stringToRemove are removed.
    /// </returns>
    public static string Remove(this string value, string stringToRemove, bool ignoreCase)
    {
        return value.Replace(stringToRemove, "", ignoreCase);
    }

    /// <summary>
    /// Returns a new string in which all occurrences of a specified string in the 
    /// current instance are replaced with another specified string.
    /// </summary>
    /// <param name="value">
    /// String to examine.
    /// </param>
    /// <param name="oldValue">
    /// The string to be replaced.
    /// </param>
    /// <param name="newValue">
    /// The string to replace all occurrences of oldValue.
    /// </param>
    /// <param name="IgnoreCase">
    /// Flag that determinate whether to be case insensitive or not.
    /// </param>
    /// <returns>
    //  A string that is equivalent to the current string except that all instances
    //  of oldValue are replaced with newValue.
    /// </returns>
    public static string Replace(this string value, string oldValue, string newValue, bool IgnoreCase)
    {
        if (IgnoreCase)
        {
            return new Regex(oldValue, RegexOptions.IgnoreCase | RegexOptions.Multiline).Replace(value, newValue);
        }

        return value.Replace(oldValue, newValue);
    }

    /// <summary>
    /// Removes all leading and trailing white-space characters from the current string if is not null.
    /// </summary>
    /// <param name="value">
    /// Value to trim.
    /// </param>
    /// <returns>
    //  The string that remains after all white-space characters are removed from
    //  the start and end of the current string.
    /// </returns>
    public static string SafeTrim(this string value)
    {
        if (value.IsNotNullOrEmpty())
        {
            return value.Trim();
        }

        return value;
    }

    /// <summary>
    /// Remove non ASCII characters
    /// </summary>
    /// <param name="value">
    /// Value to remove.
    /// </param>
    /// <returns>
    /// The string without any non ASCII character.
    /// </returns>
    public static string RemoveNonASCIICharacters(this string value)
    {
        if(value.IsNotNullOrEmpty())
        {
            return Regex.Replace(value, @"[^\u0000-\u007F]", string.Empty);
        }

        return value;
    }

    /// <summary>
    /// Remove the characters from the string.
    /// </summary>
    /// <param name="value">
    /// Value to remove the characters from.
    /// </param>
    /// <param name="charactersToRemove">
    /// Characters to remove from the string.
    /// </param>
    /// <returns>
    /// A new string that is equivalent to this string except for the removed characters.
    /// </returns>
    public static string Remove(this string value, char[] charactersToRemove)
    {
        if (value.IsNotNullOrEmpty())
        {
            return String.Join(string.Empty, value.Split(charactersToRemove));
        }

        return value;
    }

    /// <summary>
    /// Remove the last index of a character in a string.
    /// </summary>
    /// <param name="value">
    /// String to remove the char.
    /// </param>
    /// <param name="characterToRemove">
    /// Character to remove.
    /// </param>
    /// <returns>
    /// String without the last character.
    /// </returns>
    public static string RemoveLastIndexOf(this string value, char characterToRemove)
    {
        if(value.IsNotNullOrEmpty())
        {
            return value.Remove(value.LastIndexOf(','));
        }

        return value;
    }

    /// <summary>
    /// Replaces the format item in a specified string with the string representation 
    /// of a corresponding object in a specified array. 
    /// </summary>
    /// <param name="value">
    /// Value to format.
    /// </param>
    /// <param name="args">
    /// An object array that contains zero or more objects to format.
    /// </param>
    /// <returns>
    //  A copy of format in which the format items have been replaced by the string representation
    //  of the corresponding objects in args.
    /// </returns>
    public static string Format(this string value, params object[] args)
    {
        if (!value.IsNotNullOrEmpty())
        {
            return string.Format(value, args);
        }

        return string.Empty;
    }

    /// <summary>
    /// Determines whether this string and a specified System.String object have the
    /// same value i not null. A parameter specifies the culture, case, and sort rules used in the
    /// comparison.
    /// </summary>
    /// <param name="value">
    /// Instance to compare.
    /// </param>
    /// <param name="valueToCompare">
    /// The string to compare to this instance.
    /// </param>
    /// <param name="comparisonType">
    /// One of the enumeration values that specifies how the strings will be compared.
    /// </param>
    /// <returns>
    /// True if the value of the value parameter is the same as this string; otherwise, false. 
    /// <para>
    /// Exceptions: T:System.ArgumentException: comparisonType is not a System.StringComparison value.
    /// </para>
    /// </returns>
    public static bool EqualsIfNotNull(this string value, string valueToCompare, StringComparison comparisonType)
    {
        if(!value.IsNotNullOrEmpty() && !valueToCompare.IsNotNullOrEmpty())
        {
            return value.Equals(valueToCompare, comparisonType);
        }

        return false;
    }

    /// <summary>
    /// Get last characters in a string.
    /// </summary>
    /// <param name="value">
    /// Instance to use.
    /// </param>
    /// <param name="length">
    /// The number of digits to get.
    /// </param>
    /// <returns>
    /// The last characters in a string.
    /// </returns>
    public static string GetLast(this string value, int length)
    {
        if(value.IsNotNullOrEmpty())
        {
            if(value.Length > length)
            {
                return value.Substring(value.Length - length, length);
            }

            return value;
        }

        return string.Empty;
    }

    /// <summary>
    /// Get first characters in a string.
    /// </summary>
    /// <param name="value">
    /// Instance to use.
    /// </param>
    /// <param name="length">
    /// The number of digits to get.
    /// </param>
    /// <returns>
    /// The first characters in a string.
    /// </returns>
    public static string GetFirst(this string value, int length)
    {
        if (value.IsNotNullOrEmpty())
        {
            if (value.Length > length)
            {
                return value.Substring(0, length);
            }

            return value;
        }

        return string.Empty;
    }
}
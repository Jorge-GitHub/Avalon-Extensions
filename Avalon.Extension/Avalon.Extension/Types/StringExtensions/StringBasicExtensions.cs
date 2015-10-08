#region "USING STATEMENT"
using System;
using System.Globalization;
using System.Text.RegularExpressions;
#endregion "USING STATEMENT"

//  <copyright file="StringBasicExtensions.cs" company="Avalon">
//  Copyright (c) 2015 Avalon All Rights Reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//  http://opensource.org/licenses/MIT
//  </copyright>
//  <author>
//  Jorge Gonzalez
//  </author>
//  <date>
//  11/29/2013 10:00:00 PM
//  </date>
namespace Avalon.Extension.Types
{
    /// <summary>
    /// String extension methods.
    /// </summary>
    public static class StringBasicExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
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
        /// Flag that determinates whether the string is an integer or not.
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
        /// Determinate if the string is numeric or not.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the string is a valid number or not.
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
        /// Determinates whether the string is a valid Boolean or not.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the string is a valid Boolean or not.
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
            if (!value.IsNullOrEmpty())
            {
                return "";
            }
            return value;
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
        /// Determinates whether the string is all upper case.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the string is all upper case.
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
        /// Determinates whether the string is all lower case.
        /// </summary>
        /// <param name="value">
        /// Value to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the string is all lower case.
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
        /// Flag that determinates whether to be case insensitive or not.
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
        /// Flag that determinates whether to be case insensitive or not.
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
            if (!value.IsNullOrEmpty())
            {
                return string.Format(value, args);
            }
            return string.Empty;
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
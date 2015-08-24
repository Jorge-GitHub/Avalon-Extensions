#region "USING STATEMENT"
using System;
using System.Text;
#endregion "USING STATEMENT"

//  <copyright file="StringConversionExtensions.cs" company="Avalon">
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
    /// String conversion extension methods.
    /// </summary>
    public static class StringConversionExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Convert a string into an array of bytes.
        /// </summary>
        /// <param name="value">
        /// Value to convert to an array of bytes.
        /// </param>
        /// <returns>
        /// Array of bytes.
        /// </returns>
        public static Byte[] ToBytes(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Byte[] bytes = new Byte[value.Length * sizeof(char)];
                Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
                return bytes;
            }
            return null;
        }
        /// <summary>
        /// Convert a string into a Boolean.
        /// </summary>
        /// <param name="value">
        /// Value to convert.
        /// </param>
        /// <param name="defaultValue">
        /// Default value to return if the string is not a valid Boolean.
        /// </param>
        /// <returns>
        /// The string as a Boolean if is a valid Boolean, otherwise it will return the default value.
        /// </returns>
        public static bool ToBoolean(this string value, bool defaultValue)
        {
            if (value.ISABoolean())
            {
                return Boolean.Parse(value.ToLower());
            }
            return defaultValue;
        }
        /// <summary>
        /// Convert a string into an integer.
        /// </summary>
        /// <param name="value">
        /// value to convert.
        /// </param>
        /// <param name="defaultValue">
        /// Default value to return if the string is not a valid integer.
        /// </param>
        /// <returns>
        /// String as an integer.
        /// </returns>
        public static int ToInteger(this string value, int defaultValue)
        {
            if (value.IsAnInteger())
            {
                return int.Parse(value);
            }
            return defaultValue;
        }
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="value">
        /// String to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted string.
        /// </returns>
        public static string Encrypt(this string value)
        {
            Byte[] b = ASCIIEncoding.ASCII.GetBytes(value);
            return Convert.ToBase64String(b);
        }
        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="value">
        /// String to be decrypted.
        /// </param>
        /// <returns>
        /// Decrypted string.
        /// </returns>
        public static string Decrypt(this string value)
        {
            Byte[] b = Convert.FromBase64String(value);
            return ASCIIEncoding.ASCII.GetString(b);
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
#region "USING STATEMENT"
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
#endregion "USING STATEMENT"

//  <copyright file="HashExtensions.cs" company="Avalon">
//  Copyright (c) 2015 Avalon All Rights Reserved.
//  Licensed under the MIT license. See LICENSE file in the project root for full license information.
//  http://opensource.org/licenses/MIT
//  </copyright>
//  <author>
//  Jorge Gonzalez
//  </author>
namespace Avalon.Extension.Types
{
    /// <summary>
    /// Hash extensions.
    /// Other types of hash for the future: MD5, SHA384, SHA512Of.
    /// </summary>
    public static class HashExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Create a Hash Sha1. 
        /// </summary>
        /// <param name="value">
        /// Value to hash.
        /// </param>
        /// <returns>
        /// Value to hashed.
        /// </returns>
        public static string ToSHA1(this string value)
        {
            return value.ToSHA1(null);
        }
        /// <summary>
        /// Create a Hash Sha1. 
        /// </summary>
        /// <param name="value">
        /// Value to hash.
        /// </param>
        /// <param name="encode">
        /// Optional: Encoding type.
        /// </param>
        /// <returns>
        /// Value to hashed.
        /// </returns>
        public static string ToSHA1(this string value, Encoding encode)
        {
            if (encode == null)
            {
                encode = Encoding.Default;
            }
            byte[] buffer = encode.GetBytes(value);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }
    }
    #endregion "STATIC METHODS"
    #endregion "METHODS"
}
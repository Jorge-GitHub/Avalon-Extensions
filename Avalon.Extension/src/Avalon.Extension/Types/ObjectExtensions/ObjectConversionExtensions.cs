#region "USING STATEMENTS"
using System;
#endregion "USING STATEMENTS"

//  <copyright file="ObjectConversionExtensions.cs" company="Avalon">
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
    /// Object conversion extension methods.
    /// </summary>
    public static class ObjectConversionExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Convert an object into a string.
        /// </summary>
        /// <param name="value">
        /// Object to convert.
        /// </param>
        /// <param name="defaultValue">
        /// Default value to return if the object is not a valid.
        /// </param>
        /// <returns>
        /// The object as a string or the default value if is not a valid object.
        /// </returns>
        public static string ToString(this object value, string defaultValue)
        {
            if (value.IsNull())
            {
                return defaultValue;
            }
            return Convert.ToString(value).Trim();
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
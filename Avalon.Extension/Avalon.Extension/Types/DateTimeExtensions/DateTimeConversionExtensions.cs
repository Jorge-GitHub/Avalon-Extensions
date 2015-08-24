#region "USING STATEMENTS"
using System;
#endregion "USING STATEMENTS"

//  <copyright file="DateTimeConversionExtensions.cs" company="Avalon">
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
    /// DateTime conversion extension methods.
    /// </summary>
    public static class DateTimeConversionExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Convert the DateTime to ISO standard.
        /// </summary>
        /// <param name="value">
        /// ISO standard format eliminates confusion and allows you to read the dates and know better what they represent.
        /// </param>
        /// <param name="kind">
        /// DateTime kind.
        /// </param>
        /// <remarks>
        /// For more information check http://www.w3.org/TR/NOTE-datetime
        /// </remarks>
        /// <returns>
        /// DateTime in ISO standard.
        /// </returns>
        public static string ToISOString(this DateTime value, DateTimeKind kind)
        {
            if (kind == DateTimeKind.Utc)
            {
                return value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffz");
            }
            if (kind == DateTimeKind.Local)
            {
                return value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
            }
            return value.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }
        /// <summary>
        /// Convert the DateTime to ISO standard.
        /// </summary>
        /// <param name="value">
        /// ISO standard format eliminates confusion and allows you to read the dates and know better what they represent.
        /// </param>
        /// <remarks>
        /// For more information check http://www.w3.org/TR/NOTE-datetime
        /// </remarks>
        /// <returns>
        /// DateTime in ISO standard.
        /// </returns>
        public static string ToISOString(this DateTime value)
        {
            return value.ToISOString(DateTimeKind.Unspecified);
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
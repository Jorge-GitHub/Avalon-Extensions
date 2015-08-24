﻿#region "USING STATEMENTS"
using System.Globalization;
#endregion "USING STATEMENTS"

//  <copyright file="DoubleConversionExtensions.cs" company="Avalon">
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
    /// Double conversion extension methods.
    /// </summary>
    public static class DoubleConversionExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Convert a double to a string formatted using the default culture.
        /// </summary>
        /// <param name="value">
        /// Value to format as a currency.
        /// </param>
        /// <returns>
        /// String formatted as a current culture currency.
        /// </returns>
        public static string ToCurrency(this double value)
        {
            return value.ToCurrency(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Convert a double to a string formatted using the specified culture.
        /// </summary>
        /// <param name="value">
        /// Value to format as a currency.
        /// </param>
        /// <param name="cultureName">
        /// The string representation for the culture to be used, for instance "en-US" for US English.
        /// </param>
        /// <returns>
        /// String formatted based on the culture name provided.
        /// </returns>
        public static string ToCurrency(this double value, string cultureName)
        {
            return value.ToCurrency(new CultureInfo(cultureName));
        }
        /// <summary>
        /// Convert a double to a string formatted using the specified culture.
        /// </summary>
        /// <param name="value">
        /// Value to format as a currency.
        /// </param>
        /// <param name="culture">
        /// The culture to be use.
        /// </param>
        /// <returns>
        /// String formatted based on the culture info provided.
        /// </returns>
        public static string ToCurrency(this double value, CultureInfo culture)
        {
            return (string.Format(culture, "{0:C}", value));
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
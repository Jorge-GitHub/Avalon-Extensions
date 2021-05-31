#region "USING STATEMENT"
using System;
#endregion "USING STATEMENT"

//  <copyright file="DateTimeBasicExtensions.cs" company="Avalon">
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
    /// DateTime extension methods.
    /// </summary>
    public static class DateTimeBasicExtensions
    {
        #region "METHODS"
        #region "STATIC METHODS"
        /// <summary>
        /// Determinate whether the date is today or not.
        /// </summary>
        /// <param name="value">
        /// DateTime to check.
        /// </param>
        /// <returns>
        /// flag that determinates whether the DateTime is today or not.
        /// </returns>
        public static bool IsToday(this DateTime value)
        {
            return (value.Date == DateTime.Today);
        }
        /// <summary>
        /// get the number of days in the month.
        /// </summary>
        /// <param name="value">
        /// DateTime to get the number of days in the month.
        /// </param>
        /// <returns>
        /// Number of days on the month.
        /// </returns>
        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }
        /// <summary>
        /// Get the first day of the month.
        /// </summary>
        /// <param name="value">
        /// DateTime to check.
        /// </param>
        /// <returns>
        /// First day of the month.
        /// </returns>
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }
        /// <summary>
        /// Get last day of the month.
        /// </summary>
        /// <param name="value">
        /// DateTime to check.
        /// </param>
        /// <returns>
        /// Last day of the month.
        /// </returns>
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }
        /// <summary>
        /// Determinates whether the DateTime year is a leap year or not.
        /// </summary>
        /// <param name="value">
        /// DateTime to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the DateTime year is a leap year or not.
        /// </returns>
        public static bool IsLeapYear(this DateTime value)
        {
            return DateTime.IsLeapYear(value.Year);
        }
        /// <summary>
        /// Determinates whether the date time is the default value.
        /// </summary>
        /// <param name="value">
        /// DateTime to check.
        /// </param>
        /// <returns>
        /// Flag that determinates whether the date time is the default value.
        /// </returns>
        public static bool IsDefaultValue(this DateTime value)
        {
            return value == default(DateTime);
        }
        #endregion "STATIC METHODS"
        #endregion "METHODS"
    }
}
namespace Avalon.Base.Extension.Types.DateTimeExtensions;

/// <summary>
/// DateTime extension methods.
/// </summary>
public static class DateTimeBasicExtensions
{
    /// <summary>
    /// Determinate whether the date is today or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// flag that determinate whether the DateTime is today or not.
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
    /// Determinate whether the DateTime year is a leap year or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the DateTime year is a leap year or not.
    /// </returns>
    public static bool IsLeapYear(this DateTime value)
    {
        return DateTime.IsLeapYear(value.Year);
    }

    /// <summary>
    /// Determinate whether the date time is the default value.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date time is the default value.
    /// </returns>
    public static bool IsDefaultValue(this DateTime value)
    {
        return value == default(DateTime);
    }
}
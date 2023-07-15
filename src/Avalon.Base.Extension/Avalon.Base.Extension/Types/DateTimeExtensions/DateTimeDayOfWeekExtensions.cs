namespace Avalon.Base.Extension.Types.DateTimeExtensions;

/// <summary>
/// Day of the week extensions.
/// </summary>
public static class DateTimeDayOfWeekExtensions
{
    /// <summary>
    /// Determinate whether the date is a Monday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Monday or not.
    /// </returns>
    public static bool IsMonday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Monday);
    }

    /// <summary>
    /// Determinate whether the date is a Tuesday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Tuesday or not.
    /// </returns>
    public static bool IsTuesday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Tuesday);
    }

    /// <summary>
    /// Determinate whether the date is a Wednesday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Wednesday or not.
    /// </returns>
    public static bool IsWednesday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Wednesday);
    }

    /// <summary>
    /// Determinate whether the date is a Thursday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Thursday or not.
    /// </returns>
    public static bool IsThursday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Thursday);
    }

    /// <summary>
    /// Determinate whether the date is a Friday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Friday or not.
    /// </returns>
    public static bool IsFriday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Friday);
    }

    /// <summary>
    /// Determinate whether the date is a Saturday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Saturday or not.
    /// </returns>
    public static bool IsSaturday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Saturday);
    }

    /// <summary>
    /// Determinate whether the date is a Sunday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the date is a Sunday or not.
    /// </returns>
    public static bool IsSunday(this DateTime value)
    {
        return value.DayOfWeek.Equals(DayOfWeek.Sunday);
    }

    /// <summary>
    /// Determinate whether the day is a weekend day or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the day is a weekend day or not.
    /// </returns>
    public static bool IsWeekend(this DateTime value)
    {
        return value.IsSunday() || value.IsSaturday();
    }

    /// <summary>
    /// Determinate whether the day is a weekday or not.
    /// </summary>
    /// <param name="value">
    /// DateTime to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the day is a weekday or not.
    /// </returns>
    public static bool IsWeekday(this DateTime value)
    {
        return !value.IsWeekend();
    }
}
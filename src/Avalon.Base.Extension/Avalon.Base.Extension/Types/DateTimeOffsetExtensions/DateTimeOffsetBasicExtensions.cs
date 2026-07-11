using Avalon.Base.Extension.Types.DateTimeExtensions;

namespace Avalon.Base.Extension.Types.DateTimeOffsetExtensions;

/// <summary>
/// Provides extension methods for <see cref="DateTimeOffset"/> to simplify common operations 
/// such as calculating the first day of a month.
/// </summary>
public static class DateTimeOffsetBasicExtensions
{
    /// <summary>
    /// Calculates a new <see cref="DateTimeOffset"/> representing the first day of the month 
    /// for the given <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <param name="value">The <see cref="DateTimeOffset"/> instance to calculate from.</param>
    /// <returns>
    /// A <see cref="DateTimeOffset"/> instance set to the first day of the month, 
    /// preserving the time of day and offset.
    /// </returns>
    /// <remarks>
    /// This method uses the <c>FirstDayOfMonth()</c> extension method for <see cref="DateTime"/> 
    /// to calculate the corresponding date.
    /// Ensure the <c>FirstDayOfMonth()</c> method is implemented in your project or imported 
    /// from the appropriate namespace.
    /// </remarks>
    public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset value)
    {
        return new DateTimeOffset(value.DateTime.FirstDayOfMonth());
    }

    public static DateTimeOffset? ToUniversalTimeSafe(this DateTimeOffset? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToUniversalTime();
        }

        return value;
    }
}
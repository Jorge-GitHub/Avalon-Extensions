namespace Avalon.Base.Extension.Types.DateTimeExtensions;

/// <summary>
/// DateTime conversion extension methods.
/// </summary>
public static class DateTimeConversionExtensions
{
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
}
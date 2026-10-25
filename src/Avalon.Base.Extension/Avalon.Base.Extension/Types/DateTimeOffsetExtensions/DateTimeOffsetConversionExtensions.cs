namespace Avalon.Base.Extension.Types.DateTimeOffsetExtensions;

/// <summary>
/// DateTimeOffset conversion extension methods.
/// </summary>
public static class DateTimeOffsetConversionExtensions
{
    /// <summary>
    /// Re-anchors to UTC a value that lost its UTC marker crossing a
    /// DataSet boundary.
    /// </summary>
    /// <param name="value">
    /// Value read out of a DataSet.
    /// </param>
    /// <remarks>
    /// A DataColumn defaults to DataSetDateTime.UnspecifiedLocal, so a value a
    /// provider read as UTC comes back as DateTimeKind.Unspecified. Reading
    /// those bare digits into a DateTimeOffset attaches the local machine
    /// offset, which moves the instant: on a UTC-5 host 04:03Z becomes
    /// 04:03-05:00.
    ///
    /// This keeps the clock reading and re-labels it as UTC. It is NOT
    /// ToUniversalTime(), which would convert the already shifted instant to
    /// 09:03Z and lock the error in.
    ///
    /// The method is idempotent, so a value already anchored at UTC is returned
    /// untouched and hosts running in UTC are unaffected.
    /// </remarks>
    /// <returns>
    /// DateTimeOffset anchored at UTC.
    /// </returns>
    public static DateTimeOffset ToUtcFromDataSet(this DateTimeOffset value)
    {
        return value.Offset == TimeSpan.Zero
            ? value
            : new DateTimeOffset(value.DateTime, TimeSpan.Zero);
    }

    /// <summary>
    /// Re-anchors to UTC a value that lost its UTC marker crossing a
    /// DataSet boundary.
    /// </summary>
    /// <param name="value">
    /// Value read out of a DataSet.
    /// </param>
    /// <returns>
    /// DateTimeOffset anchored at UTC, or null.
    /// </returns>
    public static DateTimeOffset? ToUtcFromDataSet(this DateTimeOffset? value)
    {
        return value?.ToUtcFromDataSet();
    }

    /// <summary>
    /// Rounds a value up to the next whole microsecond.
    /// </summary>
    /// <param name="value">
    /// Value to round.
    /// </param>
    /// <remarks>
    /// Postgres timestamptz holds microsecond precision, one digit short of
    /// the 100ns ticks .NET (and Mongo's C# driver, which round-trips
    /// DateTimeOffset via a Ticks field to survive BSON's millisecond-only
    /// native date type) can represent. Persisting a tick-exact value as a
    /// watermark/cursor otherwise gets silently truncated to an instant
    /// earlier than the source value, so a later comparison against that same
    /// source value can permanently read as "still ahead of the cursor".
    /// Rounding up before persisting keeps the cursor at or after the value
    /// it represents once truncated. A value already on a microsecond
    /// boundary is returned untouched.
    /// </remarks>
    /// <returns>
    /// DateTimeOffset rounded up to the next microsecond.
    /// </returns>
    public static DateTimeOffset ToMicrosecondCeiling(this DateTimeOffset value)
    {
        const long TicksPerMicrosecond = 10;

        long ticks = value.UtcTicks;
        long remainder = ticks % TicksPerMicrosecond;

        return remainder == 0
            ? value.ToUniversalTime()
            : new DateTimeOffset(ticks + (TicksPerMicrosecond - remainder), TimeSpan.Zero);
    }

    /// <summary>
    /// Rounds a value up to the next whole microsecond.
    /// </summary>
    /// <param name="value">
    /// Value to round.
    /// </param>
    /// <returns>
    /// DateTimeOffset rounded up to the next microsecond, or null.
    /// </returns>
    public static DateTimeOffset? ToMicrosecondCeiling(this DateTimeOffset? value)
    {
        return value?.ToMicrosecondCeiling();
    }
}

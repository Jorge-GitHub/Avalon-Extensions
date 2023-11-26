namespace Avalon.Base.Extension.Types.BooleanExtensions;

/// <summary>
/// Boolean extensions. To improve code readings.
/// </summary>
public static class BooleanCodeReadingExtensions
{
    /// <summary>
    /// Determinate whether the boolean is true.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the boolean is true.
    /// </returns>
    public static bool IsTrue(this bool value)
    {
        return value;
    }

    /// <summary>
    /// Determinate whether the boolean is not true.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the boolean is not true.
    /// </returns>
    public static bool IsFalse(this bool value)
    {
        return !value;
    }

    /// <summary>
    /// Determinate whether the boolean is not true.
    /// </summary>
    /// <param name="value">
    /// Value to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the boolean is not true.
    /// </returns>
    public static bool IsNotTrue(this bool value)
    {
        return !value;
    }
}
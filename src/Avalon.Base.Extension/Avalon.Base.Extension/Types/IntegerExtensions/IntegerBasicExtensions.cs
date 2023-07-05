namespace Avalon.Base.Extension.Types.IntegerExtensions;

/// <summary>
/// Integer basic extensions.
/// </summary>
public static class IntegerBasicExtensions
{
    /// <summary>
    /// Returns the result of multiplying the specified Integer value by negative one.
    /// </summary>
    /// <param name="value">
    /// Value to negate.
    /// </param>
    /// <returns>
    /// Returns the result of multiplying the specified Integer value by negative one.
    /// </returns>
    public static int Negate(this int value)
    {
        return value * -1;
    }
}
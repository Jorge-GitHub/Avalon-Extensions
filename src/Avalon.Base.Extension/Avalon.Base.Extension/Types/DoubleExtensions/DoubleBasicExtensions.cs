namespace Avalon.Base.Extension.Types.DoubleExtensions;

/// <summary>
/// Double basic extensions.
/// </summary>
public static class DoubleBasicExtensions
{
    /// <summary>
    /// Returns the result of multiplying the specified Double value by negative one.
    /// </summary>
    /// <param name="value">
    /// Value to negate.
    /// </param>
    /// <returns>
    /// Returns the result of multiplying the specified Double value by negative one.
    /// </returns>
    public static double Negate(this double value)
    {
        return value * -1;
    }
}
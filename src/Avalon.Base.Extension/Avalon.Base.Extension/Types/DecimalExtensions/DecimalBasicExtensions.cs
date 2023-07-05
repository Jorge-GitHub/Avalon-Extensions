namespace Avalon.Base.Extension.Types.DecimalExtensions;

/// <summary>
/// Decimal basic extensions.
/// </summary>
public static class DecimalBasicExtensions
{
    /// <summary>
    /// Returns the result of multiplying the specified Decimal value by negative one.
    /// </summary>
    /// <param name="value">
    /// Value to negate.
    /// </param>
    /// <returns>
    /// Returns the result of multiplying the specified Decimal value by negative one.
    /// </returns>
    public static decimal Negate(this decimal value)
    {
        return value * -1;
    }
}
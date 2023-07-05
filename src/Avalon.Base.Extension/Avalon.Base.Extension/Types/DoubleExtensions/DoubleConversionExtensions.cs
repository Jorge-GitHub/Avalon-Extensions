using System.Globalization;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Double conversion extension methods.
/// </summary>
public static class DoubleConversionExtensions
{
    /// <summary>
    /// Convert a double to a string formatted using the default culture.
    /// </summary>
    /// <param name="value">
    /// Value to format as a currency.
    /// </param>
    /// <returns>
    /// String formatted as a current culture currency.
    /// </returns>
    public static string ToCurrency(this double value)
    {
        return value.ToCurrency(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Convert a double to a string formatted using the specified culture.
    /// </summary>
    /// <param name="value">
    /// Value to format as a currency.
    /// </param>
    /// <param name="cultureName">
    /// The string representation for the culture to be used, for instance "en-US" for US English.
    /// </param>
    /// <returns>
    /// String formatted based on the culture name provided.
    /// </returns>
    public static string ToCurrency(this double value, string cultureName)
    {
        return value.ToCurrency(new CultureInfo(cultureName));
    }

    /// <summary>
    /// Convert a double to a string formatted using the specified culture.
    /// </summary>
    /// <param name="value">
    /// Value to format as a currency.
    /// </param>
    /// <param name="culture">
    /// The culture to be use.
    /// </param>
    /// <returns>
    /// String formatted based on the culture info provided.
    /// </returns>
    public static string ToCurrency(this double value, CultureInfo culture)
    {
        return (string.Format(culture, "{0:C}", value));
    }
}
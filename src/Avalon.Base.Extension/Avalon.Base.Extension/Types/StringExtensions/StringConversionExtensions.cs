using System;
using System.Globalization;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// String conversion extension methods.
/// </summary>
public static class StringConversionExtensions
{
    /// <summary>
    /// Convert a string into an array of bytes.
    /// </summary>
    /// <param name="value">
    /// Value to convert to an array of bytes.
    /// </param>
    /// <returns>
    /// Array of bytes.
    /// </returns>
    public static Byte[] ToBytes(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Byte[] bytes = new Byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        return null;
    }

    /// <summary>
    /// Convert a string into a Boolean.
    /// </summary>
    /// <param name="value">
    /// Value to convert.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to return if the string is not a valid Boolean.
    /// </param>
    /// <returns>
    /// The string as a Boolean if is a valid Boolean, otherwise it will return the default value.
    /// </returns>
    public static bool ToBoolean(this string value, bool defaultValue)
    {
        if (value.ISABoolean())
        {
            return Boolean.Parse(value.ToLower());
        }

        return defaultValue;
    }

    /// <summary>
    /// Convert a string into an integer.
    /// </summary>
    /// <param name="value">
    /// value to convert.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to return if the string is not a valid integer.
    /// </param>
    /// <returns>
    /// String as an integer.
    /// </returns>
    public static int ToInteger(this string value, int defaultValue)
    {
        if (value.IsAnInteger())
        {
            return int.Parse(value);
        }

        return defaultValue;
    }

    /// <summary>
    /// Convert a string formated as a currency into a decimal.
    /// </summary>
    /// <param name="value">
    /// Value to convert.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to return if the string is not a valid decimal.
    /// </param>
    /// <returns>
    /// String as a decimal.
    /// </returns>
    public static decimal ToDecimalFromCurrency(this string value, decimal defaultValue)
    {
        if(!string.IsNullOrWhiteSpace(value))
        {
            return value.Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, 
                string.Empty).ToDecimal(defaultValue);
        }

        return defaultValue;
    }

    /// <summary>
    /// Convert a string into a decimal.
    /// </summary>
    /// <param name="value">
    /// Value to convert.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to return if the string is not a valid decimal.
    /// </param>
    /// <returns>
    /// String as a decimal.
    /// </returns>
    public static decimal ToDecimal(this string value, decimal defaultValue)
    {
        if (value.IsADecimal())
        {
            return decimal.Parse(value);
        }

        return defaultValue;
    }

    /// <summary>
    /// Format the string into the a currency.
    /// </summary>
    /// <param name="value">
    /// Value to format.
    /// </param>
    /// <param name="format">
    /// Currency format.
    /// Default is "C2".
    /// </param>
    /// <param name="culture">
    /// Culture to format.
    /// Default is current culture.
    /// </param>
    /// <returns>
    /// String as formated as a currency.
    /// </returns>
    public static string FormatToCurrency(this string value, string format = "C2", 
        CultureInfo culture = null)
    {
        if (value.IsNotNullOrEmpty())
        {
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
        }

        return value.RemoveNoneNumericValues().ToDecimal(0).ToString(format, culture);
    }

    /// <summary>
    /// Remove none numeric values.
    /// </summary>
    /// <param name="value">
    /// Value to clean.
    /// </param>
    /// <returns>
    /// String with only numeric values like numbers, commas, dots, and minus symbol.
    /// </returns>
    public static string RemoveNoneNumericValues(this string value)
    {            
        if(value.IsNotNullOrEmpty())
        {
            decimal result = 0;
            Decimal.TryParse(value, NumberStyles.Currency, null, out result);

            return result.ToString();
        }

        return value;
    }
}
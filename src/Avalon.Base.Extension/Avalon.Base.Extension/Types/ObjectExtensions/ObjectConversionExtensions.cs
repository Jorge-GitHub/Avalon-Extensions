using System;
using System.Text.Json;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Object conversion extension methods.
/// </summary>
public static class ObjectConversionExtensions
{
    /// <summary>
    /// Convert an object into a string.
    /// </summary>
    /// <param name="value">
    /// Object to convert.
    /// </param>
    /// <param name="defaultValue">
    /// Default value to return if the object is not a valid.
    /// </param>
    /// <returns>
    /// The object as a string or the default value if is not a valid object.
    /// </returns>
    public static string ToString(this object value, string defaultValue)
    {
        if (value.IsNull())
        {
            return defaultValue;
        }

        return Convert.ToString(value).Trim();
    }

    /// <summary>
    /// Convert a null string into an string.
    /// </summary>
    /// <param name="value">
    /// value to convert to an empty string if is null.
    /// </param>
    /// <returns>
    /// Empty string if the string is null.
    /// </returns>
    public static string ToSafeString(this object value)
    {
        if (value.IsNotNull())
        {
            return value.ToString();
        }

        return string.Empty;
    }

    /// <summary>
    /// Serialize an object into JSON format.
    /// </summary>
    /// <param name="value">
    /// Object to serialize.
    /// </param>
    /// <returns>
    /// Object in JSON format.
    /// </returns>
    public static string ToJSON(this object value)
    {
        if (value.IsNotNull())
        {
            return JsonSerializer.Serialize(value);
        }

        return string.Empty;
    }
}
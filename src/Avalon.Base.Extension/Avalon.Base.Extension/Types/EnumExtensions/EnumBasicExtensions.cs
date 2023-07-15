using System.ComponentModel;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Enum basic extensions.
/// </summary>
public static class EnumBasicExtensions
{
    /// <summary>
    /// Get the enum description.
    /// </summary>
    /// <param name="value">
    /// Enum to get the description.
    /// </param>
    /// <returns>
    /// Enum's description.
    /// </returns>
    public static string GetDescription(this Enum value)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])value
           .GetType()
           .GetField(value.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more
    /// enumerated constants to an equivalent enumerated object.
    /// </summary>
    /// <param name="value">
    /// Value to parse into an enum.
    /// </param>
    /// <returns>
    /// An object of type enumType whose value is represented by value.
    /// </returns>
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more
    /// enumerated constants to an equivalent enumerated object.
    /// </summary>
    /// <param name="value">
    /// Value to parse into an enum.
    /// </param>
    /// <param name="defaultValue">
    /// Default value.
    /// </param>
    /// <returns>
    /// An object of type enumType whose value is represented by value.
    /// </returns>
    public static T ToEnum<T>(this string value, T defaultValue)
    {
        if (value.IsNotNullOrEmpty())
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        return defaultValue;
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more
    /// enumerated constants to an equivalent enumerated object.
    /// </summary>
    /// <param name="value">
    /// Object to parse into an enum.
    /// </param>
    /// <returns>
    /// An object of type enumType whose value is represented by value.
    /// </returns>
    public static T ToEnum<T>(this object value)
    {
        return value.ToString().ToEnum<T>();
    }

    /// <summary>
    /// Converts the string representation of the name or numeric value of one or more
    /// enumerated constants to an equivalent enumerated object.
    /// </summary>
    /// <param name="value">
    /// Object to parse into an enum.
    /// </param>
    /// <param name="defaultValue">
    /// Default value.
    /// </param>
    /// <returns>
    /// An object of type enumType whose value is represented by value.
    /// </returns>
    public static T ToEnum<T>(this object value, T defaultValue)
    {
        if (value != null)
        {
            return value.ToString().ToEnum<T>();
        }

        return defaultValue;
    }    
}
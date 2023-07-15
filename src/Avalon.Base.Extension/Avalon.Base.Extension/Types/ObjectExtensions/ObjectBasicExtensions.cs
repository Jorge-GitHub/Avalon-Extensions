using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Basic object extensions.
/// </summary>
public static class ObjectBasicExtensions
{
    /// <summary>
    /// Indicates whether the specified object is null.
    /// </summary>
    /// <param name="value">
    /// Object to test.
    /// </param>
    /// <returns>
    /// True if the object is null.
    /// </returns>
    public static bool IsNull(this object value)
    {
        if (value == null || DBNull.Value.Equals(value) || Convert.IsDBNull(value))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the specified object is null.
    /// </summary>
    /// <param name="value">
    /// Object to test.
    /// </param>
    /// <returns>
    /// True if the object is null.
    /// </returns>
    public static bool IsNotNull(this object value)
    {
        return !value.IsNull();
    }

    /// <summary>
    /// Copy the object.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the object to return.
    /// </typeparam>
    /// <param name="objectToCopy">
    /// Object to be copied.
    /// </param>
    /// <returns>
    /// Copy of the object.
    /// </returns>
    public static T Copy<T>(this object objectToCopy)
    {
        if (objectToCopy.IsNotNull())
        {
            string json = JsonSerializer.Serialize(objectToCopy);
            T copy = JsonSerializer.Deserialize<T>(json);

            return copy;
        }

        return default(T);
    }

    /// <summary>
    /// Convert an object to a lower string if not null.
    /// </summary>
    /// <param name="value">
    /// Object to convert to a lower string.
    /// </param>
    /// <returns>
    /// The lower string representation of the object if not null, otherwise it returns empty.
    /// </returns>
    public static string ToLowerString(this object value)
    {
        if(!value.IsNull())
        {
            return value.ToString().ToLower();
        }
        return string.Empty;
    }

    /// <summary>
    /// Convert an object to an upper case string if not null.
    /// </summary>
    /// <param name="value">
    /// Object to convert to an upper case string.
    /// </param>
    /// <returns>
    /// The upper case string representation of the object if not null, otherwise it returns empty.
    /// </returns>
    public static string ToUpperString(this object value)
    {
        if (!value.IsNull())
        {
            return value.ToString().ToUpper();
        }

        return string.Empty;
    }

    /// <summary>
    /// Determinate whether an object is a dictionary.
    /// </summary>
    /// <param name="objectToCheck">
    /// Object to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether an object is a dictionary.
    /// </returns>
    public static bool IsDictionary(this object objectToCheck)
    {
        if (objectToCheck != null)
        {
            return objectToCheck is IDictionary 
                && objectToCheck.GetType().IsGenericType 
                && objectToCheck.GetType().GetGenericTypeDefinition().IsAssignableFrom(
                    typeof(Dictionary<,>));
        }

        return false;
    }

    /// <summary>
    /// Determinate whether an object is a list.
    /// </summary>
    /// <param name="objectToCheck">
    /// Object to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether an object is a list.
    /// </returns>
    public static bool IsList(this object objectToCheck)
    {
        if (objectToCheck != null)
        {
            return objectToCheck is IList 
                && objectToCheck.GetType().IsGenericType 
                && objectToCheck.GetType().GetGenericTypeDefinition().IsAssignableFrom(
                    typeof(List<>));
        }

        return false;
    }

    /// <summary>
    /// Determinate whether an object is a list or a dictionary.
    /// </summary>
    /// <param name="objectToCheck">
    /// Object to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether an object is a list or a dictionary.
    /// </returns>
    public static bool IsGenericList(this object objectToCheck)
    {
        return objectToCheck.IsDictionary() || objectToCheck.IsList();
    }
}
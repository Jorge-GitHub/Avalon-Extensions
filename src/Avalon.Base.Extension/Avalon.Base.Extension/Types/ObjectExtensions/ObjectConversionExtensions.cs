using System.Text.Json;
using System.Text.Json.Serialization;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// Object conversion extension methods.
/// </summary>
public static class ObjectConversionExtensions
{
    private static readonly JsonSerializerOptions MapJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

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
    public static string ToSafeString(this object value, bool removeBreaks = false)
    {
        if (value.IsNotNull())
        {
            if (removeBreaks)
            {
                return value.ToString().Replace("\r", "").Replace("\n", "");
            }

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
    /// <param name="ignoreNull">Determinate whether to ignore null values when serializing.</param>
    /// <returns>
    /// Object in JSON format.
    /// </returns>
    public static string ToJSON(this object value, 
        bool ignoreNull = true)
    {
        if (value.IsNotNull())
        {
            if (ignoreNull)
            {
                return JsonSerializer.Serialize(value,
                    new JsonSerializerOptions 
                    { 
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
                    });
            }

            return JsonSerializer.Serialize(value);
        }

        return string.Empty;
    }

    /// <summary>
    /// Serialize an object into JSON format ignoring null values.
    /// </summary>
    /// <param name="value">
    /// Object to serialize.
    /// </param>
    /// <returns>
    /// Object in JSON format.
    /// </returns>
    public static string ToJSON(this object value)
    {
        return value.ToJSON(ignoreNull: true);
    }

    /// <summary>
    /// Serialize an object into JSON format using the specified serializer options.
    /// </summary>
    /// <param name="value">
    /// Object to serialize.
    /// </param>
    /// <param name="options">
    /// Serializer options.
    /// </param>
    /// <returns>
    /// Object in JSON format.
    /// </returns>
    public static string ToJSON(this object value,
        JsonSerializerOptions options)
    {
        if (value.IsNotNull())
        {
            return JsonSerializer.Serialize(value, options);
        }

        return string.Empty;
    }

    /// <summary>
    /// Map an object to its DTO version.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? Map<T>(this object? objectToMap)
    {
        return objectToMap.Map<T>(MapJsonOptions);
    }

    /// <summary>
    /// Map an object to its DTO version.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    /// <param name="options">
    /// Serializer options.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? Map<T>(this object? objectToMap,
        JsonSerializerOptions options)
    {
        if (objectToMap is not null)
        {
            return JsonSerializer.Deserialize<T>(
                objectToMap.ToJSON(options), options);
        }

        return default;
    }

    /// <summary>
    /// Sugar coding for mapping an object to its DTO version.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToDTO">
    /// Object to map.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? ToDTO<T>(this object? objectToDTO)
    {
        return objectToDTO.Map<T>(MapJsonOptions);
    }

    /// <summary>
    /// Sugar coding for mapping an object to its DTO version.
    /// </summary>
    /// <typeparam name="T">
    /// Object to return.
    /// </typeparam>
    /// <param name="objectToDTO">
    /// Object to map.
    /// </param>
    /// <param name="options">
    /// Serializer options.
    /// </param>
    /// <returns>
    /// Object in its DTO version.
    /// </returns>
    public static T? ToDTO<T>(this object? objectToDTO,
        JsonSerializerOptions options)
    {
        return objectToDTO.Map<T>(options);
    }
}

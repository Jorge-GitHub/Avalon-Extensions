using System.Text.Json;
using System.Text.Json.Serialization;

namespace Avalon.Base.Extension.Types.ObjectExtensions;

public static class ObjectMapExtensions
{
    private static readonly JsonSerializerOptions MapJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

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
}

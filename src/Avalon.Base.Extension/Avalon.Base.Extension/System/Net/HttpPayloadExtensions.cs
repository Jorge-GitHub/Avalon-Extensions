using System.Text.Json;

namespace Avalon.Base.Extension.System.Net;

/// <summary>
/// Extension methods for converting loosely typed HTTP payloads.
/// </summary>
public static class HttpPayloadExtensions
{
    private static readonly JsonSerializerOptions SerializerOptions =
        new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Convert a loosely typed payload into the requested type.
    /// </summary>
    /// <typeparam name="TResponse">
    /// Type to convert into.
    /// </typeparam>
    /// <param name="data">
    /// Payload to convert. Already the right type, a
    /// <see cref="JsonElement"/>, or anything serializable.
    /// </param>
    /// <returns>
    /// The converted payload, or the type default when there is nothing to
    /// convert.
    /// </returns>
    public static TResponse? ConvertData<TResponse>(this object? data)
    {
        if (data is null)
        {
            return default;
        }

        if (data is TResponse typedData)
        {
            return typedData;
        }

        if (data is JsonElement jsonElement)
        {
            return jsonElement.Deserialize<TResponse>(SerializerOptions);
        }

        return JsonSerializer.Deserialize<TResponse>(
            JsonSerializer.Serialize(data, SerializerOptions),
            SerializerOptions);
    }
}

using Avalon.Base.Extension.Types;
using System.Text.Json;

namespace Avalon.Base.Extension.System.Text;

public static class JsonExtensions
{
    public static JsonElement ToParseJsonElementOrEmptyObject(this string? json)
    {
        return json.ToParseJsonElementOrDefault(defaultJson: "{}");
    }

    public static JsonElement ToParseJsonElementOrDefault(
        this string? json, string defaultJson)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(
                json.IsNullOrEmpty() ? defaultJson : json);

            return document.RootElement.Clone();
        }
        catch (JsonException)
        {
            using JsonDocument document = JsonDocument.Parse(json: defaultJson);

            return document.RootElement.Clone();
        }
    }

    public static string GetArgumentsJson(this JsonElement arguments)
    {
        return arguments.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null
            ? "{}" : arguments.GetRawText();
    }

    public static string? GetPropertyValueAsString(this JsonElement element, 
        string propertyName)
    {
        string? value = null;

        if (element.ValueKind == JsonValueKind.Object 
            && element.TryGetProperty(propertyName, out JsonElement property))
        {
            value = property.ValueKind == JsonValueKind.String
                ? property.GetString() : property.ToString();
        }

        return value;
    }

    public static JsonDocument? TryParsePayload(this string payloadJson)
    {
        JsonDocument? payload = null;

        if (!string.IsNullOrWhiteSpace(payloadJson))
        {
            try
            {
                payload = JsonDocument.Parse(payloadJson);
            }
            catch (JsonException)
            {
                payload = null;
            }
        }

        return payload;
    }
}

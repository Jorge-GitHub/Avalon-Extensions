using Avalon.Base.Extension.Types;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Avalon.Base.Extension.System.Text.JsonTypes;

public static class JsonObjectExtensions
{
    public static void SetJsonString(this JsonObject metadata, string propertyName, string value)
    {
        metadata[metadata.FindPropertyName(propertyName) ?? propertyName] = value;
    }

    public static bool TryParseMetadata(this string? metadataJson, out JsonObject? metadata)
    {
        metadata = null;

        if (string.IsNullOrWhiteSpace(metadataJson))
        {
            return false;
        }

        try
        {
            metadata = JsonNode.Parse(metadataJson) as JsonObject;
        }
        catch (JsonException)
        {
            return false;
        }

        return metadata is not null;
    }

    /// <summary>
    /// Reads a property as text, matching the name without regard to case. A string value
    /// is returned as it is; any other value is returned as its JSON text.
    /// </summary>
    /// <param name="json">
    /// Object to read.
    /// </param>
    /// <param name="propertyName">
    /// Property to read.
    /// </param>
    /// <returns>
    /// The value as text, or null when the property is missing or null.
    /// </returns>
    public static string? GetPropertyValueAsString(this JsonObject json, string propertyName)
    {
        JsonNode? node = json.GetPropertyNode(propertyName);

        if (node is null)
        {
            return null;
        }

        return node is JsonValue value && value.TryGetValue(out string? text)
            ? text : node.ToJsonString();
    }

    /// <summary>
    /// Reads a property as a boolean, matching the name without regard to case. Accepts a
    /// JSON boolean or a string that parses as one.
    /// </summary>
    /// <param name="json">
    /// Object to read.
    /// </param>
    /// <param name="propertyName">
    /// Property to read.
    /// </param>
    /// <returns>
    /// The value, or null when the property is missing or is not a boolean.
    /// </returns>
    public static bool? GetPropertyValueAsBoolean(this JsonObject json, string propertyName)
    {
        if (json.GetPropertyNode(propertyName) is not JsonValue value)
        {
            return null;
        }

        if (value.TryGetValue(out bool flag))
        {
            return flag;
        }

        return value.TryGetValue(out string? text) && bool.TryParse(text, out bool parsed)
            ? parsed : null;
    }

    /// <summary>
    /// Finds a property, matching the name without regard to case.
    /// </summary>
    /// <param name="json">
    /// Object to read.
    /// </param>
    /// <param name="propertyName">
    /// Property to find.
    /// </param>
    /// <returns>
    /// The property node, or null when the property is missing or null.
    /// </returns>
    public static JsonNode? GetPropertyNode(this JsonObject json, string propertyName)
    {
        string? actualName = json.FindPropertyName(propertyName);

        return actualName is null ? null : json[actualName];
    }

    private static string? FindPropertyName(this JsonObject json, string propertyName)
    {
        return json.FirstOrDefault(candidate =>
            candidate.Key.EqualsIgnoreCase(propertyName)).Key;
    }
}

using Avalon.Base.Extension.Types;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Avalon.Base.Extension.System.Text.JsonTypes;

public static class JsonObjectExtensions
{
    public static void SetJsonString(this JsonObject metadata, string propertyName, string value)
    {
        string actualPropertyName = metadata
            .FirstOrDefault(candidate =>
                candidate.Key.EqualsIgnoreCase(propertyName)).Key ?? propertyName;

        metadata[actualPropertyName] = value;
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
}

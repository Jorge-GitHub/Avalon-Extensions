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
        this string? json,
        string defaultJson)
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
}

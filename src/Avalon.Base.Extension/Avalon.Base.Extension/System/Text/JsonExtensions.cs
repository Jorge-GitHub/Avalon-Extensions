using Avalon.Base.Extension.Types;
using System.Text.Json;

namespace Avalon.Base.Extension.System.Text;

public static class JsonExtensions
{
    public static JsonElement ToParseJsonElementOrEmptyObject(this string? json)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(
                json.IsNullOrEmpty() ? "{}" : json);

            return document.RootElement.Clone();
        }
        catch (JsonException)
        {
            using JsonDocument document = JsonDocument.Parse(json: "{}");

            return document.RootElement.Clone();
        }
    }
}

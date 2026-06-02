using Avalon.Base.Extension.Types;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Avalon.Base.Extension.System.Text.JsonTypes;

public static  class JsonElementExtensions
{
    private const string RouteParameterRegexPattern = @"\{(?<name>[A-Za-z0-9_]+)\}";
    private const string RouteParameterNameGroup = "name";
    public const string MissingRouteArgumentValue = "";
    private static readonly Regex RouteParameterPattern = new(
        RouteParameterRegexPattern);

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

    public static string ApplyRouteArguments(this JsonElement arguments, string route)
    {
        return RouteParameterPattern.Replace(
            route,
            match =>
            {
                string name = match.Groups[
                    RouteParameterNameGroup].Value;
                string? value = arguments.GetPropertyValueAsString(name);

                return WebUtility.UrlEncode(
                    value ?? MissingRouteArgumentValue);
            });
    }
}

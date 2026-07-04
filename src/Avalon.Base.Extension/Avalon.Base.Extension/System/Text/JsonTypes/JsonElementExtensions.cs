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

    public static JsonElement ToParseJsonObjectElementOrEmptyObject(this string? json)
    {
        JsonElement element = json.ToParseJsonElementOrEmptyObject();

        if (element.ValueKind != JsonValueKind.Object)
        {
            element = "{}".ToParseJsonElementOrEmptyObject();
        }

        return element;
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
            && JsonElementExtensions.TryGetProperty(
                element, propertyName, out JsonElement property))
        {
            value = property.ValueKind == JsonValueKind.String
                ? property.GetString() : property.ToString();
        }

        return value;
    }

    public static bool? GetPropertyValueAsBoolean(this JsonElement element,
        string propertyName)
    {
        if (element.ValueKind == JsonValueKind.Object
            && JsonElementExtensions.TryGetProperty(
                element, propertyName, out JsonElement property))
        {
            if (property.ValueKind is JsonValueKind.True or JsonValueKind.False)
            {
                return property.GetBoolean();
            }

            if (property.ValueKind == JsonValueKind.String)
            {
                string? value = property.GetString();

                return bool.TryParse(value, out bool parsedValue)
                    ? parsedValue : null;
            }
        }

        return null;
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

    public static bool TryGetProperty(this JsonElement metadata, string propertyName, 
        out JsonElement property)
    {
        property = default;

        foreach (JsonProperty item in metadata.EnumerateObject())
        {
            if (string.Equals(item.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            {
                property = item.Value;
                return true;
            }
        }

        return false;
    }

    public static DateTimeOffset? GetPropertyValueAsDateTimeOffset(this JsonElement metadata, string propertyName)
    {
        DateTimeOffset? value = null;
        string dateText = metadata.GetPropertyValueAsString(propertyName) ?? string.Empty;

        if (DateTimeOffset.TryParse(dateText, out DateTimeOffset parsedDate))
        {
            value = parsedDate;
        }

        return value;
    }

    public static string ReadArgumentsJson(this JsonElement metadata, string propertyName,
        string defaultValue = "{}")
    {
        string argumentsJson = defaultValue;

        if (JsonElementExtensions.TryGetProperty(metadata, propertyName, out JsonElement property))
        {
            if (property.ValueKind == JsonValueKind.String)
            {
                string? propertyValue = property.GetString();
                JsonElement parsedArguments = propertyValue.ToParseJsonObjectElementOrEmptyObject();
                argumentsJson = parsedArguments.GetArgumentsJson();
            }
            else if (property.ValueKind == JsonValueKind.Object)
            {
                argumentsJson = property.GetArgumentsJson();
            }
        }

        return argumentsJson;
    }
}

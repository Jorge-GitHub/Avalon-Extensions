using System.Text.Json;

namespace Avalon.Base.Extension.System.Text.JsonTypes;

public static class JsonDocumentExtensions
{
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

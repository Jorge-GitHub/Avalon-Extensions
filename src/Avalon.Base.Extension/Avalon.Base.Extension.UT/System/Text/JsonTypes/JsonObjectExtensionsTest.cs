using Avalon.Base.Extension.System.Text.JsonTypes;
using System.Text.Json.Nodes;

namespace Avalon.Base.Extension.UT.System.Text.JsonTypes;

[TestClass]
public class JsonObjectExtensionsTest
{
    [TestMethod]
    public void SetJsonString_WithExistingProperty_UpdatesExistingPropertyName()
    {
        JsonObject metadata = JsonNode.Parse("{\"Status\":\"Pending\"}")!.AsObject();

        metadata.SetJsonString("status", "Completed");

        Assert.AreEqual("Completed", metadata["Status"]!.GetValue<string>());
        Assert.IsFalse(metadata.ContainsKey("status"));
    }

    [TestMethod]
    public void SetJsonString_WithMissingProperty_AddsProperty()
    {
        JsonObject metadata = [];

        metadata.SetJsonString("status", "Completed");

        Assert.AreEqual("Completed", metadata["status"]!.GetValue<string>());
    }

    [TestMethod]
    public void TryParseMetadata_WithObjectJson_ReturnsJsonObject()
    {
        bool parsed = "{\"status\":\"Pending\"}".TryParseMetadata(out JsonObject? metadata);

        Assert.IsTrue(parsed);
        Assert.IsNotNull(metadata);
        Assert.AreEqual("Pending", metadata["status"]!.GetValue<string>());
    }

    [TestMethod]
    public void TryParseMetadata_WithInvalidOrNonObjectJson_ReturnsFalse()
    {
        Assert.IsFalse(((string?)null).TryParseMetadata(out JsonObject? nullMetadata));
        Assert.IsNull(nullMetadata);

        Assert.IsFalse("not-json".TryParseMetadata(out JsonObject? invalidMetadata));
        Assert.IsNull(invalidMetadata);

        Assert.IsFalse("[\"not\", \"object\"]".TryParseMetadata(out JsonObject? arrayMetadata));
        Assert.IsNull(arrayMetadata);
    }

    [TestMethod]
    public void GetPropertyValueAsString_MatchesNameIgnoringCase_AndReturnsJsonForNonStrings()
    {
        JsonObject json = JsonNode.Parse(
            "{\"ToolName\":\"read_page\",\"count\":3,\"data\":{\"a\":1}}")!.AsObject();

        Assert.AreEqual("read_page", json.GetPropertyValueAsString("toolname"));
        Assert.AreEqual("3", json.GetPropertyValueAsString("count"));
        Assert.AreEqual("{\"a\":1}", json.GetPropertyValueAsString("data"));
        Assert.IsNull(json.GetPropertyValueAsString("missing"));
    }

    [TestMethod]
    public void GetPropertyValueAsBoolean_WithBooleanStringAndOtherValues_ReturnsParsedOrNull()
    {
        JsonObject json = JsonNode.Parse(
            "{\"Shortened\":true,\"text\":\"false\",\"number\":1}")!.AsObject();

        Assert.IsTrue(json.GetPropertyValueAsBoolean("shortened"));
        Assert.IsFalse(json.GetPropertyValueAsBoolean("text"));
        Assert.IsNull(json.GetPropertyValueAsBoolean("number"));
        Assert.IsNull(json.GetPropertyValueAsBoolean("missing"));
    }

    [TestMethod]
    public void GetPropertyNode_MatchesNameIgnoringCase()
    {
        JsonObject json = JsonNode.Parse("{\"Data\":{\"a\":1}}")!.AsObject();

        Assert.IsNotNull(json.GetPropertyNode("data"));
        Assert.IsNull(json.GetPropertyNode("other"));
    }
}

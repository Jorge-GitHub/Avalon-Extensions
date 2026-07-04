using Avalon.Base.Extension.System.Text.JsonTypes;
using System.Text.Json;

namespace Avalon.Base.Extension.UT.System.Text.JsonTypes;

[TestClass]
public class JsonElementExtensionsTest
{
    [TestMethod]
    public void ToParseJsonObjectElementOrEmptyObject_WithObject_ReturnsObject()
    {
        JsonElement element = "{\"path\":\"C:\\\\Temp\\\\note.txt\"}"
            .ToParseJsonObjectElementOrEmptyObject();

        Assert.AreEqual(JsonValueKind.Object, element.ValueKind);
        Assert.AreEqual("C:\\Temp\\note.txt", element.GetProperty("path").GetString());
    }

    [TestMethod]
    public void ToParseJsonObjectElementOrEmptyObject_WithArray_ReturnsEmptyObject()
    {
        JsonElement element = "[\"not\", \"object\"]"
            .ToParseJsonObjectElementOrEmptyObject();

        Assert.AreEqual(JsonValueKind.Object, element.ValueKind);
        Assert.AreEqual(0, element.EnumerateObject().Count());
    }

    [TestMethod]
    public void ToParseJsonObjectElementOrEmptyObject_WithInvalidJson_ReturnsEmptyObject()
    {
        JsonElement element = "not-json".ToParseJsonObjectElementOrEmptyObject();

        Assert.AreEqual(JsonValueKind.Object, element.ValueKind);
        Assert.AreEqual(0, element.EnumerateObject().Count());
    }

    [TestMethod]
    public void GetPropertyValueAsBoolean_WithBooleanAndStringValues_ReturnsParsedValues()
    {
        JsonElement element = "{\"IsActive\":true,\"isDeleted\":\"false\",\"invalid\":\"no\"}"
            .ToParseJsonObjectElementOrEmptyObject();

        Assert.AreEqual(true, element.GetPropertyValueAsBoolean("isActive"));
        Assert.AreEqual(false, element.GetPropertyValueAsBoolean("IsDeleted"));
        Assert.IsNull(element.GetPropertyValueAsBoolean("invalid"));
        Assert.IsNull(element.GetPropertyValueAsBoolean("missing"));
    }
}

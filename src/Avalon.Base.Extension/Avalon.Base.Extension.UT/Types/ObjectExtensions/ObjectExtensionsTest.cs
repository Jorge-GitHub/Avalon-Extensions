using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.BooleanExtensions;

namespace Avalon.Base.Extension.UT.Types.ObjectExtensions;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class ObjectExtensionsTest
{
    [TestMethod]
    public void TestToJSON()
    {
        TestClass test = new TestClass()
        {
            Name = "Test",
            Description = "Simple Test"
        };
        string json = test.ToJSON();
        Assert.IsTrue(json.IsNotNullOrEmpty());
    }
}

/// <summary>
/// Demo test class for testing purposes.
/// </summary>
internal class TestClass
{
    public string Name { get; set; }
    public string Description { get; set; }
}

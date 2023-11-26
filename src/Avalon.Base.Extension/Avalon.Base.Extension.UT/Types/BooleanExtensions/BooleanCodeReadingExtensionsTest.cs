using Avalon.Base.Extension.Types.BooleanExtensions;

namespace Avalon.Base.Extension.UT.Types.BooleanExtensions;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class BooleanCodeReadingExtensionsTest
{
    /// <summary>
    /// Test IsTrue extension.
    /// </summary>
    [TestMethod]
    public void TestIsTrue()
    {
        bool trueValue = true;
        bool falseValue = false;
        Assert.IsTrue(trueValue.IsTrue());
        Assert.IsFalse(falseValue.IsTrue());
    }

    /// <summary>
    /// Test IsFalse extension.
    /// </summary>
    [TestMethod]
    public void TestIsFalse()
    {
        bool trueValue = true;
        bool falseValue = false;
        Assert.IsFalse(trueValue.IsFalse());
        Assert.IsTrue(falseValue.IsFalse());
    }

    /// <summary>
    /// Test IsNotTrue extension.
    /// </summary>
    [TestMethod]
    public void TestIsNotTrue()
    {
        bool trueValue = true;
        bool falseValue = false;
        Assert.IsFalse(trueValue.IsNotTrue());
        Assert.IsTrue(falseValue.IsNotTrue());
    }
}
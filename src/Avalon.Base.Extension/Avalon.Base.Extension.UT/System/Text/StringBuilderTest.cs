using Avalon.Base.Extension.System.Text;
using Avalon.Base.Extension.Types.StringExtensions;
using System.Text;

namespace Avalon.Base.Extension.UT.System.Text;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class StringBuilderTest
{
    /// <summary>
    /// Test RemoveBetweenTags extension.
    /// </summary>
    [TestMethod]
    public void TestRemoveBetweenTags()
    {
        StringBuilder testValue = new StringBuilder("*|FirstText|*Content To Remove*|SecondText|*");
        testValue.RemoveBetweenTags("*|FirstText|*", "*|SecondText|*");
        Assert.IsFalse(testValue.ToString().Contains("Content To Remove"));
        Assert.IsTrue(testValue.ToString().Contains("*|FirstText|**|SecondText|*"));
    }

    /// <summary>
    /// Test LastIndexOf extension.
    /// </summary>
    [TestMethod]
    public void TestLastIndexOf()
    {
        StringBuilder testValue = new StringBuilder("Hello World!");
        Assert.IsTrue(testValue.LastIndexOf("Hello") > -1);
        Assert.IsTrue(testValue.LastIndexOf("Bye") == -1);
    }
}
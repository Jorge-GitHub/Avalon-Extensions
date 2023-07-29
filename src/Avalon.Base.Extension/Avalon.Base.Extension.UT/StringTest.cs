using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.StringExtensions;

namespace Avalon.Base.Extension.UT;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class StringTest
{
    [TestMethod]
    public void TestSplitByBlocks()
    {
        string value = "Hello World";
        List<string> values = value.SplitByBlocks(2);
        int lenght = value.SplitByBlocks(2).Count;
        Assert.AreEqual(lenght, 6);
        lenght = string.Empty.SplitByBlocks(2).Count;
        Assert.AreEqual(lenght, 0);
        lenght = value.SplitByBlocks(4).Count;
        Assert.AreEqual(lenght, 3);
    }

    [TestMethod]
    public void TestNotEquals()
    {
        string test1 = null; 
        string test2 = null;
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test1 = string.Empty;
        test2 = string.Empty;
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test1 = "Hello World";
        Assert.IsTrue(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test2 = "Hello World";
        Assert.IsFalse(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
        test2 = "Hola Mundo";
        Assert.IsTrue(test1.NotEquals(test2, StringComparison.OrdinalIgnoreCase));
    }

	[TestMethod]
	public void TestEmail()
    {
        Assert.IsTrue("jamesbond@gmail.com".IsAValidEmail());
		Assert.IsFalse("jamesbond".IsAValidEmail());
	}
}
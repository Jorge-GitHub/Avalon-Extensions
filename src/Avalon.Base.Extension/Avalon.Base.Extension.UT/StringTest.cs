using Avalon.Base.Extension.Types.StringExtensions;

namespace Avalon.Base.Extension.UT;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class StringTest
{
    [TestMethod]
    public void TestSplitByChunks()
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
}
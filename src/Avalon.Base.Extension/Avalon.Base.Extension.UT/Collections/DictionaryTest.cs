using Avalon.Base.Extension.Collections;

namespace Avalon.Base.Extension.UT.Collections;

/// <summary>
/// Dictionary tests.
/// </summary>
[TestClass]
public class DictionaryTest
{
    [TestMethod]
    public void TestAddRange()
    {
        Dictionary<string, string[]> dictionaryTarget = new();
        dictionaryTarget["UniqueElement"] = new[] { "Unique element." };
        dictionaryTarget["DuplicateElement"] = new[] { "Duplicate element." };

        Dictionary<string, string[]> dictionarySource = new();
        dictionarySource["UniqueSourceElement"] = new[] { "Unique source element." };
        dictionarySource["DuplicateElement"] = new[] { "Duplicate source element." };

        dictionaryTarget.AddRange(dictionarySource);

        Assert.IsTrue(dictionaryTarget.Count == 3);
    }

    [TestMethod]
    public void TestAddRangeNotUpdateDuplicate()
    {
        Dictionary<string, string[]> dictionaryTarget = new();
        dictionaryTarget["UniqueElement"] = new[] { "Unique element." };
        dictionaryTarget["DuplicateElement"] = new[] { "Duplicate element." };

        Dictionary<string, string[]> dictionarySource = new();
        dictionarySource["UniqueSourceElement"] = new[] { "Unique source element." };
        dictionarySource["DuplicateElement"] = new[] { "Duplicate source element." };

        dictionaryTarget.AddRange(dictionarySource, updateValue: false);

        Assert.AreEqual(
            "Duplicate element.",
            dictionaryTarget["DuplicateElement"][0]);
    }

    [TestMethod]
    public void TestAddRangeThrowExceptionOnDuplicate()
    {
        Dictionary<string, string[]> dictionaryTarget = new();
        dictionaryTarget["UniqueElement"] = new[] { "Unique element." };
        dictionaryTarget["DuplicateElement"] = new[] { "Duplicate element." };

        Dictionary<string, string[]> dictionarySource = new();
        dictionarySource["UniqueSourceElement"] = new[] { "Unique source element." };
        dictionarySource["DuplicateElement"] = new[] { "Duplicate element." };

        Assert.ThrowsExactly<InvalidOperationException>(() =>
           dictionaryTarget.AddRange(
            dictionarySource, updateValue: false, throwExceptionOnDuplicate: true));
    }
}

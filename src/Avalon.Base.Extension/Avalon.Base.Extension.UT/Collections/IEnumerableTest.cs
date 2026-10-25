using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using System.Text;

namespace Avalon.Base.Extension.UT.Collections;

/// <summary>
/// String tests.
/// </summary>
[TestClass]
public class IEnumerableTest
{
    [TestMethod]
    public void TestToStringStringBuilder()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        StringBuilder builder = list.ToStringStringBuilder();
        Assert.IsTrue(builder.ToString().IsNotNullOrEmpty());
    }

    [TestMethod]
    public void TestListSpanRunsAction()
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        int result = 0;

        list.Span(item => result += item);

        Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void TestSpanDoesNothingWhenListOrActionIsNull()
    {
        bool actionCalled = false;
        List<int>? list = null;
        IEnumerable<int>? enumerable = null;

        list.Span(_ => actionCalled = true);
        enumerable.Span(_ => actionCalled = true);
        new List<int> { 1 }.Span(null);

        Assert.IsFalse(actionCalled);
    }

    [TestMethod]
    public void TestAsSpanReturnsListSpan()
    {
        List<int> list = new List<int>();
        list.Add(1);

        Span<int> items = list.AsSpan();
        items[0] = 2;

        Assert.AreEqual(2, list[0]);
    }
}

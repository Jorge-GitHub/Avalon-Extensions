using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;
using System.Text;

namespace Avalon.Base.Extension.UT;

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
}
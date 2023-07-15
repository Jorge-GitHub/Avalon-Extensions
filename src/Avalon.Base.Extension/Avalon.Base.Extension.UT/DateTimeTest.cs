using Avalon.Base.Extension.Types.DateTimeExtensions;

namespace Avalon.Base.Extension.UT;

/// <summary>
/// Date time extensions.
/// </summary>
[TestClass]
public class DateTimeTest
{
    /// <summary>
    /// Test the day of the week extensions.
    /// </summary>
    [TestMethod]
    public void TestDayOfTheWeek()
    {
        DateTime date = new DateTime(2023, 07, 14);
        Assert.IsTrue(date.IsFriday());
        Assert.IsFalse(date.IsMonday());
        Assert.IsFalse(date.IsWeekend());
        Assert.IsTrue(date.IsWeekday());
    }
}
using Avalon.Base.Extension.Types;
using Avalon.Base.Extension.Types.DateTimeExtensions;

namespace Avalon.Base.Extension.UT.Types.DateTimeExtensions;

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

    /// <summary>
    /// Test the day time conversions.
    /// </summary>
    [TestMethod]
    public void TestDateTimeConvertion()
    {
        DateTime date = "07/14/2023".ToDateTime();
        DateTime now = DateTime.Now;
        DateTime invalidDate = "testing this".ToDateTime(now);
        Assert.IsTrue(invalidDate.Equals(now));
    }
}
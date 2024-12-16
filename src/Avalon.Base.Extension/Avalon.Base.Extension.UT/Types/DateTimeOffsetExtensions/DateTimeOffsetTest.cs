using Avalon.Base.Extension.Types.DateTimeOffsetExtensions;

namespace Avalon.Base.Extension.UT.Types.DateTimeOffsetExtensions;

/// <summary>
/// Contains unit tests for verifying the behavior of 
/// <see cref="DateTimeOffsetBasicExtensions"/> methods.
/// </summary>
[TestClass]
public class DateTimeOffsetTest
{
    /// <summary>
    /// Tests the <see cref="DateTimeOffsetBasicExtensions.FirstDayOfMonth"/> 
    /// method to ensure it correctly calculates the first day of 
    /// the month for a given <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <remarks>
    /// Verifies that the year and month remain unchanged and that the day is set to 1 
    /// in the returned <see cref="DateTimeOffset"/>.
    /// </remarks>
    [TestMethod]
    public void TestFirstDayOfMonth()
    {
        DateTime currentTime = DateTime.UtcNow;
        DateTimeOffset dateTimeOffsetToTest = DateTimeOffset.UtcNow;
        DateTimeOffset DateTimeOffsetToTestAgainst = dateTimeOffsetToTest.FirstDayOfMonth();
        Assert.AreEqual(dateTimeOffsetToTest.Year, DateTimeOffsetToTestAgainst.Year);
        Assert.AreEqual(dateTimeOffsetToTest.Month, DateTimeOffsetToTestAgainst.Month);
        Assert.AreEqual(DateTimeOffsetToTestAgainst.Day, 1);
    }
}

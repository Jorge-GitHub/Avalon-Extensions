using Avalon.Base.Extension.Types.DateTimeOffsetExtensions;

namespace Avalon.Base.Extension.UT.Types.DateTimeOffsetExtensions;

/// <summary>
/// Contains unit tests for verifying the behavior of
/// <see cref="DateTimeOffsetConversionExtensions"/> methods.
/// </summary>
[TestClass]
public class DateTimeOffsetConversionTest
{
    /// <summary>
    /// Offsets are built explicitly rather than read from the host so the
    /// tests behave the same on a UTC build agent and a UTC-5 workstation.
    /// </summary>
    private static readonly DateTimeOffset ValueFromDataSet =
        new(2026, 7, 23, 4, 3, 48, 228, TimeSpan.FromHours(-5));

    /// <summary>
    /// Verifies the clock reading survives and the offset becomes UTC.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetKeepsClockAndAnchorsToUtc()
    {
        DateTimeOffset normalized = ValueFromDataSet.ToUtcFromDataSet();

        Assert.AreEqual(TimeSpan.Zero, normalized.Offset);
        Assert.AreEqual(
            new DateTimeOffset(2026, 7, 23, 4, 3, 48, 228, TimeSpan.Zero),
            normalized);
    }

    /// <summary>
    /// Verifies the instant is re-labelled, not converted. ToUniversalTime()
    /// would move it to 09:03Z and preserve the corruption.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetDoesNotConvertTheInstant()
    {
        DateTimeOffset normalized = ValueFromDataSet.ToUtcFromDataSet();

        Assert.AreEqual(4, normalized.Hour);
        Assert.AreNotEqual(ValueFromDataSet.ToUniversalTime(), normalized);
    }

    /// <summary>
    /// Verifies a value already anchored at UTC is returned untouched.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetLeavesUtcValueUnchanged()
    {
        DateTimeOffset alreadyUtc =
            new(2026, 7, 23, 4, 3, 48, 228, TimeSpan.Zero);

        Assert.AreEqual(alreadyUtc, alreadyUtc.ToUtcFromDataSet());
    }

    /// <summary>
    /// Verifies applying the conversion twice is the same as applying it once.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetIsIdempotent()
    {
        DateTimeOffset once = ValueFromDataSet.ToUtcFromDataSet();

        Assert.AreEqual(once, once.ToUtcFromDataSet());
    }

    /// <summary>
    /// Verifies the nullable overload propagates null.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetPropagatesNull()
    {
        DateTimeOffset? missing = null;

        Assert.IsNull(missing.ToUtcFromDataSet());
    }

    /// <summary>
    /// Verifies the nullable overload converts a value.
    /// </summary>
    [TestMethod]
    public void TestToUtcFromDataSetConvertsNullableValue()
    {
        DateTimeOffset? normalized = ((DateTimeOffset?)ValueFromDataSet)
            .ToUtcFromDataSet();

        Assert.AreEqual(
            new DateTimeOffset(2026, 7, 23, 4, 3, 48, 228, TimeSpan.Zero),
            normalized);
    }
}

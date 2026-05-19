using Avalon.Base.Extension.Data;
using System.Data;

namespace Avalon.Base.Extension.UT.Data;

/// <summary>
/// DataRow extension tests.
/// </summary>
[TestClass]
public class DataRowExtensionsTest
{
    /// <summary>
    /// Test the ToObject extension with DateTimeOffset values.
    /// </summary>
    [TestMethod]
    public void TestToObjectWithDateTimeOffset()
    {
        DateTimeOffset expectedRequiredDate = new DateTimeOffset(2026, 5, 19, 10, 30, 0, TimeSpan.FromHours(-5));
        DateTimeOffset expectedOptionalDate = new DateTimeOffset(2026, 5, 20, 11, 45, 0, TimeSpan.FromHours(-5));

        DataTable table = new DataTable();
        table.Columns.Add("RequiredDate");
        table.Columns.Add("OptionalDate");

        DataRow row = table.NewRow();
        row["RequiredDate"] = expectedRequiredDate.ToString("O");
        row["OptionalDate"] = expectedOptionalDate.ToString("O");
        table.Rows.Add(row);

        DataRowDateTimeOffsetModel result = row.ToObject<DataRowDateTimeOffsetModel>();

        Assert.AreEqual(expectedRequiredDate, result.RequiredDate);
        Assert.AreEqual(expectedOptionalDate, result.OptionalDate);
    }

    /// <summary>
    /// Test the ToObject extension leaves nullable DateTimeOffset null for DBNull values.
    /// </summary>
    [TestMethod]
    public void TestToObjectWithNullableDateTimeOffsetDBNull()
    {
        DataTable table = new DataTable();
        table.Columns.Add("OptionalDate");

        DataRow row = table.NewRow();
        row["OptionalDate"] = DBNull.Value;
        table.Rows.Add(row);

        DataRowDateTimeOffsetModel result = row.ToObject<DataRowDateTimeOffsetModel>();

        Assert.IsNull(result.OptionalDate);
    }

    private class DataRowDateTimeOffsetModel
    {
        public DateTimeOffset RequiredDate { get; set; }

        public DateTimeOffset? OptionalDate { get; set; }
    }
}

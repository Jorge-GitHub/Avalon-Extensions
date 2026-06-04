using Avalon.Base.Extension.Types;
using System.Data;

namespace Avalon.Base.Extension.Data.Conversion;

public static class DataRowConversionExtensions
{
    public static string ToString(this DataRow row, string columnName, string defaultValue = "")
    {
        return !row.Table.Columns.Contains(columnName) || row[columnName] is DBNull
            ? defaultValue : Convert.ToString(row[columnName]) ?? defaultValue;
    }

    public static int ToInteger32(this DataRow row, string columnName, int defaultValue = 0)
    {
        return !row.Table.Columns.Contains(columnName) || row[columnName] is DBNull
            ? defaultValue : Convert.ToInt32(row[columnName]);
    }

    public static bool ToBoolean(this DataRow row, string columnName, bool defaultValue = false)
    {
        return !row.Table.Columns.Contains(columnName) || row[columnName] is DBNull
            ? defaultValue : Convert.ToBoolean(row[columnName]);
    }

    public static TEnum ToEnum<TEnum>(
        this DataRow row, string columnName, TEnum defaultValue = default)
        where TEnum : struct, Enum
    {
        return row.ToString(columnName).ToEnumSafe(defaultValue);
    }
}

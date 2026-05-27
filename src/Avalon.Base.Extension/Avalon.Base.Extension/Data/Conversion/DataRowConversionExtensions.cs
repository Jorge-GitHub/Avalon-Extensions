using Avalon.Base.Extension.Collections;
using System.Data;

namespace Avalon.Base.Extension.Data.Conversion;

public static class DataRowConversionExtensions
{
    public static string ToString(this DataRow row, string columnName, string defaultValue = "")
    {
        return row[columnName] is DBNull
            ? defaultValue : Convert.ToString(row[columnName]) ?? defaultValue;
    }

    public static int ToInteger32(this DataRow row, string columnName, int defaultValue = 0)
    {
        return row[columnName] is DBNull
            ? defaultValue : Convert.ToInt32(row[columnName]);
    }

    public static bool ToBoolean(this DataRow row, string columnName, bool defaultValue = false)
    {
        return row[columnName] is DBNull
            ? defaultValue : Convert.ToBoolean(row[columnName]);
    }

    public static bool Contains(this DataRow row, IReadOnlyCollection<string> columnNames)
    {
        if (columnNames.HasElements())
        {
            foreach (string columnName in columnNames)
            {
                if(row.Table.Columns.Contains(columnName))
                {
                    return true;
                }
            }
        }

        return false;
    }
}

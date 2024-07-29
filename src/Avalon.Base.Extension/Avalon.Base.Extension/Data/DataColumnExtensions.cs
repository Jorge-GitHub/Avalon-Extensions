using Avalon.Base.Extension.Types;
using System.Data;

namespace Avalon.Base.Extension.Data;

public static class DataColumnExtensions
{
    /// <summary>
    /// Get the column's value and if the value is null it will returns an empty string.
    /// </summary>
    /// <param name="column">
    /// Data column to get the value from.
    /// </param>
    /// <param name="row">
    /// Data columns' row.
    /// </param>
    /// <returns>
    /// Data column value as a string.
    /// </returns>
    public static string DataColumnToSafeString(this DataColumn column, DataRow row)
    {
        if (row != null && row[column] != null && row[column] != DBNull.Value) {
            return row[column].ToSafeString();
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the column's value and if is empty returns the default value.
    /// </summary>
    /// <param name="column">
    /// Data column to get the value from.
    /// </param>
    /// <param name="row">
    /// Data columns' row.
    /// </param>
    /// <returns>
    /// Data column value as an integer.
    /// </returns>
    public static int DataColumnToSafeInteger(this DataColumn column, 
        DataRow row, int defaultValue = 0)
    {
        return column.DataColumnToSafeString(row).ToInteger(defaultValue);
    }
}
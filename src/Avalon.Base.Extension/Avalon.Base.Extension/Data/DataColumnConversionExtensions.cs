using Avalon.Base.Extension.Types;
using System.Data;

namespace Avalon.Base.Extension.Data;

/// <summary>
/// Data column conversion extensions.
/// </summary>
public static class DataColumnConversionExtensions
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
    public static string DataColumnToSafeString(this DataRow row, string columnName)
    {
        if (row != null && row[columnName] != null && row[columnName] != DBNull.Value)
        {
            return row[columnName].ToSafeString();
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
    public static int DataColumnToSafeInteger(this DataRow row,
        string columnName, int defaultValue = 0)
    {
        return row.DataColumnToSafeString(columnName).ToInteger(defaultValue);
    }
}
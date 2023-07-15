using System.Data;

namespace Avalon.Base.Extension.Data;

/// <summary>
/// DataRow extensions.
/// </summary>
public static class DataRowExtensions
{
    /// <summary>
    /// Check if a DataRow collections has records or not.
    /// </summary>
    /// <param name="rows">
    /// DataRow collections to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether a DataRow collections has records or not.
    /// </returns>
    public static bool HasRecords(this DataRowCollection rows)
    {
        return rows != null && rows.Count > 0;
    }
}
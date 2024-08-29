using System.Data;

namespace Avalon.Base.Extension.Data;

/// <summary>
/// DataTable extensions.
/// </summary>
public static class DataTableExtensions
{
    /// <summary>
    /// Determinate whether the DataSet contains data.
    /// </summary>
    /// <param name="table">
    /// DataSet to test.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the DataSet contains data.
    /// </returns>
    public static bool HasData(this DataTable table)
    {
        if (table != null && table.Rows != null && table.Rows.Count > 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Convert a data table rows into a list of objects.
    /// </summary>
    /// <typeparam name="T">
    /// Object type to convert into.
    /// </typeparam>
    /// <param name="table">
    /// DataTable containing the data to convert from.
    /// </param>
    /// <returns>
    /// List of Objects that were populated with the DataTable.
    /// </returns>
    public static List<T> ToObjects<T>(this DataTable table)
    {
        List<T> objects = new List<T>();
        if (table.HasData())
        {
            foreach (DataRow row in table.Rows)
            {
                objects.Add(row.ToObject<T>());
            }
        }

        return objects;
    }
}
using System.Data;

namespace Avalon.Base.Extension.Data;

/// <summary>
/// DataSet extensions.
/// </summary>
public static class DataSetExtensions
{
    /// <summary>
    /// Determinate whether the DataSet contains data.
    /// </summary>
    /// <param name="data">
    /// DataSet to test.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the DataSet contains data.
    /// </returns>
    public static bool HasData(this DataSet data)
    {
        if (data != null && data.Tables != null && data.Tables.Count > 0)
        {
            foreach (DataTable table in data.Tables)
            {
                if (table.HasData())
                {
                    return true;
                }
            }
        }

        return false;
    }
}
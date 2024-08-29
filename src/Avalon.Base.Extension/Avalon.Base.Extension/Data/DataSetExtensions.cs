using Avalon.Base.Extension.Types;
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

    /// <summary>
    /// Get a DataTable by table's name.
    /// </summary>
    /// <param name="data">
    /// DataSet containing the DataTable.
    /// </param>
    /// <param name="tableName">
    /// Table's name.
    /// </param>
    /// <returns>
    /// DataTable.
    /// </returns>
    public static DataTable GetDataTableByNameSafe(this DataSet data, string tableName)
    {
        if (data.HasData() && data.Tables.Contains(tableName)
            && tableName.IsNotNullOrEmpty())
        {
            return data.Tables[tableName];
        }

        return null;
    }

    /// <summary>
    /// Get total number of records on the data table.
    /// </summary>
    /// <param name="dataSet">
    /// Dataset.
    /// </param>
    /// <param name="tableName">
    /// Table's name to look for the value.
    /// </param>
    /// <returns>
    /// Total number of records on the data table.
    /// </returns>
    public static int GetNumberOfRowsOnTable(this DataSet dataSet, string tableName)
    {
        DataTable data = dataSet.GetDataTableByNameSafe(tableName);
        if (data.HasData())
        {
            return data.Rows.Count;
        }

        return 0;
    }

    /// <summary>
    /// Get the first value on the data table.
    /// Similar to calling data.Rows[0][0].
    /// </summary>
    /// <param name="dataSet">
    /// Dataset.
    /// </param>
    /// <param name="tableName">
    /// Table's name to look for the value.
    /// </param>
    /// <returns>
    /// Total number of records on the data table.
    /// </returns>
    public static string GetFirstValueOnTable(this DataSet dataSet, string tableName)
    {
        DataTable data = dataSet.GetDataTableByNameSafe(tableName);
        if (data.HasData())
        {
            return data.Rows[0][0].ToSafeString();
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the first value on the first data table.
    /// Similar to calling data.Rows[0][0].
    /// </summary>
    /// <param name="dataSet">
    /// Dataset.
    /// </param>
    /// <returns>
    /// Total number of records on the data table.
    /// </returns>
    public static string GetFirstValueOnDataSet(this DataSet dataSet)
    {
        if (dataSet.HasData())
        {
            return dataSet.Tables[0].Rows[0][0].ToSafeString();
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the first value on the data table.
    /// Similar to calling data.Rows[0][0].
    /// </summary>
    /// <param name="dataSet">
    /// Dataset.
    /// </param>
    /// <param name="tableName">
    /// Table's name to look for the value.
    /// </param>
    /// <returns>
    /// Total number of records on the data table.
    /// </returns>
    public static int GetFirstValueOnTableAsInteger(this DataSet dataSet, string tableName)
    {
        DataTable data = dataSet.GetDataTableByNameSafe(tableName);
        if (data.HasData())
        {
            return data.Rows[0][0].ToSafeString().ToInteger(0);
        }

        return 0;
    }
}
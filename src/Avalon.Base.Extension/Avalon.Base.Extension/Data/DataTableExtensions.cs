﻿using Avalon.Base.Extension.Types;
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
}
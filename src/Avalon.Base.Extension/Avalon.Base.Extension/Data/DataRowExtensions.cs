﻿using Avalon.Base.Extension.Types;
using System.Data;
using System.Reflection;

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

    /// <summary>
    /// Import the object data into the data row.
    /// </summary>
    /// <param name="row">
    /// DataRow to import the data into.
    /// </param>
    /// <param name="objectToImport">
    /// Object to import.
    /// </param>
    public static void ImportData(this DataRow row, object objectToImport)
    {
        PropertyInfo[] properties = objectToImport.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType.IsPublic)
            {
                if (property.CanRead)
                {
                    row.ImportData(row.Table.Columns[property.Name],
                        property, objectToImport);
                    //if (property.PropertyType.Module.Name.Equals("mscorlib.dll"))
                    //{
                    //    if (property.GetValue(objectToImport, null) == null)
                    //    {
                    //        row[property.Name] = DBNull.Value;
                    //    }
                    //    else
                    //    {
                    //        DataColumn column = row.Table.Columns[property.Name];
                    //        row[property.Name] = property.GetValue(objectToImport, null).ToSafeString();
                    //    }
                    //}
                }
            }
        }
    }

    /// <summary>
    /// Sets the property value with the 
    /// DataColumn in the DataRow.
    /// </summary>
    /// <param name="row">
    /// DataRow.
    /// </param>
    /// <param name="property">
    /// PropertyInfo.
    /// </param>
    /// <param name="column">
    /// DataColumn.
    /// </param>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    public static void ImportData(this DataRow row,
        DataColumn column, PropertyInfo property, object objectToImport)
    {
        object value = property.GetValue(objectToImport, null);
        if (value.IsNull())
        {
            row[column] = DBNull.Value;
        }
        else
        {
            if (property.PropertyType.IsEnum)
            {
                if (row[column].ToString().IsNumeric())
                {
                    row[column] = int.Parse(value.ToSafeString());
                }
                else
                {
                    row[column] = value.ToSafeString();
                }
            }
            else
            {
                Type type = column.DataType;
                if (type.Equals(typeof(string)))
                {
                    row[column] = value.ToSafeString();
                }
                else if (type.Equals(typeof(int))
                    || type.Equals(typeof(Int64)))
                {
                    row[column] = int.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(DateTime)))
                {
                    row[column] = DateTime.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(bool)))
                {
                    row[column] = bool.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(double)))
                {
                    row[column] = double.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(decimal)))
                {
                    row[column] = decimal.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(DateTimeOffset)))
                {
                    row[column] = DateTimeOffset.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(float)))
                {
                    row[column] = float.Parse(value.ToSafeString());
                }
                else if (type.Equals(typeof(long)))
                {
                    row[column] = long.Parse(value.ToSafeString());
                }
            }
        }        
    }
}
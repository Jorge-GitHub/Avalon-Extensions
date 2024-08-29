using Avalon.Base.Extension.Types;
using System.Data;
using System.Reflection;
using System.Text;

namespace Avalon.Base.Extension.Internal.Helpers.Data;

internal class DataRowSerialization
{
    /// <summary>
    /// Load object from data row.
    /// </summary>
    /// <param name="row">
    /// DataRow containing the data.
    /// </param>
    /// <param name="objectToMap">
    /// Object to map.
    /// </param>
    public void LoadObjectFromRow(DataRow row, object objectToMap)
    {
        foreach (DataColumn column in row.Table.Columns)
        {
            if (row[column] != DBNull.Value)
            {
                PropertyInfo property = objectToMap.GetType()
                    .GetProperty(column.ColumnName);
                if (property != null)
                {
                    if (property.CanWrite)
                    {
                        this.setPropertyValueWithDataRow(row,
                            property, column, objectToMap);
                    }
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
    private void setPropertyValueWithDataRow(DataRow row,
        PropertyInfo property, DataColumn column, object objectToMap)
    {
        try
        {
            string type = property.PropertyType.ToString();
            if (type.Equals("System.String"))
            {
                property.SetValue(objectToMap, row[column].ToString(), null);
            }
            else if (type.Equals("System.Int32") | type.Equals("System.Nullable`1[System.Int32]"))
            {
                property.SetValue(objectToMap, int.Parse(row[column].ToString()), null);
            }
            else if (type.Equals("System.DateTime"))
            {
                property.SetValue(objectToMap, DateTime.Parse(row[column].ToString()), null);
            }
            else if (type.Equals("System.Boolean"))
            {
                property.SetValue(objectToMap, bool.Parse(row[column].ToString()), null);
            }
            else if (type.Equals("System.Double"))
            {
                property.SetValue(objectToMap, double.Parse(row[column].ToString()), null);
            }
            else if (type.Equals("System.Decimal"))
            {
                property.SetValue(objectToMap, decimal.Parse(row[column].ToString()), null);
            }
            else if (property.PropertyType.IsEnum)
            {
                if (row[column].ToString().IsNumeric())
                {
                    property.SetValue(objectToMap, int.Parse(row[column].ToString()));
                }
                else
                {
                    property.SetValue(objectToMap, Enum.Parse(
                        property.PropertyType, row[column].ToString(), true));
                }
            }
            else if (property.PropertyType.Equals(typeof(DateTimeOffset)))
            {
                property.SetValue(objectToMap, DateTimeOffset.Parse(row[column].ToString()), null);
            }
            else if (property.PropertyType.Equals(typeof(float)))
            {
                property.SetValue(objectToMap, float.Parse(row[column].ToString()), null);
            }
            else if (property.PropertyType.Equals(typeof(long)))
            {
                property.SetValue(objectToMap, long.Parse(row[column].ToString()), null);
            }
        }
        catch (Exception ex)
        {
            ex.HelpLink = this.prepareErrorForSetPropertyValue(property, column, row);
            throw ex;
        }
    }

    /// <summary>
    /// Create the error for the set property value.
    /// </summary>
    /// <param name="property">
    /// PropertyInfo.
    /// </param>
    /// <param name="column">
    /// DataColumn.
    /// </param>
    /// <param name="row">
    /// DataRow.
    /// </param>
    /// <returns>
    /// Error for the set property value.
    /// </returns>
    private string prepareErrorForSetPropertyValue(
        PropertyInfo property, DataColumn column, DataRow row)
    {
        try
        {
            StringBuilder error = new StringBuilder();
            error.Append("Class Name: " + property.ReflectedType?.Name);
            error.Append(" |Property Name: " + property.Name);
            error.Append(" |Property Type: " + property.PropertyType?.ToString());
            error.Append(" |Column Name: " + column.ColumnName);
            error.Append(" |Column Value: " + row[column]?.ToString());
            return error.ToString();
        }
        catch
        {
            return "Unable to set the property value help information";
        }
    }
}
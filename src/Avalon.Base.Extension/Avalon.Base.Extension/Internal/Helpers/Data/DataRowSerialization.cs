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
                PropertyInfo property = this.GetPropertyForColumn(
                    objectToMap.GetType(),
                    column.ColumnName);
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

    private PropertyInfo GetPropertyForColumn(Type objectType, string columnName)
    {
        PropertyInfo property = objectType.GetProperty(
            columnName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

        if (property != null)
        {
            return property;
        }

        string normalizedColumnName = this.NormalizeName(columnName);
        PropertyInfo[] properties = objectType.GetProperties(
            BindingFlags.Instance | BindingFlags.Public);

        foreach (PropertyInfo currentProperty in properties)
        {
            if (this.NormalizeName(currentProperty.Name).Equals(
                normalizedColumnName,
                StringComparison.OrdinalIgnoreCase))
            {
                return currentProperty;
            }
        }

        return null;
    }

    private string NormalizeName(string value)
    {
        StringBuilder result = new StringBuilder();
        foreach (char character in value)
        {
            if (character != '_' &&
                character != '-' &&
                character != ' ')
            {
                result.Append(character);
            }
        }

        return result.ToString();
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
            string value = row[column].ToString() ?? string.Empty;
            Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (propertyType.Equals(typeof(string)))
            {
                property.SetValue(objectToMap, value, null);
            }
            else if (propertyType.Equals(typeof(int)))
            {
                property.SetValue(objectToMap, int.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(DateTime)))
            {
                property.SetValue(objectToMap, DateTime.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(bool)))
            {
                property.SetValue(objectToMap, bool.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(double)))
            {
                property.SetValue(objectToMap, double.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(decimal)))
            {
                property.SetValue(objectToMap, decimal.Parse(value), null);
            }
            else if (propertyType.IsEnum)
            {
                if (value.IsNumeric())
                {
                    property.SetValue(objectToMap, Enum.ToObject(propertyType, int.Parse(value)), null);
                }
                else
                {
                    property.SetValue(objectToMap, Enum.Parse(propertyType, value, true), null);
                }
            }
            else if (propertyType.Equals(typeof(DateTimeOffset)))
            {
                property.SetValue(objectToMap, DateTimeOffset.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(float)))
            {
                property.SetValue(objectToMap, float.Parse(value), null);
            }
            else if (propertyType.Equals(typeof(long)))
            {
                property.SetValue(objectToMap, long.Parse(value), null);
            }
        }
        catch (Exception ex)
        {
            ex.HelpLink = this.prepareErrorForSetPropertyValue(property, column, row);
            throw;
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

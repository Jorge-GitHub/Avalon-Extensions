using System.Reflection;
using System.Collections;

namespace Avalon.Base.Extension.Types.TypeExtensions;

/// <summary>
/// Property info extensions.
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Check if a property is member of a collection.
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public static bool IsACollection(this PropertyInfo property)
    {
        if (property != null)
        {
            string nameSpace = property?.PropertyType?.Namespace;
            if(nameSpace.IsNotNullOrEmpty())
            {
                return nameSpace.Equals("System.Collections.Generic");
            }
        }

        return false;
    }

    /// <summary>
    /// Get the list member type.
    /// Check an array/list to see what type of array/list is based on.
    /// </summary>
    /// <param name="type">
    /// Type to check.
    /// </param>
    /// <returns>
    /// Member type.
    /// </returns>
    public static Type GetElementTypeFromCollection(this PropertyInfo property)
    {
        if(property != null)
        {
            return property.PropertyType.GetElementTypeFromCollection();
        }

        return null;
    }
}
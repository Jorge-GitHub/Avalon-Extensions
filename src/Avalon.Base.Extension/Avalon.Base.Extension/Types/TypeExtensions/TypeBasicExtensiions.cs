using System.Collections;
using System.Reflection;

namespace Avalon.Base.Extension.Types.TypeExtensions;

public static class TypeBasicExtensiions
{
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
    /// <remarks>
    /// I borrow the idea from the link below. Kudos for her code.
    /// https://www.codeproject.com/Tips/5267157/How-to-Get-a-Collection-Element-Type-Using-Reflect
    /// </remarks>
    public static Type GetElementTypeFromCollection(this Type type)
    {
        if (type != null)
        {
            var iType = typeof(IEnumerable<>);
            foreach (var genericInterface in type.GetInterfaces())
            {
                if (genericInterface.IsGenericType &&
                    genericInterface.GetGenericTypeDefinition().Equals(iType))
                {
                    return genericInterface.GetGenericArguments()[0];
                }
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return typeof(DictionaryEntry);
            }

            if (typeof(IList).IsAssignableFrom(type))
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.Name.Equals("Item", StringComparison.OrdinalIgnoreCase)
                        && property.PropertyType != typeof(object))
                    {
                        ParameterInfo[] parameters = property.GetIndexParameters();
                        if (parameters.Length == 1
                            && parameters[0].ParameterType.Equals(typeof(int)))
                        {
                            return property.PropertyType;
                        }
                    }
                }
            }

            if (typeof(ICollection).IsAssignableFrom(type))
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.Name.Equals("Add", StringComparison.OrdinalIgnoreCase))
                    {
                        ParameterInfo[] parameter = method.GetParameters();
                        if (parameter.Length == 1
                            && parameter[0].ParameterType != typeof(object))
                        {
                            return parameter[0].ParameterType;
                        }
                    }
                }
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return typeof(object);
            }
        }

        return null;
    }
}

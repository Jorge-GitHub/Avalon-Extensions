using System.Collections;

namespace Avalon.Base.Extension.Collections.ArrayListExtensions;

/// <summary>
/// ArrayList extension methods.
/// </summary>
public static class ArrayListBasicExtensions
{
    /// <summary>
    /// Determinate whether the item is in the ArrayList.
    /// </summary>
    /// <param name="values">
    /// ArrayList to search.
    /// </param>
    /// <param name="item">
    /// Item to  locate in the ArrayList.
    /// </param>
    /// <param name="ignoreCase">
    /// Flag that determinate whether to ignore case or not.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the item is within the ArrayList.
    /// </returns>
    public static bool Contains(this ArrayList values, string item, bool ignoreCase)
    {
        if (values.HasElements())
        {
            if (ignoreCase)
            {
                return values.Contains(item, StringComparison.InvariantCultureIgnoreCase);
            }

            return values.Contains(item, StringComparison.InvariantCulture);
        }

        return false;
    }

    /// <summary>
    /// Determinate whether the item is in the ArrayList.
    /// </summary>
    /// <param name="values">
    /// ArrayList to search.
    /// </param>
    /// <param name="item">
    /// Item to  locate in the ArrayList.
    /// </param>
    /// <param name="comparisonType">
    /// Flag that determinate whether to ignore case or not.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the item is within the ArrayList.
    /// </returns>
    public static bool Contains(this ArrayList values, string item, StringComparison comparisonType)
    {
        if (values.HasElements())
        {
            return values.ToArray().Contains(item, comparisonType);
        }

        return false;
    }

    /// <summary>
    /// Concatenates the array's elements by using comma as a separator.
    /// </summary>
    /// <param name="values">
    /// Value to concatenate.
    /// </param>
    /// <returns>
    /// The array elements joined by comma.
    /// </returns>
    public static string Join(this ArrayList values)
    {
        return values.Join(null);
    }

    /// <summary>
    /// Concatenates the array's elements by using the specified separator.
    /// </summary>
    /// <param name="values">
    /// Value to concatenate.
    /// </param>
    /// <param name="separator">
    /// Optional: The string use as a separator. By default is comma.
    /// </param>
    /// <returns>
    /// The array elements joined by the separator.
    /// </returns>
    public static string Join(this ArrayList values, string separator)
    {
        if (values.HasElements())
        {
            return values.ToArray().Join(separator);
        }

        return "";
    }

    /// <summary>
    /// Determinate whether the ArrayList has elements.
    /// </summary>
    /// <param name="values">
    /// ArrayList to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the ArrayList has elements or not.
    /// </returns>
    public static bool HasElements(this ArrayList values)
    {
        return (values != null && values.Count > 0);
    }

    /// <summary>
    /// determinate whether the ArrayList is null or empty.
    /// </summary>
    /// <param name="values">
    /// ArrayList to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the ArrayList is null or empty.
    /// </returns>
    public static bool IsNullOrEmpty(this ArrayList values)
    {
        return !values.HasElements();
    }
}
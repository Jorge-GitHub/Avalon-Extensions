using System.Text;

namespace Avalon.Base.Extension.Collections.ArrayExtensions;

/// <summary>
/// Array extension methods.
/// </summary>
public static class ArrayBasicExtensions
{
     /// <summary>
    /// Determinate whether the element is in the array.
    /// </summary>
    /// <param name="values">
    /// Array.
    /// </param>
    /// <param name="item">
    /// Item to locate in the array. The value can be null.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the item is in the array.
    /// </returns>
    public static bool Contains(this Array values, object item)
    {
        if (values.HasElements())
        {
            return (Array.IndexOf(values, item) > -1);
        }

        return false;
    }

    /// <summary>
    /// Determinate whether the item is in the Array.
    /// </summary>
    /// <param name="values">
    /// Array to search.
    /// </param>
    /// <param name="item">
    /// Item to  locate in the Array.
    /// </param>
    /// <param name="ignoreCase">
    /// Flag that determinate whether to ignore case or not.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the item is within the Array.
    /// </returns>
    public static bool Contains(this Array values, string item, bool ignoreCase)
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
    /// Determinate whether the item is in the Array.
    /// </summary>
    /// <param name="values">
    /// Array to search.
    /// </param>
    /// <param name="item">
    /// Item to  locate in the Array.
    /// </param>
    /// <param name="comparisonType">
    /// Flag that determinate whether to ignore case or not.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the item is within the Array.
    /// </returns>
    public static bool Contains(this Array values, string item, StringComparison comparisonType)
    {
        if (values.HasElements())
        {
            if (!String.IsNullOrEmpty(item))
            {
                item = item.Trim();
                foreach (String element in values)
                {
                    if (element != null && element.GetType().Equals(typeof(String)))
                    {
                        if (element.Trim().Equals(item, comparisonType))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
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
    public static string Join(this Array values)
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
    public static string Join(this Array values, string separator)
    {
        if (values.HasElements())
        {
            if (String.IsNullOrEmpty(separator))
            {
                separator = ",";
            }
            StringBuilder result = new StringBuilder();
            int numberOfElements = values.Length;
            object element;
            for (int i = 0; i < numberOfElements; i++)
            {
                element = values.GetValue(i);
                if (element != null)
                {
                    result.Append(element.ToString() + separator);
                }
            }

            return result.ToString().Remove(result.Length - separator.Length);
        }

        return "";
    }

    /// <summary>
    /// Determinate whether the Array has elements.
    /// </summary>
    /// <param name="values">
    /// Array to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the Array has elements or not.
    /// </returns>
    public static bool HasElements(this Array values)
    {
        return (values != null && values.Length > 0);
    }

    /// <summary>
    /// Determinate whether the Array is null or empty.
    /// </summary>
    /// <param name="values">
    /// Array to check.
    /// </param>
    /// <returns>
    /// Flag that Determinate whether the Array is null or empty.
    /// </returns>
    public static bool IsNullOrEmpty(this Array values)
    {
        return !values.HasElements();
    }
}
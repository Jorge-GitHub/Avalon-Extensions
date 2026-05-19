using Avalon.Base.Extension.Types;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Avalon.Base.Extension.Collections;

/// <summary>
/// IEnumerable collections extension methods.
/// </summary>
public static class IEnumerableCollectionBasicExtensions
{
    /// <summary>
    /// Determinate whether the list has elements.
    /// </summary>
    /// <param name="list">
    /// List to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the list has elements or not.
    /// </returns>
    public static bool HasElements<T>([NotNullWhen(true)] this IEnumerable<T>? list)
    {
        return list is ICollection<T> collection
            ? collection.Count > 0
            : list != null && list.Any();
    }

    /// <summary>
    /// Determinate whether the list is empty or null.
    /// </summary>
    /// <param name="list">
    /// List to check.
    /// </param>
    /// <returns>
    /// Flag that determinate whether the list is empty or null.
    /// </returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list)
    {
        return !list.HasElements();
    }

    /// <summary>
    /// Performs the specified action on each element.
    /// </summary>
    /// <typeparam name="T">
    /// List type.
    /// </typeparam>
    /// <param name="list">
    /// List with the items to perform the action.
    /// </param>
    /// <param name="action">
    /// Delegate to perform on each element.
    /// </param>
    public static void Each<T>(this IEnumerable<T> list, Action<T> action)
    {
        if (list.HasElements())
        {
            foreach (T element in list)
            {
                action(element);
            }
        }
    }

    /// <summary>
    /// Performs the specified action on each element asynchronous.
    /// </summary>
    /// <typeparam name="T">
    /// List type.
    /// </typeparam>
    /// <param name="list">
    /// List with the items to perform the action.
    /// </param>
    /// <param name="action">
    /// Delegate to perform on each element.
    /// </param>
    public static void Wave<T>(this IEnumerable<T> list, Action<T> action)
    {
        if (list.HasElements())
        {
            foreach (T element in list)
            {
                action(element);
            }
        }
    }

    /// <summary>
    /// Span a list of items.
    /// </summary>
    /// <typeparam name="T">
    /// List type.
    /// </typeparam>
    /// <param name="list">
    /// List with the items to perform the action.
    /// </param>
    /// <param name="action">
    /// Delegate to perform on each element.
    /// </param>
    public static void Span<T>(this IEnumerable<T>? list, Action<T>? action)
    {
        if (action is not null)
        {
            if (list is List<T> items)
            {
                items.Span(action);
            }
            else if (list is not null)
            {
                foreach (T element in list)
                {
                    action(element);
                }
            }
        }
    }

    /// <summary>
    /// Convert a list into a string builder. Each element will be append as a new line.
    /// </summary>
    /// <typeparam name="T">
    /// List type.
    /// </typeparam>
    /// <param name="list">
    /// List with the items to perform the action.
    /// </param>
    /// <returns>
    /// String builder. Each element will be append as a new line.
    /// </returns>
    public static StringBuilder ToStringStringBuilder<T>(this IEnumerable<T> list)
    {
        if (list.HasElements())
        {
            StringBuilder result = new StringBuilder();
            foreach (T item in list)
            {
                if (item.IsNotNull())
                {
                    result.AppendLine(item?.ToString());
                }
            }

            return result;
        }

        return new StringBuilder();
    }

    public static string[]? NormalizeValues(this IEnumerable<string>? values)
    {
        return values.NormalizeValues(StringComparer.OrdinalIgnoreCase);
    }

    public static string[]? NormalizeValues(this IEnumerable<string>? values,
        StringComparer comparer)
    {
        if (values.HasElements())
        {
            return values
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value.Trim())
                .Distinct(comparer)
                .ToArray();
        }

        return null;
    }
}

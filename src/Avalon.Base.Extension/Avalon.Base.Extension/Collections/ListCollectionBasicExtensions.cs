using System.Runtime.InteropServices;

namespace Avalon.Base.Extension.Collections;

/// <summary>
/// List collections extension methods.
/// </summary>
public static class ListCollectionBasicExtensions
{
    /// <summary>
    /// Randomize an array.
    /// </summary>
    /// <typeparam name="T">
    /// Generic.
    /// </typeparam>
    /// <param name="items">
    /// Items to randomize.
    /// </param>
    public static List<T> Randomize<T>(this List<T> items)
    {
        List<T> randomList = new List<T>();
        if (items.HasElements())
        {
            Random r = new Random();
            int randomIndex = 0;
            while (items.Count > 0)
            {
                randomIndex = r.Next(0, items.Count);
                randomList.Add(items[randomIndex]);
                items.RemoveAt(randomIndex);
            }
        }

        return randomList;
    }

    /// <summary>
    /// Expose the list items as a span.
    /// </summary>
    /// <typeparam name="T">
    /// List type.
    /// </typeparam>
    /// <param name="list">
    /// List to expose as a span.
    /// </param>
    /// <returns>
    /// Span of the list items.
    /// </returns>
    public static global::System.Span<T> AsSpan<T>(this List<T>? list)
    {
        return list.HasElements()
            ? CollectionsMarshal.AsSpan(list)
            : global::System.Span<T>.Empty;
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
    public static void Span<T>(this List<T>? list, Action<T>? action)
    {
        if (list.HasElements() && action is not null)
        {
            global::System.Span<T> items = list.AsSpan();
            int numberOfItems = items.Length;
            for (int i = 0; i < numberOfItems; i++)
            {
                action(items[i]);
            }
        }
    }
}

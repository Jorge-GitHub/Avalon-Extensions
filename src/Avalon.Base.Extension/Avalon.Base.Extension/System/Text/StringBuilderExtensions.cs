using Avalon.Base.Extension.Types;
using System.Text;

namespace Avalon.Base.Extension.System.Text;

/// <summary>
/// StringBuilder extensions.
/// </summary>
public static class StringBuilderExtensions
{
    /// <summary>
    /// Remove the text between two strings.
    /// </summary>
    /// <param name="builder">
    /// StringBuilder used to replace text.
    /// </param>
    /// <param name="startTag">
    /// Start text or tag.
    /// </param>
    /// <param name="endTag">
    /// End text or tag.
    /// </param>
    public static void RemoveBetween(this StringBuilder builder, 
        string startTag, string endTag)
    {
        if (builder.IsNotNull())
        {
            int start = builder.LastIndexOf(startTag) + startTag.Length;
            int end = builder.LastIndexOf(endTag);

            builder.Remove(start, end - start);
        }
    }

    /// <summary>
    /// Gets the index of the last occurrence of any character in value in the current instance.
    /// </summary>
    /// <param name="builder">
    /// StringBuilder used to search.
    /// </param>
    /// <param name="text">
    /// Text to search.
    /// </param>
    /// <returns>
    /// The index of the last occurrence of any character in value in the current instance.
    /// </returns>
    public static int LastIndexOf(this StringBuilder builder, string text)
    {
        if (builder.IsNotNull())
        {
            return builder.ToString().LastIndexOf(text);
        }

        return -1;
    }
}
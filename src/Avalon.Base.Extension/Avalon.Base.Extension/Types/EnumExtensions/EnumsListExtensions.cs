using Avalon.Base.Extension.Collections;

namespace Avalon.Base.Extension.Types.EnumExtensions;

public static class EnumsListExtensions
{
    /// <summary>
    /// Convert a list of enums into a list of strings.
    /// </summary>
    /// <param name="enums">
    /// Enums to convert.
    /// </param>
    /// <returns>
    /// list of strings.
    /// </returns>
    public static List<string> ToListString<T>(this List<T> enums)
    {
        if (enums.HasElements())
        {
            return enums.Select(
                value => value.ToString()).ToList();
        }

        return null;
    }
}
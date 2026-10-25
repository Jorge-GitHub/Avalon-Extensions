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
                value => value.ToString())
                .ToList();
        }

        return null!;
    }

    public static short[]? ToDistinctInt16Values<TEnum>(
        this IEnumerable<TEnum>? values)
        where TEnum : struct, Enum
    {
        if (values is not null)
        {
            short[] normalizedValues = values
                .Select(value => Convert.ToInt16(value))
                .Distinct()
                .ToArray();

            return normalizedValues.Length == 0
                ? null : normalizedValues;
        }

        return null;
    }
}
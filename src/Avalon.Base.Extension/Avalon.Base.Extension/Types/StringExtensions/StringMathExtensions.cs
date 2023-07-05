using System.Data;

namespace Avalon.Base.Extension.Types;

/// <summary>
/// String math extension methods.
/// </summary>
public static class StringMathExtensions
{
    /// <summary>
    /// Evaluate an string.
    /// </summary>
    /// <param name="value">
    /// String to evaluate.
    /// </param>
    /// <returns>
    /// Result.
    /// </returns>
    public static decimal Evaluate(this string value)
    {
        return (decimal)new DataTable().Compute(value, null);
    }
}
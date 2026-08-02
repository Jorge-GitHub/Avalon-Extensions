using Avalon.Base.Extension.Collections;
using Avalon.Base.Extension.Types;

namespace Avalon.Base.Extension.System.Net;

/// <summary>
/// Extension methods for the <c>http:METHOD:route</c> implementation key used
/// to describe a declared HTTP operation.
/// </summary>
public static class HttpImplementationKeyExtensions
{
    /// <summary>
    /// Scheme the key has to start with.
    /// </summary>
    public const string Scheme = "http";

    /// <summary>
    /// Number of segments a usable key has.
    /// </summary>
    public const int SegmentCount = 3;

    private const string InvalidRoute = "";

    /// <summary>
    /// Determine whether the segments form a usable implementation key.
    /// </summary>
    /// <param name="segments">
    /// Key already split on the separator.
    /// </param>
    /// <returns>
    /// True when there are three segments, the first is the scheme, and
    /// neither of the others is blank.
    /// </returns>
    public static bool CanBeParsed(this string[] segments)
    {
        return segments.HasElements()
            && segments.Length == SegmentCount
            && segments[0].EqualsIgnoreCase(Scheme)
            && segments[1].IsNotNullOrEmpty()
            && segments[2].IsNotNullOrEmpty();
    }

    /// <summary>
    /// Read the method out of the implementation key.
    /// </summary>
    /// <param name="segments">
    /// Key already split on the separator.
    /// </param>
    /// <returns>
    /// The method, or null when the key cannot be parsed.
    /// </returns>
    public static HttpMethod? GetImplementationMethod(this string[] segments)
    {
        return segments.GetImplementationMethod(segments.CanBeParsed());
    }

    /// <summary>
    /// Read the method out of the implementation key.
    /// </summary>
    /// <param name="segments">
    /// Key already split on the separator.
    /// </param>
    /// <param name="parsed">
    /// Whether the key was already found usable.
    /// </param>
    /// <returns>
    /// The method, or null when the key cannot be parsed.
    /// </returns>
    public static HttpMethod? GetImplementationMethod(this string[] segments,
        bool parsed)
    {
        return parsed
            ? new HttpMethod(segments[1].Trim().ToUpperInvariant())
            : null;
    }

    /// <summary>
    /// Read the route out of the implementation key.
    /// </summary>
    /// <param name="segments">
    /// Key already split on the separator.
    /// </param>
    /// <returns>
    /// The route, or an empty string when the key cannot be parsed.
    /// </returns>
    public static string GetImplementationRoute(this string[] segments)
    {
        return segments.GetImplementationRoute(segments.CanBeParsed());
    }

    /// <summary>
    /// Read the route out of the implementation key.
    /// </summary>
    /// <param name="segments">
    /// Key already split on the separator.
    /// </param>
    /// <param name="parsed">
    /// Whether the key was already found usable.
    /// </param>
    /// <returns>
    /// The route, or an empty string when the key cannot be parsed.
    /// </returns>
    public static string GetImplementationRoute(this string[] segments,
        bool parsed)
    {
        return parsed ? segments[2].Trim() : InvalidRoute;
    }
}

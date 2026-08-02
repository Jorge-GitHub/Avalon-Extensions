using System.Net.Http.Json;

namespace Avalon.Base.Extension.System.Net;

/// <summary>
/// Extension methods for composing HTTP request messages.
/// </summary>
public static class HttpRequestMessageExtensions
{
    /// <summary>
    /// Set the request content as JSON.
    /// </summary>
    /// <typeparam name="TRequest">
    /// Type of the object to serialize.
    /// </typeparam>
    /// <param name="requestMessage">
    /// Request to set the content on.
    /// </param>
    /// <param name="request">
    /// Object to serialize into the body.
    /// </param>
    /// <returns>
    /// The same request, so calls can be chained.
    /// </returns>
    public static HttpRequestMessage WithJsonContent<TRequest>(
        this HttpRequestMessage requestMessage,
        TRequest request)
    {
        requestMessage.Content = JsonContent.Create(request);

        return requestMessage;
    }

    /// <summary>
    /// Determine whether the method carries a request body.
    /// </summary>
    /// <param name="method">
    /// Method to check.
    /// </param>
    /// <returns>
    /// False for GET, DELETE, and HEAD; true otherwise.
    /// </returns>
    public static bool ShouldSendBody(this HttpMethod method)
    {
        return method != HttpMethod.Get
            && method != HttpMethod.Delete
            && method != HttpMethod.Head;
    }
}

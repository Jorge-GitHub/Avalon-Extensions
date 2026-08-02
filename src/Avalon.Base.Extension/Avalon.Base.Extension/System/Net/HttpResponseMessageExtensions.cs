using Avalon.Base.Extension.Types;
using System.Net;
using System.Net.Http.Headers;

namespace Avalon.Base.Extension.System.Net;

/// <summary>
/// Extension methods for reading HTTP responses.
/// </summary>
public static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Default name used when a download response names no file.
    /// </summary>
    public const string DefaultDownloadFileName = "attachment";

    /// <summary>
    /// Longest response body included in a failure message.
    /// </summary>
    public const int MaxFailureBodyLength = 4096;

    /// <summary>
    /// Deserialize the response content.
    /// </summary>
    /// <typeparam name="T">
    /// Type to deserialize into.
    /// </typeparam>
    /// <param name="message">
    /// Response to read.
    /// </param>
    /// <returns>
    /// The deserialized body, or null when it cannot be read as that type.
    /// </returns>
    public static async Task<T?> Read<T>(this HttpResponseMessage message)
        where T : class
    {
        return await message.ReadAsync<T>();
    }

    /// <summary>
    /// Deserialize the response content.
    /// </summary>
    /// <typeparam name="T">
    /// Type to deserialize into.
    /// </typeparam>
    /// <param name="message">
    /// Response to read.
    /// </param>
    /// <param name="cancellationToken">
    /// Token used to cancel the read.
    /// </param>
    /// <returns>
    /// The deserialized body, or null when it cannot be read as that type.
    /// </returns>
    public static async Task<T?> ReadAsync<T>(
        this HttpResponseMessage message,
        CancellationToken cancellationToken = default)
        where T : class
    {
        string content = await message.Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        return content.ToObject<T>();
    }

    /// <summary>
    /// Determine whether the response is an attachment rather than a payload.
    /// </summary>
    /// <param name="httpResponse">
    /// Response to check.
    /// </param>
    /// <returns>
    /// True when the response carries a content disposition header.
    /// </returns>
    public static bool IsDownloadResponse(this HttpResponseMessage httpResponse)
    {
        return httpResponse.Content.Headers.ContentDisposition is not null;
    }

    /// <summary>
    /// Read the file name a download response asks to be saved as.
    /// </summary>
    /// <param name="httpResponse">
    /// Response to read.
    /// </param>
    /// <param name="defaultDownloadFileName">
    /// Name to use when the response names none.
    /// </param>
    /// <returns>
    /// The file name without surrounding quotes.
    /// </returns>
    public static string ResolveDownloadFileName(
        this HttpResponseMessage httpResponse,
        string defaultDownloadFileName = DefaultDownloadFileName)
    {
        ContentDispositionHeaderValue? contentDisposition =
            httpResponse.Content.Headers.ContentDisposition;
        string fileName = contentDisposition?.FileNameStar
            ?? contentDisposition?.FileName
            ?? defaultDownloadFileName;

        fileName = fileName.Trim().Trim('"');

        return fileName.IsNullOrEmpty() ? defaultDownloadFileName : fileName;
    }

    /// <summary>
    /// Describe a failed request, including the response body.
    /// </summary>
    /// <param name="statusCode">
    /// Status the request came back with.
    /// </param>
    /// <param name="httpMethod">
    /// Method that was sent.
    /// </param>
    /// <param name="requestUri">
    /// Address that was called.
    /// </param>
    /// <param name="responseBody">
    /// Body returned with the failure.
    /// </param>
    /// <returns>
    /// A message naming the call, the status, and the body trimmed to
    /// <see cref="MaxFailureBodyLength"/>.
    /// </returns>
    /// <remarks>
    /// The body is included because it usually carries the only explanation of
    /// why the call failed, and dropping it leaves the caller guessing.
    /// </remarks>
    public static string CreateFailureMessage(
        this HttpStatusCode statusCode,
        HttpMethod httpMethod,
        string requestUri,
        string responseBody)
    {
        string message = $"{httpMethod} request to '{requestUri}' failed with "
            + $"status code {(int)statusCode} ({statusCode}).";

        if (responseBody.IsNotNullOrEmpty())
        {
            message = $"{message} Response body: "
                + $"{TrimFailureBody(responseBody)}";
        }

        return message;
    }

    private static string TrimFailureBody(string responseBody)
    {
        string trimmedBody = responseBody.Trim();

        return trimmedBody.Length > MaxFailureBodyLength
            ? $"{trimmedBody[..MaxFailureBodyLength]}..."
            : trimmedBody;
    }
}

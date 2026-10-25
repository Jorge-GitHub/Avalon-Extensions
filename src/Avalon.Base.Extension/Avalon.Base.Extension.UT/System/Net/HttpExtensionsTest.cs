using Avalon.Base.Extension.System.Net;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Avalon.Base.Extension.UT.System.Net;

[TestClass]
public class HttpExtensionsTest
{
    [TestMethod]
    public void WithJsonContent_SetsAJsonBody()
    {
        using HttpRequestMessage request =
            new HttpRequestMessage(HttpMethod.Post, "https://example.com")
                .WithJsonContent(new { Name = "Avalon" });

        Assert.IsNotNull(request.Content);
        Assert.AreEqual("application/json",
            request.Content.Headers.ContentType?.MediaType);
    }

    [TestMethod]
    public void ShouldSendBody_IsFalseForMethodsWithoutOne()
    {
        Assert.IsFalse(HttpMethod.Get.ShouldSendBody());
        Assert.IsFalse(HttpMethod.Delete.ShouldSendBody());
        Assert.IsFalse(HttpMethod.Head.ShouldSendBody());
    }

    [TestMethod]
    public void ShouldSendBody_IsTrueForMethodsWithOne()
    {
        Assert.IsTrue(HttpMethod.Post.ShouldSendBody());
        Assert.IsTrue(HttpMethod.Put.ShouldSendBody());
        Assert.IsTrue(HttpMethod.Patch.ShouldSendBody());
    }

    [TestMethod]
    public async Task ReadAsync_DeserializesTheBody()
    {
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("""{ "name": "Avalon" }"""),
        };

        Sample? sample = await response.ReadAsync<Sample>();

        Assert.AreEqual("Avalon", sample?.Name);
    }

    [TestMethod]
    public void IsDownloadResponse_IsFalseWithoutAContentDisposition()
    {
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("body"),
        };

        Assert.IsFalse(response.IsDownloadResponse());
    }

    [TestMethod]
    public void IsDownloadResponse_IsTrueWithAContentDisposition()
    {
        using HttpResponseMessage response = this.CreateDownload("report.pdf");

        Assert.IsTrue(response.IsDownloadResponse());
    }

    [TestMethod]
    public void ResolveDownloadFileName_ReadsTheName()
    {
        using HttpResponseMessage response = this.CreateDownload("report.pdf");

        Assert.AreEqual("report.pdf", response.ResolveDownloadFileName());
    }

    [TestMethod]
    public void ResolveDownloadFileName_FallsBackWhenThereIsNoName()
    {
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("body"),
        };

        Assert.AreEqual("attachment", response.ResolveDownloadFileName());
        Assert.AreEqual("custom", response.ResolveDownloadFileName("custom"));
    }

    [TestMethod]
    public void CreateFailureMessage_NamesTheCallAndTheStatus()
    {
        string message = HttpStatusCode.NotFound.CreateFailureMessage(
            HttpMethod.Get, "https://example.com/missing", "no such record");

        StringAssert.Contains(message, "GET");
        StringAssert.Contains(message, "https://example.com/missing");
        StringAssert.Contains(message, "404");
        StringAssert.Contains(message, "no such record");
    }

    [TestMethod]
    public void CreateFailureMessage_TrimsALongBody()
    {
        string message = HttpStatusCode.InternalServerError.CreateFailureMessage(
            HttpMethod.Post,
            "https://example.com",
            new string('x', HttpResponseMessageExtensions.MaxFailureBodyLength + 500));

        StringAssert.EndsWith(message, "...");
        Assert.IsLessThan(
            HttpResponseMessageExtensions.MaxFailureBodyLength + 300,
            message.Length);
    }

    [TestMethod]
    public void CreateFailureMessage_OmitsAnEmptyBody()
    {
        string message = HttpStatusCode.BadGateway.CreateFailureMessage(
            HttpMethod.Get, "https://example.com", string.Empty);

        Assert.DoesNotContain("Response body", message);
    }

    [TestMethod]
    public void ConvertData_ReturnsAMatchingTypeUntouched()
    {
        Sample sample = new() { Name = "Avalon" };

        Assert.AreSame(sample, ((object)sample).ConvertData<Sample>());
    }

    [TestMethod]
    public void ConvertData_ReadsAJsonElement()
    {
        JsonElement element = JsonDocument
            .Parse("""{ "name": "Avalon" }""").RootElement;

        Assert.AreEqual("Avalon", ((object)element).ConvertData<Sample>()?.Name);
    }

    [TestMethod]
    public void ConvertData_SerializesAnythingElse()
    {
        object source = new { Name = "Avalon" };

        Assert.AreEqual("Avalon", source.ConvertData<Sample>()?.Name);
    }

    [TestMethod]
    public void ConvertData_WithNull_ReturnsTheDefault()
    {
        object? source = null;

        Assert.IsNull(source.ConvertData<Sample>());
    }

    [TestMethod]
    public void ImplementationKey_ParsesMethodAndRoute()
    {
        string[] segments = "http:GET:/customers/{id}".Split(':', 3);

        Assert.IsTrue(segments.CanBeParsed());
        Assert.AreEqual(HttpMethod.Get, segments.GetImplementationMethod());
        Assert.AreEqual("/customers/{id}", segments.GetImplementationRoute());
    }

    [TestMethod]
    public void ImplementationKey_IsCaseInsensitiveOnTheScheme()
    {
        Assert.IsTrue("HTTP:post:/orders".Split(':', 3).CanBeParsed());
    }

    [TestMethod]
    public void ImplementationKey_RejectsWhatItCannotParse()
    {
        Assert.IsFalse("builtin:run_command".Split(':', 3).CanBeParsed());
        Assert.IsFalse("http:GET".Split(':', 3).CanBeParsed());
        Assert.IsFalse("http::/orders".Split(':', 3).CanBeParsed());
        Assert.IsFalse(Array.Empty<string>().CanBeParsed());
    }

    [TestMethod]
    public void ImplementationKey_WhenUnparsable_ReturnsNothing()
    {
        string[] segments = "builtin:run_command".Split(':', 3);

        Assert.IsNull(segments.GetImplementationMethod());
        Assert.AreEqual("", segments.GetImplementationRoute());
    }

    private HttpResponseMessage CreateDownload(string fileName)
    {
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("body"),
        };

        response.Content.Headers.ContentDisposition =
            new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName,
            };

        return response;
    }

    private sealed class Sample
    {
        public string Name { get; set; } = string.Empty;
    }
}

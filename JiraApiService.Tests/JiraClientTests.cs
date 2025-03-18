using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JiraApi.Objects.Jira.V3.Issue;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace JiraApi.Tests;

[TestFixture]
public class JiraClientTests
{
    private const string BaseUrl = "https://testjira.com";
    private const string Username = "testuser";
    private const string Password = "testpassword";
    private const string Path = "/rest/api/2/issue/TEST-123";
    private const string JsonContent = "{\"key\": \"value\", \"id\": \"value\"}";
    private JiraClient _jiraClient;
    private Mock<HttpMessageHandler> _handlerMock;

    [SetUp]
    public void SetUp()
    {
        _handlerMock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_handlerMock.Object) { BaseAddress = new Uri(BaseUrl) };
        _jiraClient = new JiraClient(BaseUrl, Username, Password)
            { HttpClient = httpClient, MaxRetries = 5, RetryDelayInSeconds = 1 };

        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonContent, Encoding.UTF8, "application/json")
            });
    }

    [Test]
    public async Task Get_SuccessfulRequest_ReturnsDeserializedObject()
    {
        var result = await _jiraClient.Get<JiraIssueLight>(Path);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Key, Is.EqualTo("value"));
    }

    [Test]
    public async Task Post_SuccessfulRequest_ReturnsDeserializedObject()
    {
        var result = await _jiraClient.Post<JiraIssueLight>(Path, new { Key = "value" });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Key, Is.EqualTo("value"));
    }

    [Test]
    public async Task Get_TooManyRequests_RetriesAndSucceeds()
    {
        var callCount = 0;
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                callCount++;
                if (callCount == 1)
                {
                    return new HttpResponseMessage(HttpStatusCode.TooManyRequests);
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonContent, Encoding.UTF8, "application/json")
                };
            });

        var result = await _jiraClient.Get<JiraIssueLight>(Path);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Key, Is.EqualTo("value"));
        Assert.That(callCount, Is.EqualTo(2));
    }

    [Test]
    public void Get_TooManyRequests_ExceedsRetryLimit_ThrowsException()
    {
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.TooManyRequests));
        _jiraClient.MaxRetries = 1;

        Assert.ThrowsAsync<HttpRequestException>(() => _jiraClient.Get<JiraIssueLight>(Path));
    }

    [Test]
    public async Task Get_DeserializationFails_ThrowsException()
    {
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{invalid json}", Encoding.UTF8, "application/json")
            });

        var result = await _jiraClient.Get<JiraIssueLight>(Path);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Get_NoData_ThrowsNoDataException()
    {
        _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("null", Encoding.UTF8, "application/json")
            });

        var result = await _jiraClient.Get<JiraIssueLight>(Path);

        Assert.That(result, Is.Null);
    }
}

using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiraApi.Objects;
using JiraApi.Objects.Jira;
using JiraApi.Objects.Jira.V3.Changelog;
using JiraApi.Objects.Jira.V3.Issue;
using JiraApi.Objects.Jira.V3.Project;
using JiraApi.Objects.Jira.V3.User;
using JiraApi.Tests.Helpers.Fixtures;

namespace JiraApi.Tests;

[TestFixture]
public class JiraApiServiceTests
{
    private Mock<IJiraClient> _mockFetchApi;
    private JiraApiService _jiraApiService;
    private JiraApiFixtures _fixture;

    [SetUp]
    public void SetUp()
    {
        _mockFetchApi = new Mock<IJiraClient>();
        _fixture = new JiraApiFixtures();

        _jiraApiService = new JiraApiService("http://jiraapi.fr", "username", "password")
        {
            Http = _mockFetchApi.Object
        };
    }

    [Test]
    public async Task GetUpdatedIssues_ShouldReturnUpdatedIssues()
    {
        var startDate = new DateTime(2023, 01, 01);

        _mockFetchApi
            .SetupSequence(api => api.Post<PaginatedJqlIssue<JiraIssueFullWithChangelog>>(
                It.IsAny<string>(), It.IsAny<JqlParams>()))
            .ReturnsAsync(_fixture.CreateJiraIssueFullWithChangelogs(true))
            .ReturnsAsync(_fixture.CreateJiraIssueFullWithChangelogs());

        var issues = new List<JiraIssueFullWithChangelog>();
        await foreach (var result in _jiraApiService.GetUpdatedIssues(startDate))
        {
            issues.AddRange(result.Values);
        }

        Assert.That(issues, Is.Not.Empty);
        Assert.That(issues, Has.Count.EqualTo(20));
    }

    [Test]
    public void GetUpdatedIssues_ShouldStopWhenResponseIsNull()
    {
        var startDate = DateTime.Now;
        _mockFetchApi
            .Setup(api => api.Post<PaginatedJqlIssue<JiraIssueFullWithChangelog>>(
                It.IsAny<string>(), It.IsAny<JqlParams>()))
            .ReturnsAsync((PaginatedJqlIssue<JiraIssueFullWithChangelog>)null);

        var issues = new List<PaginatedWithTotal<JiraIssueFullWithChangelog>>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetUpdatedIssues(startDate))
            {
                issues.Add(result);
            }
        });
        Assert.That(issues, Is.Empty);
    }

    [Test]
    public async Task GetAllIssuesLight_ShouldReturnIssues()
    {
        _mockFetchApi
            .SetupSequence(api => api.Post<PaginatedJqlIssue<JiraIssueLight>>(
                It.IsAny<string>(), It.IsAny<JqlParams>()))
            .ReturnsAsync(_fixture.CreateJiraIssueLight(true))
            .ReturnsAsync(_fixture.CreateJiraIssueLight());

        var issues = new List<JiraIssueLight>();
        await foreach (var result in _jiraApiService.GetAllIssuesLight())
        {
            issues.AddRange(result.Values);
        }

        Assert.That(issues, Is.Not.Empty);
        Assert.That(issues, Has.Count.EqualTo(20));
    }

    [Test]
    public void GetAllIssuesLight_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Post<PaginatedJqlIssue<JiraIssueLight>>(
                It.IsAny<string>(), It.IsAny<JqlParams>()))
            .ReturnsAsync((PaginatedJqlIssue<JiraIssueLight>)null);

        var issues = new List<PaginatedWithTotal<JiraIssueLight>>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetAllIssuesLight())
            {
                issues.Add(result);
            }
        });
        Assert.That(issues, Is.Empty);
    }

    [Test]
    public async Task GetAllProjects_ShouldReturnProjects()
    {
        _mockFetchApi
            .SetupSequence(api => api.Get<PaginatedValues<JiraProject>>(
                It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreatePaginatedProjects(20))
            .ReturnsAsync(_fixture.CreatePaginatedProjects(20));

        var projects = new List<JiraProject>();
        await foreach (var result in _jiraApiService.GetAllProjects())
        {
            projects.AddRange(result.Values);
        }

        Assert.That(projects, Is.Not.Empty);
        Assert.That(projects, Has.Count.EqualTo(20));
    }

    [Test]
    public void GetAllProjects_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Get<PaginatedValues<JiraProject>>(
                It.IsAny<string>()))
            .ReturnsAsync((PaginatedValues<JiraProject>)null);

        var projects = new List<PaginatedValues<JiraProject>>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetAllProjects())
            {
                projects.Add(result);
            }
        });
        Assert.That(projects, Is.Empty);
    }

    [Test]
    public async Task GetAllProjectKeys_ShouldReturnProjects()
    {
        _mockFetchApi
            .SetupSequence(api => api.Get<PaginatedValues<JiraProject>>(
                It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreatePaginatedProjects(20))
            .ReturnsAsync(_fixture.CreatePaginatedProjects(20));

        var projects = await _jiraApiService.GetAllProjectKeys();

        Assert.That(projects, Is.Not.Empty);
        Assert.That(projects, Has.Count.EqualTo(20));
    }

    [Test]
    public void GetAllProjectKeys_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Get<PaginatedValues<JiraProject>>(
                It.IsAny<string>()))
            .ReturnsAsync((PaginatedValues<JiraProject>)null);

        var projects = new List<string>();
        Assert.DoesNotThrowAsync(async () => { projects = await _jiraApiService.GetAllProjectKeys(); });
        Assert.That(projects, Is.Empty);
    }

    [Test]
    public async Task GetAllUsers_ShouldReturnUsers()
    {
        _mockFetchApi
            .SetupSequence(api => api.Get<List<JiraUser>>(
                It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreateManyJiraUser(1000))
            .ReturnsAsync(_fixture.CreateManyJiraUser(1));

        var users = new List<JiraUser>();
        await foreach (var result in _jiraApiService.GetAllUsers())
        {
            users.AddRange(result);
        }

        Assert.That(users, Is.Not.Empty);
        Assert.That(users, Has.Count.EqualTo(1001));
    }

    [Test]
    public void GetAllUsers_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Get<List<JiraUser>>(
                It.IsAny<string>()))
            .ReturnsAsync((List<JiraUser>)null);

        var users = new List<JiraUser>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetAllUsers())
            {
                users.AddRange(result);
            }
        });
        Assert.That(users, Is.Empty);
    }

    [Test]
    public async Task GetAllVersionsFromProject_ShouldReturnVersions()
    {
        _mockFetchApi
            .SetupSequence(api => api.Get<PaginatedValues<JiraIssueVersion>>(
                It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreatePaginatedVersions(20))
            .ReturnsAsync(_fixture.CreatePaginatedVersions(20));

        var versions = new List<JiraIssueVersion>();
        await foreach (var result in _jiraApiService.GetAllVersionsFromProject("DEVING"))
        {
            versions.AddRange(result.Values);
        }

        Assert.That(versions, Is.Not.Empty);
        Assert.That(versions, Has.Count.EqualTo(20));
    }

    [Test]
    public void GetAllVersionsFromProject_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Get<PaginatedValues<JiraIssueVersion>>(
                It.IsAny<string>()))
            .ReturnsAsync((PaginatedValues<JiraIssueVersion>)null);

        var versions = new List<JiraIssueVersion>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetAllVersionsFromProject("DEVING"))
            {
                versions.AddRange(result.Values);
            }
        });
        Assert.That(versions, Is.Empty);
    }

    [Test]
    public async Task InsertMissingChangelogsAndWorklogs_ShouldReturnIssuesWithChangelogsAndWorklogs()
    {
        _mockFetchApi
            .Setup(api => api.Get<PaginatedValues<JiraChangeHistory>>(It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreateManyJiraChangelog(20));

        _mockFetchApi
            .Setup(api => api.Get<PaginatedWorklogs>(It.IsAny<string>()))
            .ReturnsAsync(_fixture.CreateManyJiraWorklogs(20));

        var issues = await _jiraApiService.InsertMissingChangelogsAndWorklogs([
            _fixture.CreateJiraIssue(),
            _fixture.CreateJiraIssue(20),
            _fixture.CreateJiraIssue(10, 20),
            _fixture.CreateJiraIssue(20, 20),
        ]);

        Assert.That(issues, Is.Not.Empty);
        Assert.That(issues, Has.Count.EqualTo(4));
        Assert.That(issues[0].Changelog.Histories, Has.Count.EqualTo(10));
        Assert.That(issues[0].Fields.Worklog.Worklogs, Has.Count.EqualTo(10));
        Assert.That(issues[1].Changelog.Histories, Has.Count.EqualTo(20));
        Assert.That(issues[1].Fields.Worklog.Worklogs, Has.Count.EqualTo(10));
        Assert.That(issues[2].Changelog.Histories, Has.Count.EqualTo(10));
        Assert.That(issues[2].Fields.Worklog.Worklogs, Has.Count.EqualTo(20));
        Assert.That(issues[3].Changelog.Histories, Has.Count.EqualTo(20));
        Assert.That(issues[3].Fields.Worklog.Worklogs, Has.Count.EqualTo(20));
    }

    [Test]
    public void InsertMissingChangelogsAndWorklogs_ShouldStopWhenResponseIsNull()
    {
        _mockFetchApi
            .Setup(api => api.Get<PaginatedValues<JiraIssueVersion>>(
                It.IsAny<string>()))
            .ReturnsAsync((PaginatedValues<JiraIssueVersion>)null);

        _mockFetchApi
            .Setup(api => api.Get<PaginatedWorklogs>(
                It.IsAny<string>()))
            .ReturnsAsync((PaginatedWorklogs)null);

        var versions = new List<JiraIssueVersion>();
        Assert.DoesNotThrowAsync(async () =>
        {
            await foreach (var result in _jiraApiService.GetAllVersionsFromProject("DEVING"))
            {
                versions.AddRange(result.Values);
            }
        });
        Assert.That(versions, Is.Empty);
    }
}

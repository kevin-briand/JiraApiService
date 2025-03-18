using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using JiraApi.Objects.Jira;
using JiraApi.Objects.Jira.V3.Changelog;
using JiraApi.Objects.Jira.V3.Issue;
using JiraApi.Objects.Jira.V3.Project;
using JiraApi.Objects.Jira.V3.User;
using AutoFixture;

namespace JiraApi.Tests.Helpers.Fixtures;

public class JiraApiFixtures
{
    private readonly IFixture _fixture;

    public JiraApiFixtures()
    {
        _fixture = new Fixture();
    }

    public List<JiraUser> CreateManyJiraUser(int total = 10)
    {
        return _fixture.CreateMany<JiraUser>(total).ToList();
    }

    public JiraIssueFullWithChangelog CreateJiraIssue(int totalChangelogs = 10, int totalWorklogs = 10)
    {
        return _fixture.Build<JiraIssueFullWithChangelog>()
            .With(f => f.Fields, CreateJiraIssueFields(totalWorklogs))
            .With(f => f.Changelog, CreateJiraChangelog(totalChangelogs))
            .Create();
    }

    public JiraWorklogCollection CreateJiraWorklogCollection(int totalWorklogs = 10)
    {
        return new JiraWorklogCollection
        {
            Worklogs = _fixture.CreateMany<JiraWorklog>(10).ToList(),
            StartAt = 0,
            MaxResults = 10,
            Total = totalWorklogs
        };
    }

    public JiraChangelog CreateJiraChangelog(int totalChangelogs = 10)
    {
        return new JiraChangelog
        {
            Histories = _fixture.CreateMany<JiraChangeHistory>(10)
                .ToList(),
            StartAt = 0,
            MaxResults = 10,
            Total = totalChangelogs
        };
    }

    public PaginatedWorklogs CreateManyJiraWorklogs(int total = 10)
    {
        return new PaginatedWorklogs
        {
            Worklogs = _fixture.CreateMany<JiraWorklog>(10).ToList(),
            IsLast = false,
            NextPage = "nextPageToken",
            Self = "selfLink",
            StartAt = 0,
            MaxResults = 10,
            Total = total
        };
    }

    public PaginatedValues<JiraChangeHistory> CreateManyJiraChangelog(int total = 10)
    {
        return new PaginatedValues<JiraChangeHistory>
        {
            Values = _fixture.CreateMany<JiraChangeHistory>(10).ToList(),
            IsLast = false,
            NextPage = "nextPageToken",
            Self = "selfLink",
            StartAt = 0,
            MaxResults = 10,
            Total = total
        };
    }

    public PaginatedValues<JiraIssueVersion> CreatePaginatedVersions(int total = 10)
    {
        return new PaginatedValues<JiraIssueVersion>
        {
            Values = _fixture.CreateMany<JiraIssueVersion>(10)
                .ToList(),
            IsLast = false,
            NextPage = "nextPageToken",
            Self = "selfLink",
            StartAt = 0,
            MaxResults = 10,
            Total = total
        };
    }

    public PaginatedValues<JiraProject> CreatePaginatedProjects(int total = 10)
    {
        return new PaginatedValues<JiraProject>
        {
            Values = _fixture.CreateMany<JiraProject>(10)
                .ToList(),
            IsLast = false,
            NextPage = "nextPageToken",
            Self = "selfLink",
            StartAt = 0,
            MaxResults = 10,
            Total = total
        };
    }

    internal JiraIssueFields CreateJiraIssueFields(int totalWorklogs = 10)
    {
        return _fixture.Build<JiraIssueFields>()
            .With(f => f.CustomFields, new Dictionary<string, JsonElement>())
            .With(f => f.Worklog, CreateJiraWorklogCollection(totalWorklogs))
            .Create();
    }

    internal PaginatedJqlIssue<JiraIssueFullWithChangelog> CreateJiraIssueFullWithChangelogs(bool token = false)
    {
        return new PaginatedJqlIssue<JiraIssueFullWithChangelog>
        {
            Issues = _fixture.Build<JiraIssueFullWithChangelog>()
                .With(f => f.Fields, CreateJiraIssueFields())
                .CreateMany(10).ToList(),
            NextPageToken = token ? "nextPageToken" : null
        };
    }

    internal PaginatedJqlIssue<JiraIssueLight> CreateJiraIssueLight(bool token = false)
    {
        return new PaginatedJqlIssue<JiraIssueLight>
        {
            Issues = _fixture.CreateMany<JiraIssueLight>(10).ToList(),
            NextPageToken = token ? "nextPageToken" : null
        };
    }
}

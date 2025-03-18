using JiraApi.Helpers;
using JiraApi.Objects;
using JiraApi.Objects.Jira;
using JiraApi.Objects.Jira.V3.Changelog;
using JiraApi.Objects.Jira.V3.Issue;
using JiraApi.Objects.Jira.V3.Project;
using JiraApi.Objects.Jira.V3.User;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace JiraApi;

/// <summary>
/// Class for interacting with Jira's API, providing methods to retrieve and manipulate data related to issues, projects, users, and changelogs.
/// <para><b>Note:</b> Most methods return an empty list if invalid credentials are provided (no errors are returned)</para>
/// </summary>
public class JiraApiService : IJiraApiService
{
    internal IJiraClient Http;

    /// <summary>
    /// Class for interacting with Jira's API, providing methods to retrieve and manipulate data related to issues, projects, users, and changelogs.
    /// <para><b>Note:</b> Most methods return an empty list if invalid credentials are provided (no errors are returned)</para>
    /// </summary>
    /// <param name="baseUrl">The Url of your atlassian instance</param>
    /// <param name="username">The username of the user</param>
    /// <param name="password">The password/token of the user</param>
    public JiraApiService(string baseUrl, string username, string password)
    {
        Http = new JiraClient(baseUrl, username, password);
    }

    public JiraApiService(IConfiguration configuration)
    {
        var settings = configuration.GetSection("JiraApi").Get<JiraApiSettings>();
        if (settings == null) throw new NullReferenceException("JiraApi settings not found");
        Http = new JiraClient(settings.BaseUrl, settings.Username, settings.Password);
    }

    private const string BasePath = "rest/api/3";

    public async IAsyncEnumerable<PaginatedWithTotal<JiraIssueFullWithChangelog>> GetUpdatedIssues(DateTime startDate)
    {
        var jqlParams = new JqlParams
        {
            Expand = "changelog",
            Fields = ["*all"],
            Jql = $"updated > '{startDate:yyyy-MM-dd HH:mm}'",
            MaxResults = 100
        };
        await foreach (var result in SearchJql<JiraIssueFullWithChangelog>(jqlParams))
        {
            yield return result;
        }
    }

    public async IAsyncEnumerable<PaginatedWithTotal<JiraIssueLight>> GetAllIssuesLight()
    {
        var jqlParams = new JqlParams
        {
            Fields = ["id", "key"],
            Jql = "created > 0 ORDER BY key ASC",
            MaxResults = 5000
        };

        await foreach (var result in SearchJql<JiraIssueLight>(jqlParams))
        {
            yield return result;
        }
    }

    public async IAsyncEnumerable<PaginatedValues<JiraProject>> GetAllProjects()
    {
        var jiraParams = new ProjectParams
        {
            StartAt = 0,
            MaxResults = 1000
        };
        await foreach (var prjs in GetPaginatedValues<PaginatedValues<JiraProject>>($"{BasePath}/project/search",
                           jiraParams))
        {
            yield return prjs;
        }
    }

    public async Task<List<string>> GetAllProjectKeys()
    {
        var projectKeys = new List<string>();
        await foreach (var prj in GetAllProjects())
        {
            projectKeys.AddRange(prj.Values.Select(p => p.Key));
        }

        return projectKeys;
    }

    public async IAsyncEnumerable<List<JiraUser>> GetAllUsers()
    {
        var jiraParams = new ProjectParams
        {
            StartAt = 0,
            MaxResults = 1000
        };

        var total = 0;
        do
        {
            jiraParams.StartAt = total;
            var response =
                await Http.Get<List<JiraUser>>(
                    $"{BasePath}/users/search{JiraHelpers.ParamsToPath(jiraParams)}");
            if (response == null)
            {
                Log.Error(
                    $"Failed to get paginated results from url: {BasePath}/users/search{JiraHelpers.ParamsToPath(jiraParams)}");
                yield break;
            }

            total += response.Count;

            yield return response;
        } while (total > jiraParams.StartAt && total % jiraParams.MaxResults == 0);
    }

    public async IAsyncEnumerable<PaginatedValues<JiraIssueVersion>> GetAllVersionsFromProject(string projectKey)
    {
        var jiraParams = new ProjectParams
        {
            StartAt = 0,
            MaxResults = 1000
        };
        await foreach (var versions in GetPaginatedValues<PaginatedValues<JiraIssueVersion>>(
                           $"{BasePath}/project/{projectKey}/version", jiraParams))
        {
            yield return versions;
        }
    }

    public async Task<List<JiraIssueFullWithChangelog>> InsertMissingChangelogsAndWorklogs(
        List<JiraIssueFullWithChangelog> issues)
    {
        foreach (var issue in issues)
        {
            if (issue.Changelog.Histories.Count < issue.Changelog.Total)
            {
                List<JiraChangeHistory> changelogs = [];
                await foreach (var cgls in GetAllChangelogs(issue.Id))
                {
                    changelogs.AddRange(cgls.Values);
                }

                issue.Changelog.Histories = changelogs;
                issue.Changelog.Total = changelogs.Count;
            }

            if (issue.Fields.Worklog.Worklogs.Count < issue.Fields.Worklog.Total)
            {
                List<JiraWorklog> worklogs = [];
                await foreach (var wls in GetAllWorklogs(issue.Id))
                {
                    worklogs.AddRange(wls.Worklogs);
                }

                issue.Fields.Worklog.Worklogs = worklogs;
                issue.Fields.Worklog.Total = worklogs.Count;
            }
        }

        return issues;
    }

    public async IAsyncEnumerable<PaginatedValues<JiraChangeHistory>> GetAllChangelogs(string issueId)
    {
        var jiraParams = new ProjectParams
        {
            StartAt = 0,
            MaxResults = 100
        };
        await foreach (var cgls in GetPaginatedValues<PaginatedValues<JiraChangeHistory>>(
                           $"{BasePath}/issue/{issueId}/changelog", jiraParams))
        {
            yield return cgls;
        }
    }

    public async IAsyncEnumerable<PaginatedWorklogs> GetAllWorklogs(string issueId)
    {
        var jiraParams = new ProjectParams
        {
            StartAt = 0,
            MaxResults = 5000
        };
        await foreach (var wls in GetPaginatedValues<PaginatedWorklogs>($"{BasePath}/issue/{issueId}/worklog",
                           jiraParams))
        {
            yield return wls;
        }
    }

    private async Task<int> JqlCount(JqlParams jqlParams)
    {
        var res = await Http.Post<CountIssues>($"{BasePath}/search/approximate-count", jqlParams.Jql);
        return res?.Count ?? 0;
    }

    private async IAsyncEnumerable<PaginatedWithTotal<T>> SearchJql<T>(JqlParams jqlParams)
    {
        var total = await JqlCount(jqlParams);

        PaginatedJqlIssue<T>? response = null;
        do
        {
            if (response is { NextPageToken: not null })
            {
                jqlParams.NextPageToken = response.NextPageToken;
            }

            response = await Http.Post<PaginatedJqlIssue<T>>($"{BasePath}/search/jql", jqlParams);
            if (response == null)
            {
                Log.Error($"Failed to get paginated results from url: {BasePath}/search/jql");
                yield break;
            }

            yield return new PaginatedWithTotal<T>
            {
                Values = response.Issues,
                Total = total
            };
        } while (response.NextPageToken != null);
    }

    private async IAsyncEnumerable<T> GetPaginatedValues<T>(string url, ProjectParams pParams)
        where T : PaginatedResponse
    {
        T? res;
        do
        {
            res = await Http.Get<T>($"{url}{JiraHelpers.ParamsToPath(pParams)}");
            if (res == null)
            {
                Log.Error($"Failed to get paginated results from url: {url}{JiraHelpers.ParamsToPath(pParams)}");
                yield break;
            }

            yield return res;
            pParams.StartAt += res.MaxResults;
        } while (pParams.StartAt < res.Total);
    }
}

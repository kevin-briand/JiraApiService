using JiraApi.Objects;
using JiraApi.Objects.Jira;
using JiraApi.Objects.Jira.V3.Changelog;
using JiraApi.Objects.Jira.V3.Issue;
using JiraApi.Objects.Jira.V3.Project;
using JiraApi.Objects.Jira.V3.User;

namespace JiraApi;

/// <summary>
/// Interface for interacting with Jira's API, providing methods to retrieve and manipulate data related to issues, projects, users, and changelogs.
/// <para>It supports paginated data and asynchronous operations.</para>
/// <para><b>Note:</b> Most methods return an empty list if invalid credentials are provided (no errors are returned)</para>
/// </summary>
public interface IJiraApiService
{
    /// <summary>
    /// Retrieves all updated issues starting from a specified date. This method supports pagination and returns updated issues along with their changelogs and worklogs.
    /// <para>Changelogs and worklogs are limited, if you need all changelogs/worklogs, you can use the InsertMissingChangelogsAndWorklogs method after retrieving the list of issues.</para>
    /// </summary>
    /// <param name="startDate">The date from which to start retrieving updated issues.</param>
    /// <returns>An asynchronous enumerable of paginated updated issues with changelogs.</returns>
    IAsyncEnumerable<PaginatedWithTotal<JiraIssueFullWithChangelog>> GetUpdatedIssues(DateTime startDate);

    /// <summary>
    /// Retrieves all issues in a lightweight format (id, key). This method supports pagination and returns a list of issues with minimal details.
    /// </summary>
    /// <returns>An asynchronous enumerable of paginated lightweight issues.</returns>
    IAsyncEnumerable<PaginatedWithTotal<JiraIssueLight>> GetAllIssuesLight();

    /// <summary>
    /// Retrieves all Jira projects. This method supports pagination and returns a collection of projects.
    /// </summary>
    /// <returns>An asynchronous enumerable of paginated Jira projects.</returns>
    IAsyncEnumerable<PaginatedValues<JiraProject>> GetAllProjects();

    /// <summary>
    /// Retrieves all project keys from Jira.
    /// </summary>
    /// <returns>A task that returns a list of project keys.</returns>
    Task<List<string>> GetAllProjectKeys();

    /// <summary>
    /// Retrieves all Jira users. This method supports pagination and returns a list of users.
    /// </summary>
    /// <returns>An asynchronous enumerable of paginated lists of Jira users.</returns>
    IAsyncEnumerable<List<JiraUser>> GetAllUsers();

    /// <summary>
    /// Retrieves all versions for a given Jira project. This method supports pagination and returns a list of versions associated with the specified project.
    /// </summary>
    /// <param name="projectKey">The key of the Jira project for which versions are being retrieved.</param>
    /// <returns>An asynchronous enumerable of paginated Jira issue versions for the specified project.</returns>
    IAsyncEnumerable<PaginatedValues<JiraIssueVersion>> GetAllVersionsFromProject(string projectKey);

    /// <summary>
    /// Inserts missing changelogs and worklogs for a list of Jira issues.
    /// </summary>
    /// <param name="issues">A list of issues that need their changelogs and worklogs inserted.</param>
    /// <returns>A task that returns a list of issues after the missing changelogs and worklogs are inserted.</returns>
    Task<List<JiraIssueFullWithChangelog>> InsertMissingChangelogsAndWorklogs(List<JiraIssueFullWithChangelog> issues);

    /// <summary>
    /// Retrieves all changelogs for a specific Jira issue. This method supports pagination and returns the changelog history for the issue.
    /// </summary>
    /// <param name="issueId">The ID of the Jira issue for which changelogs are being retrieved.</param>
    /// <returns>An asynchronous enumerable of paginated changelog entries for the specified issue.</returns>
    IAsyncEnumerable<PaginatedValues<JiraChangeHistory>> GetAllChangelogs(string issueId);

    /// <summary>
    /// Retrieves all worklogs for a specific Jira issue. This method supports pagination and returns the worklog history for the issue.
    /// </summary>
    /// <param name="issueId">The ID of the Jira issue for which worklogs are being retrieved.</param>
    /// <returns>An asynchronous enumerable of paginated worklog entries for the specified issue.</returns>
    IAsyncEnumerable<PaginatedWorklogs> GetAllWorklogs(string issueId);
}

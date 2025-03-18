using JiraApi.Objects.Jira.V3.Changelog;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssue
{
    /// <summary>
    /// The ID of the issue.
    /// </summary>
    public required string Id { get; set; }
    /// <summary>
    /// The URL of the issue details.
    /// </summary>
    public required string Self { get; set; }
    /// <summary>
    /// The key of the issue.
    /// </summary>
    public required string Key { get; set; }
    /// <summary>
    /// The fields of the issue.
    /// </summary>
    public JiraIssueFields? Fields { get; set; }
    /// <summary>
    /// The list of changelogs.
    /// </summary>
    public JiraChangelog? Changelog { get; set; }
}

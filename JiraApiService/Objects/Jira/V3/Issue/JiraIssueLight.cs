namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueLight
{
    /// <summary>
    /// The ID of the issue.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// The key of the issue.
    /// </summary>
    public required string Key { get; set; }
}

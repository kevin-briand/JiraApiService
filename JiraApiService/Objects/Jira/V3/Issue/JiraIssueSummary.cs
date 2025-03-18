namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueSummary
{
    /// <summary>
    /// The unique identifier of the Jira issue.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// The key of the Jira issue, typically in the format PROJECT-123.
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// A brief summary or title of the Jira issue.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// The type of the Jira issue, such as "Bug", "Task", or "Story".
    /// </summary>
    public JiraIssueType? IssueType { get; set; }

    /// <summary>
    /// The current status of the Jira issue, such as "To Do", "In Progress", or "Done".
    /// </summary>
    public JiraStatus? Status { get; set; }
}

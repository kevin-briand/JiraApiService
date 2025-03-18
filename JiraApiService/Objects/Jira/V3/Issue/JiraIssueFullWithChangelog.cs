using JiraApi.Objects.Jira.V3.Changelog;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueFullWithChangelog : JiraIssue
{
    /// <summary>
    /// The fields of the Jira issue, including details like summary, description, priority,
    /// assignee, etc.
    /// </summary>
    public new required JiraIssueFields Fields { get; set; }

    /// <summary>
    /// The changelog associated with the Jira issue, including the history of field changes
    /// throughout the life of the issue.
    /// </summary>
    public new required JiraChangelog Changelog { get; set; }

    /// <summary>
    /// A list of names associated with the Jira issue, such as related components or labels.
    /// </summary>
    public List<string>? Names { get; set; }
}

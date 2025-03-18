using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraStatus : JiraEntity
{
    /// <summary>
    /// A brief description of the status (e.g., "To Do", "In Progress", "Done").
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The URL of the icon representing the status.
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// The category to which the status belongs (e.g., "New", "In Progress", "Complete").
    /// </summary>
    public JiraStatusCategory? StatusCategory { get; set; }
}

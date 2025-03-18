using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraPriority : JiraEntity
{
    /// <summary>
    /// The URL of the icon associated with the priority level.
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// The order of the priority, where lower values typically indicate higher priority.
    /// </summary>
    public int? Order { get; set; }
}

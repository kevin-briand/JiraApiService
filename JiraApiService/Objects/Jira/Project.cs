using JiraApi.Objects.Jira.v3;
using JiraApi.Objects.Jira.V3.Issue;
using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira;

public class Project : JiraEntity
{
    /// <summary>
    /// The key of the project (typically used in issue keys, e.g., "PROJ").
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// A brief description of the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The lead user of the project (typically the project manager).
    /// </summary>
    public JiraUser? Lead { get; set; }

    /// <summary>
    /// The list of issue types associated with the project (e.g., Bug, Task, etc.).
    /// </summary>
    public List<JiraIssueType>? IssueTypes { get; set; }
}

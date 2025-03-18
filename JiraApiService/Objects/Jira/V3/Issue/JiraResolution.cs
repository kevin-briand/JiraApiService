using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraResolution : JiraEntity
{
    /// <summary>
    /// The description of the resolution, providing details on how the issue was resolved.
    /// </summary>
    public string? Description { get; set; }
}

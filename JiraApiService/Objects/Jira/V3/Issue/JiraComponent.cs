using JiraApi.Objects.Jira.v3;
using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraComponent : JiraEntity
{
    /// <summary>
    /// The description of the component, providing details about its purpose or role within the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The lead user responsible for the component.
    /// </summary>
    public JiraUser? Lead { get; set; }

    /// <summary>
    /// The type of assignee for the component (e.g., "Project Lead", "Unassigned").
    /// </summary>
    public string? AssigneeType { get; set; }

    /// <summary>
    /// The actual user who is assigned to the component.
    /// </summary>
    public JiraUser? RealAssignee { get; set; }
}

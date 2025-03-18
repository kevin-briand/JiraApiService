using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Project;

public class JiraProject : JiraEntity
{
    /// <summary>
    /// The unique key of the Jira project, typically used for referencing the project in Jira queries and APIs.
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// A self-link URL pointing to the project's detailed information in Jira.
    /// </summary>
    public required string Self { get; set; }

    /// <summary>
    /// Indicates whether the project is using a simplified configuration. Simplified projects typically have fewer options for customization.
    /// </summary>
    public required bool Simplified { get; set; }

    /// <summary>
    /// The style of the project, often related to how the project's interface is displayed in Jira.
    /// </summary>
    public required string Style { get; set; }
}

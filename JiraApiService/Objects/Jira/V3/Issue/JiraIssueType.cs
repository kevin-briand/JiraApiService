using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueType : JiraEntity
{
    /// <summary>
    /// The description of the issue type.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether this issue type is a subtask type.
    /// </summary>
    public bool? Subtask { get; set; }

    /// <summary>
    /// The URL of the icon associated with this issue type.
    /// </summary>
    public string? IconUrl { get; set; }

    /// <summary>
    /// The ID of the avatar used for this issue type.
    /// </summary>
    public int? AvatarId { get; set; }
}

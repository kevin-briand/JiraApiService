using JiraApi.Objects.Jira.v3;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueVersion : JiraEntity
{
    /// <summary>
    /// The description of the version, providing additional information about the version.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the version is archived.
    /// </summary>
    public bool? Archived { get; set; }

    /// <summary>
    /// Indicates whether the version has been released.
    /// </summary>
    public bool? Released { get; set; }

    /// <summary>
    /// The release date of the version, typically in string format (e.g., "yyyy-MM-dd").
    /// </summary>
    public string? ReleaseDate { get; set; }
}

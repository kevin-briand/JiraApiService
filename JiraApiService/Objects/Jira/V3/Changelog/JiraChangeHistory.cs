using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Changelog;

public class JiraChangeHistory
{
    /// <summary>
    /// The ID of the changelog.
    /// </summary>
    public required string Id { get; set; }
    /// <summary>
    /// The user who made the change.
    /// </summary>
    public JiraUser? Author { get; set; }
    /// <summary>
    /// The date on which the change took place.
    /// </summary>
    public required DateTime Created { get; set; }
    /// <summary>
    /// The list of items changed.
    /// </summary>
    public required List<JiraChangeItem> Items { get; set; }
}

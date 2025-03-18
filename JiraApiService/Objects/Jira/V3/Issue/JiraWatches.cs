using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraWatches
{
    /// <summary>
    /// The total number of users watching the Jira issue.
    /// </summary>
    public int? WatchCount { get; set; }

    /// <summary>
    /// A flag indicating whether the current user is watching the Jira issue.
    /// </summary>
    public bool? IsWatching { get; set; }

    /// <summary>
    /// A list of users who are watching the Jira issue.
    /// </summary>
    public List<JiraUser>? Watchers { get; set; }
}

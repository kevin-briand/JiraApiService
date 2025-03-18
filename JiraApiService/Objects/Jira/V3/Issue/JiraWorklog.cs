using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraWorklog
{
    /// <summary>
    /// The unique identifier of the worklog entry.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// The user who created the worklog entry.
    /// </summary>
    public JiraUser? Author { get; set; }

    /// <summary>
    /// The date and time when the worklog entry was created.
    /// </summary>
    public DateTime? Created { get; set; }

    /// <summary>
    /// The date and time when the worklog entry was last updated.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// The comment associated with the worklog entry, typically describing the work done.
    /// </summary>
    public JiraDescription? Comment { get; set; }

    /// <summary>
    /// The total time spent on the issue, measured in seconds.
    /// </summary>
    public int? TimeSpentSeconds { get; set; }

    /// <summary>
    /// The date and time when the worklog was started.
    /// </summary>
    public DateTime? Started { get; set; }

    /// <summary>
    /// The user who last updated the worklog entry.
    /// </summary>
    public JiraUser? UpdateAuthor { get; set; }
}

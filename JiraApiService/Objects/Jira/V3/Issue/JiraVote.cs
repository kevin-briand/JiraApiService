using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraVote
{
    /// <summary>
    /// The total number of votes on the Jira issue.
    /// </summary>
    public int? VotesCount { get; set; }

    /// <summary>
    /// A flag indicating whether the current user has voted on the issue.
    /// </summary>
    public bool? HasVoted { get; set; }

    /// <summary>
    /// A list of users who have voted on the Jira issue.
    /// </summary>
    public List<JiraUser>? Voters { get; set; }
}

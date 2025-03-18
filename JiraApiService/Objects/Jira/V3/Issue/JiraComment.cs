using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraComment
{
    /// <summary>
    /// The unique identifier of the comment.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// The user who created the comment.
    /// </summary>
    public JiraUser? Author { get; set; }

    /// <summary>
    /// The user who last updated the comment, if applicable.
    /// </summary>
    public JiraUser? UpdateAuthor { get; set; }

    /// <summary>
    /// The date and time when the comment was created.
    /// </summary>
    public required DateTime Created { get; set; }

    /// <summary>
    /// The date and time when the comment was last updated.
    /// </summary>
    public required DateTime Updated { get; set; }

    /// <summary>
    /// The content of the comment, stored as a JiraDescription object that can include formatted text.
    /// </summary>
    public JiraDescription? Body { get; set; }
}

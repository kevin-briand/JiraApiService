namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraCommentCollection : JiraCollectionInfo
{
    /// <summary>
    /// A list of comments associated with a Jira issue. Each comment contains details like the author and the text of the comment.
    /// </summary>
    public required List<JiraComment> Comments { get; set; }
}

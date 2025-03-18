namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraWorklogCollection : JiraCollectionInfo
{
    /// <summary>
    /// A list of worklogs associated with a Jira issue. Each worklog contains details about time spent on the issue.
    /// </summary>
    public required List<JiraWorklog> Worklogs { get; set; }
}

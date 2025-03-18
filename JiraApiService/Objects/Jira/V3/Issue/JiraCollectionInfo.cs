namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraCollectionInfo
{
    /// <summary>
    /// The index of the first item returned on the page.
    /// </summary>
    public required int StartAt { get; set; }
    /// <summary>
    /// The maximum number of results that could be on the page.
    /// </summary>
    public required int MaxResults { get; set; }
    /// <summary>
    /// The number of results.
    /// </summary>
    public required int Total { get; set; }
}

using JiraApi.Objects.Jira.V3.Issue;

namespace JiraApi.Objects.Jira;

public class PaginatedResponse : JiraCollectionInfo
{
    /// <summary>
    /// A value indicating whether this response is the last page of the query results.
    /// </summary>
    public bool IsLast { get; set; }

    /// <summary>
    /// The token to fetch the next page of results in a paginated query.
    /// If this is null, there are no more pages of results.
    /// </summary>
    public string? NextPage { get; set; }

    /// <summary>
    /// The URI (Self link) that points to the current page of results.
    /// </summary>
    public string? Self { get; set; }
}

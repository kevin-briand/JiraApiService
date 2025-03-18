namespace JiraApi.Objects.Jira;

internal class PaginatedJqlIssue<T>
{
    public required List<T> Issues { get; set; }
    public string? NextPageToken { get; set; }
}

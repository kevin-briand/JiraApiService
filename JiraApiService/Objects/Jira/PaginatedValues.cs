namespace JiraApi.Objects.Jira;

public class PaginatedValues<T> : PaginatedResponse
{
    /// <summary>
    /// The list of values returned by the paginated query.
    /// </summary>
    public required List<T> Values { get; set; }
}

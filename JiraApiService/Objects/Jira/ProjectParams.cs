namespace JiraApi.Objects.Jira;

public class ProjectParams
{
    /// <summary>
    /// The index of the first project to return (used for pagination).
    /// </summary>
    public int? StartAt { get; set; }

    /// <summary>
    /// The maximum number of projects to return (used for pagination).
    /// </summary>
    public int? MaxResults { get; set; }

    /// <summary>
    /// The field by which to order the results.
    /// Can be used for sorting the returned projects.
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// A list of project IDs to filter the results by.
    /// </summary>
    public List<int>? Id { get; set; }

    /// <summary>
    /// A list of project keys to filter the results by.
    /// </summary>
    public List<string>? Keys { get; set; }

    /// <summary>
    /// A query string to further filter the projects based on custom criteria.
    /// </summary>
    public string? Query { get; set; }

    /// <summary>
    /// The key of the project type to filter by.
    /// </summary>
    public string? TypeKey { get; set; }

    /// <summary>
    /// The category ID to filter projects by.
    /// </summary>
    public int? CategoryId { get; set; }

    /// <summary>
    /// An action to be performed on the project (such as "create", "update", etc.).
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// A string indicating additional data to include in the project response (used for expanding related data).
    /// </summary>
    public string? Expand { get; set; }
}

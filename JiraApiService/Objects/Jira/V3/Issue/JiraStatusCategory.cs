namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraStatusCategory
{
    /// <summary>
    /// The unique identifier of the status category.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// The key representing the status category, typically used to identify the category in API calls.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// The name of the status category (e.g., "New", "In Progress", "Complete").
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The color associated with the status category, often used in the UI to distinguish between different categories.
    /// </summary>
    public string? ColorName { get; set; }
}

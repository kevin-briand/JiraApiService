namespace JiraApi.Objects.Jira;

public class JqlParams
{
    /// <summary>
    /// The expand option to include additional data in the query response.
    /// For example, to expand changelogs or other issue-related data.
    /// </summary>
    public string? Expand { get; set; }

    /// <summary>
    /// The list of fields to return in the query result.
    /// If not set, only the IDs will be returned.
    /// </summary>
    public List<string>? Fields { get; set; }

    /// <summary>
    /// A flag indicating whether to return the field keys instead of their names.
    /// If true, Jira will return the field keys for custom fields instead of their human-readable names.
    /// </summary>
    public bool? FieldsByKeys { get; set; }

    /// <summary>
    /// The JQL (Jira Query Language) query to retrieve issues based on certain criteria.
    /// </summary>
    public required string Jql { get; set; }

    /// <summary>
    /// The maximum number of results to return from the query.
    /// If not set, Jira will return the default number of results.
    /// </summary>
    public int? MaxResults { get; set; }

    /// <summary>
    /// The token to retrieve the next page of results in paginated queries.
    /// </summary>
    public string? NextPageToken { get; set; }

    /// <summary>
    /// The list of additional properties to include in the query result.
    /// These are typically specific properties that need to be returned alongside issues.
    /// </summary>
    public List<string>? Properties { get; set; }

    /// <summary>
    /// The list of issue IDs to reconcile. Used to ensure issues are synchronized or updated.
    /// </summary>
    public List<int?>? ReconcileIssues { get; set; }
}

using JiraApi.Objects.Jira.V3.Issue;

namespace JiraApi.Objects.Jira;

public class PaginatedWorklogs : PaginatedResponse
{
    /// <summary>
    /// The list of worklogs returned by the paginated query.
    /// </summary>
    public required List<JiraWorklog> Worklogs { get; set; }
}

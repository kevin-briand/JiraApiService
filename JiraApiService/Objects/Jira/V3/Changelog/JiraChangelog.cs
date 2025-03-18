using JiraApi.Objects.Jira.V3.Issue;

namespace JiraApi.Objects.Jira.V3.Changelog;

public class JiraChangelog : JiraCollectionInfo
{
    /// <summary>
    /// The list of changelogs.
    /// </summary>
    public required List<JiraChangeHistory> Histories { get; set; }
}

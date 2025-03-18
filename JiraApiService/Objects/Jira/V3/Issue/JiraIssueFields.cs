using System.Text.Json;
using System.Text.Json.Serialization;
using JiraApi.Objects.Jira.V3.User;

namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraIssueFields
{
    /// <summary>
    /// The summary (title) of the issue.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// The detailed description of the issue.
    /// </summary>
    public JiraDescription? Description { get; set; }

    /// <summary>
    /// The type of issue (e.g., Bug, Task, Story).
    /// </summary>
    public JiraIssueType? Issuetype { get; set; }

    /// <summary>
    /// The project to which this issue belongs.
    /// </summary>
    public Jira.Project? Project { get; set; }

    /// <summary>
    /// Components associated with the issue (e.g., backend, frontend).
    /// </summary>
    public List<JiraComponent>? Components { get; set; }

    /// <summary>
    /// Versions where this issue is resolved.
    /// </summary>
    public List<JiraIssueVersion>? FixVersions { get; set; }

    /// <summary>
    /// Affects versions related to the issue.
    /// </summary>
    public List<JiraIssueVersion>? Versions { get; set; }

    /// <summary>
    /// The priority of the issue (e.g., High, Medium, Low).
    /// </summary>
    public JiraPriority? Priority { get; set; }

    /// <summary>
    /// The resolution of the issue (e.g., Fixed, Won't Fix).
    /// </summary>
    public JiraResolution? Resolution { get; set; }

    /// <summary>
    /// The user assigned to work on the issue.
    /// </summary>
    public JiraUser? Assignee { get; set; }

    /// <summary>
    /// The user who reported the issue.
    /// </summary>
    public JiraUser? Reporter { get; set; }

    /// <summary>
    /// The user who created the issue.
    /// </summary>
    public JiraUser? Creator { get; set; }

    /// <summary>
    /// The timestamp when the issue was created.
    /// </summary>
    public DateTime? Created { get; set; }

    /// <summary>
    /// The timestamp when the issue was last updated.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// The date when the issue was resolved.
    /// </summary>
    public DateTime? ResolutionDate { get; set; }

    /// <summary>
    /// The due date for completing the issue.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Labels assigned to the issue.
    /// </summary>
    public List<string>? Labels { get; set; }

    /// <summary>
    /// The original estimated time in seconds.
    /// </summary>
    public int? TimeEstimate { get; set; }

    /// <summary>
    /// The total original estimated time for all sub-tasks.
    /// </summary>
    public int? AggregatetimeOriginalEstimate { get; set; }

    /// <summary>
    /// The total estimated remaining time for all sub-tasks.
    /// </summary>
    public int? AggregatetimeEstimate { get; set; }

    /// <summary>
    /// The total time spent on the issue (including sub-tasks) in seconds.
    /// </summary>
    public int? AggregatetimeSpent { get; set; }

    /// <summary>
    /// Worklogs (time tracking entries) for the issue.
    /// </summary>
    public required JiraWorklogCollection Worklog { get; set; }

    /// <summary>
    /// Comments associated with the issue.
    /// </summary>
    public JiraCommentCollection? Comment { get; set; }

    /// <summary>
    /// Vote count and information about users who voted for this issue.
    /// </summary>
    public JiraVote? Votes { get; set; }

    /// <summary>
    /// List of sub-tasks linked to this issue.
    /// </summary>
    public List<JiraIssueSummary>? Subtasks { get; set; }

    /// <summary>
    /// The environment where the issue was found (e.g., OS, Browser, Version).
    /// </summary>
    public string? Environment { get; set; }

    /// <summary>
    /// Watchers (users monitoring the issue).
    /// </summary>
    public JiraWatches? Watches { get; set; }

    /// <summary>
    /// Custom fields that do not have a predefined mapping.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? CustomFields { get; set; }
}

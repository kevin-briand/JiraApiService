namespace JiraApi.Objects.Jira.v3;

public abstract class JiraEntity
{
    /// <summary>
    /// The unique identifier of the Jira entity. This is a required field and typically used to uniquely identify the entity in Jira.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// The name of the Jira entity. This is an optional field and may represent the entity's title or label.
    /// </summary>
    public string? Name { get; set; }
}

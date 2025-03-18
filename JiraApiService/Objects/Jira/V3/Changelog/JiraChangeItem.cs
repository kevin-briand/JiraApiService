namespace JiraApi.Objects.Jira.V3.Changelog;

public class JiraChangeItem
{
    /// <summary>
    /// The name of the field changed.
    /// </summary>
    public required string Field { get; set; }
    /// <summary>
    /// The type of the field changed.
    /// </summary>
    public required string Fieldtype { get; set; }
    /// <summary>
    /// The details of the original value.
    /// </summary>
    public string? From { get; set; }
    /// <summary>
    /// The details of the original value as a string.
    /// </summary>
    public string? FromString { get; set; }
    /// <summary>
    /// The details of the new value.
    /// </summary>
    public string? To { get; set; }
    /// <summary>
    /// The details of the new value as a string.
    /// </summary>
    public new string? ToString { get; set; }
}

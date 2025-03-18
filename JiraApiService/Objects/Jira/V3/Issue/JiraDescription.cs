namespace JiraApi.Objects.Jira.V3.Issue;

public class JiraDescription
{
    /// <summary>
    /// The type of description format (usually "doc").
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// The version of the description format.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// The list of content elements that make up the description.
    /// </summary>
    public required List<JiraContent> Content { get; set; }
}

public class JiraContent
{
    /// <summary>
    /// The type of content (e.g., "paragraph", "list", "heading").
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// The list of text elements inside the content block.
    /// </summary>
    public required List<JiraTextContent> Content { get; set; }
}

public class JiraTextContent
{
    /// <summary>
    /// The actual text content.
    /// </summary>
    public string? Text { get; set; }
}

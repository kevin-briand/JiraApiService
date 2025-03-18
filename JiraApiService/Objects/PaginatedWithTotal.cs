namespace JiraApi.Objects;

public class PaginatedWithTotal<T>
{
    /// <summary>
    /// The total number of items available across all pages.
    /// This value allows for determining the total size of the dataset.
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// The list of values returned for the current page.
    /// This contains the actual data that has been retrieved in the paginated response.
    /// </summary>
    public required List<T> Values { get; set; }
}

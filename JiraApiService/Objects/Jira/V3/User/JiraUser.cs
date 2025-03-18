namespace JiraApi.Objects.Jira.V3.User;

public class JiraUser
{
    /// <summary>
    /// The account ID of the user, which uniquely identifies the user across all Atlassian products. For example, 5b10ac8d82e05b22cc7d4ef5.
    /// </summary>
    public required string AccountId { get; set; }
    /// <summary>
    /// The email address of the user. Depending on the user’s privacy settings, this may be returned as null.
    /// </summary>
    public string? EmailAddress { get; set; }
    /// <summary>
    /// The type of account represented by this user. This will be one of 'atlassian' (normal users), 'app' (application user) or 'customer' (Jira Service Desk customer user)
    /// <remarks>AccountType constant available</remarks>
    /// </summary>
    public string? AccountType { get; set; }
    /// <summary>
    /// The display name of the user. Depending on the user’s privacy settings, this may return an alternative value.
    /// </summary>
    public required string DisplayName { get; set; }
    /// <summary>
    /// Whether the user is active.
    /// </summary>
    public bool? Active { get; set; }
    /// <summary>
    /// The time zone specified in the user's profile. Depending on the user’s privacy settings, this may be returned as null.
    /// </summary>
    public string? TimeZone { get; set; }
    /// <summary>
    /// The avatars of the user.
    /// </summary>
    public Dictionary<string, string>? AvatarUrls { get; set; }
}

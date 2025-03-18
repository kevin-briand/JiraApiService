using System.Text.Json;
using JiraApi.Objects.Jira;

namespace JiraApi.Helpers;

public static class JiraHelpers
{
    internal static string ParamsToPath(ProjectParams projectParams)
    {
        var urlParams = "?";
        if (projectParams.StartAt.HasValue)
            urlParams += $"&startAt={projectParams.StartAt.Value}";

        if (projectParams.MaxResults.HasValue)
            urlParams += $"&maxResults={projectParams.MaxResults.Value}";

        if (!string.IsNullOrEmpty(projectParams.OrderBy))
            urlParams += $"&orderBy={projectParams.OrderBy}";

        if (projectParams.Id?.Count > 0)
            urlParams += "&id=" + string.Join("&id=", projectParams.Id);

        if (projectParams.Keys?.Count > 0)
            urlParams += "&keys=" + string.Join("&keys=", projectParams.Keys);

        if (!string.IsNullOrEmpty(projectParams.Query))
            urlParams += $"&query={projectParams.Query}";

        if (!string.IsNullOrEmpty(projectParams.TypeKey))
            urlParams += $"&typeKey={projectParams.TypeKey}";

        if (projectParams.CategoryId.HasValue)
            urlParams += $"&categoryId={projectParams.CategoryId.Value}";

        if (!string.IsNullOrEmpty(projectParams.Action))
            urlParams += $"&action={projectParams.Action}";

        if (!string.IsNullOrEmpty(projectParams.Expand))
            urlParams += $"&expand={projectParams.Expand}";

        return urlParams;
    }

    public static string? ToStringValue(this Dictionary<string, JsonElement>? fieldNames, string fieldName)
    {
        if (fieldNames == null || !fieldNames.TryGetValue(fieldName, out var valueObj)) return null;
        return valueObj.ValueKind == JsonValueKind.Null ? null : valueObj.GetRawText();
    }

    public static float? ToFloatValue(this Dictionary<string, JsonElement>? fieldNames, string fieldName)
    {
        if (fieldNames == null || !fieldNames.TryGetValue(fieldName, out var valueObj)) return null;
        return valueObj.ValueKind == JsonValueKind.Number ? valueObj.GetSingle() : null;
    }
}

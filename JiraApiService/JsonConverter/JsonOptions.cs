using System.Text.Json;
using System.Text.Json.Serialization;

namespace JiraApi.JsonConverter;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new DateTimeConverter() }
    };
}

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JiraApi.JsonConverter;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-ddTHH:mm:ss.fffzzz"; // 2021-01-17T12:34:00.000+0000

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString()!;

        if (DateTimeOffset.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffzzz",
                null, DateTimeStyles.None, out var dateTimeOffset))
        {
            return dateTimeOffset.UtcDateTime;
        }

        throw new JsonException($"Format de date invalide : {dateString}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}

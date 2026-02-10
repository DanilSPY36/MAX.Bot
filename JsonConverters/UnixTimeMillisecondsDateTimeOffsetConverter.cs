using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.JsonConverters
{
    public class UnixTimeMillisecondsDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var seconds = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeMilliseconds(seconds);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.ToUnixTimeSeconds());
        }
    }
}
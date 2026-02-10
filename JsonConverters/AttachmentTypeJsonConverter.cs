using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MAX.Bot.Models.Enums;

namespace MAX.Bot.JsonConverters
{
    internal class AttachmentTypeJsonConverter : JsonConverter<AttachmentType>
    {
        public override AttachmentType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            foreach (var field in typeof(AttachmentType).GetFields())
            {
                var attr = field.GetCustomAttribute<EnumMemberAttribute>();
                var rawValue = field.GetValue(value);
                if (rawValue is AttachmentType attachmentType)
                    return attachmentType;
            }

            throw new JsonException($"Unknown intent: {value}");
        }

        public override void Write(Utf8JsonWriter writer, AttachmentType value, JsonSerializerOptions options)
        {
            var field = typeof(AttachmentType).GetField(value.ToString());
            var attr = field?.GetCustomAttribute<EnumMemberAttribute>();

            writer.WriteStringValue(attr?.Value ?? value.ToString());
        }
    }
}

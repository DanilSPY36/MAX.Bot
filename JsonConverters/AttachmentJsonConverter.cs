using MAX.Bot.Models.Attachments;
using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.JsonConverters
{
    public sealed class AttachmentJsonConverter : JsonConverter<AttachmentBase>
    {
        public override AttachmentBase? Read(
       ref Utf8JsonReader reader,
       Type typeToConvert,
       JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("type", out var typeProp))
                throw new JsonException("Attachment missing type");

            var type = Enum.Parse<AttachmentType>(
                typeProp.GetString()!,
                ignoreCase: true);

            Type targetType = type switch
            {
                AttachmentType.image => typeof(AttachmentImage),
                AttachmentType.file => typeof(AttachmentFile),
                AttachmentType.inline_keyboard => typeof(AttachmentInlineKeyboard),
                AttachmentType.contact => typeof(AttachmentContact),
                AttachmentType.location => typeof(AttachmentLocation),
                AttachmentType.share => typeof(AttachmentShare),

                /*AttachmentType.audio => typeof(AttachmentAudio),
                AttachmentType.video => typeof(VideoAttachment),
                AttachmentType.sticker => typeof(StickerAttachment),*/
                _ => throw new NotSupportedException($"Attachment type {type} not supported")
            };

            return (AttachmentBase)JsonSerializer.Deserialize(
                root.GetRawText(),
                targetType,
                options
            )!;
        }

        public override void Write(Utf8JsonWriter writer, AttachmentBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}

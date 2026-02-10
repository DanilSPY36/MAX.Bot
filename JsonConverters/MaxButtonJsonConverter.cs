using MAX.Bot.Models.Buttons;
using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.JsonConverters
{
    public sealed class MaxButtonJsonConverter : JsonConverter<MaxButtonBase>
    {
        public override MaxButtonBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("type", out var typeProp))
                throw new JsonException("missing button type");

            var ButtonType = Enum.Parse<MaxButtonType>(typeProp.GetString()!, ignoreCase: true);

            Type? result = ButtonType switch
            {
                MaxButtonType.callback => typeof(MaxCallbackButton),
                MaxButtonType.link => typeof(MaxLinkButton),
                MaxButtonType.request_geo_location => typeof(MaxRequestGeoLocationButton),
                MaxButtonType.request_contact => typeof(MaxRequestContactButton),
                MaxButtonType.open_app => typeof(MaxOpenAppButton),
                MaxButtonType.message => typeof(MaxMessageButton),
                _ => throw new NotSupportedException($"Button type {ButtonType} is not supported")
            };
               
            var obj = JsonSerializer.Deserialize(root.GetRawText(), result, options);

            return obj as MaxButtonBase ?? throw new JsonException($"Failed to deserialize update of type '{result}'.");
        }

        public override void Write(Utf8JsonWriter writer, MaxButtonBase value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            writer.WriteString("type", value.Type.ToString());
            writer.WriteString("text", value.Text);

            switch (value)
            {
                case MaxLinkButton link:
                    writer.WriteString("url", link.Url);
                    break;

                case MaxCallbackButton callback:
                    writer.WriteString("payload", callback.Payload);
                    if (callback.Intent != Intent.Default)
                    {
                        writer.WritePropertyName("intent");
                        JsonSerializer.Serialize(writer, callback.Intent, options);
                    }
                    break;

                case MaxRequestGeoLocationButton geo:
                    writer.WriteBoolean("quick", geo.Quick);
                    break;

                case MaxOpenAppButton app:
                    writer.WriteString("app_id", app.WebApp);
                    break;
                default:
                    if (value.GetType() != typeof(MaxMessageButton) &&
                        value.GetType() != typeof(MaxRequestContactButton))
                    {
                        throw new JsonException(
                            $"Unhandled button type: {value.GetType().Name}");
                    }
                    break;

            }
            writer.WriteEndObject();
        }
    }
}

using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Updates;
using MAX.Bot.Models.Updates.BotStartStopUpdates;
using MAX.Bot.Models.Updates.BotUpdates;
using MAX.Bot.Models.Updates.ChatUpdates;
using MAX.Bot.Models.Updates.DialogUpdates;
using MAX.Bot.Models.Updates.MessageUpdates;
using MAX.Bot.Models.Updates.UserUpdated;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.JsonConverters
{
    public sealed class UpdateJsonConverter : JsonConverter<Update>
    {
        public override Update? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            if (!root.TryGetProperty("update_type", out var typeProp))
                throw new JsonException("Missing update_type");

            var updateType = Enum.Parse<UpdateType>(typeProp.GetString()!, ignoreCase: true);

            Update? result = updateType switch
            {
                UpdateType.message_created => JsonSerializer.Deserialize<MessageCreatedUpdate>(root.GetRawText(), options),
                UpdateType.message_callback => JsonSerializer.Deserialize<MessageCallbackUpdate>(root.GetRawText(), options),

                UpdateType.message_edited =>  JsonSerializer.Deserialize<MessageEditedUpdate>(root.GetRawText(), options),
                UpdateType.message_removed => JsonSerializer.Deserialize<MessageRemovedUpdate>(root.GetRawText(), options),

                UpdateType.bot_added => JsonSerializer.Deserialize<BotAddedUpdate>(root.GetRawText(), options),
                UpdateType.bot_removed => JsonSerializer.Deserialize<BotRemovedUpdate>(root.GetRawText(), options),

                UpdateType.dialog_muted => JsonSerializer.Deserialize<DialogMutedUpdate>(root.GetRawText(), options),
                UpdateType.dialog_unmuted => JsonSerializer.Deserialize<DialogUnmutedUpdate>(root.GetRawText(), options),
                UpdateType.dialog_cleared => JsonSerializer.Deserialize<DialogClearedUpdate>(root.GetRawText(), options),
                UpdateType.dialog_removed => JsonSerializer.Deserialize<DialogRemovedUpdate>(root.GetRawText(), options),

                UpdateType.user_added => JsonSerializer.Deserialize<UserAddedUpdate>(root.GetRawText(), options),
                UpdateType.user_removed => JsonSerializer.Deserialize<UserRemovedUpdate>(root.GetRawText(), options),

                UpdateType.bot_started => JsonSerializer.Deserialize<BotStartedUpdate>(root.GetRawText(), options),
                UpdateType.bot_stopped => JsonSerializer.Deserialize<BotStoppedUpdate>(root.GetRawText(), options),


                UpdateType.chat_title_changed => JsonSerializer.Deserialize<ChatTitleChangedUpdate>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"UpdateType {updateType} is not supported")
            };
            if (result is null)
                throw new JsonException($"Failed to deserialize update of type '{updateType}'.");

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Update value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}

using MAX.Bot.Models.Enums;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.MessageUpdates
{
    public sealed class MessageRemovedUpdate : Update
    {
        public override UpdateType UpdateType => UpdateType.message_removed;

        /// <summary>
        /// ID удаленного сообщения
        /// </summary>
        [JsonPropertyName("message_id")]
        public string? MessageId { get; init; }

        /// <summary>
        /// ID чата, где сообщение было удалено
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; init; }

        [JsonPropertyName("user_id")]
        public Int64 UserId { get; init; }

    }
}

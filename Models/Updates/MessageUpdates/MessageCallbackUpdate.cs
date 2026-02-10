using MAX.Bot.Models.Enums;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.MessageUpdates
{
    public sealed class MessageCallbackUpdate : MessageUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.message_callback;

        [JsonPropertyName("callback")]
        public required Callback Callback { get; init; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; init; }
    }
}

using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.MessageUpdates
{
    public sealed class MessageCreatedUpdate : Update
    {
        public override UpdateType UpdateType => UpdateType.message_created;

        [JsonPropertyName("message")]
        public required Message Message { get; init; }
        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47. Доступно только в диалогах
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; init; }
    }
}

using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.ChatUpdates
{
    public sealed class ChatTitleChangedUpdate : Update
    {
        public override UpdateType UpdateType => UpdateType.chat_title_changed;
        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; set; }

        /// <summary>
        /// Пользователь, который изменил название
        /// </summary>
        [JsonPropertyName("user")]
        public required User User { get; set; }

        /// <summary>
        /// Новое название чата
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }
}

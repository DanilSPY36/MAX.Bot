using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.BotUpdates
{
    public abstract class BotUpdateBase : Update
    {
        /// <summary>
        /// ID чата, откуда был удалён бот
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; set; }

        /// <summary>
        /// Пользователь, удаливший бота из чата
        /// </summary>
        [JsonPropertyName("user")]
        public required User User { get; set; }

        /// <summary>
        /// Указывает, был ли бот удалён из канала или нет
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; set; }
    }
}

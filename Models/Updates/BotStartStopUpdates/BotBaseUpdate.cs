using MAX.Bot.Models.Updates.BotUpdates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.BotStartStopUpdates
{
    public abstract class BotBaseUpdate : Update
    {
        /// <summary>
        /// ID диалога, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; set; }

        /// <summary>
        /// Пользователь, который нажал кнопку 'Start'/ 'Stop'
        /// </summary>
        [JsonPropertyName("user")]
        public required User User { get; set; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; set; }
    }
}

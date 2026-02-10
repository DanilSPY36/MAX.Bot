using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.DialogUpdates
{
    public abstract class DialogUpdateBase : Update
    {
        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; set; }

        /// <summary>
        /// пользователь который инициировал событие
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

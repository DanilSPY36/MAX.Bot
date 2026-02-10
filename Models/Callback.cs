using MAX.Bot.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class Callback
    {
        /// <summary>
        /// Unix-время, когда пользователь нажал кнопку
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixTimeMillisecondsDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get;  init; }

        /// <summary>
        /// Текущий ID клавиатуры
        /// </summary>
        [JsonPropertyName("callback_id")]
        public string? CallbackId { get; init; }

        /// <summary>
        /// Токен кнопки
        /// </summary>
        [JsonPropertyName("payload")]
        public string? payload { get; init; }

        [JsonPropertyName("user")]
        public required User User { get; init; }
    }
}

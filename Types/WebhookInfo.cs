using MAX.Bot.JsonConverters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    public sealed partial class WebhookInfo
    {
        /// <summary>
        /// URL вебхука
        /// </summary>
        [JsonPropertyName("url")]
        public required string Url { get; set; }

        /// <summary>
        /// Unix-время, когда была создана подписка
        /// </summary>
        [JsonPropertyName("time")]
        public Int64 Time { get; set; }

        /// <summary>
        /// Типы обновлений, на которые подписан бот
        /// </summary>
        [JsonPropertyName("update_types")]
        public IEnumerable<string>? UpdateTypes { get; set; }

        /// <summary>
        /// дата создания вебхука в формате DateTimeOffset
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeMilliseconds(Time);
    }
}

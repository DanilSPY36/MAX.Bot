using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class Recipient
    {
        [JsonPropertyName("chat_id")]
        public long ChatId { get;  init; }

        [JsonPropertyName("chat_type")]
        public ChatType? ChatType { get; init; }

        [JsonPropertyName("user_id")]
        public long UserId { get; init; }
    }
}

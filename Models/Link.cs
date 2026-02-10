using MAX.Bot.Models.Enums;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public partial class Link
    {
        [JsonPropertyName("type")]
        public MessageLinkType Type { get; init; }
        [JsonPropertyName("sender")]
        public User? Sender { get; init; }

        [JsonProperty("chat_id")]
        public long ChatId { get; init; }
        [JsonProperty("message")]
        public Body? Message { get; init; }
    }
}

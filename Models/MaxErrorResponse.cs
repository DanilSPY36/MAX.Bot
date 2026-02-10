using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class MaxErrorResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

    }
}

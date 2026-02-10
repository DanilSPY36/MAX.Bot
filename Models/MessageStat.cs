using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class MessageStat
    {
        [JsonPropertyName("views")]
        public int Views { get;  init; }
    }
}

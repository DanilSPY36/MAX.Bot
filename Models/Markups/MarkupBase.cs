using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Markups
{
    public abstract class MarkupBase
    {
        [JsonPropertyName("type")]
        public abstract MarkupType Type { get; }
    }
}

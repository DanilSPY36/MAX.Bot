using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates
{
    [JsonConverter(typeof(UpdateJsonConverter))]
    public abstract class Update
    {
        [JsonPropertyName("update_type")]
        [JsonConverter(typeof(UpdateTypeJsonConverter))]
        public abstract UpdateType UpdateType { get; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixTimeMillisecondsDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; set; }

    }
}

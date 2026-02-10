using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Attachments
{
    [JsonConverter(typeof(AttachmentJsonConverter))]
    public abstract class AttachmentBase
    {
        [JsonPropertyName("type")]
        public abstract AttachmentType AttachmentType { get; }
    }
    public abstract class AttachmentWithPayload<TPayload> : AttachmentBase where TPayload : PayloadBase
    {
        [JsonPropertyName("payload")]
        public abstract TPayload Payload { get; set; }
    }
}

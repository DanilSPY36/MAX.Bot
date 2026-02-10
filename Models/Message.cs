using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public partial class Message
    {
        /*internal Message() { }
        [JsonConstructor]
        internal Message(User? sender  = null, Recipient? recipient = null, Link? link = null, Body? body = null, MessageStat? stat = null, string? url = null,Int64? timestamp = null)
        {
            Sender = sender;
            Recipient = recipient;
            if(timestamp.HasValue)
                Timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp.Value);
            Link = link;
            Body = body;
            StatMessage = stat;
            Url = url;
        }*/
        [JsonIgnore]
        public Int64 ChatId
        {
            get
            {
                if (Recipient == null)
                    throw new InvalidOperationException("Recipient is null");

                return Recipient.ChatType switch
                {
                    ChatType.dialog => Sender?.UserId
                        ?? throw new InvalidOperationException("Sender is null for dialog"),

                    ChatType.chat => Recipient?.ChatId
                        ?? throw new InvalidOperationException("ChatId is null for chat"),

                    _ => throw new NotSupportedException($"Unknown chat type: {Recipient.ChatType}")
                };
            }
        }
        [JsonPropertyName("sender")]
        public User? Sender { get;  init; }
        [JsonPropertyName("recipient")]
        public Recipient? Recipient { get;  init; }
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixTimeMillisecondsDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }
        [JsonPropertyName("link")]
        public Link? Link { get;  init; }
        [JsonPropertyName("body")]
        public Body? Body { get;  init; }
        [JsonPropertyName("stat")]
        public MessageStat? StatMessage { get;  init; }
        [JsonPropertyName("url")]
        public string? Url { get;  init; }
    }
}

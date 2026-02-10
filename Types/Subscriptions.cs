using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    public sealed class Subscriptions
    {
        [JsonPropertyName("subscriptions")]
        public IEnumerable<WebhookInfo>? SubscriptionList { get; set; } 
    }
}

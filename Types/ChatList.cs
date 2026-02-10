using MAX.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    internal sealed class ChatList
    {
        [JsonPropertyName("chats")]
        public List<Chat>? Chats { get; init; }
    }
}

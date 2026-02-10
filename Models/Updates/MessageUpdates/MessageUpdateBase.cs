using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.MessageUpdates
{
    public abstract class MessageUpdateBase : Update
    {
        [JsonPropertyName("message")]
        public Message? Message { get; init; }

    }
}

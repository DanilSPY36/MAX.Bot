using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageLinkType
    {
        forward,
        reply
    }
}

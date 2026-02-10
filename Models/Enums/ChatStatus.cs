using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChatStatus
    {
        active,
        removed,
        left,
        closed
    }
}

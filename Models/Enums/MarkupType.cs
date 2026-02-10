using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MarkupType
    {
        strong,
        emphasized,
        monospaced,
        link,
        strikethrough,
        underline,
        user_mention,
    }
}

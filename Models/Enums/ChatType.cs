using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChatType
    {
        chat,
        dialog, 
        channel
    }
}

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Intent
    {
        [EnumMember(Value = "default")]
        Default,
        [EnumMember(Value = "negative")]
        Negative,
        [EnumMember(Value = "positive")]
        Positive,
    }
}

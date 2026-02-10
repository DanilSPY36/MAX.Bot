using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    internal sealed class UploadedInfo : PayloadBase
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}

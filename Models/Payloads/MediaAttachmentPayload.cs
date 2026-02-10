using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    internal sealed class MediaAttachmentPayload : PayloadBase
    {
        [JsonPropertyName("url")]
        public string url { get; set; } = string.Empty;

        [JsonPropertyName("token")]
        public string token { get; set; } = string.Empty;
    }
}

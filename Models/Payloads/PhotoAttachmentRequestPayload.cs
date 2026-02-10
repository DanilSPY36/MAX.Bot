using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    internal sealed class PhotoAttachmentRequestPayload : PayloadBase
    {
        [JsonPropertyName("photo_id")]
        public Int64 PhotoId { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}

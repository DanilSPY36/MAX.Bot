using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    public sealed class ShareAttachmentPayload : PayloadBase
    {
        /// <summary>
        /// от 1 символа URL, прикрепленный к сообщению в качестве предпросмотра медиа
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; } = string.Empty;
        /// <summary>
        /// Токен вложения
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; } = string.Empty;
    }
}

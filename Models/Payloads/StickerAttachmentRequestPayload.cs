using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    internal sealed class StickerAttachmentRequestPayload : PayloadBase
    {
        /// <summary>
        /// URL медиа-вложения. Этот URL будет получен в объекте Update после отправки сообщения в чат. 
        /// Прямую ссылку на видео также можно получить с помощью метода GET /videos/{-videoToken-}
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// ID стикера
        /// /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
    }
}

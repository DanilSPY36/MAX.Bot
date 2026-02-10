using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Attachments;
using MAX.Bot.Models.Markups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public partial class Body
    {
        /// <summary>
        /// Уникальный ID сообщения
        /// </summary>
        [JsonPropertyName("mid")]
        public string? Mid { get; init; }

        /// <summary>
        /// ID последовательности сообщения в чате
        /// </summary>
        [JsonPropertyName("seq")]
        public long? Seq { get; init; }

        /// <summary>
        /// Новый текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; init; }

        /// <summary>
        /// Вложения сообщения. Могут быть одним из типов Attachment.
        /// </summary>
        [JsonPropertyName("attachments")]
        public List<AttachmentBase>? Attachments { get; init; }

        /// <summary>
        /// Разметка текста сообщения.
        /// </summary>
        [JsonPropertyName("markup")]
        public List<Markup>? Markup { get; init; }
    }
}

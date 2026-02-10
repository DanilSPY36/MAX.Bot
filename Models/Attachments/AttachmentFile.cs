using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Attachments
{
    internal sealed class AttachmentFile : AttachmentWithPayload<MediaAttachmentPayload>
    {
        public override AttachmentType AttachmentType => AttachmentType.file;
        public override required MediaAttachmentPayload Payload { get; set; }
        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        [JsonPropertyName("filename")]
        public required string Filename { get; set; }
        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        [JsonPropertyName("size")]
        public Int64 Size { get; init; }
    }
}

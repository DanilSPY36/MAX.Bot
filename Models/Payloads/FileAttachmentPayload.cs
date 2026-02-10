using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    internal sealed class FileAttachmentPayload : PayloadBase
    {
        /// <summary>
        /// Имя загруженного файла
        /// </summary>
        [JsonPropertyName("filename")]
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        [JsonPropertyName("size")]
        public Int64 Size { get; set; }
    }
}

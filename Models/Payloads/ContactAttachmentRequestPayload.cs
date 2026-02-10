using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    public sealed class ContactAttachmentRequestPayload : PayloadBase
    {
        /// <summary>
        /// Информация о пользователе в формате VCF.
        /// </summary>
        [JsonPropertyName("vcf_info")]
        public string? Vcf_info { get; set; } = string.Empty;

        /// <summary>
        /// Информация о пользователе
        /// </summary>
        [JsonPropertyName("max_info")]
        public User? User { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class Image
    {
        /// <summary>
        /// URL изображения
        /// </summary>
        [JsonPropertyName("url")]
        public required string Url { get; init; } 
    }
}

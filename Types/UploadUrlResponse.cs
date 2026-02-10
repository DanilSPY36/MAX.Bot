using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    internal sealed class UploadUrlResponse
    {
        [JsonPropertyName("url")]
        public string UploadedUrl { get; init; } = null!;
    }
}

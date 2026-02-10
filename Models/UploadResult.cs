using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    internal sealed class UploadResult
    {
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? Media { get; set; }

        public string? GetFirstToken()
        {
            if (Media == null || Media.Count == 0)
                return null;

            if (Media.TryGetValue("token", out var tokenElement))
            {
                return tokenElement.GetString();
            }
            if(Media.TryGetValue("photos", out var PhotosTokenElement))
            {

                var firstType = Media.Values.FirstOrDefault();
                if (firstType.ValueKind == JsonValueKind.Object)
                { 
                    foreach (var prop in firstType.EnumerateObject())
                    {
                        if (prop.Value.TryGetProperty("token", out var tokenObj))
                            return tokenObj.GetString(); 
                    }
                }
            }

            return null;
        }
        public string? GetMediaType() => Media?.Keys.FirstOrDefault();
    }
}

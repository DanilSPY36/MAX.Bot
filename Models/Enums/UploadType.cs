using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    [JsonConverter(typeof(EnumConverter))]
    public enum UploadType
    {
        image,
        video,
        audio,
        file
    }
}

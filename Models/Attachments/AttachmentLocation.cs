using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Attachments
{
    public sealed class AttachmentLocation : AttachmentBase
    {
        public override AttachmentType AttachmentType => AttachmentType.location;
        /// <summary>
        /// Широта местоположения.
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        /// <summary>
        /// Долгота местоположения.
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}

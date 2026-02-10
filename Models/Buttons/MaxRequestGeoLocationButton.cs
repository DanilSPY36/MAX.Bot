using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxRequestGeoLocationButton: MaxButtonBase
    {
        public MaxRequestGeoLocationButton(string text) : base(text) { }

        public override MaxButtonType Type => MaxButtonType.request_geo_location;
        /// <summary>
        /// Если true, отправляет местоположение без запроса подтверждения пользователя
        /// по умолчанию: false
        /// </summary>
        [JsonPropertyName("quick")]
        public bool Quick { get; init; } = false;
    }
}

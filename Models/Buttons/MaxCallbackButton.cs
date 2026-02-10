using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxCallbackButton : MaxButtonBase
    {
        public MaxCallbackButton(string text) : base(text)
        {
            Text = string.IsNullOrWhiteSpace(text)
            ? throw new ArgumentException("Button text cannot be empty", nameof(text))
            : text;
        }
        public override MaxButtonType Type => MaxButtonType.callback;

        /// <summary>
        /// Токен конпки, который будет возвращён в событии нажатия (до 1024 char)
        /// </summary>
        [JsonPropertyName("payload")]
        public string Payload { get; set; }
        /// <summary>
        /// По умолчанию: "default" 
        /// Enum: "positive" "negative" "default" 
        /// Намерение кнопки.Влияет на отображение клиентом.
        /// </summary>
        [JsonPropertyName("intent")]
        public Intent? Intent { get; init; } = Enums.Intent.Default;
    }
}

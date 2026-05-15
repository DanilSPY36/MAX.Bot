using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAX.Bot.Models.Buttons
{
    public class MaxClipboardButton : MaxButtonBase
    {
        public MaxClipboardButton(string text) : base(text)
        {
            Text = string.IsNullOrWhiteSpace(text)
          ? throw new ArgumentException("Button text cannot be empty", nameof(text))
          : text;
        }

        public override MaxButtonType Type => MaxButtonType.clipboard;
        /// <summary>
        /// Текст, который будет скопирован при нажатии (до 1024 char)
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; } = default!;
    }
}

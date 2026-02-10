using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Buttons
{
    [JsonConverter(typeof(MaxButtonJsonConverter))]
    public abstract class MaxButtonBase
    {
        public MaxButtonBase(string text)
        {
            Text = text;
        }

        [JsonPropertyName("type")]
        public abstract MaxButtonType Type { get; }
        /// <summary>
        /// Текст кнопки (от 1 до 128 char)
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } 
    }
}

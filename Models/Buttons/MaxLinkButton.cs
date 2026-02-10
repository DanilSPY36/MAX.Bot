using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxLinkButton : MaxButtonBase
    {
        public MaxLinkButton(string text, string url) : base(text)
        {
            Text = text;
            Url = url;
        }
        public override MaxButtonType Type => MaxButtonType.link;
        /// <summary>
        /// до 2048 символов
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; init; } = string.Empty;
    }
}

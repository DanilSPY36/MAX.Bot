
using MAX.Bot.Models.Buttons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types.Keyboards
{
    public sealed class MaxInlineKeyboard : MaxKeyboardBase
    {
        public MaxInlineKeyboard(IEnumerable<IEnumerable<MaxButtonBase>> rows)
        {
            Payload = new MaxInlineKeyboardPayload
            {
                Buttons = rows
            };
        }
        [JsonPropertyName("type")]
        public string Type => "inline_keyboard";

        [JsonPropertyName("payload")]
        public MaxInlineKeyboardPayload Payload { get; init; }

    }
}

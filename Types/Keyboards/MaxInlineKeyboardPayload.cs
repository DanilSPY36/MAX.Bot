using MAX.Bot.Models.Buttons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types.Keyboards
{
    public sealed class MaxInlineKeyboardPayload
    {
        [JsonPropertyName("buttons")]
        public required IEnumerable<IEnumerable<MaxButtonBase>> Buttons { get; init; }
    }
}

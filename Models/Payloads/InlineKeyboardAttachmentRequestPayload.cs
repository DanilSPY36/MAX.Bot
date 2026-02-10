using MAX.Bot.Models.Buttons;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Payloads
{
    public sealed class InlineKeyboardAttachmentRequestPayload : PayloadBase
    {
        [JsonPropertyName("buttons")]
        public List<List<MaxButtonBase>> Buttons { get; set; } = new List<List<MaxButtonBase>>();
    }
}

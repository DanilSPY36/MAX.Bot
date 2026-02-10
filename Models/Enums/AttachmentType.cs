using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    public enum AttachmentType
    {
        image,
        video,
        audio,
        file,
        sticker,
        contact,
        inline_keyboard,
        location,
        share
    }
}

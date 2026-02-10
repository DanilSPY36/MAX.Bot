using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    public enum UpdateType
    {
        message_created,
        message_callback,
        message_edited,
        message_removed,
        bot_added,
        bot_removed,
        dialog_muted,
        dialog_unmuted,
        dialog_cleared,
        dialog_removed,
        user_added,
        user_removed,
        bot_started,
        bot_stopped,
        chat_title_changed
    }
}
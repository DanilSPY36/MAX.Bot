using MAX.Bot.Models.Enums;

namespace MAX.Bot.Models.Updates.MessageUpdates
{
    public sealed class MessageEditedUpdate : MessageUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.message_edited;
    }
}

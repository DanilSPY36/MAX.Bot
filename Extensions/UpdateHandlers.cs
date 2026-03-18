using MAX.Bot.Models.Updates;
using MAX.Bot.Models.Updates.BotStartStopUpdates;
using MAX.Bot.Models.Updates.BotUpdates;
using MAX.Bot.Models.Updates.DialogUpdates;
using MAX.Bot.Models.Updates.MessageUpdates;
using MAX.Bot.Models.Updates.UserUpdated;
using MAX.Bot.Types;

namespace MAX.Bot.Extensions
{
    public sealed class UpdateHandlers
    {
        public Func<BotStartedUpdate, Task>? BotStarted { get; set; }
        public Func<BotAddedUpdate, Task>? BotAdded { get; set; }
        public Func<BotRemovedUpdate, Task>? BotRemoved { get; set; }

        public Func<DialogClearedUpdate, Task>? DialogCleared { get; set; }
        public Func<DialogMutedUpdate, Task>? DialogMuted { get; set; }
        public Func<DialogUnmutedUpdate, Task>? DialogUnmuted { get; set; }

        public Func<UserAddedUpdate, Task>? UserAdded { get; set; }
        public Func<UserRemovedUpdate, Task>? UserRemoved { get; set; }

        public Func<MessageRemovedUpdate, Task>? MessageRemoved { get; set; }

        public Func<ContactInfo, Task>? MessageContact { get; set; }
        public Func<MessageCreatedUpdate, Task>? MessageCommand { get; set; }
        public Func<MessageCreatedUpdate, Task>? MessageCreated { get; set; }
        public Func<MessageEditedUpdate, Task>? MessageEdited { get; set; }
        public Func<MessageCallbackUpdate, Task>? Callback { get; set; }

        public Func<Update, Task>? Unknown { get; set; }
    }
}

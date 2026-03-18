using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Updates;
using MAX.Bot.Models.Updates.BotStartStopUpdates;
using MAX.Bot.Models.Updates.BotUpdates;
using MAX.Bot.Models.Updates.DialogUpdates;
using MAX.Bot.Models.Updates.MessageUpdates;
using MAX.Bot.Models.Updates.UserUpdated;
using MAX.Bot.Types;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MAX.Bot.Extensions
{
    public static class UpdateMatchExtensions
    {
        public static Task Match(this Update update, UpdateHandlers h)
        {
            return update switch
            {
                BotStartedUpdate b when h.BotStarted != null => h.BotStarted(b),
                BotAddedUpdate b when h.BotAdded != null => h.BotAdded(b),
                BotRemovedUpdate b when h.BotRemoved != null => h.BotRemoved(b),

                DialogClearedUpdate d when h.DialogCleared != null => h.DialogCleared(d),
                DialogMutedUpdate d when h.DialogMuted != null => h.DialogMuted(d),
                DialogUnmutedUpdate d when h.DialogUnmuted != null => h.DialogUnmuted(d),

                UserAddedUpdate u when h.UserAdded != null => h.UserAdded(u),
                UserRemovedUpdate u when h.UserRemoved != null => h.UserRemoved(u),

                MessageRemovedUpdate m when h.MessageRemoved != null => h.MessageRemoved(m),

                MessageCreatedUpdate m when h.MessageContact != null
                    && m.Message.GetContact() is { } contact
                        => h.MessageContact(contact),

                MessageCreatedUpdate m when h.MessageCommand != null
                    && m.Message.Body?.Text?.StartsWith('/') == true
                        => h.MessageCommand(m),

                MessageCreatedUpdate m when h.MessageCreated != null
                    => h.MessageCreated(m),

                MessageEditedUpdate m when h.MessageEdited != null
                    => h.MessageEdited(m),

                MessageCallbackUpdate c when h.Callback != null
                    => h.Callback(c),

                _ when h.Unknown != null
                    => h.Unknown(update),

                _ => Task.CompletedTask
            };
        }
    }
}

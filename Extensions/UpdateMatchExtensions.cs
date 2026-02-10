using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Updates;
using MAX.Bot.Models.Updates.MessageUpdates;
using MAX.Bot.Types;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MAX.Bot.Extensions
{
    public static class UpdateMatchExtensions
    {
        public static Task Match(this Update update,
            Func<ContactInfo, Task>? messageContact = null,
            Func<MessageCreatedUpdate, Task>? messageCommand = null,
            Func<MessageCreatedUpdate, Task>? messageCreated = null,
            Func<MessageEditedUpdate, Task>? messageEdited = null,
            Func<MessageCallbackUpdate, Task>? callback = null,
            Func<Update, Task>? unknown = null)
        {
            return update switch
            {
                MessageCreatedUpdate m when messageContact != null
                && m.Message.GetContact() is { } contact
                    => messageContact(contact),

                MessageCreatedUpdate m when messageCommand != null && m.Message.Body?.Text?.StartsWith('/') == true
                    => messageCommand(m),

                MessageCreatedUpdate m when messageCreated != null
                    => messageCreated(m),

                MessageEditedUpdate m when messageEdited != null
                    => messageEdited(m),

                MessageCallbackUpdate c when callback != null
                    => callback(c),

                _ when unknown != null
                    => unknown(update),

                _ => Task.CompletedTask
            };
        }
    }
}

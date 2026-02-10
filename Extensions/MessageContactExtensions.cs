using MAX.Bot.Models;
using MAX.Bot.Models.Attachments;
using MAX.Bot.Services;
using MAX.Bot.Types;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace MAX.Bot.Extensions
{
    public static class MessageContactExtensions
    {
        public static ContactInfo? GetContact(this Message message)
        {
            var attachment = message.Body?.Attachments?
            .OfType<AttachmentContact>()
            .FirstOrDefault();

            if (attachment is null)
                return null;

            var payload = attachment.Payload;

            var contact = payload.Vcf_info is not null
                ? VCardParser.Parse(payload.Vcf_info)
                : new ContactInfo();
            contact.User = payload.User;

            return contact;
        }
    }
}

using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Attachments
{
    public sealed class AttachmentInlineKeyboard : AttachmentWithPayload<InlineKeyboardAttachmentRequestPayload>
    {
        public override AttachmentType AttachmentType => AttachmentType.inline_keyboard;
        public override required InlineKeyboardAttachmentRequestPayload Payload { get; set; }
    }
}

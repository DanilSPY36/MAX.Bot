using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Attachments
{
    public sealed class AttachmentShare : AttachmentWithPayload<ShareAttachmentPayload>
    {
        public override AttachmentType AttachmentType => AttachmentType.contact;
        public override ShareAttachmentPayload Payload { get; set; }

    }
}

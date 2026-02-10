using MAX.Bot.JsonConverters;
using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Payloads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Attachments
{

    internal sealed class AttachmentImage : AttachmentWithPayload<PhotoAttachmentRequestPayload>
    {
        public override AttachmentType AttachmentType => AttachmentType.image;
        public override required PhotoAttachmentRequestPayload Payload { get; set; }
    }
}

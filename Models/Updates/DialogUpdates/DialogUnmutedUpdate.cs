using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Updates.DialogUpdates
{
    public sealed class DialogUnmutedUpdate : DialogUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.dialog_unmuted;
    }
}

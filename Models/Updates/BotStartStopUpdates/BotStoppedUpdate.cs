using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Updates.BotStartStopUpdates
{
    public sealed class BotStoppedUpdate : BotBaseUpdate
    {
        public override UpdateType UpdateType => UpdateType.bot_stopped;
    }
}

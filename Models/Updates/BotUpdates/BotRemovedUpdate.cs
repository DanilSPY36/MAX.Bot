using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Updates.BotUpdates
{
    public sealed class BotRemovedUpdate : BotUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.bot_removed;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Enums
{
    public enum MaxErrorCode
    {
        InvalidChatId,
        InvalidRequest,
        ChatNotFound,
        BotBlocked,
        Forbidden,
        RateLimited,
        Unknown
    }
}

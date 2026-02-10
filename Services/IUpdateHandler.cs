using MAX.Bot.Models;
using MAX.Bot.Models.Updates;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Services
{
    public interface IUpdateHandler
    {
        Task HandleUpdateAsync(IMaxBotClient bot, Models.Updates.Update update, CancellationToken ct);
        Task HandleErrorAsync(IMaxBotClient bot, Exception exception, CancellationToken ct);
    }
}

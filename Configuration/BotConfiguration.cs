using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Configuration
{
    public sealed class BotConfiguration
    {
        /// <summary>
        /// Токен бота MAX.
        /// </summary>
        public string BotToken { get; init; } = string.Empty;

        /// <summary>
        /// Базовый URL вебхука бота MAX.
        /// </summary>
        public string WebhookUrl { get; init; } = string.Empty;

        /// <summary>
        /// Таймаут HTTP запросов (в секундах)
        /// </summary>
        public int TimeoutSeconds { get; init; } = 30;
    }
}

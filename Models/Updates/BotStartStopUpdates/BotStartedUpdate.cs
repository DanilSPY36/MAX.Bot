using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.BotStartStopUpdates
{
    public sealed class BotStartedUpdate : BotBaseUpdate
    {
        public override UpdateType UpdateType => UpdateType.bot_started;
        /// <summary>
        /// До 512 char
        /// Дополнительные данные из дип-линков, переданные при запуске бота
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}

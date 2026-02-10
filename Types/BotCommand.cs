using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Types
{
    public sealed class BotCommand
    {
        [JsonConstructor]
        internal BotCommand(string commandName, string? description = null)
        {
            CommandName = commandName;
            Description = description;
        }

        /// <summary>
        /// от 1 до 64 символов 
        /// Название команды
        /// </summary>
        [JsonPropertyName("name")]
        public required string CommandName { get; init; }

        /// <summary>
        /// от 1 до 128 символов
        /// Описание команды
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }
    }
}

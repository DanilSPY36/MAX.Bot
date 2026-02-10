using MAX.Bot.JsonConverters;
using MAX.Bot.Types;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class MaxBot
    {
        internal MaxBot() { }

        [JsonConstructor]
        internal MaxBot(
            long botId,
            string? firstName,
            string? username,
            bool isBot,
            DateTimeOffset lastActivityTime,
            string? description = null,
            string? avatarUrl = null,
            string? fullAvatarUrl = null,
            string? name = null)
        {
            BotId = botId;
            FirstName = firstName;
            Username = username;
            IsBot = isBot;
            LastActivityTime = lastActivityTime;
            Description = description;
            AvatarUrl = avatarUrl;
            FullAvatarUrl = fullAvatarUrl;
        }

        [JsonPropertyName("user_id")]
        public long BotId { get;  init; }

        /// <summary>
        /// Отображаемое имя бота
        /// </summary>
        
        [JsonPropertyName("first_name")]
        public string? FirstName { get;  init; }

        /// <summary>
        /// Отображаемая фамилия пользователя. Для ботов это поле не возвращается
        /// </summary>
        
        [JsonPropertyName("last_name")]
        public string? LastName { get;  init; }

        /// <summary>
        /// Устаревшее поле, скоро будет удалено
        /// </summary>
        
        [JsonPropertyName("name")]
        public string? Name { get;  init; }

        /// <summary>
        /// Никнейм бота или уникальное публичное имя пользователя. 
        /// В случае с пользователем может быть null, если тот недоступен или имя не задано
        /// </summary>
        
        [JsonPropertyName("username")]
        public string? Username { get;  init; }

        /// <summary>
        /// true, если это бот
        /// </summary>
        
        [JsonPropertyName("is_bot")]
        public bool IsBot { get;  init; }

        /// <summary>
        /// Время последней активности пользователя или бота в MAX (Unix-время в миллисекундах).
        /// Если пользователь отключил в настройках профиля мессенджера MAX возможность видеть,
        /// что он в сети онлайн, поле может не возвращаться
        /// </summary>
        
        [JsonPropertyName("last_activity_time")]
        [JsonConverter(typeof(UnixTimeMillisecondsDateTimeOffsetConverter))]
        public DateTimeOffset LastActivityTime { get;  init; }

        /// <summary>
        /// до 16000 символов
        /// Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
        /// </summary>
        
        [JsonPropertyName("description")]
        public string? Description { get;  init; }

        /// <summary>
        /// URL аватара пользователя или бота в уменьшенном размере
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get;  init; }

        /// <summary>
        /// URL аватара пользователя или бота в полном размере
        /// </summary>
        

        [JsonPropertyName("full_avatar_url")]
        public string? FullAvatarUrl { get;  init; }

        [JsonIgnore]
        [JsonPropertyName("commands")]
        public IEnumerable<BotCommand>? Commands { get;  init; }
    }
}

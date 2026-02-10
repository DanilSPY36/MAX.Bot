using System.Text.Json.Serialization;
using MAX.Bot.JsonConverters;

namespace MAX.Bot.Models 
{ 
    public sealed class User
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; init; }


        /// <summary>
        /// Отображаемое имя пользователя или бота
        /// </summary>
        [JsonPropertyName("first_name")]
        public string? FirstName { get; init; }

        /// <summary>
        /// Отображаемая фамилия пользователя. Для ботов это поле не возвращается
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; init; }

        /// <summary>
        /// Устаревшее поле, скоро будет удалено
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// Никнейм бота или уникальное публичное имя пользователя. 
        /// В случае с пользователем может быть null, если тот недоступен или имя не задано
        /// </summary>

        [JsonPropertyName("username")]
        public string? Username { get; init; }

        /// <summary>
        /// true, если это бот
        /// </summary>

        [JsonPropertyName("is_bot")]
        public bool IsBot { get; init; }

        /// <summary>
        /// Время последней активности пользователя или бота в MAX (Unix-время в миллисекундах).
        /// Если пользователь отключил в настройках профиля мессенджера MAX возможность видеть,
        /// что он в сети онлайн, поле может не возвращаться
        /// </summary>
        [JsonPropertyName("last_activity_time")]
        [JsonConverter(typeof(UnixTimeMillisecondsDateTimeOffsetConverter))]
        public DateTimeOffset LastActivityTime { get; init; }
        /// <summary>
        /// до 16000 символов
        /// Описание пользователя или бота. В случае с пользователем может принимать значение null, если описание не заполнено
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// URL аватара пользователя или бота в уменьшенном размере
        /// </summary>
        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; init; }
        /// <summary>
        /// URL аватара пользователя или бота в полном размере
        /// </summary>
        [JsonPropertyName("full_avatar_url")]
        public string? FullAvatarUrl { get; init; }

    }
}

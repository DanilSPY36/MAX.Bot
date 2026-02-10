using MAX.Bot.Models.Enums;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models
{
    public sealed class Chat
    {
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; init; }

        /// <summary>
        /// Enum: "chat"
        /// Тип чата:
        /// "chat" — Групповой чат.
        /// </summary>
        [JsonPropertyName("type")]
        public ChatType Type { get; init; }

        /// <summary>
        /// Enum: "active" "removed" "left" "closed"
        /// Статус чата:
        // "active" — Бот является активным участником чата.
        // "removed" — Бот был удалён из чата.
        // "left" — Бот покинул чат.
        // "closed" — Чат был закрыт.
        /// </summary>
        [JsonPropertyName("status")]
        public ChatStatus Status { get; init; }

        /// <summary>
        /// Отображаемое название чата. Может быть null для диалогов
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// Иконка чата
        /// </summary>
        [JsonPropertyName("icon")]
        public Image? Icon { get; init; }

        /// <summary>
        /// Времф последнего события в чате в формате Unix Time
        /// </summary>
        [JsonPropertyName("last_event_time")]
        public Int64 LastEventTime { get;  init; }

        /// <summary>
        /// Количество участников чата
        /// </summary>
        [JsonPropertyName("participants_count")]
        public int ParticipantsCount { get; init; }

        /// <summary>
        /// ID владельца чата
        /// </summary>
        [JsonPropertyName("owner_id")]
        public Int64 OwnerId { get;   init; }

        /// <summary>
        /// Участники чата с временем последней активности. Может быть null, если запрашивается список чатов
        /// </summary>
        [JsonPropertyName("participants")]
        public Object? Participants { get;  init; }

        /// <summary>
        /// Доступен ли чат публично (для диалогов всегда false)
        /// </summary>
        [JsonPropertyName("is_public")]
        public bool IsPublic { get;  init; }

        /// <summary>
        /// Ссылка на чат
        /// </summary>
        [JsonPropertyName("link")]
        public string? Link { get;  init; }

        /// <summary>
        /// Описание чата
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get;  init; }

        /// <summary>
        /// Данные о пользователе в диалоге (только для чатов типа "dialog")
        /// </summary>

        [JsonPropertyName("dialog_with_user")]
        public User? DialogWithUser { get;  init; }

        /// <summary>
        /// ID сообщения, содержащего кнопку, через которую был инициирован чат
        /// </summary>
        [JsonPropertyName("chat_message_id")]
        public string? ChatMessageId { get;  init; }

        /// <summary>
        /// Закреплённое сообщение в чате (возвращается только при запросе конкретного чата)
        /// </summary>
        /// 
        //[JsonPropertyName("pinned_message")]
        // public Message? PinnedMessage { get; protected init; }

        [JsonIgnore]
        public DateTimeOffset LastEventTimeDateTimeOffset => DateTimeOffset.FromUnixTimeMilliseconds(LastEventTime);

    }
}

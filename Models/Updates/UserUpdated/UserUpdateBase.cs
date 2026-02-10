using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.UserUpdated
{
    public abstract class UserUpdateBase : Update
    {
        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public Int64 ChatId { get; set; }

        /// <summary>
        /// Пользователь, к которому относится обновление
        /// </summary>
        [JsonPropertyName("user")]
        public required User User { get; set; }

        /// <summary>
        /// Указывает, был ли пользователь удалён/добавлен из канала/чата или нет 
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; set; }
    }
}

using MAX.Bot.Models.Enums;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.UserUpdated
{
    public sealed class UserAddedUpdate : UserUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.user_added;

        /// <summary>
        /// Пользователь, который добавил пользователя в чат. 
        /// Может быть null, если пользователь присоединился к чату по ссылке
        /// </summary>
        [JsonPropertyName("inviter_id")]
        public Int64? InviterId { get; set; }

    }
}

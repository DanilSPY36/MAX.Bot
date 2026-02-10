using MAX.Bot.Models.Enums;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Updates.UserUpdated
{
    public sealed class UserRemovedUpdate : UserUpdateBase
    {
        public override UpdateType UpdateType => UpdateType.user_removed;

        [JsonPropertyName("admin_id")]
        public Int64? AdminId { get; set; }
    }
}

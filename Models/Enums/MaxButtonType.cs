using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Enums
{
    public enum MaxButtonType
    {
        callback,
        link,
        request_geo_location,
        request_contact,
        open_app,
        message
    }
}

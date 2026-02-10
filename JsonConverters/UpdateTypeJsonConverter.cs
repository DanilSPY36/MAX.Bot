using MAX.Bot.Models.Enums;
using MAX.Bot.Models.Updates;
using MAX.Bot.Models.Updates.MessageUpdates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAX.Bot.JsonConverters
{
    public sealed class UpdateTypeJsonConverter : JsonConverter<UpdateType>
    {
        public override UpdateType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            return Enum.Parse<UpdateType>(s!, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, UpdateType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLowerInvariant());
        }
    }
}

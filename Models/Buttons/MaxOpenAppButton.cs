using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxOpenAppButton : MaxButtonBase
    {
        public MaxOpenAppButton(string text) : base(text) { }
        public override MaxButtonType Type => MaxButtonType.open_app;
        /// <summary>
        /// Публичное имя (username) бота или ссылка на него, чьё мини-приложение надо запустить
        /// </summary>
        [JsonPropertyName("web_bpp")]
        public string WebApp { get; init; } = string.Empty;
        /// <summary>
        /// Идентификатор бота, чьё мини-приложение надо запустить
        /// </summary>
        [JsonPropertyName("contact_id")]
        public string ContactId { get; init; } = string.Empty;

        /// <summary>
        /// Параметр запуска, который будет передан в initData мини-приложения
        /// </summary>
        /// <see href="https://dev.max.ru/docs/webapps/bridge#WebAppData">initData</see>
        [JsonPropertyName("payload")]
        public string? Payload { get; init; }
    }
}

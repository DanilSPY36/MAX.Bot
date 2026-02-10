using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MAX.Bot.Models.Markups
{
    public abstract class Markup : MarkupBase
    {
        /// <summary>
        /// Индекс начала элемента разметки в тексте. Нумерация с нуля
        /// </summary>
        [JsonPropertyName("from")]
        public Int32 From { get; set; }

        /// <summary>
        /// Длина элемента разметки
        /// </summary>
        [JsonPropertyName("length")]
        public Int32 Length { get; set; }
    }
}

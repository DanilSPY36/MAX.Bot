using MAX.Bot.Models.Enums;
using MAX.Bot.Types.Keyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAX.Bot.Extensions
{
    public class EditMessageOptions
    {
        public string? Text { get; set; }

        public ParseMode? ParseMode { get; set; }

        /// <summary>
        /// Управление кнопками:
        /// null → не менять
        /// Empty → удалить
        /// Value → заменить
        /// </summary>
        public MaxInlineKeyboard? ReplyMarkup { get; set; }

        /// <summary>
        /// Управление вложениями:
        /// null → не менять
        /// Empty → удалить все
        /// Value → полностью заменить
        /// </summary>
        public IEnumerable<object>? Attachments { get; set; }
    }
}

using MAX.Bot.Models.Buttons;
using MAX.Bot.Types.Keyboards;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Extensions
{
    public static class MaxKeyboardExtension
    {
        public static MaxInlineKeyboard ToKeyboard(this MaxButtonBase button) =>
            new MaxInlineKeyboard(new[] { new[] { button } });

        public static MaxInlineKeyboard ToKeyboard(this IEnumerable<MaxButtonBase> buttons) =>
            new MaxInlineKeyboard(new[] { buttons });

        public static MaxInlineKeyboard ToKeyboard(this IEnumerable<IEnumerable<MaxButtonBase>> rows) =>
            new MaxInlineKeyboard(rows);
    }
}

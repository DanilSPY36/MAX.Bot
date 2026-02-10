using MAX.Bot.Models.Updates;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Extensions
{
    public static class UpdateExtensions
    {
        public static bool Is<T>(this Update update)
            where T : Update
            => update is T;
    }
}

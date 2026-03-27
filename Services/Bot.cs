using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAX.Bot.Services
{
    public static class Bot
    {
        public static IMaxBotClient Create(string token)
            => new MaxBotClient(token);
    }
}

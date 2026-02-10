using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxMessageButton : MaxButtonBase
    {
        public MaxMessageButton(string text) : base(text) { }
        public override MaxButtonType Type => MaxButtonType.message;
    }
}

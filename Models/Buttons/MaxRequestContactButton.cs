using MAX.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Models.Buttons
{
    public sealed class MaxRequestContactButton : MaxButtonBase
    {
        public MaxRequestContactButton(string text) : base(text) { }

        public override MaxButtonType Type => MaxButtonType.request_contact;

    }
}

using MAX.Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Types
{
    public sealed class ContactInfo
    {
        public User? User { get; set; }

        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

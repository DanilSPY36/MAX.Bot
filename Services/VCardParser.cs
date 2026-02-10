using MAX.Bot.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAX.Bot.Services
{
    internal static class VCardParser
    {
        public static ContactInfo Parse(string vcf)
        {
            var lines = vcf.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string? phone = string.Empty;
            string? firstName = string.Empty;
            string? lastName = string.Empty;

            foreach (var line in lines)
            {
                if(line.StartsWith("TEL"))
                    phone = line.Split(':').Last();
                if (line.StartsWith("FN:"))
                {
                    var parts = line.Substring(2).Trim().Split(' ');
                    lastName = parts.ElementAtOrDefault(0);
                    firstName = parts.ElementAtOrDefault(1);
                }
            }
            return new ContactInfo { FirstName = firstName, LastName = lastName, PhoneNumber = phone};
        }
    }
}

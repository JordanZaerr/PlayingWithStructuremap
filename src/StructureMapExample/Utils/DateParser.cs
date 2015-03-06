using System;
using System.Globalization;
using System.Linq;

namespace StructureMapExample.Utils
{
    public class DateParser : IDateParser
    {
        private readonly CultureInfo _culture = new CultureInfo("en-US");
        private readonly string[] _formatStrings = new[] 
        {
            "MMM,dd,yyyy"
        };
        public DateParser()
        {
            _culture.DateTimeFormat.AbbreviatedMonthNames = new[]
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec", ""
            };
        }

        public DateTime GetDateTime(string value)
        {
            DateTime retVal;
            if (!DateTime.TryParse(value, out retVal))
            {
                if (DateTime.TryParse(value, _culture, DateTimeStyles.None, out retVal))
                {
                    return retVal;
                }
                if (_formatStrings.Any(format => DateTime.TryParseExact(value, format, _culture, DateTimeStyles.None, out retVal)))
                {
                    return retVal;
                }
            }
            return retVal;
        }
    }
}
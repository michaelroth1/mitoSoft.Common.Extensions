using System;

namespace mitoSoft.Common.Extensions
{
    internal static class DateTimeExtensions
    {
        public static string ToFormatted(this DateTime date, string text)
        {
            return date.ToFormatted(text, "yyyy-MM-dd HH:mm:ss fff");
        }

        public static string ToFormatted(this DateTime date, string text, string defaultFormat)
        {
            return date.ToFormatted(text, defaultFormat, null);
        }

        public static string ToFormatted(this DateTime date, string text, string defaultFormat, IFormatProvider provider)
        {
            text = text.Replace("{date}", date.ToString(defaultFormat, provider));  //default format 

            text = ToCustom(date, text, provider);  //individual format

            return text;
        }

        private static string ToCustom(DateTime date, string text, IFormatProvider provider)
        {
            foreach (string match in text.FindBetween("{date:", "}"))
            {
                var format = match.Replace("{date:", "").TrimEnd('}');
                string dateString = date.ToString(format, provider);
                text = text.Replace(match, dateString);
            }

            return text;
        }
    }
}
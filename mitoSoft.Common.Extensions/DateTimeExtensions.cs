using System;

namespace mitoSoft.Common.Extensions
{
    public static class DateTimeExtensions
    {
        internal static string ToFormatted(this DateTime date, string text, string defaultFormat = "yyyy-MM-dd HH:mm:ss fff")
        {
            text = text.Replace("{date}", date.ToString(defaultFormat));  //default format 

            text = ToCustom(date, text);  //individual format

            return text;
        }

        private static string ToCustom(DateTime date, string text)
        {
            foreach (string match in text.FindBetween("{date:", "}"))
            {
                var format = match.Replace("{date:", "").TrimEnd('}');
                string dateString = date.ToString(format);
                text = text.Replace(match, dateString);
            }

            return text;
        }
    }
}
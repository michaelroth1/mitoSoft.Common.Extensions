using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mitoSoft.Common.Extensions
{
    public static class StringExtensions
    {
        public static List<string> FindBetween(this string value, string start, string end)
        {
            var result = new List<string>();
            var pattern = @"\" + start + @"(.*?)\" + end;
            var matches = Regex.Matches(value, pattern);

            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }

        public static List<string> FindBetweenBrackets(this string value)
        {
            return value.FindBetween("{", "}");
        }

        public static string ReplaceBetween(this string text, string start, string end, string find, string replace)
        {
            foreach (string match in text.FindBetween(start, end))
            {
                if (match.ToLower().Replace(" ", "") == (start + find + end).ToLower())
                {
                    text = text.Replace(match, replace);
                }
            }

            return text;
        }

        public static string ReplaceBetweenBrackets(this string text, string find, string replace)
        {
            return text.ReplaceBetween("{", "}", find, replace);
        }

        public static string ReplaceFormattedDate(this string value, DateTime date, string defaultFormat = "yyyy-MM-dd HH:mm:ss fff")
        {
            var s = value.Replace("{", "{#").Replace("}", "#}");
            s = s.ReplaceBetween("{#", "#}", "date", "{date}");
            s = s.ReplaceBetween("{#", ":", "date", "{date:");
            s = s.Replace("#}", "}");
            s = s.Replace("{#", "{");
            s = date.ToFormatted(s, defaultFormat);
            return s;
        }
    }
}
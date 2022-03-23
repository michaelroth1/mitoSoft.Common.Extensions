using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mitoSoft.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsLike(this string source, string like)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }
            else if (string.IsNullOrWhiteSpace(like))
            {
                return true;
            }
            else if (!like.Replace("*", "%").Contains("%"))
            {
                return source.Contains(like);
            }
            else
            {
                like = like.Replace("*", "%");
                return Regex.IsMatch(source, "^" + Regex.Escape(like).Replace("_", ".").Replace("%", ".*") + "$");
            }
        }

        public static List<string> FindBetween(this string value, string start, string end, bool exact = true)
        {
            var result = new List<string>();
            string pattern;
            if (value.IndexOf(end) < 0 && exact == false) //not found
            {
                pattern = @"(?<=" + start + @").*$";
            }
            else if (value.IndexOf(end) < 0 && exact) 
            {
                throw new InvalidOperationException($"End tag '{end}' not found.");
            }
            else if (value.IndexOf(start) < 0 && exact == false) //not found
            {
                pattern = @".+?(?=" + end + @")";
            }
            else if (value.IndexOf(start) < 0 && exact)
            {
                throw new InvalidOperationException($"Start tag '{start}' not found.");
            }
            else
            {
                pattern = @"(?<=" + start + @").*?(?=" + end + @")";
            }

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
                if (match.ToLower().Replace(" ", "") == find.ToLower())
                {
                    text = text.Replace(start + match + end, replace);
                }
            }

            return text;
        }

        public static string ReplaceBetweenBrackets(this string text, string find, string replace)
        {
            return text.ReplaceBetween("{", "}", find, replace);
        }

        public static string ReplaceFormattedDate(this string value, DateTime date)
        {
            return value.ReplaceFormattedDate(date, "yyyy-MM-dd HH:mm:ss fff");
        }

        public static string ReplaceFormattedDate(this string value, DateTime date, string defaultFormat)
        {
            return value.ReplaceFormattedDate(date, defaultFormat, null);
        }

        public static string ReplaceFormattedDate(this string value, DateTime date, string defaultFormat, IFormatProvider provider)
        {
            var s = value.Replace("{", "{#").Replace("}", "#}");
            s = s.ReplaceBetween("{#", "#}", "date", "{date}");
            s = s.ReplaceBetween("{#", ":", "date", "{date:");
            s = s.Replace("#}", "}");
            s = s.Replace("{#", "{");
            s = date.ToFormatted(s, defaultFormat, provider);
            return s;
        }
    }
}
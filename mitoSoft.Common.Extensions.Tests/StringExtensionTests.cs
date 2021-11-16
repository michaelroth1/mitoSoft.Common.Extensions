using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace mitoSoft.Common.Extensions.Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Test1()
        {
            var s = "{Date} {  DATE : yyyyMMdd HH:mm    } {loglevel} {message}";

            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("loglevel", "warn");
            s = s.ReplaceBetweenBrackets("level", "warn");
            s = s.ReplaceBetweenBrackets("categoryname", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("category", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("message", "Some message...");

            Assert.AreEqual("1982-03-07 06:00:00 000  19820307 06:00     warn Some message...", s);
        }

        [TestMethod]
        public void Test2()
        {
            var s = "{Date:yyyy}-{DATE:MM}-{Date:dd} {date:HH:mm} [{loglevel}] {message}";

            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("loglevel", "warn");
            s = s.ReplaceBetweenBrackets("level", "warn");
            s = s.ReplaceBetweenBrackets("categoryname", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("category", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("message", "Some message...");

            Assert.AreEqual("1982-03-07 06:00 [warn] Some message...", s);
        }

        [TestMethod]
        public void Test3()
        {
            var s = "[{Date:yyyy}-{DATE:MM}-{Date:dd} {date:HH:mm}] [{loglevel}] {message}";

            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("loglevel", "warn");
            s = s.ReplaceBetweenBrackets("level", "warn");
            s = s.ReplaceBetweenBrackets("categoryname", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("category", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("message", "Some message...");

            Assert.AreEqual("[1982-03-07 06:00] [warn] Some message...", s);
        }

        [TestMethod]
        public void Test4()
        {
            var s = "{{Date:yyyy}-{DATE:MM}-{Date:dd} {date:HH:mm}} [{loglevel}] {MESSAGE}";

            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("loglevel", "warn");
            s = s.ReplaceBetweenBrackets("level", "warn");
            s = s.ReplaceBetweenBrackets("categoryname", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("category", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("message", "Some message...");

            Assert.AreEqual("{1982-03-07 06:00} [warn] Some message...", s);
        }
    }
}

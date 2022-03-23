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
            var s = "{Date} {  DATE : yyyyMMdd HH:mm    } {  loglevel} {message}";

            s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
            s = s.ReplaceBetweenBrackets("loglevel", "warn");
            s = s.ReplaceBetweenBrackets("level", "warn");
            s = s.ReplaceBetweenBrackets("categoryname", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("category", "Microsoft.Test");
            s = s.ReplaceBetweenBrackets("message", "Some message...");

            Assert.AreEqual("1982-03-07 06:00:00 000 19820307 06:00 warn Some message...", s);
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

        [TestMethod]
        public void Test5()
        {
            var s = "some message form {find} at {date}.";

            s = s.ReplaceBetweenBrackets("find", "mitoSoft");
            s = s.ReplaceBetweenBrackets("date", new DateTime(2021, 12, 24).ToString("MMM dd", System.Globalization.CultureInfo.InvariantCulture));

            Assert.AreEqual("some message form mitoSoft at Dec 24.", s);
        }

        [TestMethod]
        public void Test6()
        {
            var s = "some message form {find} at {DATE :MMM dd}.";

            s = s.ReplaceBetweenBrackets("find", "mitoSoft");
            s = s.ReplaceFormattedDate(new DateTime(2021, 12, 24), "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            Assert.AreEqual("some message form mitoSoft at Dec 24.", s);
        }

        //End Charater is not available
        [TestMethod]
        public void Test7()
        {
            var s = "{room} in living room";

            var b = s.FindBetween("{room}", ",", false)[0];

            Assert.AreEqual(" in living room", b);

            b = (s + ",").FindBetween("{room}", ",", false)[0];

            Assert.AreEqual(" in living room", b);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test8()
        {
            var s = "{room} in living room";

            var b = s.FindBetween("{room}", ",")[0];
        }

        [TestMethod]
        public void Test10()
        {
            var s = "{room} in living room";

            Assert.IsTrue(s.ContainsLike("{room}*room"));
            Assert.IsTrue(s.ContainsLike("{room}%room"));
            Assert.IsFalse(s.ContainsLike("{room}*test"));
        }
    }
}
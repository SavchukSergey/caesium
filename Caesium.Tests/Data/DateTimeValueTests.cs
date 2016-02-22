using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class DateTimeValueTests {

        [TestCase("20160221T125434", 2016, MonthOfYear.Febuary, 21, 12, 54, 34, false)]
        [TestCase("20160221T125434Z", 2016, MonthOfYear.Febuary, 21, 12, 54, 34, true)]
        public void ParseTest(string val, int year, MonthOfYear month, int day, int hours, int minutes, int seconds, bool utc) {
            var dateTime = DateTimeValue.Parse(val);
            Assert.AreEqual(year, dateTime.Year);
            Assert.AreEqual(month, dateTime.Month);
            Assert.AreEqual(day, dateTime.Day);
            Assert.AreEqual(hours, dateTime.Hours);
            Assert.AreEqual(minutes, dateTime.Minutes);
            Assert.AreEqual(seconds, dateTime.Seconds);
            Assert.AreEqual(utc, dateTime.Utc);
        }
    }
}

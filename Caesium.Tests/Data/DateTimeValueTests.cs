using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class DateTimeValueTests {

        [TestCase("20160221T125434", 2016, MonthOfYear.Febuary, 21, 12, 54, 34, false)]
        [TestCase("20160221T125434Z", 2016, MonthOfYear.Febuary, 21, 12, 54, 34, true)]
        public void ParseTest(string val, int year, MonthOfYear month, int day, int hours, int minutes, int seconds, bool utc) {
            var dateTime = DateTimeValue.Parse(val);
            Assert.That(dateTime.Year, Is.EqualTo(year));
            Assert.That(dateTime.Month, Is.EqualTo(month));
            Assert.That(dateTime.Day, Is.EqualTo(day));
            Assert.That(dateTime.Hours, Is.EqualTo(hours));
            Assert.That(dateTime.Minutes, Is.EqualTo(minutes));
            Assert.That(dateTime.Seconds, Is.EqualTo(seconds));
            Assert.That(dateTime.Utc, Is.EqualTo(utc));
        }
    }
}

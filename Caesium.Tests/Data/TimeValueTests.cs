using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class TimeValueTests {

        [TestCase("021545", 2, 15, 45, false)]
        [TestCase("021545Z", 2, 15, 45, true)]
        public void ParseTest(string val, int hours, int minutes, int seconds, bool utc) {
            var offset = TimeValue.Parse(val);
            Assert.That(offset.Hours, Is.EqualTo(hours));
            Assert.That(offset.Minutes, Is.EqualTo(minutes));
            Assert.That(offset.Seconds, Is.EqualTo(seconds));
            Assert.That(offset.Utc, Is.EqualTo(utc));
        }
    }
}

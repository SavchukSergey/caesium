using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class UtcOffsetValueTests {

        [TestCase("+0215", '+', 2, 15, null)]
        [TestCase("-0215", '-', 2, 15, null)]
        [TestCase("+021545", '+', 2, 15, 45)]
        [TestCase("-021545", '-', 2, 15, 45)]
        public void ParseTest(string val, char sign, int hours, int minutes, int? seconds) {
            var offset = UtcOffsetValue.Parse(val);
            if (sign == '+') {
                Assert.That(offset.Negative, Is.False);
            } else {
                Assert.That(offset.Negative, Is.True);
            }
            Assert.That(offset.Hours, Is.EqualTo(hours));
            Assert.That(offset.Minutes, Is.EqualTo(minutes));
            Assert.That(offset.Seconds, Is.EqualTo(seconds));
        }
    }
}

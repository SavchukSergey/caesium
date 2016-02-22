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
                Assert.IsFalse(offset.Negative);
            } else {
                Assert.IsTrue(offset.Negative);
            }
            Assert.AreEqual(hours, offset.Hours);
            Assert.AreEqual(minutes, offset.Minutes);
            Assert.AreEqual(seconds, offset.Seconds);
        }
    }
}

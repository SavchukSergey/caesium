using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class ValueTests {

        [TestCase(@"Project XYZ Final Review\nConference Room - 3B\nCome Prepared.", "Project XYZ Final Review\nConference Room - 3B\nCome Prepared.")]
        public void ParseTextTest(string source, string result) {
            var res = Value.ParseText(source);
            Assert.That(res, Is.EqualTo(result));
        }
    }
}

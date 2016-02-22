using System;
using System.IO;
using NUnit.Framework;

namespace Caesium.Tests {
    [TestFixture]
    public class CalendarObjectTests {

        [Test]
        public void ParseUsHolidaysTest() {
            var content = ReadResource("USHolidays.ics");
            var calendar = (VCalendar)CalendarObject.Parse(content);
        }

        [Test]
        public void ParseYogamTest() {
            var content = ReadResource("yogam.ics");
            var calendar = (VCalendar)CalendarObject.Parse(content);
        }

        private string ReadResource(string name) {
            var type = GetType();
            var assembly = type.Assembly;
            var resourceName = type.Namespace + "." + name;
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName)) {
                if (resourceStream == null) throw new ArgumentException("resource is not found", nameof(name));
                using (var reader = new StreamReader(resourceStream)) {
                    return reader.ReadToEnd();
                }
            }

        }
    }
}

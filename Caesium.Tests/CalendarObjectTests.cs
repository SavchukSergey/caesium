using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

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

        [Test]
        public async Task ParseWPEventsCalendar() {
            // Modern Tribe's Demo Site (creators of popular WordPress calendar plugin)
            var calendar = await CalendarObject.LoadAsync("https://wpshindig.com/?ical=1");
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

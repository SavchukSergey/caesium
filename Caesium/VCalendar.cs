using System;
using System.Threading.Tasks;

namespace Caesium {
    public class VCalendar : CalendarObject {

        public VCalendar() : base("VCALENDAR") {
        }

        /// <summary>
        /// <para>
        /// The vendor of the implementation SHOULD assure that this is a globally unique identifier; 
        /// using some technique such as an FPI value, as defined in [ISO.9070.1991].
        /// </para>
        /// <para>
        /// This property SHOULD NOT be used to alter the interpretation of an iCalendar object beyond the semantics specified in this memo.
        /// For example, it is not to be used to further the understanding of non-standard properties.
        /// </para>
        /// </summary>
        public string ProdId {
            get { return GetText("PRODID"); }
            set { SetText("PRODID", value); }
        }

        public string Version {
            get { return GetText("VERSION"); }
            set { SetText("VERSION", value); }
        }

        public string CalScale {
            get { return GetText("CALSCALE"); }
            set { SetText("CALSCALE", value); }
        }

        public string Method {
            get { return GetText("METHOD"); }
            set { SetText("METHOD", value); }
        }

        public string XWrCalName {
            get { return GetText("X-WR-CALNAME"); }
            set { SetText("X-WR-CALNAME", value); }
        }

        public new static async Task<VCalendar> LoadAsync(string url) {
            return await LoadAsync<VCalendar>(url);
        }
    }
}

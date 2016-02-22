namespace Caesium {
    public class VCalendar : CalendarObject {

        public VCalendar() : base("VCALENDAR") {
        }

        public string ProdId {
            get { return this["PRODID"]; }
            set { this["PRODID"] = value; }
        }

        public string Version {
            get { return this["VERSION"]; }
            set { this["VERSION"] = value; }
        }

        public string CalScale {
            get { return this["CALSCALE"]; }
            set { this["CALSCALE"] = value; }
        }

        public string Method {
            get { return this["METHOD"]; }
            set { this["METHOD"] = value; }
        }
    }
}

namespace Caesium {
    public class VEvent : CalendarObject {

        public VEvent() : base("VEVENT") {
        }

        public string Created {
            get { return this["CREATED"]; }
            set { this["CREATED"] = value; }
        }

        public string LastModified {
            get { return this["LAST-MODIFIED"]; }
            set { this["LAST-MODIFIED"] = value; }
        }

        public string Uid {
            get { return this["UID"]; }
            set { this["UID"] = value; }
        }

        public string Summary {
            get { return this["SUMMARY"]; }
            set { this["SUMMARY"] = value; }
        }

        public string Status {
            get { return this["STATUS"]; }
            set { this["STATUS"] = value; }
        }

        public string RRule {
            get { return this["RRULE"]; }
            set { this["RRULE"] = value; }
        }

        public string Categories {
            get { return this["CATEGORIES"]; }
            set { this["CATEGORIES"] = value; }
        }

        public string DtStart {
            get { return this["DTSTART"]; }
            set { this["DTSTART"] = value; }
        }

        public string DtEnd {
            get { return this["DTEND"]; }
            set { this["DTEND"] = value; }
        }

        public string DtStamp {
            get { return this["DTSTAMP"]; }
            set { this["DTSTAMP"] = value; }
        }

        public string Description {
            get { return this["DESCRIPTION"]; }
            set { this["DESCRIPTION"] = value; }
        }

        public string Transp {
            get { return this["TRANSP"]; }
            set { this["TRANSP"] = value; }
        }

        public string Location {
            get { return this["LOCATION"]; }
            set { this["LOCATION"] = value; }
        }

        public string Sequence {
            get { return this["SEQUENCE"]; }
            set { this["SEQUENCE"] = value; }
        }

    }
}

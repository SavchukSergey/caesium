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

        /// <summary>
        /// <para>
        /// This property is used in the "VEVENT", "VTODO", and "VJOURNAL" calendar components to capture a short, one-line summary about the activity or journal entry.
        /// </para>
        /// <para>
        /// This property is used in the "VALARM" calendar component to capture the subject of an EMAIL category of alarm.
        /// </para>
        /// </summary>
        public string Summary {
            get { return GetText("SUMMARY"); }
            set { SetText("SUMMARY", value); }
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

        /// <summary>
        /// <para>
        /// Within the "VEVENT" calendar component, this property defines the start date and time for the event.
        /// </para>
        /// <para>
        /// Within the "VFREEBUSY" calendar component, this property defines the start date and time for the free or busy time information.
        /// The time MUST be specified in UTC time.
        /// </para>
        /// <para>
        /// Within the "STANDARD" and "DAYLIGHT" sub-components, this property defines the effective start date and time for a time zone specification.
        /// This property is REQUIRED within each "STANDARD" and "DAYLIGHT" sub-components included in "VTIMEZONE" calendar components and MUST be specified as a date with local time without the "TZID" property parameter.
        /// </para>
        /// </summary>
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

        /// <summary>
        ///<para>
        ///  This property is used in the "VEVENT" and "VTODO" to capture lengthy textual descriptions associated with the activity.
        /// </para>
        /// <para>
        /// This property is used in the "VJOURNAL" calendar component to capture one or more textual journal entries.
        /// </para>
        /// <para>
        /// This property is used in the "VALARM" calendar component to capture the display text for a DISPLAY category of alarm, and to capture the body text for an EMAIL category of alarm.
        /// </para>
        /// </summary>
        public string Description {
            get { return GetText("DESCRIPTION"); }
            set { SetText("DESCRIPTION", value); }
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

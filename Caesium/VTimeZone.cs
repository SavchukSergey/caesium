namespace Caesium {
    public class VTimeZone : CalendarObject{

        public VTimeZone() : base("VTIMEZONE") {
        }

        public string TzId {
            get { return this["TZID"]; }
            set { this["TZID"] = value; }
        }

    }
}

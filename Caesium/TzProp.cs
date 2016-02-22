using Caesium.Data;

namespace Caesium {
    public abstract class TzProp : CalendarObject{

        protected TzProp(string type) : base(type) {
        }

        public UtcOffsetValue? TzOffsetFrom {
            get {
                var str = this["TZOFFSETFROM"];
                if (str != null) return UtcOffsetValue.Parse(str);
                return null;
            }
            set { this["TZOFFSETFROM"] = value?.ToString(); }
        }

        public UtcOffsetValue? TzOffsetTo {
            get {
                var str = this["TZOFFSETTO"];
                if (str != null) return UtcOffsetValue.Parse(str);
                return null;
            }
            set { this["TZOFFSETTO"] = value?.ToString(); }
        }

        public string TzName {
            get { return this["TZNAME"]; }
            set { this["TZNAME"] = value; }
        }

        public string DtStart {
            get { return this["DTSTART"]; }
            set { this["DTSTART"] = value; }
        }

        public string RRule {
            get { return this["RRULE"]; }
            set { this["RRULE"] = value; }
        }

    }
}

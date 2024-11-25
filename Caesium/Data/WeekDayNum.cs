using System;

namespace Caesium.Data {
    public struct WeekDayNum {

        public DayOfWeek DayOfWeek { get; set; }

        public int? OrderWeek { get; set; }

        public WeekDayNum(DayOfWeek dayOfWeek, int? orderWeek = null) {
            DayOfWeek = dayOfWeek;
            OrderWeek = orderWeek;
        }

        public static WeekDayNum ReadFrom(CalendarReader reader) {
            int? order = null;
            if (reader.IsSignedInteger) {
                order = reader.ReadSignedInteger();
            }
            var wd = reader.ReadName();
            var dayOfWeek = ParseDayOfWeek(wd);
            return new WeekDayNum {
                DayOfWeek = dayOfWeek,
                OrderWeek = order
            };
        }

        public static WeekDayNum Parse(string val) {
            return ReadFrom(new CalendarReader(val));
        }

        private static DayOfWeek ParseDayOfWeek(string val) {
            val = val.ToUpperInvariant();
            return val switch {
                "SU" => DayOfWeek.Sunday,
                "MO" => DayOfWeek.Monday,
                "TU" => DayOfWeek.Tuesday,
                "WE" => DayOfWeek.Wednesday,
                "TH" => DayOfWeek.Thursday,
                "FR" => DayOfWeek.Friday,
                "SA" => DayOfWeek.Saturday,
                _ => throw new Exception("Unknown day of week " + val),
            };
        }

        public static implicit operator WeekDayNum(string val) {
            return Parse(val);
        }
    }
}

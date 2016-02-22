using System;

namespace Caesium.Data {
    public struct DateTimeValue {

        public int Year { get; set; }

        public MonthOfYear Month { get; set; }

        public int Day { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public bool Utc { get; set; }

        public TimeSpan TimeOfDay => new TimeSpan(Hours, Minutes, Seconds);

        public static DateTimeValue Parse(string src) {
            if (src.Length != 15 && src.Length != 16) throw new FormatException();
            var res = new DateTimeValue {
                Year = Value.ParseFourDigitInteger(src, 0),
                Month = (MonthOfYear)Value.ParseTwoDigitInteger(src, 4),
                Day = Value.ParseTwoDigitInteger(src, 6),
                Hours = Value.ParseTwoDigitInteger(src, 9),
                Minutes = Value.ParseTwoDigitInteger(src, 11),
                Seconds = Value.ParseTwoDigitInteger(src, 13)
            };

            if (src[8] != 'T') throw new FormatException("T specifier expected");
            if (src.Length == 16) {
                if (src[15] == 'Z') res.Utc = true;
                else throw new FormatException("Z specifier expected");
            }
            return res;
        }

        public static bool TryParse(string src, out DateTimeValue val) {
            try {
                val = Parse(src);
                return true;
            } catch (FormatException) {
                val = new DateTimeValue();
                return false;
            }
        }
    }
}

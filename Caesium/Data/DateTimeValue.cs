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

        public static DateTimeValue Parse(string val) {
            if (val.Length != 15 && val.Length != 16) throw new FormatException();
            var res = new DateTimeValue {
                Year = Value.ParseFourDigitInteger(val, 0),
                Month = (MonthOfYear)Value.ParseTwoDigitInteger(val, 4),
                Day = Value.ParseTwoDigitInteger(val, 6),
                Hours = Value.ParseTwoDigitInteger(val, 9),
                Minutes = Value.ParseTwoDigitInteger(val, 11),
                Seconds = Value.ParseTwoDigitInteger(val, 13)
            };

            if (val[8] != 'T') throw new FormatException("T specifier expected");
            if (val.Length == 16) {
                if (val[15] == 'Z') res.Utc = true;
                else throw new FormatException("Z specifier expected");
            }
            return res;
        }
    }
}

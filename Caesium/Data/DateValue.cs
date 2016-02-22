using System;

namespace Caesium.Data {
    public struct DateValue {

        public int Year { get; set; }

        public MonthOfYear Month { get; set; }

        public int Day { get; set; }

        public static DateValue Parse(string val) {
            if (val.Length != 8) throw new FormatException("Invalid Date value");
            var res = new DateValue {
                Year = Value.ParseFourDigitInteger(val, 0),
                Month = (MonthOfYear)Value.ParseTwoDigitInteger(val, 4),
                Day = Value.ParseTwoDigitInteger(val, 6)
            };
            return res;
        }

        public static bool TryParse(string src, out DateValue val) {
            try {
                val = Parse(src);
                return true;
            } catch (FormatException) {
                val = new DateValue();
                return false;
            }
        }

    }
}

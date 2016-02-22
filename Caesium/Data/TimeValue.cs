using System;
using System.Text;

namespace Caesium.Data {
    public struct TimeValue {

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public bool Utc { get; set; }

        public static TimeValue Parse(string val) {
            if (val.Length != 6 && val.Length != 7) throw new Exception("Invalid Time value");
            var res = new TimeValue {
                Hours = Value.ParseTwoDigitInteger(val, 0),
                Minutes = Value.ParseTwoDigitInteger(val, 2),
                Seconds = Value.ParseTwoDigitInteger(val, 4)
            };
            if (val.Length == 7) {
                if (val[6] == 'Z') res.Utc = true;
                else throw new FormatException("Z specifier expected");
            }
            return res;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(Hours.ToString("D2"));
            sb.Append(Minutes.ToString("D2"));
            sb.Append(Seconds.ToString("D2"));
            if (Utc) {
                sb.Append('Z');
            }
            return sb.ToString();
        }

    }
}

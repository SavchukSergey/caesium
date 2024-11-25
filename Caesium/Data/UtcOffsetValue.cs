using System;
using System.Text;

namespace Caesium.Data {
    public struct UtcOffsetValue {

        public bool Negative { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int? Seconds { get; set; }

        public static UtcOffsetValue Parse(string val) {
            if (val.Length != 5 && val.Length != 7) throw new Exception("Invalid UtcOffset value");
            var res = new UtcOffsetValue {
                Negative = val[0] switch {
                    '+' => false,
                    '-' => true,
                    _ => throw new Exception("+ or - expected"),
                },
                Hours = ParsePart(val, 1),
                Minutes = ParsePart(val, 3)
            };
            if (val.Length == 7) res.Seconds = ParsePart(val, 5);
            return res;
        }

        public override readonly string ToString() {
            var sb = new StringBuilder();
            sb.Append(Negative ? '-' : '+');
            sb.Append(Hours.ToString("D2"));
            sb.Append(Minutes.ToString("D2"));
            if (Seconds.HasValue) {
                sb.Append(Seconds.Value.ToString("D2"));
            }
            return sb.ToString();
        }

        private static int ParsePart(string val, int index) {
            if (val.Length < index + 2) throw new Exception("value expected");
            var ch1 = val[index];
            var ch2 = val[index + 1];
            if (ch1 < '0' || ch1 >'9') throw new Exception("digit expected");
            if (ch2 < '0' || ch2 >'9') throw new Exception("digit expected");
            return (ch1 - '0') * 10 + (ch2 - '0');
        }
    }
}

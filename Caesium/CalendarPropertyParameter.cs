using System;
using System.Collections.Generic;
using System.Text;

namespace Caesium {
    public class CalendarPropertyParameter {

        public string Name { get; set; }

        public string Value {
            get {
                if (Values.Count > 1) throw new Exception("Single value expected");
                return Values?[0];
            }
            set {
                if (value != null) {
                    Values.Clear();
                    Values.Add(value);
                } else {
                    Values = null;
                }
            }
        }

        public IList<string> Values { get; set; }

        public string NameUpper => Name?.ToUpperInvariant();

        public static CalendarPropertyParameter ReadFrom(CalendarReader reader) {
            var res = new CalendarPropertyParameter {
                Name = reader.ReadName()
            };
            reader.ReadChar('=');
            res.Values = ParseParamValues(reader);
            return res;
        }

        private static IList<string> ParseParamValues(CalendarReader reader) {
            var res = new List<string>();
            do {
                var val = ParseParamValue(reader);
                if (val == null) {
                    if (res.Count == 0) throw new Exception("Param value expected");
                    return res;
                }
                res.Add(val);

            } while (reader.PeekOrRead(','));
            return res;
        }

        private static string ParseParamValue(CalendarReader reader) {
            StringBuilder sb = null;
            while (!reader.IsEof) {
                var ch = reader.PeekChar();
                if (ch == ':' || ch == ';' || ch == ',' || ch == '"' || ch == 0x7f || (ch < 0x20 && ch != 0x09)) {
                    return sb?.ToString();
                }
                ch = reader.ReadChar();
                if (sb == null) sb = new StringBuilder();
                sb.Append(ch);
            }
            return sb?.ToString();
        }


        public override string ToString() {
            var sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }

        public void ToString(StringBuilder sb) {
            sb.Append(Name ?? "");
            sb.Append('=');
            if (Values != null) {
                for (var i = 0; i < Values.Count; i++) {
                    var val = Values[i];
                    if (i != 0) sb.Append(',');
                    sb.Append(val);
                }
            }
        }

    }
}

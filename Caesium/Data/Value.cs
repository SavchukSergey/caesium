using System;
using System.Text;

namespace Caesium.Data {
    public static class Value {

        public static bool ParseBoolean(string val) {
            switch (val.ToUpperInvariant()) {
                case "TRUE": return true;
                case "FALSE": return false;
                default:
                    throw new FormatException("Invalid bool value");
            }
        }

        public static int ParseTwoDigitInteger(string val, int index) {
            if (val.Length < index + 2) throw new FormatException("value expected");
            var ch1 = val[index];
            var ch2 = val[index + 1];
            if (ch1 < '0' || ch1 > '9') throw new FormatException("digit expected");
            if (ch2 < '0' || ch2 > '9') throw new FormatException("digit expected");
            return (ch1 - '0') * 10 + (ch2 - '0');
        }

        public static int ParseFourDigitInteger(string val, int index) {
            if (val.Length < index + 4) throw new FormatException("value expected");
            var ch1 = val[index];
            var ch2 = val[index + 1];
            var ch3 = val[index + 2];
            var ch4 = val[index + 3];
            if (ch1 < '0' || ch1 > '9') throw new FormatException("digit expected");
            if (ch2 < '0' || ch2 > '9') throw new FormatException("digit expected");
            if (ch3 < '0' || ch3 > '9') throw new FormatException("digit expected");
            if (ch4 < '0' || ch4 > '9') throw new FormatException("digit expected");
            return (ch1 - '0') * 1000 + (ch2 - '0') * 100 + (ch3 - '0') * 10 + (ch4 - '0');
        }

        public static string ParseText(string val) {
            var sb = new StringBuilder();
            for (var i = 0; i < val.Length; i++) {
                var ch = val[i];
                if (ch == '\\') {
                    i++;
                    if (i >= val.Length) throw new FormatException("Unexpected escape sequence end");
                    ch = val[i];
                    ch = Unescape(ch);
                }
                sb.Append(ch);
            }
            return sb.ToString();
        }

        public static string FormatText(string val) {
            var sb = new StringBuilder();
            foreach (var ch in val) {
                switch (ch) {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case ';':
                        sb.Append("\\;");
                        break;
                    case ',':
                        sb.Append("\\,");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        private static char Unescape(char symbol) {
            switch (symbol) {
                case '\\':
                    return '\\';
                case ';':
                    return ';';
                case ',':
                    return ',';
                case 'N':
                case 'n':
                    return '\n';
                default:
                    throw new FormatException("Invalid escape character");
            }
        }

    }
}

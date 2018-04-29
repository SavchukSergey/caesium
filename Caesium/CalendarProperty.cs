using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caesium {
    public class CalendarProperty {

        public string Name { get; set; }

        public string Value { get; set; }

        public string NameUpper => Name?.ToUpperInvariant();

        public string ValueUpper => Value?.ToUpperInvariant();

        public IList<CalendarPropertyParameter> Parameters;

        public string GetParameter(string parameterName) {
            var prop = FindParameter(parameterName);
            return prop?.Value;
        }

        public void SetParameter(string parameterName, string value) {
            var param = FindParameter(parameterName);
            if (value != null) {
                if (param == null) {
                    param = new CalendarPropertyParameter {
                        Name = parameterName
                    };
                    Parameters.Add(param);
                }
                param.Value = value;
            } else {
                if (param != null) Parameters?.Remove(param);
            }
        }

        private CalendarPropertyParameter FindParameter(string paramName) {
            if (paramName == null) throw new ArgumentNullException(nameof(paramName));
            paramName = paramName.ToLowerInvariant();
            return Parameters?.FirstOrDefault(item => item.Name?.ToLowerInvariant() == paramName);
        }


        public static CalendarProperty Parse(string content) {
            return ReadFrom(new CalendarReader(content));
        }

        public static CalendarProperty ReadFrom(CalendarReader reader) {
            var line = new CalendarProperty {
                Name = reader.ReadName()
            };
            if (line.Name.Length == 0) {
                return line;
            }

            while (reader.PeekOrRead(';')) {
                var param = CalendarPropertyParameter.ReadFrom(reader);
                if (line.Parameters == null) line.Parameters = new List<CalendarPropertyParameter>();
                line.Parameters.Add(param);
            }
            reader.ReadChar(':');
            line.Value = ReadValue(reader);

            if (!reader.IsEof) reader.ReadChar('\r');
            if (!reader.IsEof) reader.ReadChar('\n');
            return line;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append(NameUpper ?? "<null>");
            if (Parameters != null) {
                foreach (var param in Parameters) {
                    sb.Append(';');
                    sb.Append(param.NameUpper ?? "<null>");
                    sb.Append('=');
                    if (param.Values != null) {
                        for (var i = 0; i < param.Values.Count; i++) {
                            if (i != 0) sb.Append(',');
                            var val = param.Values[i];
                            sb.Append(val ?? "<null>");
                        }
                    } else {
                        sb.Append("<null>");
                    }
                }
            }
            sb.Append(':');
            sb.Append(Value ?? "<null>");
            return sb.ToString();
        }

        private static string ReadValue(CalendarReader reader) {
            var preview = reader.PeekChar();
            if (preview == '"') return reader.ReadQuotedString();
            var sb = new StringBuilder();
            while (!reader.IsEof) {
                var ch = reader.PeekChar();
                if ((ch == '\t' || ch >= 0x20) && ch != 0x7f) {
                    sb.Append(reader.ReadChar());
                } else {
                    return sb.ToString();
                }
            }
            return sb.ToString();
        }

    }
}

//TODO: ToString(), do not break utf-8 chacters when folding

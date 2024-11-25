using Caesium.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Caesium {
    public class CalendarObject {

        public CalendarObject(string type) {
            Type = type;
        }

        public string Type { get; }

        public IList<CalendarObject> Children { get; } = [];

        public IList<CalendarProperty> Properties { get; } = [];

        public string this[string propertyName] {
            get {
                var prop = FindProperty(propertyName);
                return prop?.Value;
            }
            set {
                var prop = FindProperty(propertyName);
                if (prop == null) {
                    prop = new CalendarProperty {
                        Name = propertyName
                    };
                    Properties.Add(prop);
                }
                prop.Value = value;
            }
        }

        protected string GetText(string propertyName) {
            var raw = this[propertyName];
            return raw != null ? Value.ParseText(raw) : null;
        }

        protected void SetText(string propertyName, string value) {
            var raw = value != null ? Value.FormatText(value) : null;
            this[propertyName] = raw;
        }

        public string this[string propertyName, string parameterName] {
            get {
                var prop = FindProperty(propertyName);
                return prop?.GetParameter(parameterName);
            }
            set {
                var prop = FindProperty(propertyName);
                if (prop == null && parameterName != null) {
                    prop = new CalendarProperty {
                        Name = propertyName
                    };
                    Properties.Add(prop);
                }
                if (prop != null) prop.Value = value;
            }
        }

        public static CalendarObject CreateObject(string type) {
            return type switch {
                "VCALENDAR" => new VCalendar(),
                "VEVENT" => new VEvent(),
                "VTIMEZONE" => new VTimeZone(),
                "DAYLIGHT" => new DayLight(),
                "STANDARD" => new Standard(),
                _ => new CalendarObject(type),
            };
        }

        public static CalendarObject Parse(string val) {
            return Parse(new CalendarReader(val));
        }

        public static CalendarObject Parse(CalendarReader reader) {
            var begin = CalendarProperty.ReadFrom(reader);
            if (begin.NameUpper != "BEGIN") throw new Exception("object expected");
            var type = begin.ValueUpper;
            var obj = CreateObject(type);
            while (!reader.IsEof) {
                var pos = reader.GetPosition();
                var line = CalendarProperty.ReadFrom(reader);
                switch (line.NameUpper) {
                    case "BEGIN":
                        reader.SetPosition(pos);
                        var sub = Parse(reader);
                        obj.Children.Add(sub);
                        break;
                    case "END":
                        if (line.ValueUpper != type) throw new Exception($"Expected {type} object close but {line.ValueUpper} appeared");
                        return obj;
                    default:
                        obj.Properties.Add(line);
                        break;
                }
            }
            return obj;

        }

        public static async Task<CalendarObject> LoadAsync(string uri) {
            using var stream = await _client.GetStreamAsync(uri) ?? throw new Exception("Content not found");
            var content = await new StreamReader(stream).ReadToEndAsync();
            return Parse(content);
        }

        public static async Task<T> LoadAsync<T>(string uri) where T : CalendarObject {
            var obj = await LoadAsync(uri);
            return (T)obj;
        }

        private CalendarProperty FindProperty(string key) {
            key = key.ToUpperInvariant();
            return Properties.FirstOrDefault(item => item.NameUpper == key);
        }

        private static readonly HttpClient _client = new();

    }
}

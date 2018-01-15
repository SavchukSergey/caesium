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

        public IList<CalendarObject> Children { get; } = new List<CalendarObject>();

        public IList<CalendarProperty> Properties { get; } = new List<CalendarProperty>();

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
            switch (type) {
                case "VCALENDAR": return new VCalendar();
                case "VEVENT": return new VEvent();
                case "VTIMEZONE": return new VTimeZone();
                case "DAYLIGHT": return new DayLight();
                case "STANDARD": return new Standard();
                default: return new CalendarObject(type);
            }
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
            using (var request = new HttpClient()) {
                using (var stream = await request.GetStreamAsync(uri)) {
                    if (stream == null) throw new Exception("Content not found");
                    var content = await new StreamReader(stream).ReadToEndAsync();
                    return Parse(content);
                }
            }
        }

        public static async Task<T> LoadAsync<T>(string uri) where T : CalendarObject {
            var obj = await LoadAsync(uri);
            return (T)obj;
        }

        private CalendarProperty FindProperty(string key) {
            key = key.ToUpperInvariant();
            return Properties.FirstOrDefault(item => item.NameUpper == key);
        }

    }
}

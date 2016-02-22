using System;
using System.Collections.Generic;
using System.Linq;

namespace Caesium.Data {
    public struct RecurrenceValue {

        public RecurrenceFrequency Frequency { get; set; }

        public DateValue? UntilDate { get; set; }

        public DateTimeValue? UntilDateTime { get; set; }

        public int? Count { get; set; }

        public int? Interval { get; set; }

        public IList<int> BySecond { get; set; }

        public IList<int> ByMinute { get; set; }

        public IList<int> ByHour { get; set; }

        public IList<WeekDayNum> ByDay { get; set; }

        public IList<int> ByMonthDay { get; set; }

        public IList<int> ByYearDay { get; set; }

        public IList<int> ByWeekNo { get; set; }

        public IList<MonthOfYear> ByMonth { get; set; }

        public IList<int> BySetPos { get; set; }

        public static RecurrenceValue Parse(CalendarReader reader) {
            RecurrenceFrequency? freq = null;
            int? interval = null;
            List<int> byMinute = null;
            List<int> byHour = null;
            List<WeekDayNum> byDay = null;
            List<int> bySetPos = null;
            List<MonthOfYear> byMonth = null;
            do {
                var param = CalendarPropertyParameter.ReadFrom(reader);
                var name = param.Name.ToUpperInvariant();
                switch (name) {
                    case "FREQ":
                        if (freq.HasValue) throw new Exception("Multiple FREQ rule parts occured");
                        freq = ParseFrequency(param.Value);
                        break;
                    case "INTERVAL":
                        if (interval.HasValue) throw new Exception("Multiple INTERVAL rule parts occured");
                        interval = int.Parse(param.Value);
                        break;
                    case "BYMINUTE":
                        if (byMinute != null) throw new Exception("Multiple BYMINUTE rule parts occured");
                        if (param.Values.Count == 0) throw new Exception("BYMINUTE values expected");
                        byMinute = param.Values.Select(int.Parse).ToList();
                        break;
                    case "BYHOUR":
                        if (byHour != null) throw new Exception("Multiple BYHOUR rule parts occured");
                        if (param.Values.Count == 0) throw new Exception("BYHOUR values expected");
                        byHour = param.Values.Select(int.Parse).ToList();
                        break;
                    case "BYDAY":
                        if (byDay != null) throw new Exception("Multiple BYDAY rule parts occured");
                        if (param.Values.Count == 0) throw new Exception("BYDAY values expected");
                        byDay = param.Values.Select(WeekDayNum.Parse).ToList();
                        break;
                    case "BYMONTH":
                        if (byMonth != null) throw new Exception("Multiple BYMONTH rule parts occured");
                        if (param.Values.Count == 0) throw new Exception("BYMONTH values expected");
                        byMonth = param.Values.Select(item => (MonthOfYear)int.Parse(item)).ToList();
                        break;
                    case "BYSETPOS":
                        if (bySetPos != null) throw new Exception("Multiple BYSETPOS rule parts occured");
                        if (param.Values.Count == 0) throw new Exception("BYSETPOS values expected");
                        bySetPos = param.Values.Select(int.Parse).ToList();
                        break;
                    default:
                        throw new Exception("Unknown rule part " + name);
                }
            } while (reader.PeekOrRead(';'));

            if (!freq.HasValue) throw new Exception("FREQ rule part is required");
            return new RecurrenceValue {
                Frequency = freq.Value,
                Interval = interval,
                ByMinute = byMinute,
                ByHour = byHour,
                ByDay = byDay,
                ByMonth = byMonth,
                BySetPos = bySetPos
            };
        }

        public static RecurrenceValue Parse(string specification) {
            return Parse(new CalendarReader(specification));
        }

        private static RecurrenceFrequency ParseFrequency(string freq) {
            switch (freq) {
                case "YEARLY": return RecurrenceFrequency.Yearly;
                case "MONTHLY": return RecurrenceFrequency.Monthly;
                case "WEEKLY": return RecurrenceFrequency.Weekly;
                case "DAILY": return RecurrenceFrequency.Daily;
                case "HOURLY": return RecurrenceFrequency.Hourly;
                case "MINUTELY": return RecurrenceFrequency.Minutely;
                case "SECONDLY": return RecurrenceFrequency.Secondly;
                default:
                    throw new ArgumentException("Unknown frequency type");
            }
        }


    }
}

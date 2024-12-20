﻿using Caesium.Data;
using NUnit.Framework;

namespace Caesium.Tests.Data {
    [TestFixture]
    public class DateValueTests {

        [TestCase("20150412", 2015, MonthOfYear.April, 12)]
        [TestCase("20151231", 2015, MonthOfYear.December, 31)]
        public void ParseTest(string val, int year, MonthOfYear month, int day) {
            var date = DateValue.Parse(val);
            Assert.That(date.Year, Is.EqualTo(year));
            Assert.That(date.Month, Is.EqualTo(month));
            Assert.That(date.Day, Is.EqualTo(day));
        }
    }
}

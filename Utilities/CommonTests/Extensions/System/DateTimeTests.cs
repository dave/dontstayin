using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace System.Tests
{
	[TestFixture]
	public class DateTime_Tests
	{
		[Test]
		public void ToRangeString_Tests()
		{
			global::System.DateTime now = global::System.DateTime.Now;
			string format = "hh:mm dd MMM yyyy";
			TimeSpan diff = new TimeSpan(0, 0, 0);
			Assert.AreEqual(now.ToString("hh:mm") + " -> " + now.Add(diff).ToString(format), now.ToRangeString(now.Add(diff), "hh:mm dd MMM yyyy"));
			diff = now.AddDays(1) - now;
			Assert.AreEqual(now.ToString("hh:mm dd") + " -> " + now.Add(diff).ToString(format), now.ToRangeString(now.Add(diff), "hh:mm dd MMM yyyy"));
			diff = now.AddMonths(1) - now;
			Assert.AreEqual(now.ToString("hh:mm dd MMM") + " -> " + now.Add(diff).ToString(format), now.ToRangeString(now.Add(diff), "hh:mm dd MMM yyyy"));
			diff = now.AddYears(1) - now;
			Assert.AreEqual(now.ToString("hh:mm dd MMM yyyy") + " -> " + now.Add(diff).ToString(format), now.ToRangeString(now.Add(diff), "hh:mm dd MMM yyyy"));
		}

		[Test]
		public void PreviousDate_Tests()
		{
			DateTime thursdayFourteenthFebruary2008 = new DateTime(2008, 2, 14);
			Assert.AreEqual(new DateTime(2008, 2, 11), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Monday));
			Assert.AreEqual(new DateTime(2008, 2, 7), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Thursday));
			Assert.AreEqual(new DateTime(2008, 2, 8), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Friday));
			Assert.AreEqual(new DateTime(2008, 2, 11), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Monday, true));
			Assert.AreEqual(new DateTime(2008, 2, 8), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Friday, true));
			Assert.AreEqual(new DateTime(2008, 2, 14), thursdayFourteenthFebruary2008.Previous(DayOfWeek.Thursday, true));

			DateTime fridayFirstOfFeb2008 = new DateTime(2008, 2, 1);
			Assert.AreEqual(new DateTime(2008, 1, 28), fridayFirstOfFeb2008.Previous(DayOfWeek.Monday, true));
		}
		[Test]
		public void NextDate_Tests()
		{
			DateTime thursdayFourteenthFebruary2008 = new DateTime(2008, 2, 14);
			Assert.AreEqual(new DateTime(2008, 2, 18), thursdayFourteenthFebruary2008.Next(DayOfWeek.Monday));
			Assert.AreEqual(new DateTime(2008, 2, 21), thursdayFourteenthFebruary2008.Next(DayOfWeek.Thursday));
			Assert.AreEqual(new DateTime(2008, 2, 15), thursdayFourteenthFebruary2008.Next(DayOfWeek.Friday));
			Assert.AreEqual(new DateTime(2008, 2, 18), thursdayFourteenthFebruary2008.Next(DayOfWeek.Monday, true));
			Assert.AreEqual(new DateTime(2008, 2, 15), thursdayFourteenthFebruary2008.Next(DayOfWeek.Friday, true));
			Assert.AreEqual(new DateTime(2008, 2, 14), thursdayFourteenthFebruary2008.Next(DayOfWeek.Thursday, true));
		}
	}
}

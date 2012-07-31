using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public static class DateTimeExtensions
	{
		public static string ToRangeString(this global::System.DateTime thisDate, global::System.DateTime otherDate, string format)
		{
			global::System.DateTime firstDate = thisDate;
			global::System.DateTime secondDate = otherDate;
			if (firstDate > secondDate)
			{
				firstDate = secondDate;
				secondDate = thisDate;
			}
			string firstDateFormat = format;
			if (firstDate.Year == secondDate.Year)
			{
				firstDateFormat = firstDateFormat.Replace("y", "");
				if (firstDate.Month == secondDate.Month)
				{
					firstDateFormat = firstDateFormat.Replace("M", "");
					if (firstDate.Day == secondDate.Day)
					{
						firstDateFormat = firstDateFormat.Replace("d", "");
					}
				}
			}
			
			while (firstDateFormat.IndexOf("  ") > -1)
			{
				firstDateFormat = firstDateFormat.Replace("  ", " ");
			}
			firstDateFormat = firstDateFormat.Trim();
			if (firstDateFormat.Length == 0 && format.Length > 0)
			{
				return secondDate.ToString(format);
			}
			return firstDate.ToString(firstDateFormat) + " -> " + secondDate.ToString(format);
			
		}

		public static DateTime Previous(this DateTime thisDate, DayOfWeek dayOfWeek)
		{
			return thisDate.Previous(dayOfWeek, false);
		}
		public static DateTime Previous(this DateTime thisDate, DayOfWeek dayOfWeek, bool orEqual)
		{
			if (orEqual && dayOfWeek == thisDate.DayOfWeek) return thisDate;
			return thisDate.AddDays(((int)dayOfWeek - (int)thisDate.DayOfWeek + 7) % 7 - 7);
		}
		public static DateTime Next(this DateTime thisDate, DayOfWeek dayOfWeek)
		{
			return thisDate.Next(dayOfWeek, false);
		}
		public static DateTime Next(this DateTime thisDate, DayOfWeek dayOfWeek, bool orEqual)
		{
			if (orEqual && dayOfWeek == thisDate.DayOfWeek) return thisDate;
			return thisDate.AddDays(((int)dayOfWeek - (int)thisDate.DayOfWeek - 7) % 7 + 7);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace System
{
	public static class Int32Extensions
	{
		public static void Times(this Int32 i, Action action)
		{
			for (int j = 0; j < i; j++) action();
		}
		public static void Times(this Int32 i, Action<Int32> action)
		{
			for (int j = 0; j < i; j++) action(j);
		}

		public static TimeSpan Days(this int i)
		{
			return new TimeSpan(i, 0, 0, 0);
		}
		public static TimeSpan Hours(this int i)
		{
			return new TimeSpan(0, i, 0, 0);
		}
		public static TimeSpan Minutes(this int i)
		{
			return new TimeSpan(0, 0, i, 0);
		}
		public static TimeSpan Seconds(this int i)
		{
			return new TimeSpan(0, 0, 0, i);
		}
		public static int? ParseToInt32(this string s)
		{
			int value;
			return int.TryParse(s, out value) ? value : (int?) null;
		}
		public static bool IsNumeric(this string s)
		{
			int value;
			return int.TryParse(s, out value);
		}

	}
}

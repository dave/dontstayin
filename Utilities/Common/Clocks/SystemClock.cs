using System;

namespace Common.Clocks
{
	public class SystemClock : Clock
	{
		protected internal override DateTime Now
		{
			get { return DateTime.Now; }
		}

	}
}

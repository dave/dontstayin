using System;

namespace Common.Clocks
{
	public class ShiftedClock : Clock
	{
		private TimeSpan offset;

		public ShiftedClock(DateTime timeValue)
		{
			this.offset = timeValue - DateTime.Now;
		}

		protected internal override DateTime Now
		{
			get { return DateTime.Now.Add(offset) ; }
		}

	}
}

using System;

namespace Common.Clocks
{
	public class FixedClock : Clock
	{
		private DateTime timeValue;

		public FixedClock(DateTime timeValue)
		{
			this.timeValue = timeValue;
		}

		protected internal override DateTime Now
		{
			get { return timeValue; }
		}

	}
}

using System;

namespace Common.Clocks
{
	public abstract class Clock
	{
		protected internal abstract DateTime Now { get; }
	}
}

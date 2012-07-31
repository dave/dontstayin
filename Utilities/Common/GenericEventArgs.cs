using System;

namespace System
{
	public class EventArgs<T> : EventArgs
	{
		public T Value {get;set;}
		public EventArgs(T argument)
		{
			this.Value = argument;
		}
	}
}

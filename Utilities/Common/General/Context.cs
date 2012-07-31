using System;
using System.Collections.Generic;
using System.Text;

namespace Common.General
{
	public class Context<T> : IDisposable
	{
		public delegate T Getter();
		public delegate void Setter(T t);
		T oldValue;
		Setter setter;
		public Context(Getter getter, Setter setter, T newValue)
		{
			oldValue = getter();
			setter(newValue);
			this.setter = setter;
		}

		#region IDisposable Members

		public void Dispose()
		{
			setter(oldValue);
		}

		#endregion
	}
}

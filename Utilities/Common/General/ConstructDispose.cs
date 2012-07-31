using System;
using System.Collections.Generic;
using System.Text;

namespace Common.General
{
	public class ConstructDispose  : IDisposable
	{
		public delegate void Function();
		Function dispose;
		public ConstructDispose(Function construct, Function dispose)
		{
			construct.Invoke();
			this.dispose = dispose;
		}

		#region IDisposable Members

		public void Dispose()
		{
			dispose.Invoke();
		}
		#endregion
	}
}

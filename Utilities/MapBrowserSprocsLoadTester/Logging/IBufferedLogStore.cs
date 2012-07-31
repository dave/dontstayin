using System;

namespace MapBrowserSprocsLoadTester.Logging
{
	interface IBufferedLogStore : ILogStore, IDisposable
	{
		void Flush();
	}
}

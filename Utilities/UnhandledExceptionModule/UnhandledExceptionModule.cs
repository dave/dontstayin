using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using System.Web;
using Bobs;

namespace WebMonitor
{
	public class UnhandledExceptionModule : IHttpModule
	{
		static object _initLock = new object();
		static bool _initialized = false;

		public void Init(HttpApplication app)
		{
			// Do this one time for each AppDomain.
			if (!_initialized)
			{
				lock (_initLock)
				{
					if (!_initialized)
					{
						AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

						_initialized = true;
					}
				}
			}
		}

		public void Dispose()
		{
		}

		void OnUnhandledException(object o, UnhandledExceptionEventArgs e)
		{
			SpottedException.TryToSaveExceptionAndChildExceptions((Exception)e.ExceptionObject, null, null, null, "", "UnhandledException", "", 0, null);
		}

	}
}

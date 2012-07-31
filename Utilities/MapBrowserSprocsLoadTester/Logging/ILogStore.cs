using System.Collections.Generic;

namespace MapBrowserSprocsLoadTester.Logging
{
	internal interface ILogStore
	{
		void Write(params KeyValuePair<string, string>[] values);
	}
}

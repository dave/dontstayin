using System.Collections.Generic;
using MapBrowserSprocsLoadTester.Logging;

namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class ViewportResultLogger : IResultLogger<Viewport>
	{
		private readonly ILogStore logStore;
		private readonly KeyValuePair<string, string>[] headers;

		public ViewportResultLogger(ILogStore logStore, KeyValuePair<string, string>[] headers)
		{
			this.logStore = logStore;
			this.headers = headers;
		}

		public void Log(Viewport v, long milliseconds, int rowCount)
		{
			var toWrite = new List<KeyValuePair<string, string>>();
			toWrite.AddRange(headers);
			toWrite.AddRange(new List<KeyValuePair<string, string>>()
			{
				new KeyValuePair<string, string>("RowCount", rowCount.ToString()),
				new KeyValuePair<string, string>("North", v.North.ToString()),
				new KeyValuePair<string, string>("South", v.South.ToString()),
				new KeyValuePair<string, string>("East", v.East.ToString()),
				new KeyValuePair<string, string>("West", v.West.ToString()),
				new KeyValuePair<string, string>("ZoomLevel", v.ZoomLevel.Value.ToString()),
				new KeyValuePair<string, string>("Ms", milliseconds.ToString())
			});
			logStore.Write
			(
				toWrite.ToArray()
			);
		}
	}
}

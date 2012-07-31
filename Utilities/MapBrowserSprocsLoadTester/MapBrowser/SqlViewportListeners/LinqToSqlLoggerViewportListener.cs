using System.Data.SqlClient;

namespace MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners
{
	abstract class LinqToSqlLoggerViewportListener : IListener<Viewport>
	{
		private IResultLogger<Viewport> logger;
		protected readonly string connectionString;

		public LinqToSqlLoggerViewportListener(IResultLogger<Viewport> logger, string connectionString)
		{
			this.logger = logger;
			this.connectionString = connectionString;
		}

		public void OnChanged(Viewport viewport)
		{
			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			int rowCount = GetResults(viewport);
			stopwatch.Stop();

			this.logger.Log(viewport, stopwatch.ElapsedMilliseconds, rowCount);
		}

		protected abstract int GetResults(Viewport viewport);
	}
}

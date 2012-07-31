using System.Data.SqlClient;

namespace MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners
{
	abstract class SqlLoggerViewportListener : IListener<Viewport>
	{
		private IResultLogger<Viewport> logger;
		protected readonly string connectionString;

		public SqlLoggerViewportListener(IResultLogger<Viewport> logger, string connectionString)
		{
			this.logger = logger;
			this.connectionString = connectionString;
		}

		public void OnChanged(Viewport viewport)
		{
			var stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			int rowCount = 0;
			using (var conn = new SqlConnection(connectionString))
			{
				using (var cmd = GetSqlCommand(viewport))
				{
					cmd.Connection = conn;
					try
					{
						conn.Open();
						var reader = cmd.ExecuteReader();
						while (reader.HasRows && reader.Read())
						{
							rowCount++;
						}
					}
					finally
					{
						conn.Close();
					}
				}
			}
			stopwatch.Stop();

			this.logger.Log(viewport, stopwatch.ElapsedMilliseconds, rowCount);
		}

		protected abstract SqlCommand GetSqlCommand(Viewport viewport);
	}
}

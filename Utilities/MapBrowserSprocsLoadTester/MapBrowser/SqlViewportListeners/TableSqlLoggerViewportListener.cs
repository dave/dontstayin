using System.Data.SqlClient;

namespace MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners
{
	class TableSqlLoggerViewportListener : SqlLoggerViewportListener
	{
		private readonly string tableName;
		private readonly string sqlScript;

		public TableSqlLoggerViewportListener(string tableName, string sqlScript,
			IResultLogger<Viewport> logger, string connectionString)
			: base(logger, connectionString)
		{
			this.tableName = tableName;
			this.sqlScript = sqlScript;
		}

		protected override SqlCommand GetSqlCommand(Viewport viewport)
		{
			var cmd = new SqlCommand()
			{
				CommandText = sqlScript,
				CommandType = System.Data.CommandType.Text,
			};
			cmd.Parameters.AddWithValue("@TableName", tableName.ToString());
			cmd.Parameters.AddWithValue("@WhereClause", tableName == "Event" ? "(K <> 160059)" : "1=1");
			cmd.Parameters.AddWithValue("@CustomVariableDeclarationSql", "");
			cmd.Parameters.AddWithValue("@OrderBy", "ORDER BY K DESC");
			cmd.Parameters.AddWithValue("@FirstRowIndex",0);
			cmd.Parameters.AddWithValue("@LastRowIndex", 200);
			cmd.Parameters.AddWithValue("@North", viewport.North);
			cmd.Parameters.AddWithValue("@South", viewport.South);
			cmd.Parameters.AddWithValue("@East", viewport.East);
			cmd.Parameters.AddWithValue("@West", viewport.West);
			cmd.CommandTimeout = 0;
			return cmd;
		}
	}
}

 

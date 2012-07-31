using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace SqlScriptRunner
{
    internal class LogTable
    {
        #region SqlToCreateLogTableIfNecessary definition
        const string SqlToCreateLogTableIfNecessary = @"
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdateScriptLog]') AND type in (N'U'))

BEGIN
	CREATE TABLE dbo.UpdateScriptLog(
		ToolStarted datetime NOT NULL,
		ScriptExecuted datetime NOT NULL,
		ScriptName varchar(250) NOT NULL,
		ScriptModified datetime NOT NULL,
        ScriptType varchar(50) NOT NULL,
		Result varchar(MAX) NOT NULL
	)  ON [PRIMARY]
	TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE dbo.UpdateScriptLog ADD CONSTRAINT
		PK_UpdateScriptLog PRIMARY KEY CLUSTERED 	(
			ToolStarted,
			ScriptExecuted,
            ScriptName
		) WITH( 
			STATISTICS_NORECOMPUTE = OFF, 
			IGNORE_DUP_KEY = OFF, 
			ALLOW_ROW_LOCKS = ON, 
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
END";
        #endregion
        static bool HaveAlreadyRunLogTableCreateScript = false;
        internal static void Write(ScriptTypes.Script script, DateTime scriptExecuted, string result)
        {
            if (!HaveAlreadyRunLogTableCreateScript)
            {
                UnitTestUtilities.Sql.SqlHelper.Execute(Global.ConnectionString, SqlToCreateLogTableIfNecessary);
            }
            SqlConnection conn = new SqlConnection(Global.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO UpdateScriptLog (ToolStarted,ScriptExecuted,ScriptName,ScriptType,ScriptModified,Result) VALUES (@ToolStarted,@ScriptExecuted,@ScriptName,@ScriptType,@ScriptModified,@Result)", conn);
            cmd.Parameters.Add(new SqlParameter("@ToolStarted", Global.ToolStarted));
            cmd.Parameters.Add(new SqlParameter("@ScriptExecuted", scriptExecuted));
            cmd.Parameters.Add(new SqlParameter("@ScriptName", script.File.Name));
            cmd.Parameters.Add(new SqlParameter("@ScriptType", script.ScriptType));
            cmd.Parameters.Add(new SqlParameter("@ScriptModified", script.File.LastWriteTime));
            cmd.Parameters.Add(new SqlParameter("@Result", result));
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

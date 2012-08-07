using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace Common.Automation.Sql
{
	public class Database
	{
		internal readonly SqlConnection conn = null;
		internal readonly string ConnectionString = null;
		internal readonly string DatabaseName = null;
		public Database(SqlConnection conn)
		{
			this.conn = conn;
			this.ConnectionString = conn.ConnectionString;
			this.DatabaseName = conn.Database;
		}
		public Database(string connectionString)
			: this(new SqlConnection(connectionString))
		{
			
		}
		#region stored procedures
		public List<StoredProcedure> StoredProcedures
		{
			get
			{

				DataSet ds = this.ExecuteAdapter("SELECT Name FROM SYS.PROCEDURES WHERE is_ms_shipped = 0");

				List<StoredProcedure> list = new List<StoredProcedure>();

				foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
				{
					list.Add(new StoredProcedure(this, (string)dr[0]));
				}

				return list;
			}
		}
		#endregion
		#region tables
		public List<Table> Tables
		{
			get
			{
				List<Table> tables = new List<Table>();
				DataTable dtTables = this.ExecuteAdapter("sp_tables").Tables[0];
				foreach (DataRow TableDetails in dtTables.Rows)
					if (TableDetails["TABLE_OWNER"] as string == "dbo")
					{
						string tableName = TableDetails["TABLE_NAME"] as string;
						Table table = new Table(this, tableName);
						if (table.Description != null)
						{
							table.SetKeys();
							tables.Add(table);
						}
					}
				
				return tables;
			}
		}
		#endregion
		#region Exists
		bool Exists
		{
			get
			{
				try
				{
					conn.Open();
					return true;
				}
				catch
				{
					return false;
				}
				finally
				{
					conn.Close();
				}
			}
		}
		#endregion
		#region create database
		public void CreateDatabaseIfDoesNotExist(){
			if (!this.Exists)
			{
				try
				{
					string masterConnectionString = conn.ConnectionString.Replace(this.DatabaseName, "master");
					using (SqlConnection masterConn = new SqlConnection(masterConnectionString))
					{
						masterConn.Open();
						SqlCommand cmd = new SqlCommand("CREATE DATABASE " + this.DatabaseName, masterConn);
						cmd.CommandTimeout = 0;
						cmd.ExecuteNonQuery();
						System.Threading.Thread.Sleep(5000);
					}
				}
				catch// (Exception ex)
				{
				//	if (ex.Message.IndexOf("already exists. Choose a different database name") == -1)
				//	{
				//		throw ex;
				//	}
				}
			}
		}
		#endregion
		#region SetupDatabaseForClrAssemblies method
		private void SetupDatabaseForClrAssemblies()
		{
			if (!ClrIsEnabledInDatabase || !DbIsOwnedBySa)
			{
				Execute(
					@"  EXEC sp_configure 'clr enabled', 1
                        RECONFIGURE
						ALTER DATABASE " + this.DatabaseName + @" SET TRUSTWORTHY ON
                        EXEC dbo.sp_changedbowner @loginame = N'sa', @map = false   ");
			}
		}

		internal bool DbIsOwnedBySa
		{
			get
			{
				return Execute("SELECT COUNT(*) FROM sys.databases WHERE owner_sid = 0x01 AND name = db_name()") == 1;
			}
		}
		public bool ClrIsEnabledInDatabase
		{
			get
			{
				System.Data.DataSet ds = ExecuteAdapter("EXEC sp_configure 'clr enabled'");
				System.Data.DataTable dt = ds.Tables[0];
				return ((int)dt.Rows[0]["run_value"] == 1);
			}
		}
		#endregion
		#region UpdateAssembly
		public void UpdateAssembly(ClrAssemblyFile assemblyFile){
			SetupDatabaseForClrAssemblies();
			Execute(
				String.Format(
                    @"
                    IF EXISTS(SELECT TOP 1 * FROM sys.assemblies WHERE name = '{1}') BEGIN
                        IF (UPPER(CONVERT(VARCHAR(MAX), assemblyproperty('{1}','mvid'))) <> UPPER(CONVERT(VARCHAR(MAX), '{3}'))) BEGIN
                            ALTER ASSEMBLY {1}
                            FROM '{2}'
                            WITH UNCHECKED DATA, PERMISSION_SET = UNSAFE
                        END
                    END ELSE BEGIN 
                        CREATE ASSEMBLY {1}
                        FROM '{2}'
                        WITH PERMISSION_SET = UNSAFE
                    END",
				this.DatabaseName,
				assemblyFile.Name,
				assemblyFile.FileInfo.FullName,
				assemblyFile.Mvid));
		}
		#endregion
		#region Execute methods
		public int Execute(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.CommandTimeout = 0;
			try
			{
				conn.Open();
				return cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}
		}
		public object ExecuteScalar(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.CommandTimeout = 0;
			try
			{
				conn.Open();
				return cmd.ExecuteScalar();
			}
			finally
			{
				conn.Close();
			}
		}
		public System.Data.DataSet ExecuteAdapter(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			System.Data.DataSet ds = new System.Data.DataSet();
			try
			{
				conn.Open();
				adapter.Fill(ds);
				return ds;
			}
			finally
			{
				conn.Close();
			}
		}
		public System.Data.DataSet ExecuteFillSchema(string sql)
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
			System.Data.DataSet ds = new System.Data.DataSet();
			try
			{
				conn.Open();
				adapter.FillSchema(ds, SchemaType.Source);
				return ds;
			}
			finally
			{
				conn.Close();
			}
		}
		#endregion
		#region ExtendedProperties property

		public ExtendedProperties ExtendedProperties
		{
			get
			{
				return new DatabaseExtendedProperties(this);
			}
		}
		#endregion

		public void RemoveAssembly(ClrAssemblyFile file)
		{
			Execute(
				String.Format(
					@"
                    IF EXISTS(SELECT TOP 1 * FROM sys.assemblies WHERE name = '{0}') BEGIN
                        DROP ASSEMBLY {0}
                    END",
					file.Name
					)
				);
		}
	}
}

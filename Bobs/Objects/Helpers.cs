using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Bobs
{

	#region Delete
	public class Delete
	{

		//public class DeleteAllReturn : Return
		//{
		//    #region InnerException
		//    public Exception InnerException
		//    {
		//        get
		//        {
		//            return innerException;
		//        }
		//        set
		//        {
		//            innerException = value;
		//        }
		//    }
		//    private Exception innerException;
		//    #endregion
		//}

		public static void DeleteAll(IDeleteAll ObjectToDelete)
		{
			ObjectToDelete.DeleteAll(null);
		}

		#region CommandTimeout
		public int CommandTimeout
		{
			get
			{
				return commandTimeout;
			}
			set
			{
				commandTimeout = value;
			}
		}
		int commandTimeout = -1;
		#endregion

		#region Where
		public Q Where
		{
			get
			{
				return where;
			}
			set
			{
				where = value;
			}
		}
		private Q where;
		#endregion
		#region From
		public TablesEnum From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}
		private TablesEnum from;
		#endregion
		public Delete(TablesEnum from, Q where)
		{
			Where = where;
			From = from;
		}
		public int Run()
		{
			return Run(null);
		}
		public int Run(Transaction transaction)
		{
			Dictionary<string, SqlParameter> paramHash = new Dictionary<string, SqlParameter>();
			string rankContribution = "";
			string sql = "DELETE FROM [" + Tables.GetTableName(From) + "] WHERE " + Where.ToString(ref paramHash, ref rankContribution);

			int rows = 0;

			SqlConnection conn;
			if (transaction == null)
				conn = new SqlConnection(Vars.DefaultConnectionString);
			else
				conn = transaction.SqlConnection;

			try
			{
				SqlCommand myCommand = new SqlCommand(sql, conn);

				if (CommandTimeout != -1)
					myCommand.CommandTimeout = CommandTimeout;

				if (transaction != null)
					myCommand.Transaction = transaction.SqlTransaction;


				foreach (string c in paramHash.Keys)
				{
					object o = paramHash[c];
					myCommand.Parameters.Add(o);
				}

				if (!conn.State.Equals(System.Data.ConnectionState.Open))
					conn.Open();

				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Delete);

				rows = myCommand.ExecuteNonQuery();
			}
			finally
			{
				if (transaction == null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
			return rows;
		}
	}
	#endregion

	#region Update
	public class Update
	{
		#region Changes
		/// <summary>
		/// UPDATE Table SET Changes [FROM From] WHERE [Where]
		/// </summary>
		public List<Assign> Changes
		{
			get
			{
				if (changes == null)
					changes = new List<Assign>();
				return changes;
			}
			set
			{
				changes = value;
			}
		}
		private List<Assign> changes;
		#endregion

		#region Where
		/// <summary>
		/// UPDATE Table SET Changes [FROM From] WHERE [Where]
		/// </summary>
		public Q Where
		{
			get
			{
				return where;
			}
			set
			{
				where = value;
			}
		}
		private Q where;
		#endregion
		#region Table
		/// <summary>
		/// UPDATE Table SET Changes [FROM From] WHERE [Where]
		/// </summary>
		public TablesEnum Table
		{
			get
			{
				return table;
			}
			set
			{
				table = value;
			}
		}
		private TablesEnum table;
		#endregion
		#region From
		/// <summary>
		/// UPDATE Table SET Changes [FROM From] WHERE [Where]
		/// </summary>
		public TableElement From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}
		private TableElement from;
		#endregion
		public Update() { }
		public Update(TablesEnum table, List<Assign> changes, Q where)
		{
			Table = table;
			Changes = changes;
			Where = where;
		}
		public Update(TablesEnum table, TableElement from, List<Assign> changes, Q where)
		{
			Table = table;
			From = from;
			Changes = changes;
			Where = where;
		}

		#region CommandTimeout
		public int CommandTimeout
		{
			get
			{
				return commandTimeout;
			}
			set
			{
				commandTimeout = value;
			}
		}
		int commandTimeout = -1;
		#endregion

		public int Run()
		{
			return Run(null);
		}
		/// <summary>
		/// UPDATE Table SET Changes [FROM From] WHERE [Where]
		/// </summary>
		public int Run(Transaction transaction)
		{
			Dictionary<string, SqlParameter> paramHash = new Dictionary<string, SqlParameter>();
			string rankContribution = "";
			StringBuilder sb = new StringBuilder();
			sb.Append("UPDATE [");
			sb.Append(Tables.GetTableName(Table));
			sb.Append("] SET ");
			
			bool first = true;
			foreach (Assign a in Changes)
			{
				if (a != null)
				{
					if (!first)
						sb.Append(", ");
					a.BuildString(sb, ref paramHash, ref rankContribution);
					first = false;
				}
			}
			
			if (From != null)
			{
				sb.Append(" FROM ");
				sb.Append(From.ToString(ref paramHash, ref rankContribution, false));
			}
			if (Where == null)
				throw new Exception("You must set Where - if you want to update the whole table, set Where = new Q(true);");

			sb.Append(" WHERE ");
			sb.Append(Where.ToString(ref paramHash, ref rankContribution));

			string sql = sb.ToString();
			//throw new Exception(sql);

			int rows = 0;

			SqlConnection conn;
			if (transaction == null)
				conn = new SqlConnection(Vars.DefaultConnectionString);
			else
				conn = transaction.SqlConnection;

			try
			{
				SqlCommand myCommand = new SqlCommand(sql, conn);
				if (transaction != null)
					myCommand.Transaction = transaction.SqlTransaction;

				if (CommandTimeout != -1)
					myCommand.CommandTimeout = CommandTimeout;

				foreach (string c in paramHash.Keys)
				{
					object o = paramHash[c];
					myCommand.Parameters.Add(o);
				}

				if (!myCommand.Connection.State.Equals(ConnectionState.Open))
					myCommand.Connection.Open();

				Global.LogSqlQuery(Bobs.Global.QueryTypes.Update);

				rows = myCommand.ExecuteNonQuery();
			}
			finally
			{
				if (transaction == null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
			return rows;
		}
	}
	#endregion

	#region Transaction
	public class Transaction : IDisposable
	{
		#region SqlConnection
		public SqlConnection SqlConnection
		{
			get
			{
				return sqlConnection;
			}
			set
			{
				sqlConnection = value;
			}
		}
		private SqlConnection sqlConnection;
		#endregion
		#region SqlTransaction
		public SqlTransaction SqlTransaction
		{
			get
			{
				return sqlTransaction;
			}
			set
			{
				sqlTransaction = value;
			}
		}
		private SqlTransaction sqlTransaction;
		#endregion
		#region IsolationLevel
		public IsolationLevel IsolationLevel
		{
			get
			{
				return isolationLevel;
			}
			set
			{
				isolationLevel = value;
			}
		}
		private IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted;
		#endregion

		public Transaction()
		{
			this.SqlConnection = new SqlConnection(Vars.DefaultConnectionString);
			this.SqlConnection.Open();
			this.SqlTransaction = this.SqlConnection.BeginTransaction(this.IsolationLevel);
		}
		public void Commit()
		{
			this.SqlTransaction.Commit();
		}
		public void Rollback()
		{
			this.SqlTransaction.Rollback();
		}
		public void Close()
		{
			this.SqlConnection.Close();
		}
		public void Dispose()
		{
			this.SqlConnection.Close();
		}
	}
	#endregion

}

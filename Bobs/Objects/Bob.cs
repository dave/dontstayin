using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Runtime.Serialization;
using Common;
using Bobs.DataHolders;
using Caching;

namespace Bobs
{
	public interface IBob
	{
		Bob Bob { get; }
		object this[object columnEnum] { get; set; }
		Bob.ExtraSelectElementHolder ExtraSelectElements { get; }
		int Update();
	}
	/// <summary>
	/// This is our base class of business object (Bob).
	/// It's an abstract class, seeing as each implementation of it will have a different SqlTable.
	/// </summary>
	public class Bob : ISerializable
	{

		#region StoreInBobCache
		void StoreInBobCache()
		{
			if (Table.HasSinglePrimaryKey)
			{
				Caching.Instances.Main.Store(this.GetCacheKey(), new Object[2]{ Table.TableCacheKey, GetFullyInitialisedOb()});
				Global.IncrementRequestCounter(Global.RequestCounter.CacheStore);
			}
		}
		#endregion

		#region GetFromBobCache
		bool GetFromBobCache(object primaryKey)
		{
			if (this.Table.ColumnName(this.Table.SinglePrimaryKey) != "K")
				return false;

			object[] objectFromCache = Caching.Instances.Main.Get(Caching.Cache.GetBobsCacheKey(Table.TableName, primaryKey.ToString())) as object[];
			
			if (objectFromCache != null)
			{
				if ((string)objectFromCache[0] != this.Table.TableCacheKey) { throw new CachedBobVersionDifferentFromCurrentException(); }
				ColumnData<object> columnData = (ColumnData<object>)objectFromCache[1];
				Global.IncrementRequestCounter(Global.RequestCounter.CacheHit);
				InitialiseFromOb(columnData);
				DbRecordExists = true;


				foreach (ColumnDef c in Table)
				{
					if (columnData[(int)c.ColumnEnum].ToString() != primaryKey.ToString())
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(
							new Exception(string.Format("BobCacheError - requested {0}, received {1}. Type={2}", primaryKey,
														columnData[(int)c.ColumnEnum], Table.TableName)));
					}
					break;
				}

				return true;
			}
			else
			{
				Global.IncrementRequestCounter(Global.RequestCounter.CacheMiss);
				return false;
			}
		}
		#endregion

		//public void ClearFromBobCache()
		//{
		//    //not needed - handled by triggers.
		//    if (Table.HasSinglePrimaryKey)
		//        Cache.Static.Cache.Add(GetCacheKey(Table.TableName, this[Table.SinglePrimaryKey].ToString()), null);
		//}


		public Bob(TableDef tableDef)
		{
			this.Table = tableDef;
		}

		public Bob(TableDef tableDef, BobSet bobSet)
			: this(tableDef)
		{
			this.BobSet = bobSet;


		}

		#region GetObjectData (ISerializable)
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Table", this.Table.TableEnum);
			info.AddValue("Ob", GetFullyInitialisedOb());
			info.AddValue("DbRecordExists", this.DbRecordExists);
		}
		#endregion
		#region Bob (ISerializable)
		public Bob(SerializationInfo info, StreamingContext context) 
			: this(Tables.GetTableDef((TablesEnum) info.GetInt32("Table")))
		{
			//This executes the particular implementation of SetTableDef to ensure we have the right TableDef
			InitialiseFromOb((ColumnData<object>)info.GetValue("Ob", typeof(ColumnData<object>)));
			this.DbRecordExists = info.GetBoolean("DbRecordExists");
		}
		#endregion
		#region InitialiseFromOb
		public void InitialiseFromOb(ColumnData<object> ObIn)
		{
			try
			{
				ob = ObIn;
				int ColumnId;
				foreach (ColumnDef c in Table)
				{
					ColumnId = (int) c.ColumnEnum;
					ObInitialised[ColumnId] = true;
					ObAtInit[ColumnId] = Ob[ColumnId];
				}
			}catch(Exception ex)
			{
				throw new Exception(String.Format("ObIn is null:{0} Table is null:{1} ObInitialised is null:{2} ObAtInit is null{3}", ObIn == null, Table == null,  ObInitialised == null, ObAtInit == null), ex);
			}
		}
		#endregion
		#region GetFullyInitialisedOb()
		public ColumnData<object> GetFullyInitialisedOb()
		{
			int ColumnId;
			foreach (ColumnDef c in Table)
			{
				//if (c.IsComputed) continue;
				ColumnId = (int)c.ColumnEnum;
				if (!ObInitialised[ColumnId])
					ObInitialise(c.ColumnEnum, ColumnId);
			}
			return Ob;
		}
		#endregion

		#region Get(ObjectType type, int k)
		public static IBob Get(Model.Entities.ObjectType type, int k)
		{
			IBob b = null;
			bool wrongType = false;
			try
			{
				switch (type)
				{
					case Model.Entities.ObjectType.Photo:
						b = new Photo(k);
						break;
					case Model.Entities.ObjectType.Event:
						b = new Event(k);
						break;
					case Model.Entities.ObjectType.Venue:
						b = new Venue(k);
						break;
					case Model.Entities.ObjectType.Place:
						b = new Place(k);
						break;
					case Model.Entities.ObjectType.Thread:
						b = new Thread(k);
						break;
					case Model.Entities.ObjectType.Country:
						b = new Country(k);
						break;
					case Model.Entities.ObjectType.Article:
						b = new Article(k);
						break;
					case Model.Entities.ObjectType.Para:
						b = new Para(k);
						break;
					case Model.Entities.ObjectType.Brand:
						b = new Brand(k);
						break;
					case Model.Entities.ObjectType.Promoter:
						b = new Promoter(k);
						break;
					case Model.Entities.ObjectType.Usr:
						b = new Usr(k);
						break;
					case Model.Entities.ObjectType.Region:
						b = new Region(k);
						break;
					case Model.Entities.ObjectType.Gallery:
						b = new Gallery(k);
						break;
					case Model.Entities.ObjectType.Group:
						b = new Group(k);
						break;
					case Model.Entities.ObjectType.Banner:
						b = new Banner(k);
						break;
					case Model.Entities.ObjectType.GuestlistCredit:
						b = new GuestlistCredit(k);
						break;
					case Model.Entities.ObjectType.Ticket:
						b = new Ticket(k);
						break;
					case Model.Entities.ObjectType.Invoice:
						b = new Invoice(k);
						break;
					case Model.Entities.ObjectType.InsertionOrder:
						b = new InsertionOrder(k);
						break;
					case Model.Entities.ObjectType.CampaignCredit:
						b = new CampaignCredit(k);
						break;
					case Model.Entities.ObjectType.UsrDonationIcon:
						b = new UsrDonationIcon(k);
						break;
					default:
						wrongType = true;
						b = null;
						break;
				}
			}
			catch { }
			if (wrongType)
				throw new Exception("Bob.Get attempted to get " + type.ToString() + " - can't do it!!! DUH!");
			return b;
		}
		#endregion

		protected readonly TableDef Table;

		#region this[object ColumnEnum]
		public object this[object columnEnum]
		{
			get
			{
				int columnId = (int)columnEnum;

				if (!ObInitialised[columnId])
					ObInitialise(columnEnum, columnId);

				if (Ob[columnId] == null || Ob[columnId].Equals(System.DBNull.Value))
					return Table[columnEnum].DefaultValue;

				return Ob[columnId];

			}
			set
			{
				int ColumnId = (int)columnEnum;

				if (!ObInitialised[ColumnId])
					ObInitialise(columnEnum, ColumnId, true, value);
				else
					Ob[ColumnId] = value;


				
				ObDirty[ColumnId] = true;
				BobDirty = true;
				
			}
		}
		#endregion

		//#region IsNull
		//public bool IsNull(object ColumnEnum)
		//{
		//    int ColumnId = (int)ColumnEnum;

		//    if (!ObInitialised[ColumnId])
		//        ObInitialise(ColumnEnum, ColumnId);

		//    return Ob[ColumnId] == null || Ob[ColumnId].Equals(System.DBNull.Value);
		//}
		//#endregion

		//#region SetNull
		////public void SetNull(object ColumnEnum)
		////{
		////    int ColumnId = (int)ColumnEnum;

		////    if (!ObInitialised[ColumnId])
		////        ObInitialise(ColumnEnum, ColumnId, true, null);
		////    else
		////        Ob[ColumnId] = null;

		////    ObDirty[ColumnId] = true;
		////    BobDirty = true;
		////}
		//#endregion

		#region ValueAtInit
		public object ValueAtInit(object ColumnEnum)
		{
			int ColumnId = (int)ColumnEnum;

			if (!ObInitialised[ColumnId])
				ObInitialise(ColumnEnum, ColumnId);

			if (ObAtInit[ColumnId] == null || ObAtInit[ColumnId].Equals(System.DBNull.Value))
				return Table[ColumnEnum].DefaultValue;

			return ObAtInit[ColumnId];
		}
		#endregion

		//#region IsNullAtInit
		//public object IsNullAtInit(object ColumnEnum)
		//{
		//    int ColumnId = (int)ColumnEnum;

		//    if (!ObInitialised[ColumnId])
		//        ObInitialise(ColumnEnum, ColumnId);

		//    return ObAtInit[ColumnId] == null || ObAtInit[ColumnId].Equals(System.DBNull.Value);
		//}
		//#endregion

		#region ReInit
		/// <summary>
		/// We call this after an insert
		/// </summary>
		void ReInit()
		{
			foreach (ColumnDef c in Table)
				ObAtInit[(int)c.ColumnEnum] = Ob[(int)c.ColumnEnum];
		}
		#endregion

		#region IsDirty
		/// <summary>
		/// Has a column been changed?
		/// </summary>
		/// <param name="ColumnEnum">The column enum</param>
		/// <returns>Has this column been changed?</returns>
		public bool IsDirty(object ColumnEnum)
		{
			return ObDirty[(int)ColumnEnum];
		}
		/// <summary>
		/// Has any column been changed?
		/// </summary>
		/// <returns>Has any column been changed?</returns>
		public bool IsDirty()
		{
			return this.BobDirty;
		}
		#endregion

		#region IncludeInUpdateQuery
		bool IncludeInUpdateQuery(object ColumnEnum)
		{
			if ((Table[ColumnEnum].SqlColumnFlags & SqlColumnFlag.AutoNumber) != 0)
				return false;
			if ((Table[ColumnEnum].SqlColumnFlags & SqlColumnFlag.IsComputed) != 0)
				return false;
			else
				return IsDirty(ColumnEnum);
		}
		#endregion
		#region IncludeInInsertQuery
		bool IncludeInInsertQuery(object ColumnEnum)
		{
			if ((Table[ColumnEnum].SqlColumnFlags & SqlColumnFlag.AutoNumber) != 0)
				return false;
			if ((Table[ColumnEnum].SqlColumnFlags & SqlColumnFlag.IsComputed) != 0)
				return false;
			if (Table[ColumnEnum].SqlDbType == SqlDbType.Timestamp)
				return false;
			else
				return true;
		}
		#endregion

		#region Ob
		ColumnData<object> Ob
		{
			get
			{
				if (ob == null)
					ob = new ColumnData<object>();
				return ob;
			}
		}
		ColumnData<object> ob;
		#endregion
		#region ObAtInit
		ColumnData<object> ObAtInit
		{
			get
			{
				if (obAtInit == null)
					obAtInit = new ColumnData<object>();
				return obAtInit;
			}
		}
		ColumnData<object> obAtInit;
		#endregion
		#region ObInitialised
		ColumnData<bool> ObInitialised
		{
			get
			{
				if (obInitialised == null)
					obInitialised = new ColumnData<bool>();
				return obInitialised;
			}
		}
		ColumnData<bool> obInitialised;
		#endregion
		#region ObDirty
		ColumnData<bool> ObDirty
		{
			get
			{
				if (obDirty == null)
					obDirty = new ColumnData<bool>();
				return obDirty;
			}
		}
		ColumnData<bool> obDirty;
		#endregion
		#region BobDirty
		bool BobDirty = false;
		#endregion

		#region ObInitialise
		//void ObInitialise(object ColumnEnum, int ColumnId, bool Read)
		void ObInitialise(object columnEnum, int columnId)
		{
			ObInitialise(columnEnum, columnId, false, null);
		}
		void ObInitialise(object columnEnum, int columnId, bool write, object newValue)
		{
			
			if (this.DataRow == null)
			{
				//This should only be after an insert...
				if (Table[columnEnum].DefaultValue == null)
					ObInitialiseNow(columnId, Table[columnEnum].SqlDbType.Equals(SqlDbType.DateTime) ? null : Table[columnEnum].DefaultValue, write, newValue);
				else
					ObInitialiseNow(columnId, Table[columnEnum].DefaultValue, write, newValue);
			}
			else if (this.BobSet == null || this.BobSet.Query.Columns == null)
			{
				//Single bob selects, or simple bobsets, with no colums specified
				 ObInitialiseNow(columnId, this.DataRow[Tables.GetColumnName(columnEnum)], write, newValue);
			}
			else if (this.ColumnPrefix != null && this.ColumnPrefix.Length > 0)
			{
				//If the bobset has a columnprefix, append it to the start of the column name
				ObInitialiseNow(columnId, this.DataRow[this.ColumnPrefix + "_" + Tables.GetColumnName(columnEnum)], write, newValue);
			}
			else
			{
				//Else the table name goes on the start of the column name
				ObInitialiseNow(columnId, this.DataRow[this.Table.TableName + "_" + Tables.GetColumnName(columnEnum)], write, newValue);
			}
			
		}
		void ObInitialiseNow(int columnId, object value, bool write, object newValue)
		{
			//if (ColumnId == 4456449)
			//{
			//    int i = 1;
			//}
			// BUG? Surely we should be cloning these objects? Investigate.
			ObInitialised[columnId] = true;
			ObAtInit[columnId] = value;
			if (write)
				Ob[columnId] = newValue;
			else
				Ob[columnId] = value;
		}
		#endregion

		protected internal bool DbRecordExists { get; private set; }

		#region ExtraSelectElements
		public ExtraSelectElementHolder ExtraSelectElements
		{
			get
			{
				if (extraSelectElements == null)
					extraSelectElements = new ExtraSelectElementHolder(this.DataRow);
				return extraSelectElements;
			}
			set
			{
				extraSelectElements = value;
			}
		}
		private ExtraSelectElementHolder extraSelectElements;
		#region ExtraSelectElementHolder
		public class ExtraSelectElementHolder
		{
			public ExtraSelectElementHolder(DataRow row)
			{
				this.SourceRow = row;
			}

			public DataRow SourceRow { set; private get; }

			public object this[string ExtraSelectElementKey]
			{
				get
				{
					return SourceRow[ExtraSelectElementKey];
				}
			}
		}
		#endregion
		#endregion

		#region ColumnPrefix
		string ColumnPrefix
		{
			get
			{
				return columnPrefix;
			}
			set
			{
				columnPrefix = value;
			}
		}
		private string columnPrefix;
		#endregion

		protected internal BobSet BobSet { get; set; }
		protected internal DataRow DataRow { get; set; }


		#region Initialise(BobSet bobSet, DataRow row)
		public virtual void Initialise(BobSet bs, DataRow row)
		{
			this.BobSet = bs;
			this.DataRow = row;
			this.DbRecordExists = true;

		}
		public virtual void Initialise(DataRow row)
		{
			this.DataRow = row;
			this.DbRecordExists = true;
		}
		#endregion

		#region GetBobFromPrimaryKey
		internal void GetBobFromPrimaryKey(object PrimaryKey)
		{
			GetBobFromPrimaryKey(PrimaryKey, null);
		}
		internal void GetBobFromPrimaryKey(object PrimaryKey, Transaction transaction)
		{
			ColumnDef PrimaryKeyColumn = Table[Table.SinglePrimaryKey];
			try
			{
				if (GetFromBobCache(PrimaryKey)) { return; }
			}
			catch (CachedBobVersionDifferentFromCurrentException)
			{
				Global.IncrementRequestCounter(Global.RequestCounter.CacheException);
			}
			catch (Exception ex)
			{
				//SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "GetBobFromPrimaryKey", "", "", 0, null);
				Global.IncrementRequestCounter(Global.RequestCounter.CacheException);
			}

			SqlConnection conn;
			if (transaction == null)
				conn = new SqlConnection(Vars.DefaultConnectionString);
			else
				conn = transaction.SqlConnection;

			try
			{
				string sqlString = "SELECT TOP 1 * FROM [" + Table.TableName + "] WITH (NOLOCK) WHERE [" + Table.TableName + "].[" + Table.ColumnName(Table.SinglePrimaryKey) + "] = @PrimaryKey";
				SqlCommand command = new SqlCommand(sqlString, conn);
				command.Parameters.Add("@PrimaryKey", PrimaryKeyColumn.SqlDbType);
				command.Parameters[0].Value = PrimaryKey;

				if (transaction != null)
					command.Transaction = transaction.SqlTransaction;

				if (Vars.SetSqlConnectionTimeout)
					command.CommandTimeout = Vars.SqlConnectionTimeout;

				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = command;
				if (Vars.TraceQueries && HttpContext.Current != null)
				{
					HttpContext.Current.Trace.Write(command.CommandText.Replace("@PrimaryKey", PrimaryKey.ToString()));
				}

				if (!conn.State.Equals(ConnectionState.Open))
					conn.Open();

				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);

				adapter.Fill(dataset);
				DataView dv = dataset.Tables[0].DefaultView;

				if (dv.Count == 1)
				{
					Initialise(dv[0].Row);
				}
				else
				{
					throw new BobNotFound("You've requested something that's not in our database. It may have been deleted. (Technical details: " + Table.TableEnum.ToString() + " #" + PrimaryKey + ")");
				}
			}
			finally
			{
				if (transaction == null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
			try
			{
				StoreInBobCache();
			}
			catch (Exception ex)
			{
				// don't do this- if anything in here throws the same exception, we'll be in an infinite loop!
				// SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "StoreInBobCache", "", "", 0, null);
				Global.IncrementRequestCounter(Global.RequestCounter.CacheException);
			}

			//#region Cache Testing code
			//try
			//{
			//    if (Settings.CacheTesting == Settings.CacheTestingOption.On)
			//    {
			//        string key = Caching.Cache.GetBobsCacheKey(Table.TableName, this[Banner.Columns.K].ToString());
			//        Caching.Instances.Main.Store(key, 0);
			//        Caching.Instances.Main.Get(key);
			//    }
			//}
			//catch { }
			//#endregion
		}
		#endregion

		#region GetBobFromPrimaryKeyArray
		internal void GetBobFromPrimaryKeyArray(Q[] PrimaryKeyArray)
		{
			if (PrimaryKeyArray.Length < 1)
				throw new BobException("No primary keys specified!");
			ArrayList primaryKeyColumnArrayList = new ArrayList();
			foreach (ColumnDef c in this.Table)
			{
				if ((c.SqlColumnFlags & SqlColumnFlag.PrimaryKey) != 0)
				{
					bool foundThisPrimaryKeyInInputArray = false;
					foreach (Q q in PrimaryKeyArray)
					{
						if (q.Column.ColumnEnum.Equals(c.ColumnEnum))
							foundThisPrimaryKeyInInputArray = true;
					}
					if (!foundThisPrimaryKeyInInputArray)
						throw new BobException("Column: " + c.ColumnName + " not found in primary key array.");
				}
			}
			foreach (Q q in PrimaryKeyArray)
			{
				if ((Table[q.Column.ColumnEnum].SqlColumnFlags & SqlColumnFlag.PrimaryKey) == 0)
					throw new BobException("Column: " + q.Column.ColumnName + " not marked as primary key in table.");
			}
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{

				conn.Open();

				StringBuilder sb = new StringBuilder();

				sb.Append("SELECT TOP 1 * FROM [");
				sb.Append(Table.TableName);
				sb.Append("] WHERE ");

				bool doneOne = false;
				foreach (Q q in PrimaryKeyArray)
				{
					sb.Append(doneOne ? " AND " : "");
					sb.Append("[");
					sb.Append(Table.TableName);
					sb.Append("].[");
					sb.Append(q.Column.ColumnName);
					sb.Append("] = @");
					sb.Append(q.Column.ColumnName);
					doneOne = true;
				}
				SqlCommand command = new SqlCommand(sb.ToString(), conn);

				if (Vars.SetSqlConnectionTimeout)
					command.CommandTimeout = Vars.SqlConnectionTimeout;

				foreach (Q q in PrimaryKeyArray)
				{
					SqlParameter p = command.Parameters.Add("@" + q.Column.ColumnName, Table[q.Column.ColumnEnum].SqlDbType);
					p.Value = q.Data;
				}

				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = command;
				if (Vars.TraceQueries && HttpContext.Current != null)
					HttpContext.Current.Trace.Write(command.CommandText);

				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);

				adapter.Fill(dataset);
				DataView dv = dataset.Tables[0].DefaultView;


				if (dv.Count == 1)
				{
					Initialise(dv[0].Row);
				}
				else
				{
					throw new BobNotFound("You've requested something that's not in our database. It may have been deleted.");
				}
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
		}
		#endregion
		#region GetBobFromParent(object PrimaryKey, Bob Parent, object Column, TablesEnum Table)
		internal void GetBobFromParent(object PrimaryKey, Bob Parent, object ColumnEnum, TablesEnum TableEnum)
		{
			string tableName = Tables.GetTableName(Tables.GetTableEnum(ColumnEnum)) + "_" + Tables.GetColumnName(ColumnEnum) + "_" + Tables.GetTableName(TableEnum);
			if (Parent.BobSet != null &&
				Parent.DataRow != null &&
				Parent.BobSet.TableNames.Contains(tableName) &&
				Parent.DataRow.Table.Columns.Contains(tableName + "_K") &&
				Parent.DataRow[tableName + "_K"].Equals(PrimaryKey))
			{
				this.ColumnPrefix = tableName;
				this.Initialise(Parent.BobSet, Parent.DataRow);
			}
			else if (
				Parent.BobSet != null &&
				Parent.DataRow != null &&
				Parent.BobSet.TableNames.Contains(Tables.GetTableName(TableEnum)) &&
				Parent.DataRow.Table.Columns.Contains(Tables.GetTableName(TableEnum) + "_K") &&
				Parent.DataRow[Tables.GetTableName(TableEnum) + "_K"].Equals(PrimaryKey))
			{
				this.ColumnPrefix = Tables.GetTableName(TableEnum);
				this.Initialise(Parent.BobSet, Parent.DataRow);
			}
			else
			{
				GetBobFromPrimaryKey(Parent[ColumnEnum]);
			}
		}
		#endregion
		#region GetBobFromParentSimple(Bob Parent, object Column, TablesEnum Table)
		internal void GetBobFromParentSimple(IBob parent, object ColumnEnum, TablesEnum TableEnum)
		{
			var bob = parent.Bob;
			string tableName = Tables.GetTableName(Tables.GetTableEnum(ColumnEnum)) + "_" + Tables.GetColumnName(ColumnEnum) + "_" + Tables.GetTableName(TableEnum);
			if (bob.BobSet != null &&
				bob.DataRow != null &&
				bob.BobSet.TableNames.Contains(tableName))
			{
				this.ColumnPrefix = tableName;
				this.Initialise(bob.BobSet, bob.DataRow);
			}
			else if (
				bob.BobSet != null &&
				bob.DataRow != null &&
				bob.BobSet.TableNames.Contains(Tables.GetTableName(TableEnum)))
			{
				this.ColumnPrefix = Tables.GetTableName(TableEnum);
				this.Initialise(bob.BobSet, bob.DataRow);
			}
		}
		#endregion

		#region Delete()
		public void Delete()
		{
			Delete(null);
		}
		public void Delete(Transaction transaction)
		{
			#region Get SQL string
			StringBuilder sb = new StringBuilder();

			sb.Append("DELETE FROM [");
			sb.Append(Table.TableName);
			sb.Append("] WHERE ");

			if (Table.HasSinglePrimaryKey)
			{
				sb.Append(" [");
				sb.Append(Table[Table.SinglePrimaryKey].ColumnName);
				sb.Append("] = @");
				sb.Append(Table[Table.SinglePrimaryKey].ColumnName);
			}
			else
			{
				bool doneOne = false;
				foreach (ColumnDef c in Table)
				{
					if ((c.SqlColumnFlags & SqlColumnFlag.PrimaryKey) != 0)
					{
						if (doneOne)
							sb.Append(" AND ");
						sb.Append(" [");
						sb.Append(c.ColumnName);
						sb.Append("] = @");
						sb.Append(c.ColumnName);

						doneOne = true;
					}
				}
			}
			#endregion

			#region Create connection
			SqlConnection conn;
			if (transaction == null)
				conn = new SqlConnection(Vars.DefaultConnectionString);
			else
				conn = transaction.SqlConnection;
			#endregion
			conn.InfoMessage +=new SqlInfoMessageEventHandler(conn_InfoMessage);
			try
			{
				#region Create command
				SqlCommand command = new SqlCommand(sb.ToString(), conn);
				if (transaction != null)
					command.Transaction = transaction.SqlTransaction;

				if (Vars.SetSqlConnectionTimeout)
					command.CommandTimeout = Vars.SqlConnectionTimeout;
				#endregion

				#region Add parameters
				if (Table.HasSinglePrimaryKey)
				{
					SqlParameter par = command.Parameters.Add("@" + Table[Table.SinglePrimaryKey].ColumnName, Table[Table.SinglePrimaryKey].SqlDbType);
					par.Value = this.ValueAtInit(Table.SinglePrimaryKey);
				}
				else
				{
					foreach (ColumnDef c in Table)
					{
						if ((c.SqlColumnFlags & SqlColumnFlag.PrimaryKey) != 0)
						{
							SqlParameter par = command.Parameters.Add("@" + c.ColumnName, c.SqlDbType);
							par.Value = this.ValueAtInit(c.ColumnEnum);
						}
					}
				}
				#endregion

				if (!conn.State.Equals(System.Data.ConnectionState.Open))
					conn.Open();

				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Delete);

				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				HandleError(ex);
			}
			finally
			{
				if (transaction == null)
				{
					conn.Close();
					conn.Dispose();
				}
			}

			//ClearFromBobCache();
		}
		#endregion

		#region UpdateFailsIfChanged
		public bool OptimisticLocking
		{
			get
			{
				return updateFailsIfChanged;
			}
			set
			{
				updateFailsIfChanged = value;
			}
		}
		bool updateFailsIfChanged = false;
		#endregion

		#region Update
		public virtual int Update()
		{
			return Update(null);
		}
		public virtual int Update(Transaction transaction)
		{
			if (DbRecordExists && this.BobDirty)
			{
				#region Do an UPDATE query

				#region Create SQL string

				StringBuilder sb = new StringBuilder();

				sb.Append("UPDATE [");
				sb.Append(Table.TableName);
				sb.Append("] SET ");

				#region Update list
				bool doneOne = false;
				foreach (ColumnDef c in Table)
				{
					if (IncludeInUpdateQuery(c.ColumnEnum))
					{
						if (doneOne)
							sb.Append(",");
						sb.Append(" [");
						sb.Append(c.ColumnName);
						sb.Append("] = @");
						sb.Append(c.ColumnName);

						doneOne = true;
					}
				}
				#endregion

				if (!doneOne)
					return 0;

				#region Where part

				sb.Append(" WHERE ");

				if (Table.HasSinglePrimaryKey)
				{
					sb.Append("[");
					sb.Append(Table[Table.SinglePrimaryKey].ColumnName);
					sb.Append("] = @");
					sb.Append(Table[Table.SinglePrimaryKey].ColumnName);
					sb.Append("_KEY");

				}
				else
				{
					doneOne = false;
					foreach (ColumnDef c in Table)
					{
						if ((c.SqlColumnFlags & SqlColumnFlag.PrimaryKey) != 0)
						{
							if (doneOne)
								sb.Append(" AND ");
							sb.Append(" [");
							sb.Append(c.ColumnName);
							sb.Append("] = @");
							sb.Append(c.ColumnName);
							sb.Append("_KEY");
							doneOne = true;
						}
					}
					if (!doneOne)
					{
						throw new BobException("Can't update - table has no primary keys.");
					}
				}
				#endregion


				#region UpdateFailsIfChanged
				if (OptimisticLocking)
				{
					foreach (ColumnDef c in Table)
					{
						if (IncludeInUpdateQuery(c.ColumnEnum))
						{
							sb.Append(" AND [");
							sb.Append(c.ColumnName);
							sb.Append("]");
							if (this.ObAtInit[(int)c.ColumnEnum].Equals(System.DBNull.Value))
							{
								sb.Append(" IS NULL");
							}
							else
							{
								sb.Append(" = @");
								sb.Append(c.ColumnName);
								sb.Append("_PREV");
							}
						}
					}
				}
				#endregion

				#endregion

				#region Create connection
				SqlConnection conn;
				if (transaction == null)
					conn = new SqlConnection(Vars.DefaultConnectionString);
				else
					conn = transaction.SqlConnection;
				#endregion

				conn.InfoMessage += new SqlInfoMessageEventHandler(conn_InfoMessage);
				int i = 0;

				try
				{
					#region Create command

					SqlCommand command = new SqlCommand(sb.ToString(), conn);

					if (transaction != null)
						command.Transaction = transaction.SqlTransaction;

					if (Vars.SetSqlConnectionTimeout)
						command.CommandTimeout = Vars.SqlConnectionTimeout;

					#endregion

					#region Add parameters
					foreach (ColumnDef c in Table)
					{
						if (IncludeInUpdateQuery(c.ColumnEnum))
						{
							SqlParameter par = command.Parameters.Add("@" + c.ColumnName, c.SqlDbType);
							if (c.SqlDbType.Equals(SqlDbType.DateTime) && (this[c.ColumnEnum] == null || this[c.ColumnEnum].Equals(DateTime.MinValue)))
								par.Value = System.DBNull.Value;
							else
								par.Value = this[c.ColumnEnum] ?? DBNull.Value;

							if (OptimisticLocking && !this.ObAtInit[(int)c.ColumnEnum].Equals(System.DBNull.Value))
							{
								SqlParameter parPrev = command.Parameters.Add("@" + c.ColumnName + "_PREV", c.SqlDbType);
								parPrev.Value = this.ObAtInit[(int)c.ColumnEnum];
							}
						}
						if (!Table.HasSinglePrimaryKey && (c.SqlColumnFlags & SqlColumnFlag.PrimaryKey) != 0)
						{
							SqlParameter par = command.Parameters.Add("@" + c.ColumnName + "_KEY", c.SqlDbType);
							par.Value = this.ValueAtInit(c.ColumnEnum);
						}

					}
					if (Table.HasSinglePrimaryKey)
					{
						SqlParameter par = command.Parameters.Add("@" + Table[Table.SinglePrimaryKey].ColumnName + "_KEY", Table[Table.SinglePrimaryKey].SqlDbType);
						par.Value = this.ValueAtInit(Table.SinglePrimaryKey);
					}
					#endregion

					if (!conn.State.Equals(System.Data.ConnectionState.Open))
						conn.Open();

					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Update);

					i = command.ExecuteNonQuery();

				}
				catch (Exception ex)
				{
					HandleError(ex);
				}
				finally
				{
					if (transaction == null)
					{
						conn.Close();
						conn.Dispose();
					}
				}
				//if (Vars.CacheEnabled)// && (BobSet == null || (BobSet.Query != null && BobSet.Query.Columns!=null && BobSet.Query.Columns.Columns.Count == 0)))
				//    ClearFromBobCache();

				return i;
				#endregion
			}
			else if (!DbRecordExists)
			{
				#region Do an INSERT query

				#region Create SQL string

				StringBuilder sb = new StringBuilder();
				bool extractFromIdentity = false;
				object identityColumnEnum = null;
				sb.Append("INSERT INTO [");
				sb.Append(Table.TableName);
				sb.Append("] ( ");

				#region Columns list

				bool doneOne = false;
				foreach (ColumnDef c in Table)
				{
					if ((c.SqlColumnFlags & SqlColumnFlag.AutoNumber) != 0)
					{
						extractFromIdentity = true;
						identityColumnEnum = c.ColumnEnum;
					}

					if (IncludeInInsertQuery(c.ColumnEnum))
					{
						if (doneOne)
							sb.Append(",");
						sb.Append(" [");
						sb.Append(c.ColumnName);
						sb.Append("] ");
						doneOne = true;
					}
				}

				if (!doneOne)
					return 0;

				#endregion

				if (extractFromIdentity)
					sb.Insert(0, "SET NOCOUNT ON; ");

				#region Values list

				sb.Append(" ) VALUES ( ");
				doneOne = false;

				foreach (ColumnDef c in Table)
				{
					if (IncludeInInsertQuery(c.ColumnEnum))
					{
						if (doneOne)
							sb.Append(",");
						sb.Append(" @");
						sb.Append(c.ColumnName);
						doneOne = true;
					}
				}
				sb.Append(" ) ");

				#endregion

				if (extractFromIdentity)
					sb.Append("; SELECT @@IDENTITY AS new_id; SET NOCOUNT ON;");

				#endregion

				#region Create connection
				SqlConnection conn;
				if (transaction == null)
					conn = new SqlConnection(Vars.DefaultConnectionString);
				else
					conn = transaction.SqlConnection;
				#endregion
				conn.InfoMessage +=new SqlInfoMessageEventHandler(conn_InfoMessage);
				int i = 0;

				try
				{
					#region Create command
					SqlCommand command = new SqlCommand(sb.ToString(), conn);
					if (transaction != null)
						command.Transaction = transaction.SqlTransaction;

					if (Vars.SetSqlConnectionTimeout)
						command.CommandTimeout = Vars.SqlConnectionTimeout;
					#endregion

					#region Add parameters
					foreach (ColumnDef c in Table)
					{
						if (IncludeInInsertQuery(c.ColumnEnum))
						{
							SqlParameter par = command.Parameters.Add("@" + c.ColumnName, c.SqlDbType);
							if (c.SqlDbType.Equals(SqlDbType.DateTime) && (this[c.ColumnEnum] == null || this[c.ColumnEnum].Equals(DateTime.MinValue)))
								par.Value = System.DBNull.Value;
							//	else if (this.IsNull(c.ColumnEnum))
							//		par.Value = System.DBNull.Value;
							else
								par.Value = this[c.ColumnEnum] ?? DBNull.Value;
						}
					}
					object identity;
					#endregion

					if (!conn.State.Equals(System.Data.ConnectionState.Open))
						conn.Open();

					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Insert);

					if (extractFromIdentity)
					{
						identity = command.ExecuteScalar();
						this[identityColumnEnum] = int.Parse(identity.ToString());// peskey @@IDENTITY returns a decimal - won't convert to int
						i = 1;
					}
					else
					{
						i = command.ExecuteNonQuery();
					}
				}
				catch(Exception ex)
				{
					HandleError(ex);
				}
				finally
				{
					if (transaction == null)
					{
						conn.Close();
						conn.Dispose();
					}
				}

				ReInit();
				DbRecordExists = true;

			//	if (Vars.CacheEnabled)
			//		StoreInBobCache();
				
				return i;
				#endregion
			}
			else
				return 0;
		}

		private void HandleError(Exception ex)
		{
			if (ex.Message.Contains("context transaction which was active"))
			{
				for (int i = 0; i < 5; i++)
				{
					System.Threading.Thread.Sleep(100);
					if (infoMessage != null) break;
				}
				throw new Exception(infoMessage, ex);
			}
			else
			{
				throw ex;
			}
		}
		string infoMessage;
		void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("from pipe");
			sb.AppendLine(e.Message);
			foreach (SqlError error in e.Errors)
			{
				sb.AppendLine(error.Message);
			}
			infoMessage = sb.ToString();
		}
		#endregion

		#region UpdateAsync
		delegate int updateAsyncDelegate();
		public void UpdateAsync()
		{
			System.Threading.Thread thread = Utilities.GetSafeThread(() => this.Update());
			thread.Start();
		}
		#endregion

		

		string GetCacheKey()
		{
			return Caching.Cache.GetBobsCacheKey(Table.TableName, this[Table.SinglePrimaryKey].ToString());
		}


		//Bob IBob.Bob
		//{
		//    get { return this; }
		//}
	}

	#region Abstract Class DataHolder
	/// <summary>
	/// BobDataHolder is an abstract base class for stubs of Bobs so that they can be stored in the page ViewState
	/// </summary>
	[Serializable]
	public abstract class BobDataHolder
	{
		#region Variables
		private DataHolderState state = DataHolderState.Unchanged;
		#endregion

		#region Properties
		public DataHolderState State
		{
			get { return this.state; }
			set { this.state = value; }
		}
		#endregion

		//#region DataHolderState
		//public enum DataHolderState
		//{
		//    Unchanged = 0,
		//    Modified = 1,
		//    Added = 2,
		//    Deleted = 3

		//}
		//#endregion

		#region MarkAsDeleted
		public void MarkAsDeleted()
		{
			this.state = DataHolderState.Deleted;
		}
		#endregion
	}
	#endregion
	class CachedBobVersionDifferentFromCurrentException : Exception{

	}
}

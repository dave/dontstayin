using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace Bobs
{
	#region Column
	public class Column
	{
		public Column(object ColumnEnum)
		{
			this.ColumnEnum = ColumnEnum;
		}
		public Column(object ParentColumnEnum, object ColumnEnum)
		{
			this.ColumnEnum = ColumnEnum;
			this.ParentColumnEnum = ParentColumnEnum;
		}
		public object ColumnEnum{get;set;}
		#region ParentColumnEnum
		public object ParentColumnEnum
		{
			get
			{
				return parentColumnEnum;
			}
			set
			{
				HasParentColumnEnum = true;
				parentColumnEnum = value;
			}
		}
		public bool HasParentColumnEnum;
		private object parentColumnEnum;
		#endregion
		
		#region TableEnum
		/// <summary>
		/// This is the current table enum (not the parent table)
		/// </summary>
		public TablesEnum TableEnum
		{
			get
			{
				if (!gotTableEnum)
				{
					tableEnum = Tables.GetTableEnum(ColumnEnum);
					gotTableEnum = true;
				}
				return tableEnum;
			}
		}
		private TablesEnum tableEnum;
		private bool gotTableEnum;
		#endregion

		#region TableName
		/// <summary>
		/// This is the current table name
		/// </summary>
		public string TableName
		{
			get
			{
				if (tableName==null)
				{
					tableName = Tables.GetTableName(TableEnum);
				}
				return tableName;
			}
		}
		private string tableName;
		#endregion
		
		#region ParentTableEnum
		/// <summary>
		/// This is the enum of the parent table
		/// </summary>
		public TablesEnum ParentTableEnum
		{
			get
			{
				if (!gotParentTableEnum)
				{
					parentTableEnum = Tables.GetTableEnum(ParentColumnEnum);
					gotParentTableEnum = true;
				}
				return parentTableEnum;
			}
		}
		private TablesEnum parentTableEnum;
		private bool gotParentTableEnum;
		#endregion

		#region ParentTableName
		/// <summary>
		/// This is the parent table name
		/// </summary>
		public string ParentTableName
		{
			get
			{
				if (parentTableName==null)
				{
					parentTableName = Tables.GetTableName(ParentTableEnum);
				}
				return parentTableName;
			}
		}
		private string parentTableName;
		#endregion

		#region ParentColumnName
		/// <summary>
		/// This is the parent column name
		/// </summary>
		public string ParentColumnName
		{
			get
			{
				if (parentColumnName == null)
				{
					parentColumnName = Tables.GetColumnName(ParentColumnEnum);
				}
				return parentColumnName;
			}
		}
		private string parentColumnName;
		#endregion

		#region ColumnName
		/// <summary>
		/// This is the current column name
		/// </summary>
		public string ColumnName
		{
			get
			{
				if (columnName == null)
				{
					columnName = Tables.GetColumnName(ColumnEnum);
				}
				return columnName;
			}
		}
		private string columnName;
		#endregion

		#region ExternalSqlTableName
		/// <summary>
		/// This is the full name of the table including the modifier for the parent 
		/// column - this is how the (joined) table instance is referenced in the sql query
		/// </summary>
		public string ExternalSqlTableName
		{
			get
			{
				if (externalSqlTableName == null)
				{
					if (HasParentColumnEnum)
					{
						externalSqlTableName = ParentTableName + "_" + ParentColumnName + "_" + TableName;
					}
					else
					{
						externalSqlTableName = TableName;
					}
				}
				return externalSqlTableName;
			}
		}
		private string externalSqlTableName;
		#endregion

		#region InternalSqlName
		/// <summary>
		/// This is the name of the column when referenced inside the SQL statement.
		/// </summary>
		public string InternalSqlName
		{
			get
			{
				if (internalSqlName == null)
				{
					internalSqlName = "[" + ExternalSqlTableName + "].[" + ColumnName + "]";
				}
				return internalSqlName;
			}
		}
		private string internalSqlName;
		#endregion
		#region ExternalSqlColumnName
		/// <summary>
		/// This is the full name of the column in the returned data set, including 
		/// unique table instance prefix.
		/// </summary>
		public string ExternalSqlColumnName
		{
			get
			{
				if (externalSqlColumnName == null)
				{
					externalSqlColumnName = ExternalSqlTableName + "_" + ColumnName;
				}
				return externalSqlColumnName;
			}
		}
		private string externalSqlColumnName;
		#endregion
	}
	#endregion
	#region ColumnSet
	public class JoinedColumnSet : ColumnSet
	{
		public JoinedColumnSet(object parentColumn, params object[] columnsIn)
		{
			foreach (object o in columnsIn)
			{
				if (o is ColumnSet)
				{
					foreach (Column c in ((ColumnSet)o).Columns)
						Columns.Add(new Column(parentColumn, c.ColumnEnum));
				}
				else
				{
					if (o is Column)
						Columns.Add(new Column(parentColumn, ((Column)o).ColumnEnum));
					else
						Columns.Add(new Column(parentColumn, o));
				}
			}
		}
	}
	public class ColumnSet
	{
		public ColumnSet(params object[] columnsIn)
		{
			fillFromArray(columnsIn);
		}
		void fillFromArray(object[] columnsIn)
		{
			foreach (object o in columnsIn)
			{
				if (o is object[])
				{
					fillFromArray((object[])o);
				}
				else if (o is ColumnSet)
				{
					Columns.AddRange(((ColumnSet)o).Columns);
				}
				else
				{
					if (o is Column)
						Columns.Add((Column)o);
					else
						Columns.Add(new Column(o));
				}
			}
		}
		public List<Column> Columns
		{
			get
			{
				if (columns == null)
					columns = new List<Column>();
				return columns;
			}
			set
			{
				columns = value;
			}
		}
		List<Column> columns;
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			this.BuildString(sb);
			return sb.ToString();
		}
		public void BuildString(StringBuilder sb)
		{
			foreach (Column c in Columns)
			{
				sb.Append(sb.Length == 0 ? "" : ", ");
				sb.Append(c.InternalSqlName);
				sb.Append(" AS ");
				sb.Append(c.ExternalSqlColumnName);
			}
		}
	}
	#endregion

	#region Query
	public class Query : Caching.ICacheKeyProvider 
	{
		#region Paging
		public PagingDescriptor Paging
		{
			get
			{
				if (paging == null)
					paging = new PagingDescriptor();
				return paging;
			}
			set
			{
				paging = value;
			}
		}
		private PagingDescriptor paging;
		#endregion
		#region PagingDescriptor
		public class PagingDescriptor
		{
			#region RecordsPerPage
			public int RecordsPerPage
			{
				get
				{
					return recordsPerPage;
				}
				set
				{
					recordsPerPage = value;
				}
			}
			private int recordsPerPage;
			#endregion
			#region IsEnabled
			public bool IsEnabled
			{
				get
				{
					return RecordsPerPage > 0;
				}
			}
			#endregion
			#region RequestedPage
			public int RequestedPage
			{
				get
				{
					return requestedPageIndex + 1;
				}
				set
				{
					if (value < 1)
						requestedPageIndex = 0;
					else
						requestedPageIndex = value - 1;
				}
			}
			#endregion
			#region RequestedPageIndex
			public int RequestedPageIndex
			{
				get
				{
					return requestedPageIndex;
				}
				set
				{
					if (value < 0)
						requestedPageIndex = 0;
					else
						requestedPageIndex = value;
				}
			}
			private int requestedPageIndex;
			#endregion
			#region ForceUseRowNumber
			public bool ForceUseRowNumber
			{
				get
				{
					return forceUseRowNumber;
				}
				set
				{
					forceUseRowNumber = value;
				}
			}
			bool forceUseRowNumber = false;
			#endregion
		}
		#endregion
		
		#region Sql
		public SqlGenerator Sql
		{
			get
			{
				if (sql == null)
					sql = new SqlGenerator(this);
				return sql;
			}
			set
			{
				sql = value;
			}
		}
		SqlGenerator sql;
		#endregion
		#region SqlGenerator
		public class SqlGenerator
		{
			public SqlGenerator(Query query)
			{
				this.Query = query;
			}

			#region ToString()
			public override string ToString()
			{
				string sqlString = "";
				if (Query.OverideSql.Length > 0)
				{
					#region Text override
					sqlString = Query.OverideSql;
					#endregion
				}
				else if (Query.ReturnCountOnly)
				{
					#region COUNT
					if (Query.Distinct)
					{
						sqlString = "SELECT COUNT(DISTINCT " + DistinctColumn + ") " + From + Where;
					}
					else
					{
						sqlString = "SELECT COUNT(*) " + From + Where;
					}
					#endregion
				}
				else if (Query.Paging.IsEnabled)
				{
					#region Paging
					int firstRow = (Query.Paging.RequestedPageIndex * Query.Paging.RecordsPerPage) + 1;
					int lastRow = ((Query.Paging.RequestedPageIndex + 1) * Query.Paging.RecordsPerPage) + 1;
					if (Query.Paging.RequestedPage <= 5 && !Query.Paging.ForceUseRowNumber)
					{
						if (Query.Distinct)
						{
							sqlString = "SELECT TOP " + lastRow + " " + Columns + DistinctFrom + " WHERE " + DistinctColumn + " IN (SELECT DISTINCT " + DistinctColumn + From + Where + ") " + OrderBy;
						}
						else
							sqlString = "SELECT TOP " + lastRow + " " + Columns + From + Where + GroupBy + Having + OrderBy;
					}
					else
					{
						if (Query.Distinct)
						{
							sqlString = @";WITH Tmp_Table AS (
											SELECT ROW_NUMBER() OVER (" + OrderBy + @") AS Row_Number, " + DistinctColumn + @" AS Primarary_Key FROM (
												SELECT DISTINCT " + DistinctColumn + ", " + OrderByColumns + From + Where + @"
											) AS [" + Query.MainTable.TableName + @"]
										) 
										SELECT " + Columns + DistinctFrom + @" WHERE EXISTS (SELECT * FROM Tmp_Table WHERE Primarary_Key = " + PagingColumn + @" AND Row_Number BETWEEN " + firstRow + " AND " + lastRow + ")" + OrderBy + ";";
						}
						else if (Query.MainTable.HasSinglePrimaryKey)
						{
						
							sqlString = @";WITH Tmp_Table AS (SELECT ROW_NUMBER() OVER (" + OrderBy + @") AS Row_Number, " + PagingColumn + @" AS Primarary_Key " + From + Where + @") 
										SELECT " + Columns + From + Where + @" AND EXISTS (SELECT * FROM Tmp_Table WHERE Primarary_Key = " + PagingColumn + @" AND Row_Number BETWEEN " + firstRow + " AND " + lastRow + ")" + OrderBy + ";";
						}
						else
						{
							sqlString = @";WITH Tmp_Table AS (SELECT ROW_NUMBER() OVER (" + OrderBy + @") AS Row_Number, " + Columns + From + Where + @") 
										SELECT * FROM Tmp_Table WHERE Row_Number BETWEEN " + firstRow + " AND " + lastRow + ";";
						}
					}
					#endregion
				}
				else if (Query.Distinct)
				{
					#region DISTINCT
					//I've tried to remove the IN query and replace with the faster EXISTS query. 
					//Not sure how they work, because it didn't work. Will have to do more reading...
					sqlString = "SELECT " + Top + Columns + DistinctFrom + " WHERE " + DistinctColumn + " IN (SELECT DISTINCT " + DistinctColumn + From + Where + ") " + OrderBy;
					#endregion
				}
				else
				{
					#region SELECT
					sqlString = "SELECT " + Top + Columns + From + Where + GroupBy + Having + OrderBy;
					#endregion
				}
				
				return sqlString;
			}
			#endregion

			public Query Query{get;set;}

			#region Where
			public string Where
			{
				get
				{
					if (where == null || where.Length==0)
					{
						where = " ";
						if (Query.QueryCondition != null)
						{
							string s = Query.QueryCondition.ToString(ref Parameters, ref RankContribution);
							if (s.Length > 0)
								where = " WHERE " + s;
						}
					}
					return where;
				}
			}
			string where;
			#endregion
			#region OrderBy
			public string OrderBy
			{
				get
				{
					if (orderBy == null || orderBy.Length==0)
					{
						orderBy = " ";
						if (Query.OrderBy != null)
						{
							orderBy = Query.OrderBy.ToString();
						}
					}
					return orderBy;
				}
			}
			string orderBy;
			#endregion
			#region Top
			public string Top
			{
				get
				{
					if (top == null || top.Length==0)
					{
						top = " ";
						if (Query.TopRecords != -1)
						{
							top = " TOP " + Query.TopRecords.ToString() + " ";
						}
					}
					return top;
				}
			}
			string top;
			#endregion
			#region From
			public string From
			{
				get
				{
					if (from == null || from.Length == 0)
					{
						from = " FROM " + Query.TableElement.ToString(ref Parameters, ref RankContribution, Query.NoLock);
					}
					return from;
				}
			}
			string from;
			#endregion
			#region Columns
			public string Columns
			{
				get
				{
					if (columns == null || columns.Length==0)
					{
						columns = " ";
						if (Query.Columns == null)
							columns = " * ";
						else
							columns = Query.Columns.ToString();

						if (Query.ExtraSelectElements != null && Query.ExtraSelectElements.Count > 0)
						{
							foreach (string c in Query.ExtraSelectElements.Keys)
							{
								if (columns.Length > 0)
									columns += ", ";
								columns += Query.ExtraSelectElements[c] + " AS " + c;
							}
						}
					}
					return columns;
				}
			}
			string columns;
			#endregion
			#region GroupBy
			public string GroupBy
			{
				get
				{
					if (groupBy == null || groupBy.Length==0)
					{
						groupBy = " ";
						if (Query.GroupBy != null)
						{
							groupBy = Query.GroupBy.ToString();
						}
					}
					return groupBy;
				}
				set
				{
					groupBy = value;
				}
			}
			string groupBy;
			#endregion
			#region Having
			public string Having
			{
				get
				{
					if (having == null || having.Length==0)
					{
						having = " ";
						if (Query.Having != null)
						{
							string s = Query.Having.ToString(ref Parameters, ref RankContribution);
							if (s.Length > 0)
								having = " HAVING " + s;
						}
					}
					return having;
				}
			}
			string having;
			#endregion
			#region DistinctFrom
			public string DistinctFrom
			{
				get
				{
					if (distinctFrom == null || distinctFrom.Length==0)
					{
						if (Query.DataTableElement == null)
							distinctFrom = " FROM [" + Tables.GetTableName(Tables.GetTableEnum(Query.DistinctColumn)) + "]" + (Query.NoLock ? " WITH (NOLOCK)" : "");
						else
							distinctFrom = " FROM " + Query.DataTableElement.ToString(ref Parameters, ref RankContribution, Query.NoLock);
					}
					return distinctFrom;
				}
			}
			string distinctFrom;
			#endregion
			#region DistinctColumn
			public string DistinctColumn
			{
				get
				{
					if (distinctColumn == null || distinctColumn.Length==0)
					{
						distinctColumn = "[" + Tables.GetTableName(Tables.GetTableEnum(Query.DistinctColumn)) + "].[" + Tables.GetColumnName(Query.DistinctColumn) + "]";
					}
					return distinctColumn;
				}
			}
			string distinctColumn;
			#endregion
			#region OrderByColumns
			string OrderByColumns
			{
				get
				{
					if (orderByColumns == null || orderByColumns.Length==0)
					{
						orderByColumns = " ";
						foreach (Column c in Query.OrderBy.AllColumns)
						{
							orderByColumns += (orderByColumns.Length > 1 ? ", " : "") + c.InternalSqlName;
						}
					}
					return orderByColumns;
				}
			}
			string orderByColumns;
			#endregion

			#region PagingColumn
			public string PagingColumn
			{
				get
				{
					if (pagingColumn == null || pagingColumn.Length==0)
					{
						if (Query.MainTable.HasSinglePrimaryKey)
							pagingColumn = "[" + Query.MainTable.TableName + "].[" + Query.MainTable[Query.MainTable.SinglePrimaryKey].ColumnName + "]";
						else
							pagingColumn = " ";
					}
					return pagingColumn;
				}
				set
				{
					pagingColumn = value;
				}
			}
			string pagingColumn;
			#endregion
			#region PagingFrom (removed)
			//public string PagingFrom
			//{
			//    get
			//    {
			//        if (pagingFrom == null)
			//        {
			//            if (Query.DataTableElement == null)
			//                pagingFrom = " FROM [" + Query.MainTable.TableName + "]";
			//            else
			//                pagingFrom = " FROM " + Query.DataTableElement.ToString(ref Parameters, ref RankContribution, Query.NoLock);
			//        }
			//        return pagingFrom;
			//    }
			//}
			//string pagingFrom;
			#endregion

			#region Parameters
			public Dictionary<string, SqlParameter> Parameters = new Dictionary<string, SqlParameter>();
			#endregion
			#region RankContribution
			public string RankContribution = "";
			#endregion

		}
		#endregion

		private List<SqlParameter> extraParameters = new List<SqlParameter>();
		public List<SqlParameter> ExtraParameters { get { return extraParameters; } }
		public TableDef MainTable { get; set; }

		public bool NoLock { get { return noLock; } set { noLock = value; } }
		private bool noLock = true;

		public ColumnSet Columns { get;set;}

		public Hashtable ExtraSelectElements { get { return extraSelectElements; } set { extraSelectElements = value; } }
		private Hashtable extraSelectElements = new Hashtable();

		public TableElement TableElement { get; set; }
		public Q QueryCondition { get; set; }
		public GroupBy GroupBy { get; set; }
		public Q Having { get; set; }
		public OrderBy OrderBy { get; set; }
		public int TopRecords { get; set; }
		public bool ReturnCountOnly { get; set; }
		public int FillStartingAt { get; set; }
		public int FillMaxRecords { get; set; }
		public string OverideSql { get; set; }
		public TimeSpan? CacheDuration { get; set; }

		public bool Distinct { get; set; }
		public object DistinctColumn { get; set; }
		#region DataTableElement
		/// <summary>
		/// This is used in a Paging and Destinct scenario, and provides the Join for the data out. TableElement provides just the join for the logic.
		/// </summary>
		public TableElement DataTableElement { get; set; }
		#endregion
		public Query(params Q[] queryConditions) : this(new And(queryConditions)) { }
		public Query(Q QueryCondition):
			this(null, null, QueryCondition, null, -1, false, 0, -1) { }

		public Query(Q QueryCondition, OrderBy OrderBy):
			this(null, null, QueryCondition, OrderBy, -1, false, 0, -1) { }

		public Query(Q QueryCondition, OrderBy OrderBy, int TopRecords):
			this(null, null, QueryCondition, OrderBy, TopRecords, false, 0, -1) { }

		public Query(TableElement TableElement, Q QueryCondition, OrderBy OrderBy, int TopRecords):
			this(null, TableElement, QueryCondition, OrderBy, TopRecords, false, 0, -1) { }

		public Query(Q QueryCondition, bool CountOnly):
			this(null, null, QueryCondition, null, 0, true, 0, -1) { }

		public Query(bool OverrideSqlFlag, string OverideSql)
		{
			this.TableElement = null;
			this.QueryCondition = null;
			this.OrderBy = null;
			this.TopRecords = -1;
			this.ReturnCountOnly = false;
			this.FillStartingAt = 0;
			this.FillMaxRecords = 0;
			this.OverideSql = OverideSql;
		}

		public Query()
		{
			this.TableElement = null;
			this.QueryCondition = null;
			this.OrderBy = null;
			this.TopRecords = -1;
			this.ReturnCountOnly = false;
			this.FillStartingAt = 0;
			this.FillMaxRecords = 0;
			this.OverideSql = "";
		}

		public Query(object QueryColumnRemoved, TableElement TableElement, Q QueryCondition, OrderBy OrderBy, int TopRecords, bool ReturnCountOnly, int FillStartingAt, int FillMaxRecords)
		{
			this.TableElement = TableElement;
			this.QueryCondition = QueryCondition;
			this.OrderBy = OrderBy;
			this.TopRecords = TopRecords;
			this.ReturnCountOnly = ReturnCountOnly;
			this.FillStartingAt = FillStartingAt;
			if (FillMaxRecords == -1)
				this.FillMaxRecords = 0;
			else
				this.FillMaxRecords = FillMaxRecords;
			this.OverideSql = "";
		}

		#region ICacheKeyProvider Members

		public string GetCacheKey()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Sql.ToString());
			sb.Append("(");
			foreach (string c in Sql.Parameters.Keys)
			{
				sb.Append("@");
				sb.Append(c);
				sb.Append("(");
				sb.Append(Sql.Parameters[c].Value.ToString());
				sb.Append("),");
				
			}
			sb.Append(")");
			return sb.ToString();

		}

		#endregion
	}
	#endregion

	#region TableElement
	public class TableElement
	{

		#region GetJoinTypeString
		public static string GetJoinTypeString(QueryJoinType type)
		{
			switch (type)
			{
				case QueryJoinType.Inner: return "INNER";
				case QueryJoinType.Outer: return "OUTER";
				case QueryJoinType.Left: return "LEFT";
				case QueryJoinType.Right: return "RIGHT";
				default: return "";
			}
		}
		#endregion

		public TablesEnum TableEnum { get; set; }
		#region TableName
		/// <summary>
		/// This is the current table name
		/// </summary>
		public string TableName
		{
			get
			{
				if (tableName == null)
				{
					tableName = Tables.GetTableName(TableEnum);
				}
				return tableName;
			}
		}
		private string tableName;
		#endregion
		#region ParentColumnEnum
		public object ParentColumnEnum
		{
			get
			{
				return parentColumnEnum;
			}
			set
			{
				HasParentColumnEnum = true;
				parentColumnEnum = value;
			}
		}
		public bool HasParentColumnEnum;
		private object parentColumnEnum;
		#endregion
		#region ParentColumnName
		/// <summary>
		/// This is the parent column name
		/// </summary>
		public string ParentColumnName
		{
			get
			{
				if (parentColumnName == null)
				{
					parentColumnName = Tables.GetColumnName(ParentColumnEnum);
				}
				return parentColumnName;
			}
		}
		private string parentColumnName;
		#endregion
		#region ParentTableEnum
		/// <summary>
		/// This is the enum of the parent table
		/// </summary>
		public TablesEnum ParentTableEnum
		{
			get
			{
				if (!gotParentTableEnum)
				{
					parentTableEnum = Tables.GetTableEnum(ParentColumnEnum);
					gotParentTableEnum = true;
				}
				return parentTableEnum;
			}
		}
		private TablesEnum parentTableEnum;
		private bool gotParentTableEnum;
		#endregion
		#region ParentTableName
		/// <summary>
		/// This is the parent table name
		/// </summary>
		public string ParentTableName
		{
			get
			{
				if (parentTableName == null)
				{
					parentTableName = Tables.GetTableName(ParentTableEnum);
				}
				return parentTableName;
			}
		}
		private string parentTableName;
		#endregion

		#region ExternalSqlTableName
		public string ExternalSqlTableName
		{
			get
			{
				if (externalSqlTableName == null)
				{
					if (HasParentColumnEnum)
					{
						externalSqlTableName = ParentTableName + "_" + ParentColumnName + "_" + TableName;
					}
					else
					{
						externalSqlTableName = TableName;
					}
				}
				return externalSqlTableName;
			}
		}
		private string externalSqlTableName;
		#endregion

		#region Initialisers
		protected void Init(object ob)
		{
			if (ob is TableElement)
			{
				this.TableEnum = ((TableElement)ob).TableEnum;
				if (((TableElement)ob).HasParentColumnEnum)
					this.ParentColumnEnum = ((TableElement)ob).ParentColumnEnum;
			}
			else if (ob is TablesEnum)
			{
				this.TableEnum = (TablesEnum)ob;
			}
			else if (ob is Column)
			{
				this.TableEnum = ((Column)ob).TableEnum;
				if (((Column)ob).HasParentColumnEnum)
					this.ParentColumnEnum = ((Column)ob).ParentColumnEnum;
			}
			else if (ob is TableDef)
			{
				this.TableEnum = ((TableDef)ob).TableEnum;
			}
			else
			{
				this.TableEnum = Tables.GetTableEnum(ob);
			}
		}
		public TableElement(TablesEnum tableEnum)
		{
			Init(tableEnum);
		}
		public TableElement(Column column)
		{
			Init(column);
		}
		public TableElement(object columnEnum)
		{
			Init(columnEnum);
		}
		public TableElement(TableDef Table)
		{
			Init(Table);
			this.TableEnum = Table.TableEnum;
		}
		public TableElement()
		{
		}
		#endregion
		#region BuildString
		public virtual void BuildString(StringBuilder sb, bool NoLock)
		{
			if (HasOverrideString)
			{
				sb.Append(OverrideString);
			}
			else
			{
				sb.Append("[");
				sb.Append(TableName);
				sb.Append("]");
				if (HasParentColumnEnum)
				{
					sb.Append(" AS [");
					sb.Append(ExternalSqlTableName);
					sb.Append("]");
				}
				if (NoLock)
					sb.Append(" WITH (NOLOCK)");
			}
		}

		public virtual void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
		{
			this.BuildString(sb, NoLock);
		}
		#endregion
		#region ToString
		public virtual string ToString(bool NoLock)
		{
			if (HasOverrideString)
			{
				return OverrideString;
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				this.BuildString(sb, NoLock);
				return sb.ToString();
			}
		}
		public virtual string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
		{
			return this.ToString(NoLock);
		}
		#endregion
		#region GetTableNames
		public virtual void GetTableNames(ArrayList al)
		{
			if (!HasOverrideString)
			{
				if (!al.Contains(this.ExternalSqlTableName))
					al.Add(this.ExternalSqlTableName);
			}
		}
		#endregion
		#region HasOverrideString
		public bool HasOverrideString
		{
			get
			{
				return hasOverrideString;
			}
			set
			{
				hasOverrideString = value;
			}
		}
		bool hasOverrideString = false;
		#endregion
		public string OverrideString { get; set; }

	}
	#endregion
	#region JoinLeft
	public class JoinLeft : Join
	{
		/// <summary>
		/// Very simple join, joining two tables (not table elements) with a specified join type.
		/// </summary>
		public JoinLeft(object ColumnEnum1, object ColumnEnum2)
		{
			Init(ColumnEnum1);
			this.TableElement1 = new TableElement(ColumnEnum1);
			this.TableElement2 = new TableElement(ColumnEnum2);
			this.JoinCondition = new Q(ColumnEnum1, ColumnEnum2, true);
			this.JoinType = QueryJoinType.Left;
		}
	}
	#endregion
	#region JoinDouble
	public class JoinDouble : Join
	{
		/// <summary>
		/// Very simple many-many join, joining two tables via another.
		/// </summary>
		public JoinDouble(object LeftTableColumn, object LinkingTableLeftColumn, object LinkingTableRightColumn, object RightTableColumn)
		{
			Init(LeftTableColumn);
			this.TableElement1 = new Join(LeftTableColumn, LinkingTableLeftColumn);
			this.TableElement2 = new TableElement(RightTableColumn);
			this.JoinCondition = new Q(LinkingTableRightColumn, RightTableColumn, true);
			this.JoinType = QueryJoinType.Inner;
		}
	}
	#endregion
	#region Join
	public class Join : TableElement
	{
		public Join() { }

		public class StringOverride : TableElement
		{
			public StringOverride(string overrideString)
			{
				this.OverrideString = overrideString;
				this.HasOverrideString = true;
			}
		}


		public class Series : TableElement
		{

			#region TableElements
			public List<TableElement> TableElements
			{
				get
				{
					if (tableElements == null)
						tableElements = new List<TableElement>();
					return tableElements;
				}
				set
				{
					tableElements = value;
				}
			}
			List<TableElement> tableElements;
			#endregion

			#region JoinConditions
			public List<Q> JoinConditions
			{
				get
				{
					if (joinConditions == null)
						joinConditions = new List<Q>();
					return joinConditions;
				}
				set
				{
					joinConditions = value;
				}
			}
			List<Q> joinConditions;
			#endregion

			#region InitialTableElement
			public TableElement InitialTableElement
			{
				get
				{
					return initialTableElement;
				}
				set
				{
					initialTableElement = value;
				}
			}
			TableElement initialTableElement;
			#endregion

			public Series(params object[] columns)
			{
				InitialTableElement = new TableElement(columns[0]);

				for (int i = 1; i < columns.Length; i++)
				{
					TableElement t = new TableElement(columns[i]);

					if (TableElements.Count>0 && t.ExternalSqlTableName.Equals(TableElements[TableElements.Count - 1].ExternalSqlTableName))
						continue;

					TableElements.Add(t);
					JoinConditions.Add(new Q(columns[i - 1], columns[i], true));

				}
			}


			public override void BuildString(StringBuilder sb, bool NoLock)
			{
				sb.Append(" ");
				InitialTableElement.BuildString(sb, NoLock);
				for (int i = 0; i < TableElements.Count; i++)
				{
					sb.Append(" INNER JOIN ");
					TableElements[i].BuildString(sb, NoLock);
					sb.Append(" ON ");
					JoinConditions[i].BuildString(sb);
					sb.Append(" ");
				}
			}
			public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
			{
				sb.Append(" ");
				InitialTableElement.BuildString(sb, ref paramHash, ref rankContribution, NoLock);
				for (int i = 0; i < TableElements.Count; i++)
				{
					sb.Append(" INNER JOIN ");
					TableElements[i].BuildString(sb, ref paramHash, ref rankContribution, NoLock);
					sb.Append(" ON ");
					JoinConditions[i].BuildString(sb, ref paramHash, ref rankContribution);
					sb.Append(" ");
				}
			}

			public override string ToString(bool NoLock)
			{
				StringBuilder sb = new StringBuilder();
				BuildString(sb, NoLock);
				return sb.ToString();
			}
			public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
			{
				StringBuilder sb = new StringBuilder();
				BuildString(sb, ref paramHash, ref rankContribution, NoLock);
				return sb.ToString();
			}
			public override void GetTableNames(ArrayList al)
			{
				foreach (TableElement te in TableElements)
					te.GetTableNames(al);
			}
		}

		public Q JoinCondition { get { return joinCondition; } set { joinCondition = value; } } private Q joinCondition;
		public TableElement TableElement1 { get { return tableElement1; } set { tableElement1 = value; } } private TableElement tableElement1;
		public TableElement TableElement2 { get { return tableElement2; } set { tableElement2 = value; } } private TableElement tableElement2;
		public QueryJoinType JoinType { get { return joinType; } set { joinType = value; } } private QueryJoinType joinType;

		public override void BuildString(StringBuilder sb, bool NoLock)
		{
			sb.Append(" ");
			TableElement1.BuildString(sb,NoLock);
			sb.Append(" ");
			sb.Append(TableElement.GetJoinTypeString(JoinType));
			sb.Append(" JOIN ");
			TableElement2.BuildString(sb,NoLock);
			sb.Append(" ON ");
			JoinCondition.BuildString(sb);
			sb.Append(" ");
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
		{
			sb.Append(" ");
			TableElement1.BuildString(sb, ref paramHash, ref rankContribution, NoLock);
			sb.Append(" ");
			sb.Append(TableElement.GetJoinTypeString(JoinType));
			sb.Append(" JOIN ");
			TableElement2.BuildString(sb, ref paramHash, ref rankContribution, NoLock);
			sb.Append(" ON ");
			JoinCondition.BuildString(sb, ref paramHash, ref rankContribution);
			sb.Append(" ");
		}

		public override string ToString(bool NoLock)
		{
			StringBuilder sb = new StringBuilder();
			BuildString(sb, NoLock);
			return sb.ToString();
		}
		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, bool NoLock)
		{
			StringBuilder sb = new StringBuilder();
			BuildString(sb, ref paramHash, ref rankContribution, NoLock);
			return sb.ToString();
		}
		public override void GetTableNames(ArrayList al)
		{
			TableElement1.GetTableNames(al);
			TableElement2.GetTableNames(al);
		}
		/// <summary>
		/// Complex join, joining two table elements on a complex join condition.
		/// </summary>
		public Join(object Element1, object Element2, Q JoinCondition) : this(Element1, Element2, QueryJoinType.Inner, JoinCondition) { }
		/// <summary>
		/// Complex join, joining two table elements on a complex join condition.
		/// </summary>
		public Join(object Element1, object Element2, QueryJoinType JoinType, Q JoinCondition)
		{
			TableElement TableElement1, TableElement2;

			if (Element1 is TableElement)
				TableElement1 = (TableElement)Element1;
			else
				TableElement1 = new TableElement(Element1);

			if (Element2 is TableElement)
				TableElement2 = (TableElement)Element2;
			else
				TableElement2 = new TableElement(Element2);

			Init(TableElement1);
			this.TableElement1 = TableElement1;
			this.TableElement2 = TableElement2;
			if (Element1 is TableElement || Element2 is TableElement)
				this.JoinCondition = JoinCondition;
			else
				this.JoinCondition = new And(new Q(Element1, Element2, true), JoinCondition);

			this.JoinType = JoinType;
		}

		/// <summary>
		/// Simple join with two table elements:
		/// TableElement1 JoinType JOIN TableElement2 ON ColumnEnum1 = ColumnEnum2
		/// </summary>
		public Join(TableElement TableElement1, TableElement TableElement2, QueryJoinType JoinType, object ColumnEnum1, object ColumnEnum2)
		{
			Init(TableElement1);
			this.TableElement1 = TableElement1;
			this.TableElement2 = TableElement2;
			this.JoinCondition = new Q(ColumnEnum1, ColumnEnum2, true);
			this.JoinType = JoinType;
		}
		/// <summary>
		/// Complex join, joining two table elements on a complex join condition.
		/// </summary>
		public Join(TableElement TableElement1, TableElement TableElement2, QueryJoinType JoinType, Q JoinCondition)
		{
			Init(TableElement1);
			this.TableElement1 = TableElement1;
			this.TableElement2 = TableElement2;
			this.JoinCondition = JoinCondition;
			this.JoinType = JoinType;
		}
		/// <summary>
		/// Very simple join, joining two tables (not table elements) with an inner join.
		/// </summary>
		public Join(object ColumnEnum1, object ColumnEnum2) : this(ColumnEnum1, ColumnEnum2, QueryJoinType.Inner) { }
		/// <summary>
		/// Very simple join, joining a TableElement (a join) to a table with an inner join.
		/// </summary>
		public Join(TableElement t1, object ColumnEnum2, object ColumnEnum1ForCondition)
		{
			Init(t1);
			this.TableElement1 = t1;
			this.TableElement2 = new TableElement(ColumnEnum2);
			this.JoinCondition = new Q(ColumnEnum1ForCondition, ColumnEnum2, true);
			this.JoinType = QueryJoinType.Inner;
		}
		/// <summary>
		/// Very simple join, joining two tables (not table elements) with a specified join type.
		/// </summary>
		public Join(object ColumnEnum1, object ColumnEnum2, QueryJoinType JoinType)
		{
			Init(ColumnEnum1);
			this.TableElement1 = new TableElement(ColumnEnum1);
			this.TableElement2 = new TableElement(ColumnEnum2);
			this.JoinCondition = new Q(ColumnEnum1, ColumnEnum2, true);
			this.JoinType = JoinType;
		}

	}
	#endregion
	#region QueryJoinType
	public enum QueryJoinType
	{
		Inner,
		Outer,
		Left,
		Right
	}
	#endregion

	#region Assign - reprasents an assignment - used in the Update function
	public class Assign
	{

		public Column Column { get; set; }
		public object Data { get; set; }

		public Assign() { }

		#region Assign
		/// <summary>
		/// Assign
		/// </summary>
		/// <param name="ColumnEnum"></param>
		/// <param name="Data"></param>
		public Assign(object ColumnEnum, object Data)
		{
			if (ColumnEnum is Column)
				this.Column = (Column)ColumnEnum;
			else
				this.Column = new Column(ColumnEnum);
			
			this.Data = Data;
		}
		#endregion

		#region ToString methods (2 overloads)
		public virtual void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{

			string paramName = Q.GetUniqueParamName(paramHash, Column.ColumnName);

			sb.Append(" ");
			sb.Append(Column.InternalSqlName);
			sb.Append(" =");
			if (Data == null)
			{
				sb.Append(" NULL ");
			}
			else
			{
				sb.Append(" @");
				sb.Append(paramName);
				sb.Append(" ");

				SqlParameter p = new SqlParameter("@" + paramName, Data);
				paramHash.Add(paramName, p);
			}

		}
		public virtual string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			StringBuilder sb = new StringBuilder();
			this.BuildString(sb, ref paramHash, ref rankContribution);
			return sb.ToString();
		}
		#endregion

		public class Increment : Assign
		{

			#region Increment
			/// <summary>
			/// Increment
			/// </summary>
			/// <param name="ColumnEnum"></param>
			public Increment(object ColumnEnum)
			{
				if (ColumnEnum is Column)
					this.Column = (Column)ColumnEnum;
				else
					this.Column = new Column(ColumnEnum);
			}
			#endregion

			public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
			{
				sb.Append(" ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" = ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" + 1 ");
			}
		}

		public class Addition : Assign
		{

			#region Addition
			/// <summary>
			/// Addition
			/// </summary>
			/// <param name="ColumnEnum"></param>
			/// <param name="Data"></param>
			public Addition(object ColumnEnum, object Data)
			{
				if (ColumnEnum is Column)
					this.Column = (Column)ColumnEnum;
				else
					this.Column = new Column(ColumnEnum);
				
				this.Data = Data;
			}
			#endregion

			public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
			{
				string paramName = Q.GetUniqueParamName(paramHash, Column.ColumnName);

				sb.Append(" ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" = ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" + @");
				sb.Append(paramName);
				sb.Append(" ");

				SqlParameter p = new SqlParameter("@" + paramName, Data);
				paramHash.Add(paramName, p);
			}
		}

		public class Subtraction : Assign
		{

			#region Subtraction
			/// <summary>
			/// Subtraction
			/// </summary>
			/// <param name="ColumnEnum"></param>
			/// <param name="Data"></param>
			public Subtraction(object ColumnEnum, object Data)
			{
				if (ColumnEnum is Column)
					this.Column = (Column)ColumnEnum;
				else
					this.Column = new Column(ColumnEnum);
				
				this.Data = Data;
			}
			#endregion

			public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
			{
				string paramName = Q.GetUniqueParamName(paramHash, Column.ColumnName);

				sb.Append(" ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" = ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" - @");
				sb.Append(paramName);
				sb.Append(" ");

				SqlParameter p = new SqlParameter("@" + paramName, Data);
				paramHash.Add(paramName, p);
			}
		}


		public class Override : Assign
		{

			#region Override
			/// <summary>
			/// Subtraction
			/// </summary>
			/// <param name="ColumnEnum"></param>
			/// <param name="Data"></param>
			public Override(object ColumnEnum, string OverrideString)
			{
				if (ColumnEnum is Column)
					this.Column = (Column)ColumnEnum;
				else
					this.Column = new Column(ColumnEnum);

				this.OverrideString = OverrideString;
			}
			#endregion

			#region OverrideString
			public string OverrideString
			{
				get
				{
					return overrideString;
				}
				set
				{
					overrideString = value;
				}
			}
			string overrideString;
			#endregion

			public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
			{
				sb.Append(" ");
				sb.Append(Column.InternalSqlName);
				sb.Append(" = ");
				sb.Append(OverrideString);
				sb.Append(" ");
			}
		}

	}
	#endregion

	
	#region And - And condition (derives from Q) - reprasents a group of query conditions that are AND'ed together in a query
	/// <summary>
	/// And - And condition (derives from Q) - reprasents a group of query conditions that are AND'ed together in a query
	/// </summary>
	public class And : MultiQ
	{
		public And(params Q[] conditionArray) : base(conditionArray) {}
		public static void BuildAndOrString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, string Op, IEnumerable<Q> conditionArray, bool useParameters)
		{
			bool doneOne = false;
			foreach (Q q in conditionArray)
			{
				if (q != null)
				{
					string s;
					if (useParameters)
						s = q.ToString(ref paramHash, ref rankContribution);
					else
						s = q.ToString();

					if (s.Length > 0)
					{
						if (doneOne)
						{
							sb.Append(" ");
							sb.Append(Op);
							sb.Append(" ");
						}
						else
							sb.Append(" ( ");

						sb.Append(s);

						doneOne = true;
					}
				}
			}
			if (doneOne)
				sb.Append(" ) ");
		}
		//public static string GetAndOrString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution, string Op, Q[] conditionArray, bool useParameters)
		//{
		//    StringBuilder sb = new StringBuilder();
		//    And.BuildAndOrString(sb, ref paramHash, ref rankContribution, Op, conditionArray, useParameters);
		//    return sb.ToString();
		//}
		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			StringBuilder sb = new StringBuilder();
			And.BuildAndOrString(sb, ref paramHash, ref rankContribution, "AND", this.ConditionArray, true);
			return sb.ToString();
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = String.Empty;
			And.BuildAndOrString(sb, ref h, ref s, "AND", this.ConditionArray, false);
			return sb.ToString();
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			And.BuildAndOrString(sb, ref paramHash, ref rankContribution, "AND", this.ConditionArray, true);
		}
		public override void BuildString(StringBuilder sb)
		{
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = String.Empty;
			And.BuildAndOrString(sb, ref h, ref s, "AND", this.ConditionArray, false);
		}
		public override string GetCacheKey()
		{
			return "(" + string.Join(" AND ", ConditionArray.ConvertAll<string>(q => q.GetCacheKey()).ToArray()) + ")";
		}
		
	}
	#endregion
	#region Or - Or condition (derives from Q) - reprasents a group of query conditions that are OR'ed together in a query
	/// <summary>
	/// Or - Or condition (derives from Q) - reprasents a group of query conditions that are OR'ed together in a query
	/// </summary>
	public class Or : MultiQ
	{

		public Or(params Q[] conditionArray) : base(conditionArray) { }

		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			StringBuilder sb = new StringBuilder();
			And.BuildAndOrString(sb, ref paramHash, ref rankContribution, "OR", this.ConditionArray, true);
			return sb.ToString();
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = String.Empty;
			And.BuildAndOrString(sb, ref h, ref s, "OR", this.ConditionArray, false);
			return sb.ToString();
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			And.BuildAndOrString(sb, ref paramHash, ref rankContribution, "OR", this.ConditionArray, true);
		}
		public override void BuildString(StringBuilder sb)
		{
			Dictionary<string, SqlParameter> h = new Dictionary<string, SqlParameter>();
			string s = String.Empty;
			And.BuildAndOrString(sb, ref h, ref s, "OR", this.ConditionArray, false);
		}
		public override string GetCacheKey()
		{
			return "(" + ConditionArray.ConvertAll(q => q.GetCacheKey()).Join(" OR ") + ")";
		}
	 
	}
	#endregion
	public abstract class MultiQ : Q
	{
		public List<Q> ConditionArray { get; set; }
		public MultiQ(params Q[] conditionArray)
		{
			this.ConditionArray = new List<Q>(conditionArray);
		}
		public override IEnumerable<Column> Columns()
		{
			foreach (Q q in this.ConditionArray)
			{

				foreach (Column c in q.Columns())
					yield return c;
			}
		}
	}
	#region QOrNull
	public class QOrNull : Q
	{
		public Q InnerQ;

		#region Constructors
		/// <summary>
		/// Query condition
		/// </summary>
		/// <param name="ColumnEnum"></param>
		/// <param name="OpEnum"></param>
		/// <param name="Data"></param>
		public QOrNull(object ColumnEnum, QueryOperator OpEnum, object Data)
		{
			InnerQ = new Or(
				new Q(ColumnEnum, OpEnum, Data),
				new Q(ColumnEnum, QueryOperator.IsNull, null)
			);
		}
		/// <summary>
		/// Query condition with default QueryOperator of EqualTo
		/// </summary>
		/// <param name="ColumnEnum"></param>
		/// <param name="Data"></param>
		public QOrNull(object ColumnEnum, object Data) : this(ColumnEnum, QueryOperator.EqualTo, Data) { }
		public QOrNull() { }
		#endregion

		#region ToString methods (2 overloads)
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			InnerQ.BuildString(sb, ref paramHash, ref rankContribution);
		}
		public override void BuildString(StringBuilder sb)
		{
			InnerQ.BuildString(sb);
		}
		public override string ToString()
		{
			return InnerQ.ToString();
		}
		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			return InnerQ.ToString(ref paramHash, ref rankContribution);
		}
		#endregion

		public override IEnumerable<Column> Columns()
		{
			foreach (Column c in InnerQ.Columns())
				yield return c;
		}

	}
	#endregion
	#region NotQ
	public class NotQ : Q
	{

		#region InnerQ
		public Q InnerQ
		{
			get
			{
				return innerQ;
			}
			set
			{
				innerQ = value;
			}
		}
		private Q innerQ;
		#endregion

		public NotQ(Q InnerQ)
		{
			this.InnerQ = InnerQ;
		}

		#region ToString methods (2 overloads)
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			sb.Append(" NOT ( ");
			InnerQ.BuildString(sb, ref paramHash, ref rankContribution);
			sb.Append(" ) ");
		}
		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			return " NOT ( " + InnerQ.ToString(ref paramHash, ref rankContribution) + " ) ";
		}
		#endregion

		public override IEnumerable<Column> Columns()
		{
			foreach (Column c in InnerQ.Columns())
				yield return c;
		}

	}
	#endregion
	#region StringQueryCondition
	public class StringQueryCondition : Q
	{
		#region QueryCondition
		public string QueryCondition
		{
			get
			{
				return queryCondition;
			}
			set
			{
				queryCondition = value;
			}
		}
		private string queryCondition;
		#endregion
		public StringQueryCondition(string queryCondition)
		{
			this.QueryCondition = queryCondition;
		}
		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			return QueryCondition;
		}
		public override string ToString()
		{
			return QueryCondition;
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			sb.Append(QueryCondition);
		}
		public override void BuildString(StringBuilder sb)
		{
			sb.Append(QueryCondition);
		}

		public override IEnumerable<Column> Columns()
		{
			throw new Exception("Can't get columns on StringQueryCondition");
		}
	}
	#endregion
	#region BetweenQ
	public class BetweenQ : Q
	{
		
		public BetweenQ(object Column, int MinValue, int MaxValue)
		{
			if (Column is Column)
				this.Column = (Column)Column;
			else
				this.Column = new Column(Column);

			this.MinValue = MinValue;
			this.MaxValue = MaxValue;
		}

		#region MinValue
		public int MinValue
		{
			get
			{
				return minValue;
			}
			set
			{
				minValue = value;
			}
		}
		int minValue;
		#endregion
		#region MaxValue
		public int MaxValue
		{
			get
			{
				return maxValue;
			}
			set
			{
				maxValue = value;
			}
		}
		int maxValue;
		#endregion

		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			return this.ToString();
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			BuildString(sb);
			return sb.ToString();
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			BuildString(sb);
		}
		public override void BuildString(StringBuilder sb)
		{
			sb.Append(" ( ");
			sb.Append(Column.InternalSqlName);
			sb.Append(" BETWEEN ");
			sb.Append(MinValue.ToString());
			sb.Append(" AND ");
			sb.Append(MaxValue.ToString());
			sb.Append(" ) ");
		}

	}
	#endregion
	#region InListQ
	public class InListQ : Q
	{

		#region InListQ(object Column, List<string> List)
		public InListQ(object Column, List<string> List)
		{
			if (Column is Column)
				this.Column = (Column)Column;
			else
				this.Column = new Column(Column);


			this.StringList = List;
		}
		#endregion
		#region InListQ(object Column, List<int> List)
		public InListQ(object Column, List<int> List)
		{
			if (Column is Column)
				this.Column = (Column)Column;
			else
				this.Column = new Column(Column);


			this.IntList = List;
		}
		#endregion
		#region InListQ(object Column, params object[] EnumItems)
		public InListQ(object Column, params object[] EnumItems)
		{
			if (Column is Column)
				this.Column = (Column)Column;
			else
				this.Column = new Column(Column);

			this.IntList = new List<int>();

			foreach (object o in EnumItems)
				this.IntList.Add((int)o);
			//this.IntList = (List<int>)EnumItems;
		}
		#endregion

		#region StringList
		public List<string> StringList
		{
			get
			{
				return stringList;
			}
			set
			{
				stringList = value;
			}
		}
		List<string> stringList;
		#endregion
		#region IntList
		public List<int> IntList
		{
			get
			{
				return intList;
			}
			set
			{
				intList = value;
			}
		}
		List<int> intList;
		#endregion

		public override string ToString(ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			return this.ToString();
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			BuildString(sb);
			return sb.ToString();
		}
		public override void BuildString(StringBuilder sb, ref Dictionary<string, SqlParameter> paramHash, ref string rankContribution)
		{
			BuildString(sb);
		}
		public override void BuildString(StringBuilder sb)
		{
			sb.Append(" ( ");
			sb.Append(Column.InternalSqlName);
			sb.Append(" IN (");
			if (StringList != null)
			{
				bool doneOne = false;
				foreach (string s in StringList)
				{
					if (doneOne)
						sb.Append(", ");
					sb.Append("'");
					sb.Append(Cambro.Misc.Db.PStr(s));
					sb.Append("'");
					doneOne = true;
				}
			}
			else
			{
				bool doneOne = false;
				foreach (int i in IntList)
				{
					if (doneOne)
						sb.Append(", ");
					sb.Append(i.ToString());
					doneOne = true;
				}
			}
			sb.Append(") ) ");
		}
	}
	#endregion
	#region QueryOperator enum - the comparison operator that is performed in a query condition
	/// <summary>
	/// QueryOperator enum - the comparison operator that is performed in a query condition
	/// </summary>
	public enum QueryOperator
	{
		EqualTo,
		NotEqualTo,
		GreaterThan,
		GreaterThanOrEqualTo,
		LessThan,
		LessThanOrEqualTo,
		TextStartsWith,
		TextEndsWith,
		TextContains,
		TextContainsAnyWord,
		IsNull,
		IsNotNull,
		BitwiseEnumContains,
		BitwiseEnumDoesntContain,
        BitwiseAndEqualsZero


	}
	#endregion

	#region OrderBy - specifies a column to order by, and a direction (ascending or descending)
	/// <summary>
	/// OrderBy - specifies a column to order by, and a direction (ascending or descending)
	/// </summary>
	public class OrderBy
	{
		public Column Column { get; set; }
		public OrderDirection Direction { get; set; }
		OrderBy[] orderByArray;
		public string OverideString { get { return overideString; } set { overideString = value; } } private string overideString;
		

		public OrderBy(object column)
		{
			if (column is Column)
				this.Column = (Column)column;
			else
				this.Column = new Column(column);

			this.Direction = OrderDirection.Ascending;
		}
		public OrderBy(object column, OrderDirection direction)
		{
			if (column is Column)
				this.Column = (Column)column;
			else
				this.Column = new Column(column);

			this.Direction = direction;
		}
		public OrderBy(string overideString)
		{
			this.OverideString = overideString;
		}
		/// <summary>
		/// This is needed when using Distinct
		/// </summary>
		/// <param name="overideString"></param>
		/// <param name="overrideColumns"></param>
		public OrderBy(string overideString, params object[] overrideColumns)
		{
			this.OverideString = overideString;
			ColumnSet c = new ColumnSet(overrideColumns);
			allColumns = c.Columns;
		}
		
		/// <summary>
		/// Use OrderDirection=OrderDirection.Random
		/// </summary>
		/// <param name="direction"></param>
		public OrderBy(OrderDirection direction)
		{
			this.Direction = direction;
		}
		public OrderBy(params OrderBy[] ary)
		{
			orderByArray = ary;
		}
		public enum OrderDirection
		{
			Ascending,
			Descending,
			Random
		}
		
		#region AllColumns
		public List<Column> AllColumns
		{
			get
			{
				if (allColumns == null)
				{
					allColumns = new List<Column>();
					if (orderByArray == null || orderByArray.Length < 1)
					{
						if (!allColumns.Contains(Column))
							allColumns.Add(Column);
					}
					else
					{
						foreach (OrderBy o in orderByArray)
							allColumns.AddRange(o.AllColumns);
					}
				}
				return allColumns;
			}
		}
		List<Column> allColumns;
		#endregion

		void getPart(StringBuilder sb)
		{
			getPart(sb, false);
		}
		void getPart(StringBuilder sb, bool ignoreTableName)
		{
			if (OverideString != null && OverideString.Length > 0)
			{
				sb.Append(OverideString);
			}
			else if (Direction.Equals(OrderDirection.Random))
			{
				sb.Append(" NEWID() ");
			}
			else
			{
				sb.Append(" ");
				sb.Append(ignoreTableName ? Column.ColumnName : Column.InternalSqlName);
				if (Direction.Equals(OrderDirection.Descending))
					sb.Append(" DESC");
				sb.Append(" ");
			}
		}
		public override string ToString()
		{
			return this.ToString(false);
		}
		public string ToString(bool ignoreTableName)
		{
			StringBuilder sb = new StringBuilder();
			if (OverideString != null && OverideString.Length > 0)
			{
				sb.Append(" ORDER BY ");
				sb.Append(OverideString);
				sb.Append(" ");
			}
			else if (orderByArray == null || orderByArray.Length < 1)
			{
				sb.Append(" ORDER BY ");
				getPart(sb, ignoreTableName);
				sb.Append(" ");
			}
			else
			{
				sb.Append(" ORDER BY ");
				for (int i = 0; i < orderByArray.Length; i++)
				{
					if (i != 0)
						sb.Append(", ");
					orderByArray[i].getPart(sb, ignoreTableName);
				}
			}
			return sb.ToString();
		}
	}

	public static class OrderByExtensions
	{
		public static string ToSql(this OrderBy.OrderDirection direction)
		{
			switch (direction)
			{
				case OrderBy.OrderDirection.Ascending:
					return "";
				case OrderBy.OrderDirection.Descending:
					return "DESC";
				case OrderBy.OrderDirection.Random:
					return "NEWID()";
				default:
					throw new NotImplementedException(direction.ToString());
			}
		}
	}
	#endregion

	#region GroupBy - specifies a column / columns to group by
	/// <summary>
	/// GroupBy - specifies a column / columns to group by
	/// </summary>
	public class GroupBy
	{
		#region Column
		public Column Column
		{
			get
			{
				return column;
			}
			set
			{
				column = value;
			}
		}
		private Column column;
		#endregion

		GroupBy[] groupByArray;
		public string OverideString { get { return overideString; } set { overideString = value; } } private string overideString;

		public GroupBy(object column)
		{
			if (column is Column)
				this.Column = (Column)column;
			else
				this.Column = new Column(column);
		}
		public GroupBy(string overideString)
		{
			this.OverideString = overideString;
		}
		public GroupBy(params GroupBy[] ary)
		{
			groupByArray = ary;
		}
		void getPart(StringBuilder sb)
		{
			sb.Append(" ");
			sb.Append(Column.InternalSqlName);
			sb.Append(" ");
		}
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (OverideString != null && OverideString.Length > 0)
			{
				sb.Append(" GROUP BY ");
				sb.Append(OverideString);
				sb.Append(" ");
			}
			else if (groupByArray == null || groupByArray.Length < 1)
			{
				sb.Append(" GROUP BY ");
				getPart(sb);
				sb.Append(" ");
			}
			else
			{
				sb.Append(" GROUP BY ");
				for (int i = 0; i < groupByArray.Length; i++)
				{
					if (i != 0)
						sb.Append(", ");
					groupByArray[i].getPart(sb);
				}
			}
			return sb.ToString();
		}
	}
	#endregion

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace Common.Automation.Sql
{
	public class Table
	{
		string name;
		Database database;
		public Table(Database database, string name)
		{
			this.name = name;
			this.database = database;
			
		}
		public string Name { get { return name; } }
		public string Hash
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				DataTable table = DsSpHelp.Tables[1];
				foreach (DataRow row in table.Rows)
				{
					string columnName = row[0] as string;
					if (this.Columns.Find(c => c.Name == columnName) != null)
					{
						for (int i = 0; i < table.Columns.Count; i++)
						{
							if (table.Columns[i].ColumnName != "Prec" && table.Columns[i].ColumnName != "Scale")
							{
								sb.Append(row[i].ToString());
								sb.Append(",");
							}
						}
					}
				}
				return DoHash(sb.ToString()).Substring(0, 8);
			}
		}
		public void PrintHash()
		{
			StringBuilder sb = new StringBuilder();
			DataTable table = DsSpHelp.Tables[1];
			Console.WriteLine("rows: " + table.Rows.Count.ToString());
			foreach (DataRow row in table.Rows)
			{
				string columnName = row[0] as string;
				if (this.Columns.Find(c => c.Name == columnName) != null)
				{
					//sb.Append(row[""]
					for (int i = 0; i < table.Columns.Count; i++)
					{
						if (table.Columns[i].ColumnName != "Prec" && table.Columns[i].ColumnName != "Scale")
						{
							sb.Append(row[i].ToString());
							sb.Append(",");
						}
					}
				}
			}
			Console.WriteLine("data: " + sb.ToString());
			//Console.WriteLine("hash: " + DoHash(sb.ToString()));
			//return DoHash(sb.ToString()).Substring(0, 8);
		}

		public static string DoHash(string input)
		{
			byte[] data = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
			byte[] result = new SHA1CryptoServiceProvider().ComputeHash(data);
			return BitConverter.ToString(result).Replace("-", "");
		}
 
		public string Description
		{
			get
			{
				string sql = "SELECT objName, cast(value as varchar(100)) as description   FROM ::FN_LISTEXTENDEDPROPERTY('MS_Description',     'User',  'dbo',     'table', '" + this.name + "', null, null)";
				DataTable dtDescription = this.database.ExecuteAdapter(sql).Tables[0];

				if (dtDescription.Rows.Count != 0)
				{
					return dtDescription.Rows[0]["Description"] as string;
				}
				else
				{
					return null;
				}
			}
		}
		public List<Column> InvalidationColumns
		{
			get
			{
				return this.Columns.FindAll(c => c.CausesInvalidation);
			}
		}
 
		List<Column> columns = null;
		public List<Column> Columns
		{
			get
			{
				if (columns == null)
				{
					columns = new List<Column>();

					string[] extendedProperties = new[] { "MS_Description", "CausesInvalidation", "IsNotNull" };
					StringBuilder columnsSql = new StringBuilder(
@"declare @columnInfo table 
(
    TABLE_QUALIFIER  sysname null,
    TABLE_OWNER      sysname null,
    TABLE_NAME       sysname null,
    COLUMN_NAME      sysname null,
    DATA_TYPE        sysname null,
    TYPE_NAME        sysname null,
    PRECISION        int null, 
    LENGTH           int null,
    SCALE            int null,
    RADIX            int null,
    NULLABLE         bit null,
    REMARKS          nvarchar(4000) Null,
    COLUMN_DEF       sysname null,
    SQL_DATA_TYPE    int null,
    SQL_DATETIME_SUB int null,
    CHAR_OCTET_LENGTH int null,
    ORDINAL_POSITION  int null,
    IS_NULLABLE       char(3) null,
    SS_DATA_TYPE      int null
)
insert into @columnInfo
exec sp_columns [");
					columnsSql.Append(this.Name);
					columnsSql.Append("]");
					columnsSql.Append(Environment.NewLine);
					columnsSql.Append("select c.*, clmns.is_computed AS IsComputed");

					foreach (string property in extendedProperties)
					{
						columnsSql.Append(", ");
						columnsSql.Append(property);
						columnsSql.Append(".value as '");
						columnsSql.Append(property);
						columnsSql.Append("'");
					}

					columnsSql.Append(@" from @columnInfo c ");
					foreach (string property in extendedProperties)
					{
						columnsSql.Append(Environment.NewLine);
						columnsSql.Append("outer apply fn_listextendedproperty('");
						columnsSql.Append(property);
						columnsSql.Append("', 'SCHEMA', 'dbo', 'TABLE', c.TABLE_NAME, 'COLUMN', c.COLUMN_NAME) AS ");
						columnsSql.Append(property);
					}

					columnsSql.Append(
						" INNER JOIN sys.tables tbl INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id ON (clmns.name=c.COLUMN_NAME)and((tbl.name=c.TABLE_NAME and SCHEMA_NAME(tbl.schema_id)=N'dbo'))");

					//Get the table containing this table's Column Details
					DataTable dtColumnDetails = database.ExecuteAdapter(columnsSql.ToString()).Tables[0];

					//Get column descriptions
					//Dictionary<string, string> columnDescriptions = GetColumnDescriptions();

					//As the stored proc doesn't return the SQLDBType for use in .NET, run this query against each column in the list
					Dictionary<string, SqlDbType> columnSqlDbTypes = this.GetColumnSqlDbTypes();

					//For each row which repesents a column's attributes, create a ColumnDataContainer
					//and add it to the ColumnInfo ArrayList
					foreach (DataRow drColumnDetail in dtColumnDetails.Rows)
					{
						//get the details
						Column column = new Column(this, drColumnDetail, columnSqlDbTypes);
						string description = drColumnDetail["MS_Description"] as string;

						if (description != null && description.Length > 0)
						{
							column.Description = description;
							columns.Add(column);
						}
					}

					SetKeys();
				}
				return columns;

			}
		}

		//private Dictionary<string, string> GetColumnDescriptions()
		//{
		//    Dictionary<string, string> columnDescriptions = new Dictionary<string, string>();
		//    string getDescriptionsSql = "SELECT objName, cast(value as varchar(100)) as description   FROM ::FN_LISTEXTENDEDPROPERTY('MS_Description',     'User',  'dbo',     'table', '" + this.name + "',     'column', null)";
		//    DataTable dtDescriptionDetails = this.database.ExecuteAdapter(getDescriptionsSql).Tables[0];
		//    foreach (DataRow dr in dtDescriptionDetails.Rows)
		//    {
		//        columnDescriptions[(string) dr["objName"]] = dr["description"] as string;
		//    }
		//    return columnDescriptions;
		//}

		public IEnumerable<Table> ChildTables
		{
			get
			{
				string getParentTableNamesSql =
					@"SELECT DISTINCT
					FkTables.[Name] as FkTable
				FROM sys.tables PkTables
				INNER JOIN sys.Columns PkColumns ON PkTables.Object_Id = PkColumns.Object_id
				INNER JOIN sys.columns FkColumns ON 
					FkColumns.[Name] = PkTables.[Name] + 'K' --OR
					--CASE WHEN LEN(FkColumns.[Name]) < LEN( PkTables.[Name] + 'K') THEN null ELSE RIGHT(FkColumns.[Name], LEN( PkTables.[Name] + 'K')) END = PkTables.[Name] + 'K'
					
				INNER JOIN sys.tables FkTables ON  FkTables.Object_Id = FkColumns.Object_id
				INNER JOIN sys.extended_properties ep ON ep.major_id = PkTables.object_id and ep.minor_id = 0 and ep.Name = 'MS_Description'
				INNER JOIN sys.extended_properties ep2 ON ep2.major_id = FkTables.object_id and ep2.minor_id = 0 and ep2.Name = 'MS_Description'
				WHERE PkColumns.[Name] = 'K' AND PkTables.Name = '" + this.Name + "'";
				DataTable dt = this.database.ExecuteAdapter(getParentTableNamesSql).Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					yield return new Table(this.database, (string)dr["FkTable"]);
				}
			}
		}
		public IEnumerable<Table> ParentTables
		{
			get
			{
				string getParentTableNamesSql =
					@"SELECT 
					PkTables.[Name] as PkTable--, FkTables.[Name] as FkTable, FkColumns.[Name] as FkColumn
				FROM sys.tables PkTables
				INNER JOIN sys.Columns PkColumns ON PkTables.Object_Id = PkColumns.Object_id
				INNER JOIN sys.columns FkColumns ON 
					FkColumns.[Name] = PkTables.[Name] + 'K' --OR
					--CASE WHEN LEN(FkColumns.[Name]) < LEN( PkTables.[Name] + 'K') THEN null ELSE RIGHT(FkColumns.[Name], LEN( PkTables.[Name] + 'K')) END = PkTables.[Name] + 'K'
					
				INNER JOIN sys.tables FkTables ON  FkTables.Object_Id = FkColumns.Object_id
				WHERE PkColumns.[Name] = 'K' AND FkTables.Name = '" + this.Name + "'";
				DataTable dt = this.database.ExecuteAdapter(getParentTableNamesSql).Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					yield return new Table(this.database, (string)dr["PkTable"]);
				}
			}
		}
		private List<Column> PrimaryKeys
		{
			get
			{
				return this.Columns.FindAll(c => c.IsPrimaryKey);
			}
		}

		public bool HasSinglePrimaryKey
		{
			get { return this.PrimaryKeys.Count == 1; }
		}
		private List<Column> UniqueKeys
		{
			get
			{
				return this.Columns.FindAll(c => c.IsUniqueKey);
			}
		}

		public bool HasUniqueKey
		{
			get { return this.UniqueKeys.Count == 1; }
		}

		public Column SinglePrimaryKey
		{
			get
			{
				if (this.HasSinglePrimaryKey)
				{
					return this.PrimaryKeys[0];
				}
				return null;
			}
		}


		DataSet dsSpHelp;
		DataSet DsSpHelp
		{
			get
			{
				if (dsSpHelp == null)
				{
					dsSpHelp = this.database.ExecuteAdapter("exec sp_help [" + this.name + "]");
				}
				return dsSpHelp;
			}
		}
		public void SetKeys()
		{
			

			if (DsSpHelp.Tables.Count >= 7)
			{
				DataTable dtConstraints = DsSpHelp.Tables[6];
				if (dtConstraints.Rows.Count == 0)
				{
					throw new Exception("Constraints table is empty for " + this.name + ".");
				}
				//we now have constraint info
				foreach (DataRow drConstraint in dtConstraints.Rows)
				{
					string constraintColumnsInCsv = drConstraint["constraint_keys"] as string;
					string[] constraintColumnNames = constraintColumnsInCsv.Split(",".ToCharArray());

					foreach (string constraintColumnName in constraintColumnNames)
					{
						foreach (Column column in this.Columns)
						{
							if (column.Name == constraintColumnName.Trim()
							|| column.Name + "(-)" == constraintColumnName.Trim())
							{
								column.ConstraintType = drConstraint["constraint_type"] as string;
							}
						}
					}
				}
			}
		}

		//bool flagHasPrimaryKey = false;

		//foreach (Column Column in this.Columns)
		//{
		//    if (Column.ConstraintType != null && Column.ConstraintType.Contains("PRIMARY KEY"))
		//    {
		//        // if there already exists one primary key, then another primary key indicates multiple keys
		//        if (flagHasPrimaryKey == true)
		//            this._hasSinglePrimaryKey = false;
		//        else
		//            this._hasSinglePrimaryKey = true;

		//        Column.IsPrimaryKey = true;
		//        flagHasPrimaryKey = true;
		//    }
		//}

		//foreach (Column Column in this._columnInfo)
		//{
		//    if (Column.ConstraintType != null && Column.ConstraintType.Contains("UNIQUE"))
		//    {
		//        Column.IsUniqueKey = true;
		//        this._hasUniqueKey = true;
		//    }
		//}
		public Dictionary<string, SqlDbType> GetColumnSqlDbTypes()
		{
			SqlConnection conn = new SqlConnection(database.ConnectionString);
			SqlCommand cmd = new SqlCommand("SELECT top 1 * FROM [" + this.name + "]", conn);
			DataTable schemaTable;
			try
			{
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly);
				schemaTable = reader.GetSchemaTable();
				reader.Close();
			}
			finally
			{
				conn.Close();
			}

			Dictionary<string, SqlDbType> columnSqlDbTypes = new Dictionary<string, SqlDbType>();
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				string name = (string)schemaTable.Rows[i]["ColumnName"];
				SqlDbType sqlDbType = (SqlDbType)schemaTable.Rows[i]["ProviderType"];
				columnSqlDbTypes[name] = sqlDbType;
			}
			return columnSqlDbTypes;
		}


	}

}

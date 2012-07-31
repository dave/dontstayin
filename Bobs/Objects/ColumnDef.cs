using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Bobs
{

	public struct ColumnDef
	{
		#region Constructor
		public ColumnDef(
			object columnEnum,
			string columnName,
			SqlDbType sqlDbType,
			int length,
			SqlColumnFlag sqlColumnFlags,
			object defaultValue)
		{
			ColumnEnum = columnEnum;
			ColumnName = columnName;
			SqlDbType = sqlDbType;
			#region Length
			if (length > 0)
				Length = length;
			else
			{
				switch (SqlDbType)
				{
					case System.Data.SqlDbType.Int:
					case System.Data.SqlDbType.Real:
					case System.Data.SqlDbType.SmallMoney:
					case System.Data.SqlDbType.SmallDateTime:
						Length = 4;
						break;
					case System.Data.SqlDbType.Bit:
					case System.Data.SqlDbType.TinyInt:
						Length = 1;
						break;
					case System.Data.SqlDbType.DateTime:
					case System.Data.SqlDbType.Float:
					case System.Data.SqlDbType.BigInt:
					case System.Data.SqlDbType.Money:
					case System.Data.SqlDbType.Timestamp:
						Length = 8;
						break;
					case System.Data.SqlDbType.Decimal:
						Length = 9;
						break;
					case System.Data.SqlDbType.SmallInt:
						Length = 2;
						break;
					case System.Data.SqlDbType.NChar:
					case System.Data.SqlDbType.Char:
						Length = 10;
						break;
					case System.Data.SqlDbType.Text:
					case System.Data.SqlDbType.UniqueIdentifier:
					case System.Data.SqlDbType.NText:
					case System.Data.SqlDbType.Image:
						Length = 16;
						break;
					case System.Data.SqlDbType.Binary:
					case System.Data.SqlDbType.VarChar:
					case System.Data.SqlDbType.VarBinary:
					case System.Data.SqlDbType.NVarChar:
						Length = 50;
						break;
					default:
						throw new Exception("Must assign a length to this data type");
				}
			}
			#endregion
			SqlColumnFlags = sqlColumnFlags;
			DefaultValue = defaultValue;
		}

		public ColumnDef(object ColumnEnum, string ColumnName, SqlDbType SqlDbType, int Length, object defaultValue)
			: this(ColumnEnum, ColumnName, SqlDbType, Length, SqlColumnFlag.None, defaultValue) { }

		public ColumnDef(object ColumnEnum, string ColumnName, SqlDbType SqlDbType, object defaultValue)
			: this(ColumnEnum, ColumnName, SqlDbType, -1, SqlColumnFlag.None, defaultValue) { }

		#endregion


		public object ColumnEnum;
		public string ColumnName;
		public SqlDbType SqlDbType;
		public int Length;
		public SqlColumnFlag SqlColumnFlags;
		public object DefaultValue;
		
	}

	#region SqlColumnFlag enum - various modifiers for SqlTableColumns
	/// <summary>
	/// SqlColumnFlag enum - various modifiers for SqlTableColumns
	/// </summary>
	[Flags]
	public enum SqlColumnFlag
	{
		None = 0,
		AutoNumber = 1,
		PrimaryKey = 2,
		IsComputed = 4,
	}
	#endregion


}

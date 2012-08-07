using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Common.Automation.Sql
{
	public class Column
	{
		#region Properties

		public string Name { get; private set; }
		public int OrdinalPosition { get; private set; }
		public SqlDbType SqlDbType { get; private set; }
		public string Description { get; set; }
		public string ConstraintType { get; set; }
		public bool IsIdentity { get; set; }
		public bool IsComputed { get; set; }
		public int Length { get; private set; }
		public bool IsNullable { get { return IsNullableSqlColumn && !HasIsNotNullExtendedProperty; } }
		public bool IsNullableSqlColumn { get; private set; }
		public bool HasIsNotNullExtendedProperty { get; private set; }

		public bool IsPrimaryKey
		{
			get { return this.ConstraintType != null && this.ConstraintType.Contains("PRIMARY KEY"); }
		}

		public bool IsUniqueKey
		{
			get { return this.ConstraintType != null && this.ConstraintType.Contains("UNIQUE"); }
		}

		public bool IsReadOnly
		{
			get { return (SqlDbType == SqlDbType.Timestamp); }
		}

		private string columnDefault;
		public string ColumnDefault
		{
			get
			{
				if (columnDefault != null)
				{
					return columnDefault;
				}
				else if (this.HasIsNotNullExtendedProperty)
				{
					return GetDefaultByType();
				}
				else
				{
					return null;
				}
			}
			private set
			{
				columnDefault = value;
			}
		}

		public string Flags
		{
			get
			{
				string s = "";
				if (this.IsPrimaryKey || this.IsUniqueKey)
					s = "SqlColumnFlag.PrimaryKey";
				if (this.IsIdentity)
					s += (s.Length > 0 ? " | " : "") + "SqlColumnFlag.AutoNumber";
				if (this.IsComputed)
					s += (s.Length > 0 ? " | " : "") + "SqlColumnFlag.IsComputed";
				return s;
			}
		}

		public bool CausesInvalidation { get; private set; }

		#endregion

		public Table Table { get; private set; }
		public Column(Table table, DataRow columnDetail, Dictionary<string, SqlDbType> columnSqlDbTypes)
		{
			this.Table = table;
			this.Name = columnDetail["COLUMN_NAME"] as string;
			this.SqlDbType = columnSqlDbTypes[this.Name];
			this.Length = (int)columnDetail["LENGTH"];
			this.OrdinalPosition = (int)columnDetail["ORDINAL_POSITION"];
			this.IsNullableSqlColumn = (columnDetail["IS_NULLABLE"] as string == "YES");
			this.HasIsNotNullExtendedProperty = (columnDetail["IsNotNull"] as string == "true");
			this.CausesInvalidation = (columnDetail["CausesInvalidation"] as string == "true");

			this.IsIdentity = ((string)columnDetail["TYPE_NAME"]).EndsWith(" identity");
			this.IsComputed = (bool)columnDetail["IsComputed"];

			if (columnDetail["COLUMN_DEF"] != DBNull.Value)
			{
				string colDefault = ((string)columnDetail["COLUMN_DEF"]);
				if (colDefault.ToLower() != "(newid())")
				{
					colDefault = colDefault.Substring(2, colDefault.Length - 4) + GetValueSuffixByType();
				}
				this.ColumnDefault = colDefault;
			}
		}

		public string NativeType
		{
			get
			{
				switch (this.SqlDbType)
				{
					case SqlDbType.BigInt: return "long" + (this.IsNullable ? "?" : "");
					case SqlDbType.Binary: return "???";
					case SqlDbType.Bit: return "bool" + (this.IsNullable ? "?" : "");
					case SqlDbType.Char: return "string";
					case SqlDbType.DateTime: return "DateTime" + (this.IsNullable ? "?" : "");
					case SqlDbType.Decimal: return "decimal" + (this.IsNullable ? "?" : "");
					case SqlDbType.Float: return "double" + (this.IsNullable ? "?" : "");
					case SqlDbType.Image: return "???";
					case SqlDbType.Int: return "int" + (this.IsNullable ? "?" : "");
					case SqlDbType.Money: return "???";
					case SqlDbType.NChar: return "string";
					case SqlDbType.NText: return "string";
					case SqlDbType.NVarChar: return "string";
					case SqlDbType.Real: return "???";
					case SqlDbType.SmallDateTime: return "DateTime" + (this.IsNullable ? "?" : "");
					case SqlDbType.SmallInt: return "int" + (this.IsNullable ? "?" : "");
					case SqlDbType.SmallMoney: return "???";
					case SqlDbType.Text: return "string";
					case SqlDbType.Timestamp: return "byte[]";
					case SqlDbType.TinyInt: return "byte" + (this.IsNullable ? "?" : "");
					case SqlDbType.Udt: return "???";
					case SqlDbType.UniqueIdentifier: return "Guid" + (this.IsNullable ? "?" : "");
					case SqlDbType.VarBinary: return "System.Data.SqlTypes.SqlBinary";
					case SqlDbType.VarChar: return "string";
					case SqlDbType.Variant: return "object";
					case SqlDbType.Xml: return "string";
					default: return "";
				}
			}
		}

		public string GetCSharpCodeToInitialisePropertyToDataValueDependingOnSqlDbTypeOfData()
		{
			string defaultValue = this.ColumnDefault ?? "null";

			switch (this.NativeType)
			{
				case "string": return defaultValue;
				case "Guid": return defaultValue.ToLower() == "(newid())" ? "Guid.NewGuid()" : "Guid.Empty";
				case "Guid?": return "null";
				case "byte": return defaultValue;
				case "byte?": return "null";
				case "byte[]": return "null"; // timestamp
				case "DateTime": return "DateTime.Parse(\"" + defaultValue + "\")";
				case "DateTime?": return "null";
				case "decimal": return defaultValue;
				case "decimal?": return "null";
				case "int": return defaultValue;
				case "int?": return "null";
				case "double": return defaultValue;
				case "double?": return "null";
				case "bool": return defaultValue == "1" ? "true" : "false";
				case "bool?": return "null";
				case "long": return defaultValue;
				case "long?": return "null";
				case "object": return defaultValue;
				case "System.Data.SqlTypes.SqlBinary": return "new System.Data.SqlTypes.SqlBinary(BitConverter.GetBytes(" + defaultValue + "))";
				case "System.Data.SqlTypes.SqlDecimal": return "new decimal(" + defaultValue + ")";
				default: return "???";
			}

		}

		private string GetDefaultByType()
		{
			switch (this.SqlDbType)
			{
				case System.Data.SqlDbType.Bit:
					return "false";
				case System.Data.SqlDbType.BigInt:
					return "0L";
				case System.Data.SqlDbType.Decimal:
					return "0m";
				case System.Data.SqlDbType.Int:
				case System.Data.SqlDbType.Money:
				case System.Data.SqlDbType.Real:
				case System.Data.SqlDbType.SmallInt:
				case System.Data.SqlDbType.SmallMoney:
				case System.Data.SqlDbType.TinyInt:
					return "0";
				case System.Data.SqlDbType.Float:
					return "0.0";
				case System.Data.SqlDbType.Char:
				case System.Data.SqlDbType.NChar:
				case System.Data.SqlDbType.NText:
				case System.Data.SqlDbType.NVarChar:
				case System.Data.SqlDbType.Text:
				case System.Data.SqlDbType.VarChar:
					return "\"\"";
				case System.Data.SqlDbType.DateTime:
					return "01/01/0001";
				case System.Data.SqlDbType.Binary:
				case System.Data.SqlDbType.Image:
				case System.Data.SqlDbType.SmallDateTime:
				case System.Data.SqlDbType.Timestamp:
				case System.Data.SqlDbType.UniqueIdentifier:
				case System.Data.SqlDbType.VarBinary:
				case System.Data.SqlDbType.Variant:
					return "System.DBNull.Value";
				default:
					return "System.DBNull.Value";
			}
		}

		private string GetValueSuffixByType()
		{
			switch (this.SqlDbType)
			{
				case System.Data.SqlDbType.BigInt:
					return "L";
				case System.Data.SqlDbType.Decimal:
					return "m";
				default:
					return "";
			}
		}

	}
}

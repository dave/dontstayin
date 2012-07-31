using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Common.Automation.Sql;
using System.Security.Cryptography;
using BobsCommonFileGenerator.CodeSections;
using NVelocityTemplateEngine.Interfaces;
using NVelocityTemplateEngine;
namespace BobsCommonFileGenerator
{
	class CodeWriter
	{
		static ArrayList dbTypes = new ArrayList();

		#region GenerateColumnsEnum

		public static string GenerateColumnsEnum(string tableName, List<Column> ColumnDetails, int tableNumber)
		{
			string TopBit = @"
		#region Columns enum
		public enum Columns
		{";
			string EnumTemplate = @"
			/// <summary>
			/// @DESCRIPTION@
			/// </summary>
			@COLUMNNAME@ = @ENUMVAL@,";

			string BottomBit = @"
		}
		#endregion";

			//this sets the table number part of 0xTTTTCCCC
			int EnumBase = 65536 * tableNumber;

			StringBuilder ColumnsEnum = new StringBuilder();

			ColumnsEnum.Append(TopBit);



			string NewEnum;
			Column NewColumn;

			for (int i = 0; i < ColumnDetails.Count; i++)
			{
				NewColumn = ((Column)ColumnDetails[i]);
				int ColumnIndex = EnumBase + i + 1;

				NewEnum = EnumTemplate;

				NewEnum = NewEnum.Replace("@DESCRIPTION@", NewColumn.Description);
				NewEnum = NewEnum.Replace("@COLUMNNAME@", NewColumn.Name);
				NewEnum = NewEnum.Replace("@ENUMVAL@", String.Format("0x{0:x8}", ColumnIndex));

				ColumnsEnum.Append(NewEnum);
			}

			ColumnsEnum.Append(BottomBit);

			return ColumnsEnum.ToString();
		}
		#endregion


		#region GenerateCommon
		public static string GenerateCommonCodeForPrimaryKey(Table Table)
		{
			string Constructor = @"
		#region Common
 
		public " + Table.Name + @"(SerializationInfo info, StreamingContext context) { this.Bob = new Bob(info, context);  }
		public " + Table.Name + @"()
		{
			this.Bob = new Bob(Bobs.Tables.Defs." + Table.Name + @");
		}
		public " + Table.Name + @"(BobSet bs)
		{
			this.Bob = new Bob(bs.Table, bs);
		}
		public " + Table.Name + @"(" + Table.SinglePrimaryKey.NativeType + @" " + Table.Name + Table.SinglePrimaryKey.Name + @", IBob Parent, object Column)
			: this()
		{
			Bob.GetBobFromParent(" + Table.Name + Table.SinglePrimaryKey.Name + @", Parent.Bob, Column, TablesEnum." + Table.Name + @");
		}
		public " + Table.Name + @"(" + Table.SinglePrimaryKey.NativeType + @" " + Table.Name + Table.SinglePrimaryKey.Name + @")
			: this()
		{
			Bob.GetBobFromPrimaryKey(" + Table.Name + Table.SinglePrimaryKey.Name + @");
		}
		#endregion";

			return Constructor;
		}



		public static string GenerateCommonCodeForMultiKey(Table Table)
		{
			//needs tidy up
			string Constructor = @"
		#region Common
	 
		public " + Table.Name + @"(SerializationInfo info, StreamingContext context) { this.Bob = new Bob(info, context);}
		public " + Table.Name + @"()
		{
			this.Bob = new Bob(Bobs.Tables.Defs." + Table.Name + @");
		}
		public " + Table.Name + @"(BobSet bs)
		{
			this.Bob = new Bob(Bobs.Tables.Defs." + Table.Name + @", bs);
		}
		public " + Table.Name + @"(IBob Parent, object Column) : this()
		{
			this.Bob.GetBobFromParentSimple(Parent, Column, TablesEnum." + Table.Name + @");
		}";


			bool NeedComma = false;

			string Qs = string.Empty; ;
			string Ps = string.Empty; ;
			foreach (Column Col in Table.Columns)
			{
				if (Col.IsUniqueKey || Col.IsPrimaryKey)
				{
					if (NeedComma)
					{
						Qs += ", ";
						Ps += ", ";
					}

					Qs += "new Q(" + Table.Name + @".Columns." + Col.Name + "," + Col.Name + ")";
					Ps += Col.NativeType + " " + Col.Name;
					NeedComma = true;
				}
			}

			string KeyConstructor = @"
		public " + Table.Name + @"(" + Ps + @") : this()
		{
			this.Bob.GetBobFromPrimaryKeyArray(new Q[] {" + Qs + @"});
		} 
		#endregion";

			return Constructor + KeyConstructor;
		}

		#endregion

		#region GenerateTemplate

		public static string GenerateTemplate(Table table)
		{


			string Template = string.Empty;

			Template += @"
	#region " + table.Name + @"Template
	/*
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region " + table.Name + @"
	/// <summary>
	/// " + table.Description + @"
	/// </summary>
	[Serializable]
	public partial class " + table.Name + @" 
	{
		
		#region Simple members";

			string TemplateBottom = @"
		#endregion

	}
	#endregion

	*/
	#endregion";;

			foreach (Column column in table.Columns)
			{
				if (column.NativeType == "Guid")
				{
					Template += @"
		/// <summary>
		/// " + column.Description + @"
		/// </summary>
		public Guid	" + column.Name + @"
		{
			get{ return Cambro.Misc.Db.GuidConvertor(this[" + table.Name + @".Columns." + column.Name + @"]);}		
			set{ this[" + table.Name + @".Columns." + column.Name + @"] = new System.Data.SqlTypes.SqlGuid(value);}
		}";
				}
				else if (column.IsReadOnly)
				{
					Template += @"
		/// <summary>
		/// " + column.Description + @"
		/// </summary>
		public " + column.NativeType + @" " + column.Name + @"
		{
			get { return (" + column.NativeType + @")this[" + table.Name + @".Columns." + column.Name + @"]; }
		}";
				}
				else
				{
					Template += @"
		/// <summary>
		/// " + column.Description + @"
		/// </summary>
		public override " + column.NativeType + @" " + column.Name + @"
		{
			get { return (" + column.NativeType + @")this[" + table.Name + @".Columns." + column.Name + @"]" + (column.Name == "K" ? " as int? ?? 0 " : "") + @"; }
			set { this[" + table.Name + @".Columns." + column.Name + @"] = value; }
		}";
					
				}

			}

			Template += TemplateBottom;

			return Template;
		}
		#endregion

		#region GenerateBobSet
		public static string GenerateBobSet(Table table)
		{
			return CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.BobSet.vm", new Dictionary<string, object>() { { "table", table } });
		}
		#endregion

		#region GenerateTableDef

		#region GenerateTableDefCommon
		public static string GenerateTableDef(Table table)
		{
		//	if (table.Name == "Banner")
		//	{
		//		table.PrintHash();
		//	}
			string tableDefinitionStart = @"
		public override string TableName
		{
			get
			{
				return " + Quotes(table.Name) + @";
			}
		}

		public override string TableCacheKey
		{
			get
			{
				return " + Quotes(table.Hash) + @";
			}
		}

		public override string ColumnName(object ColumnEnum)
		{
			return " + table.Name + @".GetColumnName((" + table.Name + @".Columns)ColumnEnum);
		}

		public override TablesEnum TableEnum
		{
			get
			{
				return TablesEnum." + table.Name + @";
			}
		}

		public override bool HasSinglePrimaryKey
		{
			get
			{
				return " + table.HasSinglePrimaryKey.ToString().ToLower() + @";
			}
		}
";

			if (table.HasSinglePrimaryKey)
			{
				tableDefinitionStart +=
			@"
		public override object SinglePrimaryKey
		{
			get
			{
				return " + table.Name + @".Columns." + table.SinglePrimaryKey.Name + @";
			}
		}
";
			}
			else
			{
				tableDefinitionStart +=
			@"
		public override object SinglePrimaryKey
		{
			get
			{
				return null;
			}
		}
";
			}

			return tableDefinitionStart;

		}
		#endregion

		#region GenerateColumnData
		public static string GenerateColumnData(string tableName, List<Column> ColumnDetails)
		{
 

			StringBuilder ColumnEnumCode = new StringBuilder();

			StringBuilder ColumnEnumLine = new StringBuilder();


			ColumnEnumCode.Append(
@"		#region InitializeAllColumns
		protected override void InitializeAllColumns()
		{
			if (!IsAllSet)
			{
				lock (ColumnStore)
				{
					if (!IsAllSet)
					{
");

			foreach (Column ColumnInfo in ColumnDetails)
			{
				ColumnEnumLine.Length = 0;
				ColumnEnumLine.Append("\t\t\t\t\t\tInitColumn(" + tableName + ".Columns." + ColumnInfo.Name + ");");
				ColumnEnumLine.Append(Environment.NewLine);

				ColumnEnumCode.Append(ColumnEnumLine.ToString());
			}

			ColumnEnumCode.Append(

@"				
						IsAllSet = true;
					}
				}
			}
		}
		#endregion
		#region Columns
		public override ColumnData<ColumnDef> Columns
		{
			get
			{
				InitializeAllColumns();
				return ColumnStore;
			}
		}
		#endregion
");

			return ColumnEnumCode.ToString();

		}

		#endregion

		#region GenerateInitColumn
		public static string GenerateInitColumn(string tableName, List<Column> columnDetails)
		{



			StringBuilder tableDefCode = new StringBuilder();

			StringBuilder columnDef = new StringBuilder();


			tableDefCode.Append(
@"
		#region InitColumn
		protected override void InitColumn(object ColumnEnum)
		{
			int i = (int)ColumnEnum;
			" + tableName + @".Columns c = (" + tableName + @".Columns)ColumnEnum;
			if (!IsSet[i])
			{
				switch (c)
				{
");

			foreach (Column columnInfo in columnDetails)
			{
				columnDef.Length = 0;
				columnDef.Append("\t\t\t\t\tcase " + tableName + ".Columns." + columnInfo.Name + ": ColumnStore[i] = new ColumnDef(c, " + Quotes(columnInfo.Name) + ", SqlDbType.");
				columnDef.Append(columnInfo.SqlDbType.ToString());
				if (columnInfo.Flags.Length == 0)
				{
					if (!IsDefaultLength(columnInfo.SqlDbType, columnInfo.Length))
						columnDef.Append(", " + columnInfo.Length);
				}
				else
				{
					//If we have flags, need to inclue length reagrdless of whether its default or not, otherwise won't match method signature
					columnDef.Append(", " + columnInfo.Length);
					columnDef.Append(", " + columnInfo.Flags);
				}
				//if (columnInfo.ColumnDefault != null || columnInfo.IsNullable)
				{
					//columnDef.Append(", (");
					//columnDef.Append(columnInfo.NativeType);
					//columnDef.Append(")");
					columnDef.Append(", ");
					columnDef.Append(columnInfo.GetCSharpCodeToInitialisePropertyToDataValueDependingOnSqlDbTypeOfData());
				}


				columnDef.Append("); break;");
				columnDef.Append(Environment.NewLine);

				tableDefCode.Append(columnDef.ToString());
			}

			tableDefCode.Append(
@"					default: throw new Exception(""Column not found"");
				}
				IsSet[i] = true;
			}
		}
		#endregion

");

			return tableDefCode.ToString();
		}
		#endregion

		#endregion

		#region Helpers

		private static string Quotes(string thing)
		{
			return "\"" + thing + "\"";
		}

		private static bool IsDefaultLength(SqlDbType sqlDBType, int length)
		{
			int DefaultLength;
			switch (sqlDBType)
			{
				case System.Data.SqlDbType.Int:
				case System.Data.SqlDbType.Real:
				case System.Data.SqlDbType.SmallMoney:
				case System.Data.SqlDbType.SmallDateTime:
					DefaultLength = 4;
					break;
				case System.Data.SqlDbType.Bit:
				case System.Data.SqlDbType.TinyInt:
					DefaultLength = 1;
					break;
				case System.Data.SqlDbType.DateTime:
				case System.Data.SqlDbType.Float:
				case System.Data.SqlDbType.BigInt:
				case System.Data.SqlDbType.Money:
				case System.Data.SqlDbType.Timestamp:
					DefaultLength = 8;
					break;
				case System.Data.SqlDbType.Decimal:
					DefaultLength = 9;
					break;
				case System.Data.SqlDbType.SmallInt:
					DefaultLength = 2;
					break;
				case System.Data.SqlDbType.NChar:
				case System.Data.SqlDbType.Char:
					DefaultLength = 10;
					break;
				case System.Data.SqlDbType.Text:
					DefaultLength = 2147483647;
					break;
				case System.Data.SqlDbType.UniqueIdentifier:
				case System.Data.SqlDbType.NText:
				case System.Data.SqlDbType.Image:
					DefaultLength = 16;
					break;
				case System.Data.SqlDbType.Binary:
				case System.Data.SqlDbType.VarChar:
				case System.Data.SqlDbType.VarBinary:
				case System.Data.SqlDbType.NVarChar:
					DefaultLength = 50;
					break;
				default:
					DefaultLength = -1;
					return false;
			}

			if (sqlDBType.Equals(SqlDbType.Text))
				return true;

			return (DefaultLength == length);
		}
		


		#endregion



		
		internal static string RunNVelocityTemplate(string template, Dictionary<string, object> context)
		{
			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			INVelocityEngine embeddedEngine = NVelocityEngineFactory.CreateNVelocityAssemblyEngine(assemblyName, true);
			return embeddedEngine.Process(context, template);
		}
	}
	internal class OpenCloseWriter : IDisposable
	{
		StringBuilder sb;
		string close;
		internal OpenCloseWriter(string open, string close, StringBuilder sb)
		{
			this.sb = sb;
			this.close = close;
			sb.AppendLine(open);

		}


		#region IDisposable Members

		public void Dispose()
		{
			sb.AppendLine(close);
		}

		#endregion
	}
	

}

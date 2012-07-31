using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using Bobs.CachedDataAccess;
namespace Bobs
{

	public class Tables
	{
		public static string GetTableName(TablesEnum tableEnum)
		{
			switch (tableEnum)
			{
				case TablesEnum.ExampleTable: return "ExampleTable";
				default: throw new Exception("Table not found in Tables.GetTableName()");
			}
		}
		public static string GetColumnName(object columnEnum)
		{
			switch (GetTableEnum(columnEnum))
			{
				case TablesEnum.ExampleTable: return ExampleTable.GetColumnName((ExampleTable.Columns)columnEnum);
				default: return "";
			}
		}
		public static bool DoesColumnCauseInvalidation(object columnEnum)
		{
			switch(GetTableEnum(columnEnum))
			{
				case TablesEnum.ExampleTable: return ExampleTable.DoesColumnCauseInvalidation((ExampleTable.Columns)columnEnum);
				default: return false;
			}
		}
		public static TablesEnum GetTableEnum(object columnEnum)
		{
			return (TablesEnum)((int)columnEnum >> 16);
		}
	}


	#region TablesEnum
	public enum TablesEnum
	{
		/// <summary>
		/// An example table
		/// </summary>
		ExampleTable = 0x0001,
	}
	#endregion

	#region ExampleTable
	/// <summary>
	/// An example table
	/// </summary>
	public partial class ExampleTable : Bob
	{
		#region Columns enum
		public enum Columns
		{
			/// <summary>
			/// An example primary key column
			/// </summary>
			K = 0x00010001,
			/// <summary>
			/// An example varchar column
			/// </summary>
			VarcharColumn = 0x00010002,
			/// <summary>
			/// An example int column
			/// </summary>
			IntColumn = 0x00010003,
			/// <summary>
			/// An example int column with a default value of -1
			/// </summary>
			IntColumnWithDefaultOfMinusOne = 0x00010004,
			/// <summary>
			/// An example datetime column
			/// </summary>
			DateTimeColumn = 0x00010005,
			/// <summary>
			/// An example guid column
			/// </summary>
			GuidColumn = 0x00010006,
			/// <summary>
			/// An example guid column with default of NEWID()
			/// </summary>
			GuidColumnWithDefaultOfNewId = 0x00010007,
			/// <summary>
			/// An example bit column
			/// </summary>
			BitColumn = 0x00010008,
			/// <summary>
			/// An example bit column with a default of zero
			/// </summary>
			BitColumnWithDefaultOfZero = 0x00010009,
			/// <summary>
			/// An example bigint column
			/// </summary>
			BigIntColumn = 0x0001000a,
			/// <summary>
			/// An example bigint column with default of zero
			/// </summary>
			BigIntColumnWithDefaultOfZero = 0x0001000b,
			/// <summary>
			/// An example char column
			/// </summary>
			CharColumn = 0x0001000c,
			/// <summary>
			/// An example float column
			/// </summary>
			FloatColumn = 0x0001000d,
			/// <summary>
			/// An example NChar column
			/// </summary>
			NCharColumn = 0x0001000e,
			/// <summary>
			/// An example ntext column
			/// </summary>
			NTextColumn = 0x0001000f,
			/// <summary>
			/// An example nvarchar column
			/// </summary>
			NVarcharColumn = 0x00010010,
			/// <summary>
			/// An example small datetime column
			/// </summary>
			SmallDateTimeColumn = 0x00010011,
			/// <summary>
			/// An example small int column
			/// </summary>
			SmallIntColumn = 0x00010012,
			/// <summary>
			/// An example text column
			/// </summary>
			TextColumn = 0x00010013,
			/// <summary>
			/// An example timestamp column for RowVersion
			/// </summary>
			RowVersion = 0x00010014,
			/// <summary>
			/// an example tinyint column
			/// </summary>
			TinyIntColumn = 0x00010015,
			/// <summary>
			/// An example xml column
			/// </summary>
			XmlColumn = 0x00010016,
			/// <summary>
			/// An example varbinary column
			/// </summary>
			VarBinaryColumn = 0x00010017,
			/// <summary>
			/// An example varbinary column with a default of zero
			/// </summary>
			VarBinaryColumnWithDefaultOfZero = 0x00010018,
			/// <summary>
			/// An example sql_variant column
			/// </summary>
			SqlVariantColumn = 0x00010019,
		}
		#endregion
		public static string GetColumnName(ExampleTable.Columns columnEnum)
		{
			switch (columnEnum)
			{
						case ExampleTable.Columns.K: return "K";
						case ExampleTable.Columns.VarcharColumn: return "VarcharColumn";
						case ExampleTable.Columns.IntColumn: return "IntColumn";
						case ExampleTable.Columns.IntColumnWithDefaultOfMinusOne: return "IntColumnWithDefaultOfMinusOne";
						case ExampleTable.Columns.DateTimeColumn: return "DateTimeColumn";
						case ExampleTable.Columns.GuidColumn: return "GuidColumn";
						case ExampleTable.Columns.GuidColumnWithDefaultOfNewId: return "GuidColumnWithDefaultOfNewId";
						case ExampleTable.Columns.BitColumn: return "BitColumn";
						case ExampleTable.Columns.BitColumnWithDefaultOfZero: return "BitColumnWithDefaultOfZero";
						case ExampleTable.Columns.BigIntColumn: return "BigIntColumn";
						case ExampleTable.Columns.BigIntColumnWithDefaultOfZero: return "BigIntColumnWithDefaultOfZero";
						case ExampleTable.Columns.CharColumn: return "CharColumn";
						case ExampleTable.Columns.FloatColumn: return "FloatColumn";
						case ExampleTable.Columns.NCharColumn: return "NCharColumn";
						case ExampleTable.Columns.NTextColumn: return "NTextColumn";
						case ExampleTable.Columns.NVarcharColumn: return "NVarcharColumn";
						case ExampleTable.Columns.SmallDateTimeColumn: return "SmallDateTimeColumn";
						case ExampleTable.Columns.SmallIntColumn: return "SmallIntColumn";
						case ExampleTable.Columns.TextColumn: return "TextColumn";
						case ExampleTable.Columns.RowVersion: return "RowVersion";
						case ExampleTable.Columns.TinyIntColumn: return "TinyIntColumn";
						case ExampleTable.Columns.XmlColumn: return "XmlColumn";
						case ExampleTable.Columns.VarBinaryColumn: return "VarBinaryColumn";
						case ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero: return "VarBinaryColumnWithDefaultOfZero";
						case ExampleTable.Columns.SqlVariantColumn: return "SqlVariantColumn";
						default: return "";
			}
		}

		public static bool DoesColumnCauseInvalidation(ExampleTable.Columns columnEnum)
		{
			switch (columnEnum)
			{
																													default: return false;
			}
		}


		#region Common
		public override void SetTableDef()
		{
			this.Table = new ExampleTableTableDef();
		}
		public ExampleTable(SerializationInfo info, StreamingContext context) : base(info, context) { }
		public ExampleTable()
		{
			SetTableDef();
		}
		public ExampleTable(BobSet bs)
		{
			this.BobSet = bs;
			this.Table = bs.Table;
		}
		public ExampleTable(int ExampleTableK, Bob Parent, object Column)
			: this()
		{
			GetBobFromParent(ExampleTableK, Parent, Column, TablesEnum.ExampleTable);
		}
		public ExampleTable(int ExampleTableK)
			: this()
		{
			GetBobFromPrimaryKey(ExampleTableK);
		}
		#endregion

		#region RowVersion column index
		/// <summary>
		/// RowVersion column index, where exists, or -1 otherwise
		/// </summary>
		public override int RowVersionColumn
		{
			get { return (int)Columns.RowVersion; }
		}
		#endregion

	}
	#endregion
	#region ExampleTableTemplate
	/*
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region ExampleTable
	/// <summary>
	/// An example table
	/// </summary>
	[Serializable]
	public partial class ExampleTable: Bob
	{

		#region Simple members
		/// <summary>
		/// An example primary key column
		/// </summary>
		public int K
		{
			get { return (int)this[ExampleTable.Columns.K]; }
			set { this[ExampleTable.Columns.K] = value; }
		}
		/// <summary>
		/// An example varchar column
		/// </summary>
		public string VarcharColumn
		{
			get { return (string)this[ExampleTable.Columns.VarcharColumn]; }
			set { this[ExampleTable.Columns.VarcharColumn] = value; }
		}
		/// <summary>
		/// An example int column
		/// </summary>
		public int IntColumn
		{
			get { return (int)this[ExampleTable.Columns.IntColumn]; }
			set { this[ExampleTable.Columns.IntColumn] = value; }
		}
		/// <summary>
		/// An example int column with a default value of -1
		/// </summary>
		public int IntColumnWithDefaultOfMinusOne
		{
			get { return (int)this[ExampleTable.Columns.IntColumnWithDefaultOfMinusOne]; }
			set { this[ExampleTable.Columns.IntColumnWithDefaultOfMinusOne] = value; }
		}
		/// <summary>
		/// An example datetime column
		/// </summary>
		public DateTime DateTimeColumn
		{
			get { return (DateTime)this[ExampleTable.Columns.DateTimeColumn]; }
			set { this[ExampleTable.Columns.DateTimeColumn] = value; }
		}
		/// <summary>
		/// An example guid column
		/// </summary>
		public Guid	GuidColumn
		{
			get{ return Cambro.Misc.Db.GuidConvertor(this[ExampleTable.Columns.GuidColumn]);}		
			set{ this[ExampleTable.Columns.GuidColumn] = new System.Data.SqlTypes.SqlGuid(value);}
		}
		/// <summary>
		/// An example guid column with default of NEWID()
		/// </summary>
		public Guid	GuidColumnWithDefaultOfNewId
		{
			get{ return Cambro.Misc.Db.GuidConvertor(this[ExampleTable.Columns.GuidColumnWithDefaultOfNewId]);}		
			set{ this[ExampleTable.Columns.GuidColumnWithDefaultOfNewId] = new System.Data.SqlTypes.SqlGuid(value);}
		}
		/// <summary>
		/// An example bit column
		/// </summary>
		public bool BitColumn
		{
			get { return (bool)this[ExampleTable.Columns.BitColumn]; }
			set { this[ExampleTable.Columns.BitColumn] = value; }
		}
		/// <summary>
		/// An example bit column with a default of zero
		/// </summary>
		public bool BitColumnWithDefaultOfZero
		{
			get { return (bool)this[ExampleTable.Columns.BitColumnWithDefaultOfZero]; }
			set { this[ExampleTable.Columns.BitColumnWithDefaultOfZero] = value; }
		}
		/// <summary>
		/// An example bigint column
		/// </summary>
		public Int64 BigIntColumn
		{
			get { return (Int64)this[ExampleTable.Columns.BigIntColumn]; }
			set { this[ExampleTable.Columns.BigIntColumn] = value; }
		}
		/// <summary>
		/// An example bigint column with default of zero
		/// </summary>
		public Int64 BigIntColumnWithDefaultOfZero
		{
			get { return (Int64)this[ExampleTable.Columns.BigIntColumnWithDefaultOfZero]; }
			set { this[ExampleTable.Columns.BigIntColumnWithDefaultOfZero] = value; }
		}
		/// <summary>
		/// An example char column
		/// </summary>
		public string CharColumn
		{
			get { return (string)this[ExampleTable.Columns.CharColumn]; }
			set { this[ExampleTable.Columns.CharColumn] = value; }
		}
		/// <summary>
		/// An example float column
		/// </summary>
		public double FloatColumn
		{
			get { return (double)this[ExampleTable.Columns.FloatColumn]; }
			set { this[ExampleTable.Columns.FloatColumn] = value; }
		}
		/// <summary>
		/// An example NChar column
		/// </summary>
		public string NCharColumn
		{
			get { return (string)this[ExampleTable.Columns.NCharColumn]; }
			set { this[ExampleTable.Columns.NCharColumn] = value; }
		}
		/// <summary>
		/// An example ntext column
		/// </summary>
		public string NTextColumn
		{
			get { return (string)this[ExampleTable.Columns.NTextColumn]; }
			set { this[ExampleTable.Columns.NTextColumn] = value; }
		}
		/// <summary>
		/// An example nvarchar column
		/// </summary>
		public string NVarcharColumn
		{
			get { return (string)this[ExampleTable.Columns.NVarcharColumn]; }
			set { this[ExampleTable.Columns.NVarcharColumn] = value; }
		}
		/// <summary>
		/// An example small datetime column
		/// </summary>
		public DateTime SmallDateTimeColumn
		{
			get { return (DateTime)this[ExampleTable.Columns.SmallDateTimeColumn]; }
			set { this[ExampleTable.Columns.SmallDateTimeColumn] = value; }
		}
		/// <summary>
		/// An example small int column
		/// </summary>
		public int SmallIntColumn
		{
			get { return (int)this[ExampleTable.Columns.SmallIntColumn]; }
			set { this[ExampleTable.Columns.SmallIntColumn] = value; }
		}
		/// <summary>
		/// An example text column
		/// </summary>
		public string TextColumn
		{
			get { return (string)this[ExampleTable.Columns.TextColumn]; }
			set { this[ExampleTable.Columns.TextColumn] = value; }
		}
		/// <summary>
		/// An example timestamp column for RowVersion
		/// </summary>
		public byte[] RowVersion
		{
			get { return (byte[])this[ExampleTable.Columns.RowVersion]; }
		}
		/// <summary>
		/// an example tinyint column
		/// </summary>
		public byte TinyIntColumn
		{
			get { return (byte)this[ExampleTable.Columns.TinyIntColumn]; }
			set { this[ExampleTable.Columns.TinyIntColumn] = value; }
		}
		/// <summary>
		/// An example xml column
		/// </summary>
		public string XmlColumn
		{
			get { return (string)this[ExampleTable.Columns.XmlColumn]; }
			set { this[ExampleTable.Columns.XmlColumn] = value; }
		}
		/// <summary>
		/// An example varbinary column
		/// </summary>
		public System.Data.SqlTypes.SqlBinary VarBinaryColumn
		{
			get { return (System.Data.SqlTypes.SqlBinary)this[ExampleTable.Columns.VarBinaryColumn]; }
			set { this[ExampleTable.Columns.VarBinaryColumn] = value; }
		}
		/// <summary>
		/// An example varbinary column with a default of zero
		/// </summary>
		public System.Data.SqlTypes.SqlBinary VarBinaryColumnWithDefaultOfZero
		{
			get { return (System.Data.SqlTypes.SqlBinary)this[ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero]; }
			set { this[ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero] = value; }
		}
		/// <summary>
		/// An example sql_variant column
		/// </summary>
		public object SqlVariantColumn
		{
			get { return (object)this[ExampleTable.Columns.SqlVariantColumn]; }
			set { this[ExampleTable.Columns.SqlVariantColumn] = value; }
		}
		#endregion

	}
	#endregion

	*/
	#endregion
}

namespace Bobs.ChildInterfaces
{
	public interface IHasChildExampleTables
	{
		CachedSqlSelect<ExampleTable> ChildExampleTables();
		CachedSqlSelect<ExampleTable> ChildExampleTables(Q where);
		CachedSqlSelect<ExampleTable> ChildExampleTables(Dictionary<string, OrderBy.OrderDirection> orderBy);
		CachedSqlSelect<ExampleTable> ChildExampleTables(Q where, Dictionary<string, OrderBy.OrderDirection> orderBy);
		
		
	}
}
namespace Bobs
{
	public partial class ExampleTable 
		{
		}

	#region ExampleTableDataHolderTemplate
	/*
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct DataHolder class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.
	#region ExampleTableDataHolder
	/// <summary>
	/// An example tableDataHolder
	/// </summary>
	[Serializable]
	public partial class ExampleTableDataHolder : DataHolder<ExampleTable>
	{
		ExampleTable dataHolder;
		[NonSerializedAttribute] ExampleTable bob;

		public ExampleTableDataHolder()
		{
			this.dataHolder = new ExampleTable();
		}
		
		void CopyValues(ExampleTable source, ExampleTable destination)
		{
			destination[Bobs.ExampleTable.Columns.K] = source[Bobs.ExampleTable.Columns.K];
			destination[Bobs.ExampleTable.Columns.VarcharColumn] = source[Bobs.ExampleTable.Columns.VarcharColumn];
			destination[Bobs.ExampleTable.Columns.IntColumn] = source[Bobs.ExampleTable.Columns.IntColumn];
			destination[Bobs.ExampleTable.Columns.IntColumnWithDefaultOfMinusOne] = source[Bobs.ExampleTable.Columns.IntColumnWithDefaultOfMinusOne];
			destination[Bobs.ExampleTable.Columns.DateTimeColumn] = source[Bobs.ExampleTable.Columns.DateTimeColumn];
			destination[Bobs.ExampleTable.Columns.GuidColumn] = source[Bobs.ExampleTable.Columns.GuidColumn];
			destination[Bobs.ExampleTable.Columns.GuidColumnWithDefaultOfNewId] = source[Bobs.ExampleTable.Columns.GuidColumnWithDefaultOfNewId];
			destination[Bobs.ExampleTable.Columns.BitColumn] = source[Bobs.ExampleTable.Columns.BitColumn];
			destination[Bobs.ExampleTable.Columns.BitColumnWithDefaultOfZero] = source[Bobs.ExampleTable.Columns.BitColumnWithDefaultOfZero];
			destination[Bobs.ExampleTable.Columns.BigIntColumn] = source[Bobs.ExampleTable.Columns.BigIntColumn];
			destination[Bobs.ExampleTable.Columns.BigIntColumnWithDefaultOfZero] = source[Bobs.ExampleTable.Columns.BigIntColumnWithDefaultOfZero];
			destination[Bobs.ExampleTable.Columns.CharColumn] = source[Bobs.ExampleTable.Columns.CharColumn];
			destination[Bobs.ExampleTable.Columns.FloatColumn] = source[Bobs.ExampleTable.Columns.FloatColumn];
			destination[Bobs.ExampleTable.Columns.NCharColumn] = source[Bobs.ExampleTable.Columns.NCharColumn];
			destination[Bobs.ExampleTable.Columns.NTextColumn] = source[Bobs.ExampleTable.Columns.NTextColumn];
			destination[Bobs.ExampleTable.Columns.NVarcharColumn] = source[Bobs.ExampleTable.Columns.NVarcharColumn];
			destination[Bobs.ExampleTable.Columns.SmallDateTimeColumn] = source[Bobs.ExampleTable.Columns.SmallDateTimeColumn];
			destination[Bobs.ExampleTable.Columns.SmallIntColumn] = source[Bobs.ExampleTable.Columns.SmallIntColumn];
			destination[Bobs.ExampleTable.Columns.TextColumn] = source[Bobs.ExampleTable.Columns.TextColumn];
			destination[Bobs.ExampleTable.Columns.RowVersion] = source[Bobs.ExampleTable.Columns.RowVersion];
			destination[Bobs.ExampleTable.Columns.TinyIntColumn] = source[Bobs.ExampleTable.Columns.TinyIntColumn];
			destination[Bobs.ExampleTable.Columns.XmlColumn] = source[Bobs.ExampleTable.Columns.XmlColumn];
			destination[Bobs.ExampleTable.Columns.VarBinaryColumn] = source[Bobs.ExampleTable.Columns.VarBinaryColumn];
			destination[Bobs.ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero] = source[Bobs.ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero];
			destination[Bobs.ExampleTable.Columns.SqlVariantColumn] = source[Bobs.ExampleTable.Columns.SqlVariantColumn];
		}
		
		public ExampleTableDataHolder(ExampleTable bob) : this()
		{
			CopyValues(bob, this.dataHolder);
		}

		#region Simple members
		/// <summary>
		/// An example primary key column
		/// </summary>
		public int K
		{
			get { return dataHolder.K; }
			set { this.dataHolder.K = value; }
		}
		/// <summary>
		/// An example varchar column
		/// </summary>
		public string VarcharColumn
		{
			get { return dataHolder.VarcharColumn; }
			set { this.dataHolder.VarcharColumn = value; }
		}
		/// <summary>
		/// An example int column
		/// </summary>
		public int IntColumn
		{
			get { return dataHolder.IntColumn; }
			set { this.dataHolder.IntColumn = value; }
		}
		/// <summary>
		/// An example int column with a default value of -1
		/// </summary>
		public int IntColumnWithDefaultOfMinusOne
		{
			get { return dataHolder.IntColumnWithDefaultOfMinusOne; }
			set { this.dataHolder.IntColumnWithDefaultOfMinusOne = value; }
		}
		/// <summary>
		/// An example datetime column
		/// </summary>
		public DateTime DateTimeColumn
		{
			get { return dataHolder.DateTimeColumn; }
			set { this.dataHolder.DateTimeColumn = value; }
		}
		/// <summary>
		/// An example guid column
		/// </summary>
		public Guid GuidColumn
		{
			get { return dataHolder.GuidColumn; }
			set { this.dataHolder.GuidColumn = value; }
		}
		/// <summary>
		/// An example guid column with default of NEWID()
		/// </summary>
		public Guid GuidColumnWithDefaultOfNewId
		{
			get { return dataHolder.GuidColumnWithDefaultOfNewId; }
			set { this.dataHolder.GuidColumnWithDefaultOfNewId = value; }
		}
		/// <summary>
		/// An example bit column
		/// </summary>
		public bool BitColumn
		{
			get { return dataHolder.BitColumn; }
			set { this.dataHolder.BitColumn = value; }
		}
		/// <summary>
		/// An example bit column with a default of zero
		/// </summary>
		public bool BitColumnWithDefaultOfZero
		{
			get { return dataHolder.BitColumnWithDefaultOfZero; }
			set { this.dataHolder.BitColumnWithDefaultOfZero = value; }
		}
		/// <summary>
		/// An example bigint column
		/// </summary>
		public Int64 BigIntColumn
		{
			get { return dataHolder.BigIntColumn; }
			set { this.dataHolder.BigIntColumn = value; }
		}
		/// <summary>
		/// An example bigint column with default of zero
		/// </summary>
		public Int64 BigIntColumnWithDefaultOfZero
		{
			get { return dataHolder.BigIntColumnWithDefaultOfZero; }
			set { this.dataHolder.BigIntColumnWithDefaultOfZero = value; }
		}
		/// <summary>
		/// An example char column
		/// </summary>
		public string CharColumn
		{
			get { return dataHolder.CharColumn; }
			set { this.dataHolder.CharColumn = value; }
		}
		/// <summary>
		/// An example float column
		/// </summary>
		public double FloatColumn
		{
			get { return dataHolder.FloatColumn; }
			set { this.dataHolder.FloatColumn = value; }
		}
		/// <summary>
		/// An example NChar column
		/// </summary>
		public string NCharColumn
		{
			get { return dataHolder.NCharColumn; }
			set { this.dataHolder.NCharColumn = value; }
		}
		/// <summary>
		/// An example ntext column
		/// </summary>
		public string NTextColumn
		{
			get { return dataHolder.NTextColumn; }
			set { this.dataHolder.NTextColumn = value; }
		}
		/// <summary>
		/// An example nvarchar column
		/// </summary>
		public string NVarcharColumn
		{
			get { return dataHolder.NVarcharColumn; }
			set { this.dataHolder.NVarcharColumn = value; }
		}
		/// <summary>
		/// An example small datetime column
		/// </summary>
		public DateTime SmallDateTimeColumn
		{
			get { return dataHolder.SmallDateTimeColumn; }
			set { this.dataHolder.SmallDateTimeColumn = value; }
		}
		/// <summary>
		/// An example small int column
		/// </summary>
		public int SmallIntColumn
		{
			get { return dataHolder.SmallIntColumn; }
			set { this.dataHolder.SmallIntColumn = value; }
		}
		/// <summary>
		/// An example text column
		/// </summary>
		public string TextColumn
		{
			get { return dataHolder.TextColumn; }
			set { this.dataHolder.TextColumn = value; }
		}
		/// <summary>
		/// An example timestamp column for RowVersion
		/// </summary>
		public byte[] RowVersion
		{
			get { return dataHolder.RowVersion; }
			set { this.dataHolder.RowVersion = value; }
		}
		/// <summary>
		/// an example tinyint column
		/// </summary>
		public byte TinyIntColumn
		{
			get { return dataHolder.TinyIntColumn; }
			set { this.dataHolder.TinyIntColumn = value; }
		}
		/// <summary>
		/// An example xml column
		/// </summary>
		public string XmlColumn
		{
			get { return dataHolder.XmlColumn; }
			set { this.dataHolder.XmlColumn = value; }
		}
		/// <summary>
		/// An example varbinary column
		/// </summary>
		public System.Data.SqlTypes.SqlBinary VarBinaryColumn
		{
			get { return dataHolder.VarBinaryColumn; }
			set { this.dataHolder.VarBinaryColumn = value; }
		}
		/// <summary>
		/// An example varbinary column with a default of zero
		/// </summary>
		public System.Data.SqlTypes.SqlBinary VarBinaryColumnWithDefaultOfZero
		{
			get { return dataHolder.VarBinaryColumnWithDefaultOfZero; }
			set { this.dataHolder.VarBinaryColumnWithDefaultOfZero = value; }
		}
		/// <summary>
		/// An example sql_variant column
		/// </summary>
		public object SqlVariantColumn
		{
			get { return dataHolder.SqlVariantColumn; }
			set { this.dataHolder.SqlVariantColumn = value; }
		}
		#endregion
		public ExampleTable ExampleTable
		{
			get
			{
				if (bob == null || dataHolder.IsDirty())
				{
					if (K > 0)
					{
						bob = new ExampleTable(K);
					}
					else
					{
						bob = new ExampleTable();
					}
					CopyValues(this.dataHolder, bob);
				}
				return bob;
			}
		}
	}
	#endregion
	*/

	#endregion
	#region ExampleTableSet
	public partial class ExampleTableSet : BobSet, IEnumerable<ExampleTable>
	{
		public ExampleTableSet(Query query) : base(query) { }

		public ExampleTable this[int i]
		{
			get
			{
				if (BobCache[i] == null)
				{
					BobCache[i] = new ExampleTable(this);
					((ExampleTable)BobCache[i]).Initialise(DataSet.Tables[0].DefaultView[i].Row);
				}
				return (ExampleTable)BobCache[i];
			}
		}

		public override void InitTable()
		{
			this.Table = new ExampleTableTableDef();
		}

		public override object Current
		{
			get
			{
				return this[CurrentResultIndex];
			}
		}

		public override object GetFromIndex(int index)
		{
			return ((object)this[index]);
		}

		public List<ExampleTable> ToList()
		{
			List<ExampleTable> list = new List<ExampleTable>();
			foreach (ExampleTable item in (System.Collections.IEnumerable) this)
			{
				list.Add(item);
			}
			return list;
		}

		#region IEnumerable<ExampleTable> Members

		public new IEnumerator<ExampleTable> GetEnumerator()
		{
			return this.ToList().GetEnumerator();
		}

		#endregion

	}
	#endregion
	#region ExampleTableTableDef
	public class ExampleTableTableDef : TableDef
	{
		public override string TableName
		{
			get
			{
				return "ExampleTable";
			}
		}

		public override string TableCacheKey
		{
			get
			{
				return "F291F5BE";
			}
		}

		public override string ColumnName(object ColumnEnum)
		{
			return ExampleTable.GetColumnName((ExampleTable.Columns)ColumnEnum);
		}

		public override TablesEnum TableEnum
		{
			get
			{
				return TablesEnum.ExampleTable;
			}
		}

		public override bool HasSinglePrimaryKey
		{
			get
			{
				return true;
			}
		}

		public override object SinglePrimaryKey
		{
			get
			{
				return ExampleTable.Columns.K;
			}
		}

		#region InitColumn
		protected override void InitColumn(object ColumnEnum)
		{
			int i = (int)ColumnEnum;
			ExampleTable.Columns c = (ExampleTable.Columns)ColumnEnum;
			if (!IsSet[i])
			{
				switch (c)
				{
					case ExampleTable.Columns.K: ColumnStore[i] = new ColumnDef(c, "K", SqlDbType.Int, 4, SqlColumnFlag.PrimaryKey | SqlColumnFlag.AutoNumber); break;
					case ExampleTable.Columns.VarcharColumn: ColumnStore[i] = new ColumnDef(c, "VarcharColumn", SqlDbType.VarChar); break;
					case ExampleTable.Columns.IntColumn: ColumnStore[i] = new ColumnDef(c, "IntColumn", SqlDbType.Int); break;
					case ExampleTable.Columns.IntColumnWithDefaultOfMinusOne: ColumnStore[i] = new ColumnDef(c, "IntColumnWithDefaultOfMinusOne", SqlDbType.Int, (object)-1); break;
					case ExampleTable.Columns.DateTimeColumn: ColumnStore[i] = new ColumnDef(c, "DateTimeColumn", SqlDbType.DateTime, 16); break;
					case ExampleTable.Columns.GuidColumn: ColumnStore[i] = new ColumnDef(c, "GuidColumn", SqlDbType.UniqueIdentifier); break;
					case ExampleTable.Columns.GuidColumnWithDefaultOfNewId: ColumnStore[i] = new ColumnDef(c, "GuidColumnWithDefaultOfNewId", SqlDbType.UniqueIdentifier, (object)Guid.NewGuid()); break;
					case ExampleTable.Columns.BitColumn: ColumnStore[i] = new ColumnDef(c, "BitColumn", SqlDbType.Bit); break;
					case ExampleTable.Columns.BitColumnWithDefaultOfZero: ColumnStore[i] = new ColumnDef(c, "BitColumnWithDefaultOfZero", SqlDbType.Bit, (object)false); break;
					case ExampleTable.Columns.BigIntColumn: ColumnStore[i] = new ColumnDef(c, "BigIntColumn", SqlDbType.BigInt); break;
					case ExampleTable.Columns.BigIntColumnWithDefaultOfZero: ColumnStore[i] = new ColumnDef(c, "BigIntColumnWithDefaultOfZero", SqlDbType.BigInt, (Int64)0); break;
					case ExampleTable.Columns.CharColumn: ColumnStore[i] = new ColumnDef(c, "CharColumn", SqlDbType.Char); break;
					case ExampleTable.Columns.FloatColumn: ColumnStore[i] = new ColumnDef(c, "FloatColumn", SqlDbType.Float); break;
					case ExampleTable.Columns.NCharColumn: ColumnStore[i] = new ColumnDef(c, "NCharColumn", SqlDbType.NChar, 20); break;
					case ExampleTable.Columns.NTextColumn: ColumnStore[i] = new ColumnDef(c, "NTextColumn", SqlDbType.NText, 2147483646); break;
					case ExampleTable.Columns.NVarcharColumn: ColumnStore[i] = new ColumnDef(c, "NVarcharColumn", SqlDbType.NVarChar, 100); break;
					case ExampleTable.Columns.SmallDateTimeColumn: ColumnStore[i] = new ColumnDef(c, "SmallDateTimeColumn", SqlDbType.SmallDateTime, 16); break;
					case ExampleTable.Columns.SmallIntColumn: ColumnStore[i] = new ColumnDef(c, "SmallIntColumn", SqlDbType.SmallInt); break;
					case ExampleTable.Columns.TextColumn: ColumnStore[i] = new ColumnDef(c, "TextColumn", SqlDbType.Text); break;
					case ExampleTable.Columns.RowVersion: ColumnStore[i] = new ColumnDef(c, "RowVersion", SqlDbType.Timestamp); break;
					case ExampleTable.Columns.TinyIntColumn: ColumnStore[i] = new ColumnDef(c, "TinyIntColumn", SqlDbType.TinyInt); break;
					case ExampleTable.Columns.XmlColumn: ColumnStore[i] = new ColumnDef(c, "XmlColumn", SqlDbType.Xml, 2147483646); break;
					case ExampleTable.Columns.VarBinaryColumn: ColumnStore[i] = new ColumnDef(c, "VarBinaryColumn", SqlDbType.VarBinary, 2147483647); break;
					case ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero: ColumnStore[i] = new ColumnDef(c, "VarBinaryColumnWithDefaultOfZero", SqlDbType.VarBinary, 2147483647, (object)new System.Data.SqlTypes.SqlBinary(BitConverter.GetBytes(0))); break;
					case ExampleTable.Columns.SqlVariantColumn: ColumnStore[i] = new ColumnDef(c, "SqlVariantColumn", SqlDbType.Variant, 8000); break;
					default: throw new Exception("Column not found");
				}
				IsSet[i] = true;
			}
		}
		#endregion

		#region Columns
		public override ColumnData<ColumnDef> Columns
		{
			get
			{
				if (!IsAllSet)
				{
					InitColumn(ExampleTable.Columns.K);
					InitColumn(ExampleTable.Columns.VarcharColumn);
					InitColumn(ExampleTable.Columns.IntColumn);
					InitColumn(ExampleTable.Columns.IntColumnWithDefaultOfMinusOne);
					InitColumn(ExampleTable.Columns.DateTimeColumn);
					InitColumn(ExampleTable.Columns.GuidColumn);
					InitColumn(ExampleTable.Columns.GuidColumnWithDefaultOfNewId);
					InitColumn(ExampleTable.Columns.BitColumn);
					InitColumn(ExampleTable.Columns.BitColumnWithDefaultOfZero);
					InitColumn(ExampleTable.Columns.BigIntColumn);
					InitColumn(ExampleTable.Columns.BigIntColumnWithDefaultOfZero);
					InitColumn(ExampleTable.Columns.CharColumn);
					InitColumn(ExampleTable.Columns.FloatColumn);
					InitColumn(ExampleTable.Columns.NCharColumn);
					InitColumn(ExampleTable.Columns.NTextColumn);
					InitColumn(ExampleTable.Columns.NVarcharColumn);
					InitColumn(ExampleTable.Columns.SmallDateTimeColumn);
					InitColumn(ExampleTable.Columns.SmallIntColumn);
					InitColumn(ExampleTable.Columns.TextColumn);
					InitColumn(ExampleTable.Columns.RowVersion);
					InitColumn(ExampleTable.Columns.TinyIntColumn);
					InitColumn(ExampleTable.Columns.XmlColumn);
					InitColumn(ExampleTable.Columns.VarBinaryColumn);
					InitColumn(ExampleTable.Columns.VarBinaryColumnWithDefaultOfZero);
					InitColumn(ExampleTable.Columns.SqlVariantColumn);
					IsAllSet = true;
				}
				return ColumnStore;
			}
		}
		#endregion

	}
	#endregion

	#region stored procedures
	public static class StoredProcedures
	{
		public static class OuterClass
		{
			public static class InnerClass
			{
				public static void MethodName(int? parameter1, Guid? parameter2, DateTime? parameter3)
				{
					System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(global::Bobs.Vars.DefaultConnectionString);
					System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("[OuterClass.InnerClass.MethodName]", conn);
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@Parameter1", parameter1.HasValue ? (object)parameter1 : DBNull.Value);
					cmd.Parameters.AddWithValue("@Parameter2", parameter2.HasValue ? (object)parameter2 : DBNull.Value);
					cmd.Parameters.AddWithValue("@Parameter3", parameter3.HasValue ? (object)parameter3 : DBNull.Value);
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
	}
	#endregion

}

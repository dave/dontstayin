using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Bobs
{
	/// <summary>
	/// A TableDef object stores configuration data about the database table - e.g. name, primarary key, columns
	/// </summary>
	public abstract class TableDef : IEnumerable
	{
		#region TableDef()
		public TableDef()
		{
		}
		#endregion

		#region GetEnumerator()
		public IEnumerator GetEnumerator()
		{
			return Columns.GetEnumerator();
		}
		#endregion

		#region ColumnName
		/// <summary>
		/// Get a column name fron the column enum
		/// </summary>
		/// <param name="ColumnEnum">The enum of the column</param>
		/// <returns>The name of the column</returns>
		public abstract string ColumnName(object ColumnEnum);
		#endregion

		#region TableEnum
		/// <summary>
		/// This is the enum of the table
		/// </summary>
		public abstract TablesEnum TableEnum { get; }
		#endregion

		#region TableName
		/// <summary>
		/// Table name
		/// </summary>
		public abstract string TableName { get; }
		public abstract string TableCacheKey { get; }
		#endregion

		#region HasSinglePrimaryKey
		/// <summary>
		/// Does this table have a single primarary key?
		/// </summary>
		public abstract bool HasSinglePrimaryKey { get; }
		#endregion

		#region SinglePrimaryKey
		/// <summary>
		/// If the table has a single primarary key, this is it.
		/// </summary>
		public abstract object SinglePrimaryKey { get; }
		#endregion

		#region ColumnDef this[object ColumnEnum]
		/// <summary>
		/// Accesses column configuration data
		/// </summary>
		/// <param name="ColumnEnum">The column enum</param>
		/// <returns>A ColemnDef object - contains column config data - e.g. data type</returns>
		public ColumnDef this[object ColumnEnum]
		{
			get
			{
				//InitColumn(ColumnEnum);
				InitializeAllColumns();
				return ColumnStore[(int)ColumnEnum];
			}
		}
		#endregion

		#region ColumnData<ColumnDef> Columns
		/// <summary>
		/// This populates the entire column definition store, and returns it.
		/// </summary>
		public abstract ColumnData<ColumnDef> Columns { get; }
		#endregion

		#region InitColumn
		/// <summary>
		/// This initialises a specifierd column definition object
		/// </summary>
		/// <param name="ColumnEnum">The column enum</param>
		protected abstract void InitColumn(object ColumnEnum);
		#endregion

		protected abstract void InitializeAllColumns();

		#region ColumnStore
		/// <summary>
		/// This is the store where the column configuration objects are kept
		/// </summary>
		protected ColumnData<ColumnDef> ColumnStore
		{
			get
			{
				if (columnStore == null)
					columnStore = new ColumnData<ColumnDef>();
				return columnStore;
			}
		}
		private ColumnData<ColumnDef> columnStore;
		#endregion

		#region IsSet
		/// <summary>
		/// This store tracks which column configuration objects have been initialised already
		/// </summary>
		protected ColumnData<bool> IsSet
		{
			get
			{
				if (isSet == null)
					isSet = new ColumnData<bool>();
				return isSet;
			}
		}
		private ColumnData<bool> isSet;
		#endregion

		#region IsAllSet
		/// <summary>
		/// This determines if all columns config objects have been initialised (this happens when the Columns member is accessed)
		/// </summary>
		protected bool IsAllSet;
		#endregion

	}
}

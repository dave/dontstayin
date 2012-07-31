using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.DataHolders
{
	[Serializable]
	public abstract class DataHolder<T> where T : IBob
	{
		protected T dataHolder;
		public DataHolderState State { get; set; }
		#region this[object ColumnEnum]
		internal protected object this[object columnEnum]
		{
			get { return dataHolder[columnEnum]; }
			set { dataHolder[columnEnum] = value; }
		}
		#endregion
	}
	public enum DataHolderState
	{
		Unchanged = 0,
		Modified = 1,
		Added = 2,
		Deleted = 3
	}

}

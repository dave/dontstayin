using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using DataInterface;

namespace LinqToSql.Classes
{
	public class ModelToLinqTable<T, U> : ITable<T> where U : class, T 
	{
		private readonly Table<U> table;

		public ModelToLinqTable(Table<U> table)
		{
			this.table = table;
		}


 

		void ITable<T>.InsertOnSubmit(T item)
		{
			this.table.InsertOnSubmit((U) item);
		}

		void ITable<T>.DeleteOnSubmit(T item)
		{
			this.table.DeleteOnSubmit((U) item);
		}

 

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.table.Cast<T>().GetEnumerator();
		}

	 

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((ITable<T>) this).GetEnumerator();
		}

	 

		Type IQueryable.ElementType
		{
			get { return this.table.Cast<T>().ElementType; }
		}

		System.Linq.Expressions.Expression IQueryable.Expression
		{
			get { return this.table.Cast<T>().Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return this.table.Cast<T>().Provider; }
		}
 
	}
}

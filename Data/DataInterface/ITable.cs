using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInterface
{
	public interface ITable<T> : IQueryable<T>
	{
		void InsertOnSubmit(T item);
		void DeleteOnSubmit(T item);
	}
}

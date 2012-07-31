using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common;
using Caching;

namespace Bobs.CachedDataAccess
{
	public interface ICachedSqlSelectDefinition<T>
	{
		Bobs.TablesEnum Table { get; }
		string TableHash { get; }
		SqlCommand SelectCommand { get; }
		IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> OrderBy { get; }
		Getter<T, DataRow> CreateTFromDataRow { get; }
		CacheKey CacheKey { get; }
		//Q WhereClause { get; }
	}
}

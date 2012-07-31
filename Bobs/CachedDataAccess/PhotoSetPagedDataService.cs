using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.Data.SqlClient;
using System.Data;
using Common;
using Caching;

namespace Bobs.CachedDataAccess
{
	public class PhotoSetPagedDataService : IPagedDataService<Photo>
	{
		private Query query;
		public PhotoSetPagedDataService(Query query)
		{
			this.query = query;
		}

		#region IPagedDataService<Photo> Members

		private int? count;
		public int Count
		{
			get
			{
				if (!count .HasValue)
				{
					Query q = new Query
					{
						QueryCondition = query.QueryCondition,
						ReturnCountOnly = true
					};
					count = new PhotoSet(q).Count;
				}

				return count.Value;
			}
		}

		public Photo[] Page(int pageNumber, int pageSize)
		{
			query.Paging.RequestedPage = pageNumber;
			query.Paging.RecordsPerPage = pageSize;
			return new PhotoSet(query).ToList().ToArray();
		}

		#endregion
	}
}

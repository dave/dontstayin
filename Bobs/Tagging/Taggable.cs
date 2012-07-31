using System;
using System.Collections.Generic;
using System.Text;
using Bobs.CachedDataAccess;

namespace Bobs.Tagging
{
	[Serializable]
	public class Taggable : ITaggable
	{
		public int K { get; private set; }
		public int ItemOwnerUsrK { get; private set; }
		public Taggable(int K, int creatorUsrK)
		{
			this.K = K;
			this.ItemOwnerUsrK = creatorUsrK;
		}

		#region IHasChildTags Members

		public CachedSqlSelect<Tag> ChildTags()
		{
			Photo photo = new Photo(K);
			return photo.ChildTags();
		}

		public CachedSqlSelect<Tag> ChildTags(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			Photo photo = new Photo(K);
			return photo.ChildTags(orderBy);
		}

		public CachedSqlSelect<Tag> ChildTags(Q where)
		{
			Photo photo = new Photo(K);
			return photo.ChildTags(where);
		}

		public CachedSqlSelect<Tag> ChildTags(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			Photo photo = new Photo(K);
			return photo.ChildTags(where, orderBy);
		}

		#endregion
	}
}

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
	public class TagSearchCachedSqlSelectDefinition : ICachedSqlSelectDefinition<Bobs.Photo>
	{
		IEnumerable<string> TagsQueryParts { get; set; }
		public TagSearchCachedSqlSelectDefinition(IEnumerable<string> tagsQueryParts)
		{
			this.TagsQueryParts = tagsQueryParts;
			this.Table = TablesEnum.Photo;
			this.TableHash = new PhotoTableDef().TableCacheKey;
			this.OrderBy = new Dictionary<object, OrderBy.OrderDirection>()
				{
					{Photo.Columns.DateTime, Bobs.OrderBy.OrderDirection.Descending}
				};
		}
		public TablesEnum Table { get; private set; }
		public string TableHash { get; private set; }

		SqlCommand ICachedSqlSelectDefinition<Photo>.SelectCommand
		{
			get
			{
				SqlCommand cmd = new SqlCommand();
				StringBuilder sql = new StringBuilder();
				sql.AppendFormat("SELECT {0}.* FROM {0} WITH (NOLOCK) WHERE 1=1 ",
					Tables.GetTableName(TablesEnum.Photo)
				);
				int counter = 0;
				bool hasParts = false;
				foreach (var queryPart in TagsQueryParts)
				{
					Tag tag = Tag.GetTag(queryPart);
					if (!tag.Blocked)
					{
						if (hasParts == false) { hasParts = true; }
						sql.AppendFormat(
							" AND [{5}] IN (SELECT [{0}] FROM [{1}] WITH (NOLOCK) WHERE [{2}] = @TagK{3} AND {1}.{4} = 0)",
							Tables.GetColumnName(Bobs.TagPhoto.Columns.PhotoK),
							Tables.GetTableName(Bobs.TablesEnum.TagPhoto),
							Tables.GetColumnName(Bobs.TagPhoto.Columns.TagK),
							counter,
							Tables.GetColumnName(Bobs.TagPhoto.Columns.Disabled),
							Tables.GetColumnName(Bobs.Photo.Columns.K));
						cmd.Parameters.AddWithValue("@TagK" + counter.ToString(), tag.K);
					}
					counter++;
				}
				if (!hasParts)
				{
					sql.AppendFormat(" AND 1=0");
				}
				cmd.CommandText = sql.ToString();
				return cmd;
			}
		}
	 
 


		Getter<Bobs.Photo, DataRow> ICachedSqlSelectDefinition<Photo>.CreateTFromDataRow
		{
			get
			{
				return dr =>
				{
					Photo newPhoto = new Photo();
					newPhoto.Initialise(dr);
					return newPhoto;
				};
			}
		}


		CacheKey ICachedSqlSelectDefinition<Photo>.CacheKey
		{
			get 
			{
				
				List<CacheKey> namespaceCacheKeys = new List<CacheKey>();
				List<string> cacheKeyParts = new List<string>();

				foreach (var tagQueryPart in this.TagsQueryParts)
				{
					Tag tag = Tag.GetTag(tagQueryPart);
					namespaceCacheKeys.Add(Caching.CacheKeys.Tag.TagPhotos(tag.K));
					namespaceCacheKeys.Add(new Caching.CacheKeys.BobChildFieldVersion(Tables.GetTableName(TablesEnum.Tag), tag.K, Tables.Defs.TagPhoto.TableName, Tables.Defs.TagPhoto.TableCacheKey, Tables.GetColumnName(TagPhoto.Columns.Disabled)));
				}
				
		        return new NamespacedCacheKey
		        (
		            CacheKeyPrefix.PhotosForTagQuery,
					namespaceCacheKeys.ToArray(),
					cacheKeyParts.ToArray()
				);
			}
		}

		public IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> OrderBy { get; private set; }
		public Q WhereClause { get; set; }

	}
}

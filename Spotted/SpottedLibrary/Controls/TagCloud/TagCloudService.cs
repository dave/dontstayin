using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Caching;
using System.Linq;

namespace SpottedLibrary.Controls.TagCloud
{
	class TagCloudService
	{
		internal List<KeyValuePair<ILinkable, int>> GetTags(int numberOfItems)
		{
			Random r = new Random();
			return Caching.Instances.Main.GetWithLocalCaching(new NamespacedCacheKey(CacheKeyPrefix.TagCloudData, new CacheKey(CacheKeyPrefix.TagCloudVersion), r.Next(10).ToString(), numberOfItems.ToString()),
				() =>
				{
					List<int> tagKs = GetTagKs(numberOfItems);
			


					Query getTags = new Query(new And(new Q(new Column(Tag.Columns.K), tagKs.ToArray())));
					getTags.TableElement = new Join(new TableElement(Bobs.TablesEnum.TagPhoto), Tag.Columns.K, TagPhoto.Columns.TagK);
					getTags.Columns = new ColumnSet(Tag.Columns.TagText, Tag.Columns.ShowInTagCloud, Tag.Columns.K, Tag.Columns.Blocked, Tag.Columns.BlockedByUsrK, Tag.Columns.BlockedDateTime);
					getTags.ExtraSelectElements.Add("Frequency", "CAST(CEILING(LOG(COUNT(" + new Column(TagPhoto.Columns.TagK).InternalSqlName + "))+1) AS INT)");
					getTags.GroupBy = new GroupBy(new GroupBy(Tag.Columns.K), new GroupBy(Tag.Columns.TagText), new GroupBy(Tag.Columns.Blocked), new GroupBy(Tag.Columns.BlockedByUsrK), new GroupBy(Tag.Columns.BlockedDateTime), new GroupBy(Tag.Columns.ShowInTagCloud));
					getTags.OrderBy = new OrderBy(Tag.Columns.TagText, OrderBy.OrderDirection.Ascending);

					List<Tag> tags = new TagSet(getTags).ToList();
					return tags.ToList().ConvertAll(t => new KeyValuePair<ILinkable, int>(t, (int)t.ExtraSelectElements["Frequency"]));
				},
				new TimeSpan(0, 0, 30),
				new TimeSpan(1, 0, 0)
			);


		}

		private List<int> GetTagKs(int numberOfItems)
		{
			Query q = new Query(
				new Q(Tag.Columns.Blocked, false),
				new Q(TagPhoto.Columns.Disabled, false),
				new Q(Tag.Columns.ShowInTagCloud, true)
			);
			q.Columns = new ColumnSet(new Column(Tag.Columns.K));
			q.DistinctColumn = new Column(Tag.Columns.K);
			q.TableElement = new Join(new TableElement(Bobs.TablesEnum.TagPhoto), Tag.Columns.K, TagPhoto.Columns.TagK);
			q.TopRecords = numberOfItems;
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			return new TagSet(q).ToList().ConvertAll(t => t.K);
		}
	}
}

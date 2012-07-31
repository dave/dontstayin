using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Admin.RecentlyEndedBanners
{
	public class RecentlyEndedBannersService
	{
		internal List<Bobs.Banner> GetBannersCompletedBetween(DateTime firstDate, DateTime secondDate)
		{
			if (firstDate > secondDate)
			{
				firstDate.SwitchValueWith(ref secondDate);
			}
			Query query = new Query(new Bobs.And(
											new Q(Bobs.Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, firstDate),
											new Q(Bobs.Banner.Columns.LastDay, QueryOperator.LessThan, secondDate),
											new Q(Bobs.Banner.Columns.TotalRequiredImpressions, QueryOperator.GreaterThan, 0)
											)
									);
			query.OrderBy = new OrderBy(Bobs.Banner.Columns.LastDay, OrderBy.OrderDirection.Descending );
			return new BannerSet(query).ToList();
		}
	}
}

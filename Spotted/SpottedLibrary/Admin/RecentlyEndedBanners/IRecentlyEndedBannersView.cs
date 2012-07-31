using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.RecentlyEndedBanners
{
	public interface IRecentlyEndedBannersView : IView
	{
		List<Bobs.Banner> Banners { set; }
		DateTime FirstDate { get; set; }
		DateTime SecondDate { get; set; }
		event EventHandler DateRangeChanged;
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.BannerImpressionStats
{
	public interface IBannerImpressionStatsView : IView
	{
		System.Data.DataTable Stats { set; }
		DateTime FirstDate { get; set; }
		DateTime SecondDate { get; set; }
		event EventHandler DateRangeChanged;
	}
}

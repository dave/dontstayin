using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.BannerImpressionStats
{
	public class BannerImpressionStatsController
	{
		IBannerImpressionStatsView view;
		BannerImpressionStatsService service;
		public BannerImpressionStatsController(IBannerImpressionStatsView view, BannerImpressionStatsService service)
		{
			this.view = view;
			this.view.DateRangeChanged += new EventHandler(view_DateRangeChanged);
			this.view.Load += new EventHandler(view_Load);
			this.service = service;
		}

		void view_Load(object sender, EventArgs e)
		{
			if (!view.IsPostBack)
			{
				view.FirstDate = Common.Time.Today.AddDays(-7);
				view.SecondDate = Common.Time.Today;
				view.Stats = service.GetImpressionStatsBetween(view.FirstDate, view.SecondDate);
				view.DataBind();
			}
		}

		void view_DateRangeChanged(object sender, EventArgs e)
		{
			view.Stats = service.GetImpressionStatsBetween(view.FirstDate, view.SecondDate);
			view.DataBind();
		}
	}
}

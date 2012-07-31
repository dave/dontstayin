using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.RecentlyEndedBanners
{
	public class RecentlyEndedBannersController
	{
		IRecentlyEndedBannersView view;
		RecentlyEndedBannersService service;
		public RecentlyEndedBannersController(IRecentlyEndedBannersView view, RecentlyEndedBannersService service)
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
				view.SecondDate = DateTime.Today;
				view.FirstDate = DateTime.Today.AddMonths(-1);
				view.Banners = service.GetBannersCompletedBetween(view.FirstDate, view.SecondDate);
				view.DataBind();
			}
		}

		void view_DateRangeChanged(object sender, EventArgs e)
		{
			view.Banners = service.GetBannersCompletedBetween(view.FirstDate, view.SecondDate);
			view.DataBind();
		}
	}
}

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpottedLibrary.Admin;
using SpottedLibrary.Admin.RecentlyEndedBanners;

namespace Spotted.Admin
{
	public partial class RecentlyEndedBanners : AdminUserControl, IRecentlyEndedBannersView
	{
		RecentlyEndedBannersController controller;
		public RecentlyEndedBanners()
		{
			controller = new RecentlyEndedBannersController(this, new RecentlyEndedBannersService());
		}

		#region IRecentlyEndedBannersView Members
		
		public DateTime FirstDate
		{
			get { return this.uiFirstDate.Date; }
			set { this.uiFirstDate.Date = value; }
		}
		public DateTime SecondDate
		{
			get { return this.uiSecondDate.Date; }
			set { this.uiSecondDate.Date = value; }
		}
		public System.Collections.Generic.List<Bobs.Banner> Banners
		{
			set { this.uiBanners.DataSource = value; }
		}

		public event EventHandler DateRangeChanged;

		#endregion

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.uiChangeDateRange.Click += new EventHandler(uiChangeDateRange_Click);
			
		}

		void uiChangeDateRange_Click(object sender, EventArgs e)
		{
			if (this.DateRangeChanged != null) { this.DateRangeChanged(this, EventArgs.Empty); }
		}



		#region IView Members


		public bool IsValid
		{
			get { return this.IsValid; }
		}

		#endregion
	}
}

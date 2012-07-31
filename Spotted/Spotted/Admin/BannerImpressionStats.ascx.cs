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
using SpottedLibrary.Admin.BannerImpressionStats;

namespace Spotted.Admin
{
	public partial class BannerImpressionStats : AdminUserControl, IBannerImpressionStatsView
	{
		BannerImpressionStatsController controller;
		public BannerImpressionStats()
		{
			controller = new BannerImpressionStatsController(this, new BannerImpressionStatsService());
		}

		#region IBannerImpressionStatsView Members

		public DataTable Stats
		{
			set { GridView.DataSource = value; }
		}

		public DateTime FirstDate
		{
			get { return uiFirstDate.Date; }
			set { uiFirstDate.Date = value; }
		}

		public DateTime SecondDate
		{
			get { return uiSecondDate.Date; }
			set { uiSecondDate.Date = value; }
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

		private string currentDate = "";
		protected void GridView_RowDataBound(object o, GridViewRowEventArgs e)
		{
			// only show date for first item in every batch of positions
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.Cells[0].Text != currentDate)
				{
					currentDate = e.Row.Cells[0].Text;
				}
				else
				{
					e.Row.Cells[0].Text = "";
				}
			}
		}
	}
}

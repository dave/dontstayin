using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class FindHotel : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string placeName = Request.QueryString["place"];
			string date = Request.QueryString["date"];
			int source = int.Parse(Request.QueryString["source"]);

			this.LogLinkClick((Model.Entities.Event.HotelLinkSources)source);
			Response.Redirect("http://www.laterooms.com/en/p4852/Hotels.aspx?k=" + placeName + "&d=" + date + "&mapType=1");
		}

		private void LogLinkClick(Model.Entities.Event.HotelLinkSources source)
		{
			switch (source)
			{
				case Model.Entities.Event.HotelLinkSources.Icon:
					Log.Increment(Model.Entities.Log.Items.HotelLinkIconClicked);
					break;
				case Model.Entities.Event.HotelLinkSources.Banner:
					Log.Increment(Model.Entities.Log.Items.HotelLinkBannerClicked);
					break;
				default:
					SpottedException.TryToSaveExceptionAndChildExceptions(new Exception("Hotel link source unknown!"));
					break;
			}
		}
	}
}

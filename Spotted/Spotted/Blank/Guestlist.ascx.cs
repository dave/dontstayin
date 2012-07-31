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
using Bobs;

namespace Spotted.Blank
{
	public partial class Guestlist : BlankUserControl
	{
		protected DataList GuestlistDataList;
		protected Label EventLabel, PriceLabel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Event ev = new Event(int.Parse(Request.QueryString["EventK"]));
			if (!Usr.Current.IsAdmin)
			{
				if (!Usr.Current.IsPromoterK(ev.GuestlistPromoterK))
					throw new DsiUserFriendlyException("You're not the promoter of this event!");
				if (!ev.HasGuestlist)
					throw new DsiUserFriendlyException("Event doesn't have guestlist!");
				if (!ev.GuestlistFinished)
					throw new DsiUserFriendlyException("Guestlist isn't closed yet!");
			}

			EventLabel.Text = ev.Name + " @ " + ev.Venue.FriendlyName + "<br>" + ev.DateTime.ToString("dd MMMMM yyyy");
			PriceLabel.Text = "Entry price for this guestlist: " + ev.GuestlistPrice.ToString("£0.##") + ".";
			if (ev.GuestlistDetails.Length > 0)
				PriceLabel.Text += "<br>Additional details: " + ev.GuestlistDetails;


			GuestlistDataList.DataSource = ev.GuestlistUsrs;
			if (Request.QueryString["Type"] != null && Request.QueryString["Type"].Equals("1"))
				GuestlistDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/GuestlistNoPic.ascx");
			else
				GuestlistDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Guestlist.ascx");
			GuestlistDataList.DataBind();
		}
	}
}
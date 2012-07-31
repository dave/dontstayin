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
using System.Data.SqlClient;
using Spotted.Controls;

namespace Spotted.Pages.Venues
{
	public partial class Merge : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsSuper)
				throw new Exception("You aren't an admin!");

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
					ChangePanel(PanelMerge);
			}

			MergeButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			if (Mode.Equals(Modes.None))
			{
			}
			ContainerPage.UseLeftHandSideForContent = true;
		}

		#region PanelMerge
		protected Picker uiMasterVenuePicker, uiMergeVenuePicker;
		public void Merge_Click(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsSuper)
				throw new Exception("You aren't an admin!");

			Venue master = this.uiMasterVenuePicker.Venue;
			Venue merge = this.uiMergeVenuePicker.Venue;

			if (!Usr.Current.IsAdmin && merge.TotalEvents>50)
			{
				throw new DsiUserFriendlyException("Venue to merge has too many events... Admin can merge these venues. Contact admin.");
			}

			//foreach (Event ev in merge.Events)
			//{
			//    if (ev.TicketRuns.Count > 0)
			//        throw new DsiUserFriendlyException("Cannot merge venue: " + merge.FriendlyName + " because at least one of its events has a ticket run.");
			//}

			if (
				master.PromoterK > 0 &&
				master.Promoter.IsEnabled &&
				master.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed) &&
				merge.PromoterK > 0 &&
				merge.Promoter.IsEnabled &&
				merge.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed) &&
				master.PromoterK != merge.PromoterK)
				throw new Exception("Can't merge these venues - they are controlled by different promoters - contact admin.");
			
			master.MergeAndDelete(merge);

			Cambro.Web.Helpers.WriteAlertFooter(master.Url());
		}
	 
		 
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelMerge.Visible = p.Equals(PanelMerge);
		}
		#endregion

		 
	}
}

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

namespace Spotted.Pages.Events
{
	public partial class Merge : DsiUserControl
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.ContainerPage.UseLeftHandSideForContent = true;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (!Usr.Current.IsSuper)
				throw new DsiUserFriendlyException("You aren't an admin!");

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
					ChangePanel(PanelMerge);
			}

			MergeButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			if (Mode.Equals(Modes.None))
			{
			}
			//this.uiMasterEventAutoComplete.Parameters.Add("showK", "true");
			//this.uiMergeEventAutoComplete.Parameters.Add("showK", "true");
		}

		#region PanelMerge 
		public void Merge_Click(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (!Usr.Current.IsSuper)
				throw new DsiUserFriendlyException("You aren't an admin!");

			Event master = this.uiMainEventFinder.Event;
			Event merge = this.uiMergeEventFinder.Event;


			//if(merge.TicketRuns.Count > 0)
			//    throw new DsiUserFriendlyException("Cannot merge event: " + merge.FriendlyName + " because it has at least one ticket run.");

			ArrayList promotersMaster = new ArrayList();
			foreach (Brand b in master.Brands)
			{
				if (b.PromoterK > 0 && b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed) && !promotersMaster.Contains(b.PromoterK))
					promotersMaster.Add(b.PromoterK);
			}
			if (promotersMaster.Count > 0)
			{
				foreach (Brand b in merge.Brands)
				{
					if (b.PromoterK > 0 && b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed) && !promotersMaster.Contains(b.PromoterK))
						throw new DsiUserFriendlyException("Can't merge these events - they are controlled by different promoters. Contact admin.");
				}
			}

			if (master.HasGuestlist && merge.HasGuestlist)
				throw new DsiUserFriendlyException("Can't merge these events - they both have guestlists - contact admin!");


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

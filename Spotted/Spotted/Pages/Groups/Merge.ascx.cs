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

namespace Spotted.Pages.Groups
{
	public partial class Merge : DsiUserControl
	{
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
			ContainerPage.UseLeftHandSideForContent = true;

		}

		#region PanelMerge
		public void Merge_Click(object o, System.EventArgs e)
		{
			Group master = new Group(int.Parse(this.uiMasterGroupAutoComplete.Value));
			Group merge = new Group(int.Parse(this.uiMergeGroupAutoComplete.Value));

			if (merge.BrandK > 0)
				throw new DsiUserFriendlyException("can't merge a brand group.");

			if (master.PrivateChat || master.PrivateGroupPage || master.PrivateMemberList ||
				merge.PrivateChat || merge.PrivateGroupPage || merge.PrivateMemberList)
				throw new DsiUserFriendlyException("Can't merge private groups. Make sure both groups are FULLY public before merging.");

			master.MergeAndDelete(merge);

			Cambro.Web.Helpers.WriteAlertFooter(master.Url());
		}
		private void PanelMerge_Load(object sender, System.EventArgs e)
		{
			MergeButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			if (Mode.Equals(Modes.None))
			{
			}
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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.PanelMerge_Load);

		}
		#endregion
	}
}

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

namespace Spotted.Pages.Brands
{
	public partial class Edit : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must log in to use this page!");
			if (!Usr.Current.CanEdit(CurrentBrand))
				throw new DsiUserFriendlyException("You can't edit this brand!");
			Manage_Load(sender, e);
			if (!IsPostBack)
			{
				this.uiManageGotoAutoComplete.Parameters.Add("showK", "true");
				this.uiManageGotoAutoComplete.Parameters.Add("ShowPromoters", "true");
			}
		}

		#region Manage

		public void Manage_Load(object o, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Manage brand - " + CurrentBrand.Name);

			ManageBrandAnchor.InnerText = CurrentBrand.Name;
			ManageBrandAnchor.HRef = CurrentBrand.Url();

			ManagePic.InputObject = CurrentBrand;
			if (!Page.IsPostBack)
			{
				ManagePic.InitPic();
				ManagePicUploadPanel.Visible = true;
			}

			SuperAdminOptions.Visible = Usr.Current.IsSuper;
			if (Usr.Current.IsSuper)
			{
				ManageDeleteButton.Attributes["onclick"] = "return confirm('Delete THIS brand?');";
				if (!Page.IsPostBack)
					ManageNameTextBox.Text = CurrentBrand.Name;
			}
		}
		protected HtmlGenericControl RenameError;
		public void ManageNameSave(object o, System.EventArgs e)
		{
			if (!Usr.Current.CanEdit(CurrentBrand))
				throw new DsiUserFriendlyException("You can't edit this brand!");
			string newName = Cambro.Web.Helpers.StripHtml(ManageNameTextBox.Text.Trim());

			//is name duplicate?
			BrandSet bs = new BrandSet(new Query(new Q(Brand.Columns.Name, newName)));
			if (bs.Count > 0)
			{
				RenameError.Visible = true;
			}
			else
			{

				CurrentBrand.Name = newName;
				if (!Usr.Current.IsSuper)
					CurrentBrand.IsEdited = true;

				CurrentBrand.Update();
				CurrentBrand.CreateUniqueUrlName(true);
				CurrentBrand.Group.Name = CurrentBrand.Name;
				CurrentBrand.Group.UrlName = "parties/" + CurrentBrand.UrlName;
				CurrentBrand.Group.Update();

				RedirectDone();
			}
		}
		protected void ManagePicNoPic(object o, EventArgs e)
		{
			Bobs.Utilities.DeletePic(CurrentBrand.Group);
			RedirectDone();
		}
		protected void ManagePicSaved(object o, EventArgs e)
		{
			Bobs.Utilities.CopyPic(CurrentBrand, CurrentBrand.Group);
			if (!Usr.Current.IsSuper)
			{
				CurrentBrand.IsEdited = true;
				CurrentBrand.Update();
			}
			RedirectDone();
		}

		public void ManageDeleteClick(object o, System.EventArgs e)
		{
			if (!Usr.Current.IsSuper)
				throw new DsiUserFriendlyException("Only super admin!");
			if (CurrentBrand.Promoter != null && CurrentBrand.Promoter.IsEnabled && CurrentBrand.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed))
			{
				throw new DsiUserFriendlyException("Can't delete this brand - it is owned by promoter " + CurrentBrand.Promoter.K.ToString() + " - " + CurrentBrand.Promoter.Name);
			}
			if (CurrentBrand.Group.TotalComments > 0 || CurrentBrand.Group.TotalMembers > 0)
				throw new DsiUserFriendlyException("Can't delete this brand - it has an active group associated with it. Try merging it with another brand...");
			Delete.DeleteAll(CurrentBrand);
			ManageDeleteDone.Visible = true;
		}

		public void ManageGotoClick(object o, System.EventArgs e)
		{
			if (!Usr.Current.IsSuper)
				throw new Exception("Only super admin!");
			Brand gotoTarget = new Brand(int.Parse(this.uiManageGotoAutoComplete.Value));
			Response.Redirect(gotoTarget.Url());
		}

		#endregion

		public void RedirectDone()
		{
			if (ContainerPage.Url["promoterk"].IsInt && ContainerPage.Url["promoterk"] > 0)
			{
				Promoter p = new Promoter(ContainerPage.Url["promoterk"]);
				if (Usr.Current.IsAdmin || Usr.Current.IsPromoterK(ContainerPage.Url["promoterk"]))
					Response.Redirect(p.Url());
				else
					throw new DsiUserFriendlyException("Can't redirect to this promoter!");
			}
			else
				Response.Redirect(CurrentBrand.Url());
		}

		#region BrandK
		int BrandK
		{
			get
			{
				return CurrentBrand.K;
			}
		}
		#endregion
		#region CurrentBrand
		public Brand CurrentBrand
		{
			get
			{
				return ContainerPage.Url.ObjectFilterBrand;
			}
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
			this.Load += new System.EventHandler(this.Manage_Load);
		}
		#endregion
	}
}

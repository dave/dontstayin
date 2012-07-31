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
using Microsoft.JScript;

namespace Spotted.Blank
{
	public partial class NewBrand : BlankUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			
			if (!Page.IsPostBack)
				ViewState["BrandDuplicateGuid"] = Guid.NewGuid();

			if (ContainerPage.Url["K"].Exists)
			{
				if (!Usr.Current.IsSuper && Usr.Current.K != CurrentBrand.OwnerUsrK)
					throw new DsiUserFriendlyException("You may not edit this brand");
				Pic.InputObject = CurrentBrand;
				if (!Page.IsPostBack)
				{
					Pic.InitPic();
					PicUploadPanel.Visible = true;
					ChangePanel(PanelPic);
				}
			}
			else
				ChangePanel(PanelName);

			this.DataBind();



		}
		protected string NewBrandParams
		{
			get
			{
				if (CurrentBrand != null)
				{
					return CurrentBrand.K.ToString() + ",'" + GlobalObject.escape(CurrentBrand.Name) + "'";
				}
				else
					return "";
			}
		}

		#region PanelName
		public TextBox NameTextBox;
		public Panel PanelName;
		public void PanelNameNext(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Guid DuplicateGuid = (Guid)ViewState["BrandDuplicateGuid"];
				BrandSet bs = new BrandSet(new Query(new Q(Brand.Columns.DuplicateGuid, DuplicateGuid)));
				if (bs.Count > 0)
					Response.Redirect("/popup/newbrand/k-" + bs[0].K.ToString());
				else
				{
					Brand b = new Brand();
					b.DateTimeCreated = DateTime.Now;
					b.Name = Cambro.Web.Helpers.StripHtml(NameTextBox.Text.Trim());
					b.IsNew = true;
					b.OwnerUsrK = Usr.Current.K;
					b.IsEdited = false;
					b.DuplicateGuid = DuplicateGuid;
					b.Update();
					b.CreateUniqueUrlName(false);

					//create a new group
					Group g = new Group();
					g.Name = b.Name;
					g.ThemeK = 1;
					g.Description = b.Name + " regulars group for discussing " + b.Name + " parties";
					g.PostingRules = "Discussions about " + b.Name + " parties only. Other topics may be deleted by group moderators.";
					g.DateTimeCreated = DateTime.Now;
					g.PrivateGroupPage = false;
					g.PrivateChat = false;
					g.PrivateMemberList = false;
					g.Restriction = Bobs.Group.RestrictionEnum.None;
					g.CountryK = 0;
					g.PlaceK = 0;
					g.MusicTypeK = 0;
					g.BrandK = b.K;
					g.UrlName = "parties/" + b.UrlName;
					g.EmailOnAllThreads = false;
					g.DuplicateGuid = Guid.NewGuid();

					g.Update();
					b.GroupK = g.K;
					b.Update();

					Response.Redirect("/popup/newbrand/k-" + b.K.ToString());
				}
			}
		}
		public void NameVal(object o, ServerValidateEventArgs e)
		{
			string name = Cambro.Web.Helpers.StripHtml(NameTextBox.Text.Trim());
			NameTextBox.Text = name;
			BrandSet bs = new BrandSet(new Query(new Q(Brand.Columns.Name, name)));
			e.IsValid = bs.Count == 0;
		}
		#endregion

		#region PanelPic
		protected Panel PanelPic;
		protected Panel PicUploadPanel;
		protected Controls.Pic Pic;
		public void PicSaved(object o, System.EventArgs e)
		{
			Bobs.Utilities.CopyPic(CurrentBrand, CurrentBrand.Group);

			if (!Usr.Current.IsSuper)
			{
				CurrentBrand.IsEdited = true;
				CurrentBrand.Update();
			}
			PicNext();
		}
		public void PicNoPic(object o, System.EventArgs e)
		{
			Bobs.Utilities.DeletePic(CurrentBrand.Group);

			PicNext();
		}
		void PicNext()
		{
			ChangePanel(PanelDone);
		}
		#endregion

		#region PanelDone
		public Panel PanelDone;
		#endregion

		#region BrandK
		int BrandK
		{
			get
			{
				return ContainerPage.Url["K"];
			}
		}
		#endregion
		#region CurrentBrand
		public Brand CurrentBrand
		{
			get
			{
				if (currentBrand == null && BrandK > 0)
					currentBrand = new Brand(BrandK);
				return currentBrand;
			}
		}
		Brand currentBrand;
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelName.Visible = p.Equals(PanelName);
			PanelPic.Visible = p.Equals(PanelPic);
			PanelDone.Visible = p.Equals(PanelDone);
		}
		#endregion
	}
}

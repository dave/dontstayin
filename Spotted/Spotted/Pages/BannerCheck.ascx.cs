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

namespace Spotted.Pages
{
	public partial class BannerCheck : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Usr.Current.IsSuper)
				throw new Exception("Must be superadmin!");

			ContainerPage.SetPageTitle("Admin banner check");

			ChangePanel(PanelList);

		}

		#region PanelList
		protected Panel PanelList;
		protected DataGrid MiscDataGrid;
		public void PanelList_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None) || Mode.Equals(Modes.List))
			{
				ChangePanel(PanelList);
				BindMisc();
			}
		}
		void BindMisc()
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Q(Bobs.Misc.Columns.NeedsAuth, true);
			q.OrderBy = new OrderBy(Bobs.Misc.Columns.DateTime, OrderBy.OrderDirection.Descending);
			MiscSet ms = new MiscSet(q);
			MiscDataGrid.DataSource = ms;
			MiscDataGrid.DataBind();
		}
		public void MiscDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			MiscDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindMisc();
		}
		#endregion

		#region Delete
		public void Delete_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Delete) && CurrentMisc != null)
			{
				#region Delete file
				BannerSet bs = new BannerSet(
					new Query(
						new And(
							Banner.IsBookedQ,
							new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
							new Q(Banner.Columns.MiscK, CurrentMisc.K)
						)
					)
				);
				if (bs.Count == 0)
				{
					int promoterK = CurrentMisc.PromoterK;
					Delete.DeleteAll(CurrentMisc);
					Response.Redirect(UrlInfo.PageUrl("BannerCheck", "Mode", "List"));
				}
				else
				{
					throw new Exception("That's weird! this file is in use by a live banner!!!");
				}
				#endregion
			}
		}
		#endregion

		#region PanelView
		#region PanelView_Load
		public void PanelView_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.View))
			{
				ChangePanel(PanelView);

				Spotted.Controls.Banners.FlashBanner bannerPopup = (Spotted.Controls.Banners.FlashBanner)this.LoadControl("/Controls/Banners/FlashBanner.ascx");
				bannerPopup.BannerUrl = CurrentMisc.Url();
				bannerPopup.LinkTargetBlank = true;
				bannerPopup.LinkUrl = "http://" + Vars.DomainName + "/popup/checkbannerblank";
				bannerPopup.Width = 300;
				bannerPopup.Height = 300;
				bannerPopup.ShowClickHelper = false;
				bannerPopup.DataBind();
				ViewPopupP.Controls.Add(bannerPopup);


			}
		}
		#endregion

		public void ViewPass_Click(object o, System.EventArgs e)
		{
			Save(true);
		}
		public void ViewFail_Click(object o, System.EventArgs e)
		{
			if (FailTextBox.Text.Length == 0)
				throw new DsiUserFriendlyException("Make sure you add a reason if you fail a banner!");

			Save(false);
		}
		public void Save(bool pass)
		{
			if (!(ViewFlashLinkTagYes.Checked || ViewFlashLinkTagNo.Checked))
				throw new DsiUserFriendlyException("Form not completed!");

			CurrentMisc.BannerLinkTag = ViewFlashLinkTagYes.Checked;

			CurrentMisc.BannerBroken = !pass;

			if (!pass)
				CurrentMisc.BannerBrokenReason = FailTextBox.Text;
		
			CurrentMisc.NeedsAuth = false;
			CurrentMisc.Update();

			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Q(Banner.Columns.NewMiscK, CurrentMisc.K);
			BannerSet bs = new BannerSet(q);
			foreach (Banner b in bs)
			{
				Bobs.Misc.CanUseAsBannerReturn ret = CurrentMisc.CanUseAsBanner(b.Position);
				if (ret.CanUseNow)
				{
					b.MiscK = CurrentMisc.K;
					b.DisplayType = CurrentMisc.DisplayType;
					b.NewMiscK = 0;
					if (!b.StatusArtwork && b.FirstDay < DateTime.Today) 
						b.FirstDay = DateTime.Today;
					b.StatusArtwork = true;
					b.Update();
					foreach (Usr u in b.Promoter.AdminUsrs)
					{
						Mailer sm = new Mailer();
						sm.Subject = "Banner file passed admin check for banner-" + b.K;
						sm.Body += "<p>Banner file passed admin check for banner-" + b.K + "</p>";
						if (ret.LinkTagWarning)
						{
							sm.Body += "<p><center><img src=\"[WEB-ROOT]gfx/icon-warning.png\" width=\"26\" height=\"21\"></center></p>";
							sm.Body += "<p>Your banner doesn't support link tags - this means some features may not work. See the <a href=\"[WEB-ROOT]misc/banners.pdf\">banner instructions</a> for how to fix this. If you need to send the instructions to your designer, use this link: <b>www.dontstayin.com/misc/banners.pdf</b></p>";
						}
						sm.Body += "<p><a href=\"[LOGIN(" + b.Url() + ")]\">Banner options page</a></p>";
						sm.Body += "<p><a href=\"[LOGIN(" + CurrentMisc.ViewUrl() + ")]\">View file details</a></p>";
						sm.To = u.Email;
						sm.UsrRecipient = u;
						sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						sm.RedirectUrl = b.Url();
						sm.Send();
					}
				}
				else
				{
					b.NewMiscK = 0;
					b.Update();
					foreach (Usr u in b.Promoter.AdminUsrs)
					{
						Mailer sm = new Mailer();
						sm.Subject = "Banner file FAILED admin check for banner-" + b.K;
						sm.Body += "<p>Banner file FAILED admin check for banner-" + b.K + "</p>";
						sm.Body += "<p>Because:<br>";
						foreach (string s in ret.Errors)
							sm.Body += s + "<br>";
						sm.Body += "</p>";
						if (ret.BrokenError.Length>0)
						{
							sm.Body += "<p>One of our admins added this information, which might be useful:</p><p><i>" + ret.BrokenError + "</i></p>";
						}
						sm.Body += "<h2>How to fix the problems</h2>";
						sm.Body += "<p>See the <a href=\"[WEB-ROOT]misc/banners.pdf\">banner instructions</a> for how to get your banners working. If you need to send the instructions to your designer, use this link: <b>www.dontstayin.com/misc/banners.pdf</b></p>";
						sm.Body += "<p><a href=\"[LOGIN(" + b.Url() + ")]\">Banner options page</a></p>";
						sm.Body += "<p><a href=\"[LOGIN(" + CurrentMisc.ViewUrl() + ")]\">View file details</a></p>";
						sm.To = u.Email;
						sm.UsrRecipient = u;
						sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						sm.RedirectUrl = CurrentMisc.ViewUrl();
						sm.Send();
					}
				}
			}
			if (bs.Count == 0)
			{
				if (CurrentMisc.Promoter != null)
				{
					foreach (Usr u in CurrentMisc.Promoter.AdminUsrs)
					{
						Mailer sm = new Mailer();
						sm.Subject = "Banner file checked by admin";
						sm.Body += "<p>See the file details page for banner usage restrictions.</p>";
						sm.Body += "<p><a href=\"[LOGIN(" + CurrentMisc.ViewUrl() + ")]\">View file details</a></p>";
						sm.To = u.Email;
						sm.UsrRecipient = u;
						sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						sm.RedirectUrl = CurrentMisc.ViewUrl();
						sm.Send();
					}
				}
				else
				{
					Mailer sm = new Mailer();
					sm.Subject = "Banner file checked by admin";
					sm.Body += "<p>See the file details page for banner usage restrictions.</p>";
					sm.Body += "<p><a href=\"[LOGIN(" + CurrentMisc.ViewUrl() + ")]\">View file details</a></p>";
					sm.To = CurrentMisc.Usr.Email;
					sm.UsrRecipient = CurrentMisc.Usr;
					sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
					sm.RedirectUrl = CurrentMisc.ViewUrl();
					sm.Send();
				}
			}
			Response.Redirect(UrlInfo.PageUrl("BannerCheck", "Mode", "List"));


		}
		#endregion

		#region MiscK
		int MiscK
		{
			get
			{
				return ContainerPage.Url["MiscK"];
			}
		}
		#endregion
		#region CurrentMisc
		public Bobs.Misc CurrentMisc
		{
			get
			{
				if (currentMisc == null && MiscK > 0)
					currentMisc = new Bobs.Misc(MiscK);
				return currentMisc;
			}
		}
		Bobs.Misc currentMisc;
		#endregion

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("List"))
					return Modes.List;
				else if (ContainerPage.Url["Mode"].Equals("View"))
					return Modes.View;
				else if (ContainerPage.Url["Mode"].Equals("Delete"))
					return Modes.Delete;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			List,
			View,
			Delete
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelList.Visible = p.Equals(PanelList);
			PanelView.Visible = p.Equals(PanelView);
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
			this.Load += new System.EventHandler(this.Delete_Load);
			this.Load += new System.EventHandler(this.PanelList_Load);
			this.Load += new System.EventHandler(this.PanelView_Load);
		}
		#endregion
	}
}

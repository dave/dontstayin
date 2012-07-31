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
using System.IO;

namespace Spotted.Pages.Promoters
{
	public partial class Files : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		protected Spotted.CustomControls.h1 Header;
		protected Panel PanelDelete;
		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("My files");

			if (Mode.Equals(Modes.None) || Mode.Equals(Modes.List))
				ChangePanel(PanelList);
			else if (Mode.Equals(Modes.Upload))
				ChangePanel(PanelUpload);
			else if (Mode.Equals(Modes.View))
				ChangePanel(PanelView);
			else if (Mode.Equals(Modes.Delete))
			{
				#region Delete file
				if (Usr.Current.K != CurrentMisc.UsrK && (CurrentMisc.PromoterK == 0 || !Usr.Current.IsPromoterK(CurrentMisc.PromoterK)) && !Usr.Current.IsAdmin)
					throw new Exception("You can't delete this file!");
				else
				{
					BannerSet bs = new BannerSet(
						new Query(
							new And(
								Banner.IsBookedQ,
								new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
								new Or(
									new Q(Banner.Columns.MiscK, CurrentMisc.K),
									new Q(Banner.Columns.NewMiscK, CurrentMisc.K)
								)
							)
						)
					);
					if (bs.Count == 0)
					{
						Promoter p = CurrentMisc.Promoter;
						Delete.DeleteAll(CurrentMisc);
						Response.Redirect(p.UrlApp("files"));
					}
					else
					{
						ChangePanel(PanelDelete);
					}
				}
				#endregion
			}
			else
				throw new Exception("Funny mode?");

		}

		protected string RedirectUrl
		{
			get
			{
				if (BannerK > 0)
				{
					if (CurrentBanner.LinkTarget.Equals(Banner.LinkTargets.Event))
						return CurrentBanner.OptionsUrl("eventk", CurrentBanner.EventK.ToString());
					else
						return CurrentBanner.OptionsUrl();
				}
				else if (CurrentPromoter != null && CurrentPromoter.K > 0)
					return ListFilesLink;
				else
					return "";
			}
		}

		#region PanelList
		protected Panel PanelList;
		protected DataGrid MiscDataGrid;
		protected HtmlGenericControl MiscNoFilesP, MiscDataGridP;
		public void PanelList_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None) || Mode.Equals(Modes.List))
			{
				if (CurrentPromoter != null)
				{
					if (!Usr.Current.IsPromoterK(CurrentPromoter.K) && !Usr.Current.IsAdmin)
						throw new DsiUserFriendlyException("You can't view this file list!");
					Header.InnerText = "Files uploaded for promoter: " + CurrentPromoter.Name;
				}
				BindMisc();
			}
		}
		void BindMisc()
		{
			Q UsrPromoterQ = null;
			if (CurrentPromoter != null)
				UsrPromoterQ = new Q(Bobs.Misc.Columns.PromoterK, CurrentPromoter.K);
			else
				UsrPromoterQ = new Q(Bobs.Misc.Columns.UsrK, Usr.Current.K);

			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(
				new Q(Bobs.Misc.Columns.Folder, CurrentFolder),
				UsrPromoterQ);
			q.OrderBy = new OrderBy(Bobs.Misc.Columns.DateTime, OrderBy.OrderDirection.Descending);
			MiscSet ms = new MiscSet(q);
			MiscNoFilesP.Visible = ms.Count == 0;
			MiscDataGridP.Visible = ms.Count > 0;
			if (ms.Count > 0)
			{
				MiscDataGrid.DataSource = ms;
				MiscDataGrid.DataBind();
			}
		}
		public void MiscDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			MiscDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindMisc();
		}
		protected string AddFilesLink
		{
			get
			{
				return CurrentPromoter.UrlApp("files", "mode", "upload");
			}
		}
		#endregion

		#region PanelUpload
		protected Panel PanelUpload;
		public void PanelUpload_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Upload))
			{
				ContainerPage.TemplateForm.Enctype = "multipart/form-data";
				//ContainerPage.TemplateForm.Attributes["ENCTYPE"] = "multipart/form-data";
			}
		}
		protected string ListFilesLink
		{
			get
			{
				return CurrentPromoter.UrlApp("files");
			}
		}
		#region UploadNow_Click
		protected void UploadNow_Click(object sender, EventArgs eventArgs)
		{

			if (InputFile.PostedFile != null)
			{

				#region Upload file
				Bobs.Misc m = new Bobs.Misc();

				m.UsrK = Usr.Current.K;

				if (CurrentPromoter != null)
					m.PromoterK = CurrentPromoter.K;

				m.DateTime = DateTime.Now;
				m.Folder = CurrentFolder;

				m.Guid = Guid.NewGuid();

				if (InputFile.PostedFile.FileName.IndexOf(".") == -1)
					m.Extention = "";
				else
					m.Extention = InputFile.PostedFile.FileName.Substring(InputFile.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();

				if (m.Extention.Equals("jpeg") || m.Extention.Equals("jpe"))
					m.Extention = "jpg";

				if (!(m.Extention.Equals("jpg") ||
					m.Extention.Equals("gif") ||
					m.Extention.Equals("png") ||
					m.Extention.Equals("flv") ||
					m.Extention.Equals("mp4") ||
					(Usr.Current.IsAdmin && m.Extention.Equals("zip")) ||
					m.Extention.Equals("swf")))
					throw new DsiUserFriendlyException("You can only upload gif, jpg, png, flv or swf files with this page.");

				m.Size = InputFile.PostedFile.ContentLength;
				m.Name = InputFile.PostedFile.FileName.Substring(InputFile.PostedFile.FileName.LastIndexOf("\\") + 1);

				byte[] bytes = new byte[InputFile.PostedFile.InputStream.Length];
				InputFile.PostedFile.InputStream.Read(bytes, 0, (int)InputFile.PostedFile.InputStream.Length);

				if (m.Extention.Equals("jpg") || m.Extention.Equals("gif") || m.Extention.Equals("png"))
				{
					using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bytes)))
					{
						m.Width = image.Width;
						m.Height = image.Height;
					}
				}

				if (CurrentPromoter != null)
				{
					if (m.Extention.Equals("swf"))
					{
                        if (m.Size <= 150 * 1024)
							m.NeedsAuth = true;
					}
				}

				m.Update();
				try
				{
					Storage.AddToStore(bytes, Storage.Stores.Pix, m.Guid, m.Extention, m, "");
				}
				catch
				{
					m.Delete();
				}

				
				if (m.NeedsAuth)
				{
					Mailer adminMail = new Mailer();
					adminMail.Subject = "New files waiting to be approved!!! uploaded by" + Usr.Current.NickNameSafe;
					adminMail.To = "promoters@dontstayin.com";
					adminMail.Body += "<p>New FILES uploaded by <a href=\"[LOGIN(" + Usr.Current.Url() + ")]\">" + Usr.Current.NickNameSafe + "</a></p>";
					if (CurrentPromoter != null)
						adminMail.Body += "<p>... for promoter <a href=\"[LOGIN(" + CurrentPromoter.Url() + ")]\">" + CurrentPromoter.Name + "</a></p>";
					adminMail.Body += "<h2>Files:</h2>";
					adminMail.Body += "<p><a href=\"" + m.Url() + "\">" + HttpUtility.HtmlEncode(m.Name) + "</a> - " + m.FileSizeString + "</p>";
					adminMail.TemplateType = Mailer.TemplateTypes.AdminNote;
					adminMail.RedirectUrl = Usr.Current.Url();
					adminMail.Send();
				}
				if (CurrentBanner != null)
					CurrentBanner.AssignMisc(m);

				#endregion

				if (CurrentBanner != null)
					Response.Redirect(CurrentBanner.Url());
				else
					Response.Redirect(ContainerPage.Url.CurrentUrl("mode","view","misck",m.K));

			}
		
		}
		#endregion
		#endregion

		protected string ExtraParams
		{
			get
			{
				if (CurrentBanner != null)
				{
					string ret = "";
					ret += "<param name=\"MaxFileCount\" value=\"1\">";
					if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
					{
						ret += "<param name=\"MaxTotalSize\" value=\"20480\">";
					}
					else
					{
						ret += "<param name=\"MaxTotalSize\" value=\"61440\">";
					}
					return ret;
				}
				else
				{
					return "<param name=\"MaxTotalSize\" value=\"104857600\">";
				}
			}
		}

		#region PanelView
		protected Panel PanelView;
		protected HtmlAnchor ViewBackAnchor, ViewNameAnchor;
		protected TextBox ViewUrlTextBox, ViewImageHtmlTextBox;
		protected HtmlTableCell ViewNameCell, ViewImageWidthCell, ViewImageHeightCell, ViewImageFileSizeCell;
        protected HtmlImage ViewLeaderboardImg, ViewHotboxImg, ViewPhotoBannerImg, ViewEmailBannerImg, ViewSkyscraperImg;
		protected Label ViewLeaderboardLabel, ViewHotboxLabel, ViewPhotoBannerLabel, ViewEmailBannerLabel, ViewSkyscraperLabel;
		protected HtmlTableRow ViewImageHtmlTr;
		public void PanelView_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.View))
			{
				if (!Usr.Current.CanEdit(CurrentMisc))
					throw new Exception("You can't view this file!");

				#region Back link
				ViewBackAnchor.HRef = CurrentPromoter.UrlApp("files");
				#endregion

				ViewLeaderboardLabel.Text = "";
				ViewHotboxLabel.Text = "";
				ViewSkyscraperLabel.Text = "";
				ViewPhotoBannerLabel.Text = "";
				ViewEmailBannerLabel.Text = "";
                ViewBrokenLabel.Text = "";

				ViewNameAnchor.HRef = CurrentMisc.Url();
				ViewNameAnchor.InnerText = CurrentMisc.Name;
				ViewUrlTextBox.Text = CurrentMisc.Url();

				RequiredFlashVersionTr.Visible = CurrentMisc.Extention.Equals("swf");
				if (!Page.IsPostBack)
				{
					RequiredFlashVersion.Text = CurrentMisc.RequiredFlashVersion;
					if (CurrentMisc.Width > 0 || CurrentMisc.Height > 0)
					{
						SizeWidth.Text = CurrentMisc.Width.ToString();
						SizeHeight.Text = CurrentMisc.Height.ToString();
					}
				}

				ViewImageFileSizeCell.InnerText = CurrentMisc.FileSizeString;

				if (CurrentMisc.Extention.Equals("jpg") || CurrentMisc.Extention.Equals("png") || CurrentMisc.Extention.Equals("gif") || CurrentMisc.Extention.Equals("swf"))
					ViewBannerBody.Visible = true;
				else
					ViewBannerBody.Visible = false;


				if (CurrentMisc.Extention.Equals("jpg") || CurrentMisc.Extention.Equals("gif") || CurrentMisc.Extention.Equals("png"))
				{
					ViewImageBody.Visible = true;
					ViewImageWidthCell.InnerText = CurrentMisc.Width.ToString();
					ViewImageHeightCell.InnerText = CurrentMisc.Height.ToString();
				}
				else
					ViewImageBody.Visible = false;

				if (CurrentMisc.Extention.Equals("jpg") || CurrentMisc.Extention.Equals("gif") || CurrentMisc.Extention.Equals("png"))
				{
					ViewImageHtmlTr.Visible = true;
					ViewImageHtmlTextBox.Text = "<img src=\"" + CurrentMisc.Url() + "\" width=\"" + CurrentMisc.Width.ToString() + "\" height=\"" + CurrentMisc.Height.ToString() + "\" border=\"0\">";
				}
				else
					ViewImageHtmlTr.Visible = false;

				Bobs.Misc.CanUseAsBannerReturn UseLeaderboard = CurrentMisc.CanUseAsBanner(Banner.Positions.Leaderboard);
				if (!UseLeaderboard.CanUseNow)
				{
					ViewLeaderboardImg.Src = "~/gfx/icon-cross-up.png";
					if (UseLeaderboard.CanUseAfterAdminCheck)
						ViewLeaderboardLabel.Text = "Waiting for admin check";
					else
					{
						foreach (string s in UseLeaderboard.Errors)
							ViewLeaderboardLabel.Text += (ViewLeaderboardLabel.Text.Length > 0 ? "<br>" : "") + s;
					}
				}


				Bobs.Misc.CanUseAsBannerReturn UseHotbox = CurrentMisc.CanUseAsBanner(Banner.Positions.Hotbox);
				if (!UseHotbox.CanUseNow)
				{
					ViewHotboxImg.Src = "~/gfx/icon-cross-up.png";
					if (UseHotbox.CanUseAfterAdminCheck)
						ViewHotboxLabel.Text = "Waiting for admin check";
					else
					{
						foreach (string s in UseHotbox.Errors)
							ViewHotboxLabel.Text += (ViewHotboxLabel.Text.Length > 0 ? "<br>" : "") + s;
					}
				}

				Bobs.Misc.CanUseAsBannerReturn UseSkyscraper = CurrentMisc.CanUseAsBanner(Banner.Positions.Skyscraper);
				if (!UseSkyscraper.CanUseNow)
				{
					ViewSkyscraperImg.Src = "~/gfx/icon-cross-up.png";
					if (UseSkyscraper.CanUseAfterAdminCheck)
						ViewSkyscraperLabel.Text = "Waiting for admin check";
					else
					{
						foreach (string s in UseSkyscraper.Errors)
							ViewSkyscraperLabel.Text += (ViewSkyscraperLabel.Text.Length > 0 ? "<br>" : "") + s;
					}
				}

				Bobs.Misc.CanUseAsBannerReturn UsePhotoBanner = CurrentMisc.CanUseAsBanner(Banner.Positions.PhotoBanner);
				if (!UsePhotoBanner.CanUseNow)
				{
					ViewPhotoBannerImg.Src = "~/gfx/icon-cross-up.png";
					if (UsePhotoBanner.CanUseAfterAdminCheck)
						ViewPhotoBannerLabel.Text = "Waiting for admin check";
					else
					{
						foreach (string s in UsePhotoBanner.Errors)
							ViewPhotoBannerLabel.Text += (ViewPhotoBannerLabel.Text.Length > 0 ? "<br>" : "") + s;
					}
				}

				Bobs.Misc.CanUseAsBannerReturn UseEmailBanner = CurrentMisc.CanUseAsBanner(Banner.Positions.EmailBanner);
				if (!UseEmailBanner.CanUseNow)
				{
					ViewEmailBannerImg.Src = "~/gfx/icon-cross-up.png";
					if (UseEmailBanner.CanUseAfterAdminCheck)
						ViewEmailBannerLabel.Text = "Waiting for admin check";
					else
					{
						foreach (string s in UseEmailBanner.Errors)
							ViewEmailBannerLabel.Text += (ViewEmailBannerLabel.Text.Length > 0 ? "<br>" : "") + s;
					}
				}

                if (CurrentMisc.NeedsAuth)
                {
                    ViewBrokenImg.Visible = false;
                    ViewBrokenLabel.Text = "Waiting for admin check. We aim to check all uploaded banners within one business day.";
                }
                else if (CurrentMisc.BannerBroken)
                {
                    ViewBrokenImg.Src = "~/gfx/icon-cross-up.png";
                    ViewBrokenLabel.Text = CurrentMisc.BannerBrokenReason + " See the <a href=\"/misc/banners.pdf\">banner instructions</a> for how to fix this. If you need to send the instructions to your designer, use this link: <b>www.dontstayin.com/misc/banners.pdf</b>";
                }
                else if (!CurrentMisc.BannerLinkTag)
                {
                    ViewBrokenImg.Src = "~/gfx/icon-warning.png";
                    ViewBrokenLabel.Text = "Your banner doesn't support link tags - this means some features may not work. See the <a href=\"/misc/banners.pdf\">banner instructions</a> for how to fix this. If you need to send the instructions to your designer, use this link: <b>www.dontstayin.com/misc/banners.pdf</b>";
                }


				if (ViewLeaderboardLabel.Text.Length == 0)
					ViewLeaderboardLabel.Text = "&nbsp;";

				if (ViewHotboxLabel.Text.Length == 0)
					ViewHotboxLabel.Text = "&nbsp;";

				if (ViewSkyscraperLabel.Text.Length == 0)
					ViewSkyscraperLabel.Text = "&nbsp;";

				if (ViewPhotoBannerLabel.Text.Length == 0)
					ViewPhotoBannerLabel.Text = "&nbsp;";

				if (ViewEmailBannerLabel.Text.Length == 0)
					ViewEmailBannerLabel.Text = "&nbsp;";

                if (ViewBrokenLabel.Text.Length == 0)
                    ViewBrokenLabel.Text = "&nbsp;";
			}
		}
		#region UpdateFlashVersion
		protected void UpdateFlashVersion(object sender, EventArgs eventArgs)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^0-9\\.]");
			CurrentMisc.RequiredFlashVersion = r.Replace(RequiredFlashVersion.Text, String.Empty);
			RequiredFlashVersion.Text = CurrentMisc.RequiredFlashVersion;
			CurrentMisc.Update();
			UpdateFlashVersionDone.Text = "Done";

		}
		#endregion
		#region UpdateSize
		protected void UpdateSize(object sender, EventArgs eventArgs)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^0-9]");
			CurrentMisc.Width = int.Parse(r.Replace(SizeWidth.Text, String.Empty));
			CurrentMisc.Height = int.Parse(r.Replace(SizeHeight.Text, String.Empty));
			SizeWidth.Text = CurrentMisc.Width.ToString();
			SizeHeight.Text = CurrentMisc.Height.ToString();
			CurrentMisc.Update();
			UpdateSizeDone.Text = "Done.";
		}
		#endregion
		#endregion

		#region BannerK
		int BannerK
		{
			get
			{
				return ContainerPage.Url["BannerK"];
			}
		}
		#endregion
		#region CurrentBanner
		public Banner CurrentBanner
		{
			get
			{
				if (currentBanner == null && BannerK > 0)
					currentBanner = new Banner(BannerK);
				return currentBanner;
			}
		}
		Banner currentBanner;
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

		protected string CurrentFolder
		{
			get
			{
				return ContainerPage.Url["Folder"];
			}
		}

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("List"))
					return Modes.List;
				else if (ContainerPage.Url["Mode"].Equals("Upload"))
					return Modes.Upload;
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
			Upload,
			View,
			Delete
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelList.Visible = p.Equals(PanelList);
			PanelUpload.Visible = p.Equals(PanelUpload);
			PanelView.Visible = p.Equals(PanelView);
			PanelDelete.Visible = p.Equals(PanelDelete);



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
			this.Load += new System.EventHandler(this.PanelList_Load);
			this.Load += new System.EventHandler(this.PanelUpload_Load);
			this.Load += new System.EventHandler(this.PanelView_Load);
		}
		#endregion
	}
}

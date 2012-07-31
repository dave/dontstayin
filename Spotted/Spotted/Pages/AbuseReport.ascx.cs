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
	public partial class AbuseReport : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsSenior)
				throw new Exception("Only photo moderators!");
		}

		#region PanelNone
		protected Panel PanelNone;
		private void PanelNone_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
				ChangePanel(PanelNone);
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.New),
					new Q(Abuse.Columns.ObjectType, Model.Entities.ObjectType.Photo)
				);
				q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
				q.TopRecords = 1;
				AbuseSet abs = new AbuseSet(q);
				if (abs.Count > 0)
					Response.Redirect(UrlInfo.PageUrl("abusereport", "k", abs[0].K));

			}
		}
		#endregion

		#region PanelAbuse
		protected Panel PanelAbuse;
		protected Label PhotoKLabel, PhotoStringLabel;
		protected HtmlAnchor PhotoAnchor;
		protected HtmlImage PhotoImg;
		protected Panel PhotoPanel, NoPhotoPanel;
		protected HtmlGenericControl AbuseByP, ReportByP, GalleriesP, ThisGalleryP;
		protected HtmlGenericControl ReportDescriptionP, ResolveDescriptionP;
		protected Panel ActionsPanel, ResolvedPanel, ThisGalleryPanel;
		protected Label ResolvedLabel;
		private void PanelAbuse_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Abuse))
			{
				ChangePanel(PanelAbuse);
				PhotoKLabel.Text = CurrentAbuse.ObjectK.ToString();
				PhotoStringLabel.Text = CurrentAbuse.ObjectString;
				Photo p = null;
				try
				{
					p = new Photo(CurrentAbuse.ObjectK);
				}
				catch { }

				if (p != null)
				{
					ThisGalleryP.InnerHtml = "<a href=\"" + p.Gallery.PagedUrl() + "\" target=\"_blank\">" + p.Gallery.UrlNoSkip() + "</a> - <a href=\"" + p.Gallery.UrlApp("edit") + "\" target=\"_blank\">[edit]</a> - (" + p.Gallery.TotalPhotos + " photos, " + p.Gallery.LivePhotos + " live)<br>";

					PhotoAnchor.HRef = p.Url();
					PhotoImg.Src = p.WebPath;
					PhotoImg.Height = p.WebHeight;
					PhotoImg.Width = p.WebWidth;
					PhotoPanel.Visible = true;
					NoPhotoPanel.Visible = false;
				}
				else
				{
					ThisGalleryPanel.Visible = false;
					PhotoPanel.Visible = false;
					NoPhotoPanel.Visible = true;
				}

				AbuseByP.InnerHtml = CurrentAbuse.AbuseUsr.LinkNewWindow() + "<br>";
				AbuseByP.InnerHtml += "Pending abuse accusations: " + CurrentAbuse.AbuseUsr.AbuseAccusationsPending + "<br>";
				AbuseByP.InnerHtml += "Abuse in the past: " + CurrentAbuse.AbuseUsr.AbuseAccusationsAbuse + "<br>";
				AbuseByP.InnerHtml += "Accusations with no abuse: " + CurrentAbuse.AbuseUsr.AbuseAccusationsNoAbuse;

				ReportByP.InnerHtml = CurrentAbuse.ReportUsr.LinkNewWindow() + "<br>";
				ReportByP.InnerHtml += "Pending abuse reports: " + CurrentAbuse.ReportUsr.AbuseReportsPending + "<br>";
				ReportByP.InnerHtml += "Successful abuse reports: " + CurrentAbuse.ReportUsr.AbuseReportsUseful + "<br>";
				ReportByP.InnerHtml += "Overturned abuse reports: " + CurrentAbuse.ReportUsr.AbuseReportsOverturned;

				ReportDescriptionP.InnerText = CurrentAbuse.ReportDescription;



				Query q = new Query();
				q.QueryCondition = new Q(Gallery.Columns.OwnerUsrK, CurrentAbuse.AbuseUsrK);
				q.TopRecords = 20;
				q.OrderBy = new OrderBy(Gallery.Columns.LastLiveDateTime, OrderBy.OrderDirection.Descending);
				GallerySet gs = new GallerySet(q);
				GalleriesP.InnerHtml = "";
				foreach (Gallery g in gs)
				{
					GalleriesP.InnerHtml += "<a href=\"" + g.PagedUrl() + "\" target=\"_blank\">" + g.UrlNoSkip() + "</a> - <a href=\"" + g.UrlApp("edit") + "\" target=\"_blank\">[edit]</a> - (" + g.TotalPhotos + " photos, " + g.LivePhotos + " live)<br>";
				}

				if (CurrentAbuse.Status.Equals(Abuse.StatusEnum.Done))
				{
					ActionsPanel.Visible = false;
					ResolvedPanel.Visible = true;
					if (CurrentAbuse.ResolveStatus.Equals(Abuse.ResolveStatusEnum.Abuse))
						ResolvedLabel.Text = "There was abuse.";
					else if (CurrentAbuse.ResolveStatus.Equals(Abuse.ResolveStatusEnum.NoAbuse))
						ResolvedLabel.Text = "There was no abuse, but the report was helpful.";
					else if (CurrentAbuse.ResolveStatus.Equals(Abuse.ResolveStatusEnum.Overturned))
						ResolvedLabel.Text = "There was no abuse, the report was overturned.";

					ResolvedLabel.Text += " Resolved by " + CurrentAbuse.ResolveUsr.Link() + ", " + Cambro.Misc.Utility.FriendlyTime(CurrentAbuse.ResolveDateTime);
					ResolveDescriptionP.InnerText = CurrentAbuse.ResolveDescription;

				}
				else
				{
					ActionsPanel.Visible = true;
					ResolvedPanel.Visible = false;
				}
			}
		}
		protected RadioButton OverturnRadio, NoAbuseRadio, NoAbuseDeleteRadio, AbuseDeleteRadio, AbuseDeleteWatchRadio, AbuseDeleteBanRadio, AbuseDeleteBanModerateRadio;
		protected TextBox ResolveDescriptionTextBox;
		public void Action_Click(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Abuse))
			{
				if (Page.IsValid)
				{
					if (CurrentAbuse.Status.Equals(Abuse.StatusEnum.Done))
						throw new DsiUserFriendlyException("Oops - this abuse report has already been resolved - maybe someone beat you to it...");

					if (!(OverturnRadio.Checked || NoAbuseRadio.Checked || NoAbuseDeleteRadio.Checked || AbuseDeleteRadio.Checked || AbuseDeleteWatchRadio.Checked || AbuseDeleteBanRadio.Checked || AbuseDeleteBanModerateRadio.Checked))
					{
						throw new DsiUserFriendlyException("You must choose an option!");
					}

					if (OverturnRadio.Checked)
					{
						CurrentAbuse.ResolveDateTime = DateTime.Now;
						CurrentAbuse.Status = Abuse.StatusEnum.Done;
						CurrentAbuse.ResolveStatus = Abuse.ResolveStatusEnum.Overturned;
						CurrentAbuse.ResolveDescription = ResolveDescriptionTextBox.Text;
						CurrentAbuse.ResolveUsrK = Usr.Current.K;
						CurrentAbuse.Update();

						Photo p = null;
						try
						{
							p = new Photo(CurrentAbuse.ObjectK);
						}
						catch { }

						Mailer m = new Mailer();
						m.Subject = "Your abuse report has been resolved";
						m.Body = "<p>You recently filed an abuse report about a photo</p>";
						m.Body += "<p>Our moderators have reviewed the photo, and found no abuse. Please only report photos when there is a clear abuse of the photo rules. If you mis-use this abuse report service, you will not be able to make further reports.</p>";
						m.Body += "<p>Our moderator included the following note:</p>";
						m.Body += "<p><i>" + CurrentAbuse.ResolveDescription + "</i></p>";
						if (p != null)
							m.RedirectUrl = p.Url();
						m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						m.UsrRecipient = CurrentAbuse.ReportUsr;
						m.Send();

						CurrentAbuse.ReportUsr.UpdateAbuseTrackers();
						CurrentAbuse.AbuseUsr.UpdateAbuseTrackers();
						Bobs.Global.UpdatePhotoAbuseReports();

					}
					else if (NoAbuseRadio.Checked || NoAbuseDeleteRadio.Checked)
					{
						CurrentAbuse.ResolveDateTime = DateTime.Now;
						CurrentAbuse.Status = Abuse.StatusEnum.Done;
						CurrentAbuse.ResolveStatus = Abuse.ResolveStatusEnum.NoAbuse;
						CurrentAbuse.ResolveDescription = ResolveDescriptionTextBox.Text;
						CurrentAbuse.ResolveUsrK = Usr.Current.K;
						CurrentAbuse.Update();

						Photo p = null;
						try
						{
							p = new Photo(CurrentAbuse.ObjectK);
							if (NoAbuseDeleteRadio.Checked)
								p.DeleteAll(null);
						}
						catch { }

						Mailer m = new Mailer();
						m.Subject = "Your abuse report has been resolved";
						m.Body = "<p>You recently filed an abuse report about a photo</p>";
						m.Body += "<p>Our moderators have reviewed the photo, and found no abuse. Your report was helpful however.</p>";
						m.Body += "<p>Our moderator included the following note:</p>";
						m.Body += "<p><i>" + CurrentAbuse.ResolveDescription + "</i></p>";
						if (p != null)
							m.RedirectUrl = p.Url();
						m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						m.UsrRecipient = CurrentAbuse.ReportUsr;
						m.Send();

						CurrentAbuse.ReportUsr.UpdateAbuseTrackers();
						CurrentAbuse.AbuseUsr.UpdateAbuseTrackers();
						Bobs.Global.UpdatePhotoAbuseReports();
					}
					else if (AbuseDeleteRadio.Checked || AbuseDeleteWatchRadio.Checked || AbuseDeleteBanRadio.Checked || AbuseDeleteBanModerateRadio.Checked)
					{
						try
						{
							Photo ph = new Photo(CurrentAbuse.ObjectK);
							ph.DeleteAll(null);
						}
						catch { }

						if (AbuseDeleteWatchRadio.Checked)
						{
							try
							{
								CurrentAbuse.AbuseUsr.ModeratePhotos = true;
								CurrentAbuse.AbuseUsr.Update();
							}
							catch { }
						}

						if (AbuseDeleteBanRadio.Checked)
						{
							try
							{
								CurrentAbuse.AbuseUsr.Banned = true;
								CurrentAbuse.AbuseUsr.BannedByUsrK = Usr.Current.K;
								CurrentAbuse.AbuseUsr.BannedDateTime = DateTime.Now;
								CurrentAbuse.AbuseUsr.BannedReason = ResolveDescriptionTextBox.Text;
								CurrentAbuse.AbuseUsr.Update();

								Mailer sm = new Mailer();
								sm.Body = "<p>Banned user: <a href=\"[LOGIN(" + CurrentAbuse.AbuseUsr.Url() + ")]\">" + CurrentAbuse.AbuseUsr.NickName + "</a> (" + CurrentAbuse.AbuseUsr.K + " - " + CurrentAbuse.AbuseUsr.Email + ")</p>";
								sm.Body += "<p>They were banned by: <a href=\"[LOGIN(" + Usr.Current.Url() + ")]\">" + Usr.Current.NickName + "</a> (" + Usr.Current.K + " - " + Usr.Current.Email + ")</p>";
								sm.Body += "<p>DateTime: " + DateTime.Now.ToString() + "</p>";
								sm.Body += "<p>Reason: " + ResolveDescriptionTextBox.Text + "</p>";
								sm.TemplateType = Mailer.TemplateTypes.AdminNote;
								sm.Subject = "New banned user - " + CurrentAbuse.AbuseUsr.NickName + " was banned by " + Usr.Current.NickName;
								sm.To = "abuse@dontstayin.com";
								sm.Send();

							}
							catch { }
						}

						if (AbuseDeleteBanModerateRadio.Checked)
						{
							Query q = new Query();
							q.QueryCondition = new Q(Gallery.Columns.OwnerUsrK, CurrentAbuse.AbuseUsrK);
							GallerySet gs = new GallerySet(q);
							foreach (Gallery g in gs)
							{
								Query qP = new Query();
								qP.QueryCondition = new Q(Photo.Columns.GalleryK, g.K);
								PhotoSet ps = new PhotoSet(qP);
								foreach (Photo ph in ps)
								{
									ph.Status = Photo.StatusEnum.Moderate;
									ph.Update();
								}

								g.UpdateStats(null, true);
								g.UpdatePhotoOrder(null);

								if (g.Event != null)
									g.Event.UpdateTotalPhotos(null);
							}
						}

						CurrentAbuse.ResolveDateTime = DateTime.Now;
						CurrentAbuse.Status = Abuse.StatusEnum.Done;
						CurrentAbuse.ResolveStatus = Abuse.ResolveStatusEnum.Abuse;
						CurrentAbuse.ResolveDescription = ResolveDescriptionTextBox.Text;
						CurrentAbuse.ResolveUsrK = Usr.Current.K;
						CurrentAbuse.Update();

						Mailer m = new Mailer();
						m.Subject = "Your abuse report has been resolved";
						m.Body = "<p>You recently filed an abuse report about a photo</p>";
						m.Body += "<p>Our moderators have reviewed the photo, and found it breaks our rules. It has been deleted.</p>";
						m.Body += "<p>Our moderator included the following note:</p>";
						m.Body += "<p><i>" + CurrentAbuse.ResolveDescription + "</i></p>";
						m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						m.UsrRecipient = CurrentAbuse.ReportUsr;
						m.Send();

						CurrentAbuse.ReportUsr.UpdateAbuseTrackers();
						CurrentAbuse.AbuseUsr.UpdateAbuseTrackers();
						Bobs.Global.UpdatePhotoAbuseReports();
					}
					PanelAbuse_Load(null, null);
				}
			}
		}
		#endregion

		public Abuse CurrentAbuse
		{
			get
			{
				if (currentAbuse == null && ContainerPage.Url["k"].IsInt)
				{
					currentAbuse = new Abuse(ContainerPage.Url["k"]);
				}
				return currentAbuse;
			}
		}
		private Abuse currentAbuse;

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["k"].IsInt)
					return Modes.Abuse;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Abuse
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelNone.Visible = p.Equals(PanelNone);
			PanelAbuse.Visible = p.Equals(PanelAbuse);
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
			this.Load += new System.EventHandler(this.PanelAbuse_Load);
			this.Load += new System.EventHandler(this.PanelNone_Load);
		}
		#endregion
	}
}

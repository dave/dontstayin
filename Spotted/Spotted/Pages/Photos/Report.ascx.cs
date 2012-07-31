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

namespace Spotted.Pages.Photos
{
	public partial class Report : DsiUserControl
	{
		protected HtmlImage PhotoImg;
		protected HtmlAnchor PhotoAnchor;

		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentPhoto != null)
				CurrentPhoto.AddRelevant(ContainerPage);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must log in to use this page.");
			this.SetPageTitle("Report a photo to our moderators");
			PhotoImg.Src = CurrentPhoto.WebPath;
			PhotoImg.Width = CurrentPhoto.WebWidth;
			PhotoImg.Height = CurrentPhoto.WebHeight;
			PhotoAnchor.HRef = CurrentPhoto.Url();

		}

		#region CurrentPhoto
		public Photo CurrentPhoto
		{
			get
			{
				return ContainerPage.Url.ObjectFilterPhoto;
			}
		}
		#endregion

		#region Report_Click
		protected TextBox ReportMessageTextBox;
		protected HtmlGenericControl ReportP;
		protected Panel BuddyCheckBoxesPanel;
		protected CheckBoxList BuddyCheckBoxList;
		protected CheckBox BuddyCheckBoxAll;
		protected Panel DonePanel, ReportPanel;
		public void Report_Click(object o, System.EventArgs e)
		{
			if (Usr.Current.AbuseReportsOverturned > 10 && Usr.Current.AbuseReportsOverturnedFraction > 0.5)
			{
				DonePanel.Visible = true;
				ReportPanel.Visible = false;
				ReportP.InnerHtml = "You have " + Usr.Current.AbuseReportsOverturned + " abuse reports that have been overturned by our moderators. You can't report any more photos! <a href=\"" + CurrentPhoto.Url() + "\">Click here to go back to the photo</a>";
				return;
			}

			if (Usr.Current.AbuseReportsPending > 10)
			{
				DonePanel.Visible = true;
				ReportPanel.Visible = false;
				ReportP.InnerHtml = "You have " + Usr.Current.AbuseReportsPending + " abuse reports that are being investigated by our moderators. You can't report any more photos until these have been resolved. <a href=\"" + CurrentPhoto.Url() + "\">Click here to go back to the photo</a>";
				return;
			}

			Query q = new Query();
			q.QueryCondition = new And(
				new Q(Abuse.Columns.ReportUsrK, Usr.Current.K),
				new Q(Abuse.Columns.ObjectType, Model.Entities.ObjectType.Photo),
				new Q(Abuse.Columns.ObjectK, CurrentPhoto.K)
			);
			AbuseSet abs = new AbuseSet(q);
			if (abs.Count > 0)
			{
				DonePanel.Visible = true;
				ReportPanel.Visible = false;
				ReportP.InnerHtml = "You have already reported this photo. Please wait for a response from our moderators. <a href=\"" + CurrentPhoto.Url() + "\">Click here to go back to the photo</a>";
				return;
			}

			Abuse a = new Abuse();
			a.ReportUsrK = Usr.Current.K;
			a.AbuseUsrK = CurrentPhoto.UsrK;
			a.ObjectType = Model.Entities.ObjectType.Photo;
			a.ObjectK = CurrentPhoto.K;
			if (CurrentPhoto.EventK > 0)
				a.ObjectString = "Photo in " + CurrentPhoto.Event.FriendlyName;
			else if (CurrentPhoto.ArticleK > 0)
				a.ObjectString = "Photo in " + CurrentPhoto.Article.FriendlyName;
			a.ReportDescription = Cambro.Web.Helpers.CleanHtml(ReportMessageTextBox.Text);
			a.ReportDateTime = DateTime.Now;
			a.Status = Abuse.StatusEnum.New;
			a.Update();

			Usr.Current.UpdateAbuseTrackers();
			CurrentPhoto.Usr.UpdateAbuseTrackers();
			Bobs.Global.UpdatePhotoAbuseReports();

			DonePanel.Visible = true;
			ReportPanel.Visible = false;
			ReportP.InnerHtml = "Thanks for reporting this. Our moderators will investigate, and you will receive an email when it's resolved. <a href=\"" + CurrentPhoto.Url() + "\">Click here to go back to the photo</a>";


		}
		#endregion

	}
}

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

namespace Spotted.Pages.Galleries
{
	public partial class EditOld : DsiUserControl, IRefreshable
	{

		//iu.addParam("Action", "http://<%= Vars.DomainName %>/Support/PhotoUpload.aspx?UsrK=<%= Bobs.Usr.Current.K %>&LoginString=<%= Bobs.Usr.Current.LoginString %>&GalleryK=<%= CurrentGallery.K %>");


		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentGallery != null)
				CurrentGallery.AddRelevant(ContainerPage);

			ContainerPage.UseLeftHandSideForContent = true;
			
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!CanEdit)
				throw new DsiUserFriendlyException("You can't edit this gallery!");

			ContainerPage.TemplateForm.Attributes["enctype"] = "multipart/form-data";
			ContainerPage.SetPageTitle("Edit gallery");
			BackToEditArticlePanel.Visible = CurrentGallery.Article != null;
			bindNow();
			Info_Load(sender, e);
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			//this.uiYourPhotos.Visible = this.PhotoProcessingPanel.Visible || this.PhotoModeratePanel.Visible || this.PhotoEnabledPanel.Visible;

			 
			ScriptManager.RegisterStartupScript(uiYourPhotos, uiYourPhotos.GetType(), "PhotoKList", "PhotoKList = new Array(" + Ks + ");", true);
			int filesLeft = FilesLeft;
			ScriptManager.RegisterStartupScript(uiYourPhotos, uiYourPhotos.GetType(), "MaxFileCount", "ChangeMaxFileCount(" + filesLeft.ToString() + ");", true);
			ScriptManager.RegisterStartupScript(uiYourPhotos, uiYourPhotos.GetType(), "AlterUploaderVisibility", "AlterUploaderVisibility(" + filesLeft.ToString() + ");", true);

			if (CurrentGallery.WatchUploads.HasValue)
				WatchCheckBox.Checked = CurrentGallery.WatchUploads.Value;

			this.PhotoUpload_PreRender(sender, e);
		}

		protected void WatchChange(object sender, EventArgs args)
		{
			CurrentGallery.WatchUploads = WatchCheckBox.Checked;
			CurrentGallery.Update();

		}

		protected string Serial
		{
			get
			{
				string s = "";
				if (HttpContext.Current == null || HttpContext.Current.Request.Url.Host.Equals("www.dontstayin.com"))
					s = "71050-10000-58FBC-DC76F-11A3B;72050-10000-6DC39-626E6-3A807";
				else if (HttpContext.Current.Request.Url.Host.Equals("solo"))
					s = "71050-1D28A-32FFB-82E44-29196;72050-1D28A-22FAD-13662-E9D8B";
				else if (HttpContext.Current.Request.Url.Host.Equals("endor"))
					s = "71050-1D28A-D28B7-8D04F-78302;72050-1D28A-5C090-3DE48-37AC0";
				else if (HttpContext.Current.Request.Url.Host.Equals("naboo"))
					s = "71050-1D28A-18D50-0204C-7ADE9;72050-1D28A-553BE-B86B3-A96EB";
				else if (HttpContext.Current.Request.Url.Host.Equals("hoth"))
					s = "71050-1D28A-46452-A9C54-3CECD;72050-1D28A-198F8-3CC48-134BD";
				else if (HttpContext.Current.Request.Url.Host.Equals("test.dontstayin.com"))
					s = "71050-1D28A-382CF-74C09-3552C;72050-1D28A-813C3-967E3-CF0A0";
				else if (HttpContext.Current.Request.Url.Host.Equals("beta.dontstayin.com"))
					s = "71050-1D28A-76787-7AFE5-75867;72050-1D28A-4BF81-6524C-B724A";
				else if (HttpContext.Current.Request.Url.Host.Equals("dev0.dontstayin.com"))
					s = "72050-10000-59BF9-D28F4-982C1;71050-10000-AE0BF-4545D-CCD5B";
				else if (HttpContext.Current.Request.Url.Host.Equals("localhost"))
					return "";
				else
					s = "71050-10000-58FBC-DC76F-11A3B;72050-10000-6DC39-626E6-3A807";

				return "iu.addParam(\"LicenseKey\", \"" + s + "\");";
			}
		}

		public string ActiveX
		{
			get
			{
				return ContainerPage.Url["java"].Exists ? "false" : "true";
			}
		}

		#region InfoPanel
		public void Info_Load(object o, System.EventArgs e)
		{
			if (!Page.IsPostBack)
				GalleryName.Text = CurrentGallery.Name;
			BindTitlePhoto();

		}

		public void BindTitlePhoto()
		{
			if (CurrentGallery.MainPhotoK > 0 && CurrentGallery.MainPhoto != null)
			{
				TitleImgCell.Visible = true;
				TitlePicImg.Src = CurrentGallery.MainPhoto.IconPath;
			}
			else
				TitleImgCell.Visible = false;
		}

		public void NameUpdateClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (!CanEdit)
					throw new DsiUserFriendlyException("You can't edit this gallery!");
				if (GalleryName.Text.Length > 30)
					throw new DsiUserFriendlyException("Name must be 30 chars or less");
				CurrentGallery.Name = Cambro.Web.Helpers.StripHtml(GalleryName.Text);
				CurrentGallery.Update();
			}
		}
		#endregion

		#region PhotoPanel
		#region DeleteSelected
		public void DeleteSelected(object o, System.EventArgs e)
		{
		//	DeleteSelectedOutput.InnerHtml = "";
			int deleted = 0;
			foreach (string str in Request.Form.Keys)
			{
				if (str.StartsWith("ucEditGalleryPhotoSelectK") && Request.Form[str].Equals("1"))
				{
					deleted++;
					string str1 = str.Substring(25);
		//			DeleteSelectedOutput.InnerHtml += "Deleting photo " + str1 + "...";
					try
					{
						int photoK = int.Parse(str1);
						Photo p = new Photo(photoK);
						if (p.GalleryK == CurrentGallery.K)
						{
							Delete.DeleteAll(p);
		//					DeleteSelectedOutput.InnerHtml += " Done.";
						}
						else
						{
		//					DeleteSelectedOutput.InnerHtml += " <b>FAILED</b> - photo is not in current gallery. Please contact admin with details.";
						}
					}
					catch
					{
		//				DeleteSelectedOutput.InnerHtml += " <b>FAILED</b> - exception while deleting photo. Maybe a photo moderator already deleted it.";
					}
		//			DeleteSelectedOutput.InnerHtml += "<br>";
				}
			}

		//	if (deleted == 0)
		//		DeleteSelectedOutput.InnerHtml += "<b>NO PHOTOS DELETED</b> - You didn't select any photos.";

		//	DeleteSelectedOutput.Visible = true;

			bindNow();
		}
		#endregion
		string Ks = "0";
		void bindNow()
		{
			Ks = "0";
			BindProcessing();
			BindEnabled(ref Ks);
			BindModerate(ref Ks);
			PhotoActionsPanel.Visible = PhotoEnabledPanel.Visible || PhotoModeratePanel.Visible;
			GalleryEmptyPanel.Visible = !PhotoProcessingPanel.Visible && !PhotoEnabledPanel.Visible && !PhotoModeratePanel.Visible;


		}
		public void BindProcessing()
		{
			Query q = new Query();
			q.Columns = Templates.Photos.AdminProcessing.Columns;
			q.QueryCondition = new And(new Q(Photo.Columns.GalleryK, CurrentGallery.K), new Q(Photo.Columns.Status, Photo.StatusEnum.Processing));
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			PhotoSet ps = new PhotoSet(q);

			if (ps.Count == 0)
				PhotoProcessingPanel.Visible = false;
			else
			{
				PhotoProcessingPanel.Visible = true;
				PhotoProcessingDataList.Controls.Clear();
				PhotoProcessingDataList.DataSource = ps;
				PhotoProcessingDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/AdminProcessing.ascx");
				PhotoProcessingDataList.DataBind();
			}
		}

		public void BindEnabled(ref string Ks)
		{
			Query q = new Query();
			q.Columns = Templates.Photos.Admin.Columns;
			q.QueryCondition = new And(new Q(Photo.Columns.GalleryK, CurrentGallery.K), new Q(Photo.Columns.Status, Photo.StatusEnum.Enabled));
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			PhotoSet ps = new PhotoSet(q);

			if (ps.Count == 0)
				PhotoEnabledPanel.Visible = false;
			else
			{
				PhotoEnabledPanel.Visible = true;
				PhotoEnabledDataList.Controls.Clear();
				PhotoEnabledDataList.DataSource = ps;
				PhotoEnabledDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Admin.ascx");
				PhotoEnabledDataList.DataBind();
				foreach (Photo p in ps)
				{
					Ks += (Ks == "" ? "" : ",") + p.K.ToString();
				}
			}
			
		}
		public void BindModerate(ref string Ks)
		{
			Query q = new Query();
			q.Columns = Templates.Photos.Admin.Columns;
			q.QueryCondition = new And(new Q(Photo.Columns.GalleryK, CurrentGallery.K), new Q(Photo.Columns.Status, Photo.StatusEnum.Moderate));
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			PhotoSet ps = new PhotoSet(q);

			if (ps.Count == 0)
				PhotoModeratePanel.Visible = false;
			else
			{
				PhotoModeratePanel.Visible = true;
				PhotoModerateDataList.Controls.Clear();
				PhotoModerateDataList.DataSource = ps;
				PhotoModerateDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Admin.ascx");
				PhotoModerateDataList.DataBind();
				foreach (Photo p in ps)
				{
					Ks += (Ks == "" ? "" : ",") + p.K.ToString();
				}
			}
		}
		public void PhotoRefreshButton_Click(object o, System.EventArgs e)
		{
			bindNow();
		}

		#region IRefreshable Members
		public void Refresh()
		{
			bindNow();
			BindTitlePhoto();
		}

		#endregion
		#endregion

		#region PhotoUploadPanel
		public void PhotoUpload_PreRender(object o, System.EventArgs e)
		{
			this.DataBind();
			NotSpotterPanel.Visible = !Usr.Current.IsSpotter;
			//PhotoUploadPanel.Visible = Usr.Current.IsSpotter || FilesLeft > 0;

			JavaPanel.Visible = !ContainerPage.Url["java"].Exists;

			//if (Usr.Current.CanUploadTo(CurrentGallery))
			//{
			//    PanelNoUpload.Style["display"] = "none";
			//    PanelUpload.Style.Remove("display");
			//}
			//else
			//{
			//    PanelNoUpload.Style.Remove("display");
			//    PanelUpload.Style["display"] = "none";
			//}
		}
		public int FilesLeft
		{
			get
			{
				int files = 10;

				if (Usr.Current.IsSpotter)
					files = 100;

				files = files - CurrentGallery.GetTotalPhotosIncludingProcessing();

				if (files < 0)
					files = 0;

				return files;
			}
		}
		#endregion

		#region CurrentGallery
		public Bobs.Gallery CurrentGallery
		{
			get
			{
				return ContainerPage.Url.ObjectFilterGallery;
			}
		}
		#endregion

		#region CanEdit
		protected bool CanEdit
		{
			get
			{
				return (Usr.Current.K == CurrentGallery.OwnerUsrK || Usr.Current.IsSenior);
			}
		}
		#endregion

 
	}
}

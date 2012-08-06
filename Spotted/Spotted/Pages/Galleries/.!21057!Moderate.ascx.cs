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
	public partial class Moderate : DsiUserControl, IRefreshable
	{
		protected DataGrid GalleriesDataGrid;
		protected DataList PhotoDataList;
		protected Panel PhotosPanel;
		protected Button DeleteButton, DeleteSelectedButton;
		protected Button DeleteButton1, DeleteSelectedButton1;
		protected TextBox AdminNoteTextBox;
		protected Panel GalleriesPanel;
		protected Panel DonePanel, InfoPanel;

		void Bind()
		{
			if (CurrentGallery != null)
			{
				Query q = new Query();
				q.Columns = Templates.Photos.Admin.Columns;
				q.QueryCondition = new And(
					new Q(Photo.Columns.GalleryK, CurrentGallery.K), 
					new Q(Photo.Columns.Status, Photo.StatusEnum.Moderate));
				PhotoSet ps = new PhotoSet(q);

				ArrayList al = new ArrayList();
				foreach (Photo p in ps)
					al.Add(p.K);
				this.ViewState["Photos"] = al;

				PhotoDataList.DataSource = ps;
				PhotoDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Admin.ascx");
				PhotoDataList.DataBind();
				PhotosPanel.Visible = ps.Count > 0;


				Query tsq = new Query();
				tsq.QueryCondition = new Q(Gallery.Columns.K, CurrentGallery.K);
				GallerySet gs = new GallerySet(tsq);
				GalleriesDataGrid.DataSource = gs;
				GalleriesDataGrid.DataBind();

			}
			else
			{
				Gallery g = GetNextGallery();
				if (g != null)
					Response.Redirect(g.UrlApp("moderate"));
				else
				{
					DonePanel.Visible = true;
					InfoPanel.Visible = false;
				}
				PhotosPanel.Visible = false;
			}
		}


		public void FixDodgey(object o, System.EventArgs e)
		{
			Query tsq = new Query();
			tsq.QueryCondition = new Q(Gallery.Columns.TotalPhotos, QueryOperator.NotEqualTo, Gallery.Columns.LivePhotos, true);
			GallerySet gs = new GallerySet(tsq);

			foreach (Gallery g in gs)
			{
				g.UpdateStats(null, true);
			}

		}

		public string GalleryCount
		{
			get
			{
				Query tsq = new Query();
				tsq.QueryCondition = new Q(Gallery.Columns.TotalPhotos, QueryOperator.NotEqualTo, Gallery.Columns.LivePhotos, true);
				tsq.ReturnCountOnly = true;
				GallerySet gs = new GallerySet(tsq);
				return gs.Count.ToString();
			}
		}

		bool Random
		{
			get
			{
				return (!ContainerPage.Url["mode"].IsNull && ContainerPage.Url["mode"].Equals("random"));
			}
		}

		public Gallery GetNextGallery()
		{
			Query tsq = new Query();
			tsq.QueryCondition = new Q(Gallery.Columns.TotalPhotos, QueryOperator.NotEqualTo, Gallery.Columns.LivePhotos, true);
			// order by: if in Random mode, any gallery, otherwise favour Article galleries first
			if (Random)
			{
				tsq.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			}
			else
			{
				tsq.OrderBy = new OrderBy(new OrderBy("case when [ArticleK] > 0 then 1 else 0 end desc"), new OrderBy(OrderBy.OrderDirection.Random));
			}

			tsq.TopRecords = 1;
			GallerySet gs = new GallerySet(tsq);
			if (gs.Count > 0)
				return gs[0];
			else
				return null;
		}

		public void GalleriesDataGrid_ItemDataBound(object o, DataGridItemEventArgs e)
		{
			if (e.Item.DataItem is Gallery)
			{
				Gallery g = (Gallery)e.Item.DataItem;
				if (g.K == CurrentGallery.K)
				{
					e.Item.Style["background-color"] = "#FED551";
				}
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Usr.Current == null || !Usr.Current.IsSenior)
				throw new DsiUserFriendlyException("You can't access this page");

			DeleteButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			DeleteSelectedButton.Attributes["onclick"] = "return confirm('Are you sure?');";

			DeleteButton1.Attributes["onclick"] = "return confirm('Are you sure?');";
			DeleteSelectedButton1.Attributes["onclick"] = "return confirm('Are you sure?');";

			Bind();

			if (!Page.IsPostBack && CurrentGallery != null)
				AdminNoteTextBox.Text = CurrentGallery.AdminNote;

			SetPageTitle("New Photos");
		}

		public void SaveAdminNote(object o, System.EventArgs e)
		{
			CurrentGallery.AdminNote = AdminNoteTextBox.Text + "\n[" + Usr.Current.NickName + ", " + DateTime.Now.ToString() + "]\n\n";
			CurrentGallery.Update();
			AdminNoteTextBox.Text = CurrentGallery.AdminNote;
		}

		protected HtmlGenericControl SelectedOutputP;
		#region EnableSelected
		public void EnableSelected(object o, System.EventArgs e)
		{
			int enabled = 0;
			bool sentChatAlert = false;

			//			bool galleryEmptyBefore = false;
			//			if (CurrentGallery.LivePhotos==0)
			//				galleryEmptyBefore = true;


			SelectedOutputP.InnerHtml = "";
			foreach (string str in Request.Form.Keys)
			{
				if (str.StartsWith("ucEditGalleryPhotoSelectK") && Request.Form[str].Equals("1"))
				{
					enabled++;
					string str1 = str.Substring(25);
					SelectedOutputP.InnerHtml += "Enabling photo " + str1 + "...";
					try
					{
						int photoK = int.Parse(str1);
						Photo p = new Photo(photoK);
						if (p.GalleryK != CurrentGallery.K)
						{
							SelectedOutputP.InnerHtml += " <b>FAILED</b> - photo is not in current gallery. Please contact admin with details.";
						}
						else if (!p.Status.Equals(Photo.StatusEnum.Moderate))
						{
							SelectedOutputP.InnerHtml += " <b>FAILED</b> - photo is not new. Maybe someone already enabled it?";
						}
						else
						{
							p.Status = Photo.StatusEnum.Enabled;
							p.EnabledByUsrK = Usr.Current.K;
							p.EnabledDateTime = DateTime.Now;
							p.Update();
							if (!sentChatAlert)
							{
								p.SendPhotoChatAlerts();
								sentChatAlert = true;
							}
							SelectedOutputP.InnerHtml += " Done.";
						}

					}
					catch
					{
						SelectedOutputP.InnerHtml += " <b>FAILED</b> - exception while deleting photo. Maybe someone already deleted it?";
					}
					SelectedOutputP.InnerHtml += "<br>";
				}
			}

			if (enabled == 0)
				SelectedOutputP.InnerHtml += "<b>NO PHOTOS ENABLED</b> - You didn't select any photos.";

			SelectedOutputP.Visible = true;

			if (enabled > 0)
			{
				CurrentGallery.LastLiveDateTime = DateTime.Now;
				CurrentGallery.UpdateStats(null, true);
				CurrentGallery.UpdatePhotoOrder(null);

				CurrentGallery.Owner.UpdateTotalPhotos(null);

				if (CurrentGallery.Event != null)
				{
					CurrentGallery.Event.UpdateTotalPhotos(null);
					CurrentGallery.Event.UpdateTotalComments(null);
				}
				if (CurrentGallery.Article != null)
					CurrentGallery.Article.UpdateTotalComments(null);

				//	if (galleryEmptyBefore && CurrentGallery.LivePhotos>0)
				//	{
				//		//Send emails...
				//		new System.Threading.Thread(new ThreadStart(CurrentGallery.SendNewGalleryEmails)).Start();
				//	}
			}

			Bind();
			ContainerPage.AnchorSkip("ActionsPanel");
		}
		#endregion
		#region DeleteSelected
		public void DeleteSelected(object o, System.EventArgs e)
		{
			SelectedOutputP.InnerHtml = "";
			int deleted = 0;
			foreach (string str in Request.Form.Keys)
			{
				if (str.StartsWith("ucEditGalleryPhotoSelectK") && Request.Form[str].Equals("1"))
				{
					deleted++;
					string str1 = str.Substring(25);
					SelectedOutputP.InnerHtml += "Deleting photo " + str1 + "...";
					try
					{
						int photoK = int.Parse(str1);
						Photo p = new Photo(photoK);
						if (p.GalleryK == CurrentGallery.K)
						{
							try
							{
								Bobs.Delete.DeleteAll(p);
								SelectedOutputP.InnerHtml += " Done.";
							}
							catch
							{
								SelectedOutputP.InnerHtml += " <b>FAILED</b> - Exception while deleting photo: " + p.K;
							}
						}
						else
						{
							SelectedOutputP.InnerHtml += " <b>FAILED</b> - photo is not in current gallery. Please contact admin with details.";
						}
					}
					catch
					{
						SelectedOutputP.InnerHtml += " <b>FAILED</b> - exception while deleting photo. Maybe a photo moderator already deleted it.";
					}
					SelectedOutputP.InnerHtml += "<br>";
				}
			}

			if (deleted == 0)
				SelectedOutputP.InnerHtml += "<b>NO PHOTOS DELETED</b> - You didn't select any photos.";

			SelectedOutputP.Visible = true;

			if (deleted > 0)
			{
				CurrentGallery.UpdateStats(null, true);
				CurrentGallery.UpdatePhotoOrder(null);

				CurrentGallery.Owner.UpdateTotalPhotos(null);

				if (CurrentGallery.Event != null)
				{
					CurrentGallery.Event.UpdateTotalPhotos(null);
					CurrentGallery.Event.UpdateTotalComments(null);
				}
				if (CurrentGallery.Article != null)
					CurrentGallery.Article.UpdateTotalComments(null);

				Mailer sm = new Mailer();
				sm.UsrRecipient = CurrentGallery.Owner;
				sm.Body = @"<p>
			Our photo moderators have deleted some of your photos. " + deleted.ToString() + " " + (deleted == 1 ? "photo was" : "photos were") + @" deleted from the <a href=""[LOGIN(" + CurrentGallery.UrlApp("edit") + @")]"">" + CurrentGallery.NameSafe + @"</a>.
		</p>
		<p>
			Please take a couple of minutes to read through the instructions below.
		</p>
		<p>
			Please DO NOT upload photos which...
		</p>
		<ul>
			<li>need rotating.</li>
			<li>are duplicated.</li>
			<li>are out of focus.</li>

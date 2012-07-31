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

namespace Spotted.Pages
{
	public partial class MyPicture : DsiUserControl
	{
		protected Panel PanelNoPhotosMe;

		protected HtmlGenericControl AllPhotosPageP1, AllPhotosPageP2;
		protected HyperLink AllPhotosPrevPage1, AllPhotosNextPage1, AllPhotosPrevPage2, AllPhotosNextPage2;
		protected Controls.Cropper Cropper;
		protected Panel PanelUsrPic;
		protected HtmlImage PicImg;
		protected Button ChatReCropButton, ReCropButton, CancelButton;
		protected HtmlAnchor PicAnchor;

		public bool UseMasterImageForCropping{get{return false;}}

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (Usr.Current.HasPic)
			{
				PicImg.Src = Usr.Current.PicPath;
				if (Usr.Current.PicPhotoK > 0)
					PicAnchor.HRef = ContainerPage.Url.CurrentUrl("type", "pic", "k", Usr.Current.PicPhotoK);

				
			}
			else
			{
				PanelUsrPic.Visible = false;
			}

			if (Usr.Current.HasChatPic)
			{
				
				ChatPicImg.Src = Usr.Current.ChatPicPath;
				if (Usr.Current.ChatPicPhotoK > 0)
					ChatPicAnchor.HRef = ContainerPage.Url.CurrentUrl("type", "chat", "k", Usr.Current.PicPhotoK);
			}
			else
			{
				PanelChatPic.Visible = false;
			}


			if (CurrentPhoto != null)
			{
				ChangePanel(PanelCrop);
				try
				{
					UsrPhotoMe pm = new UsrPhotoMe(Usr.Current.K, CurrentPhoto.K);
				}
				catch
				{
					throw new DsiUserFriendlyException("You're not in this photo!");
				}

				if (CurrentPhoto.K == Usr.Current.PicPhotoK && !Page.IsPostBack)
				{
					if (ContainerPage.Url["type"] == "pic")
						Cropper.SetState(Usr.Current.PicState);
					else
						Cropper.SetState(Usr.Current.ChatPicState);
				}

				if (UseMasterImageForCropping)
				{
					Cropper.ImageUrl = CurrentPhoto.MasterPath;
					Cropper.ImageGuid = CurrentPhoto.Master;
					Cropper.ImageStore = Storage.Stores.Master;
				}
				else
				{
					Cropper.ImageUrl = CurrentPhoto.CropPath;
					Cropper.ImageGuid = CurrentPhoto.Crop;
					Cropper.ImageStore = Storage.Stores.Pix;
				}

				if (ContainerPage.Url["type"] == "pic")
				{
					Cropper.CropWidth = 100;
					Cropper.CropHeight = 100;
				}
				else
				{
					Cropper.CropWidth = 300;
					Cropper.CropHeight = 100;
				}

				this.ViewState["PhotoK"] = CurrentPhoto.K;
			}
			else if (Usr.Current.PhotosMeCount == 0)
			{
				ChangePanel(PanelNoPhotosMe);
			}
			//else if (Usr.Current.PhotosMeCount == 1)
			//{
			//    ChangePanel(PanelCrop);
			//    CancelButton.Visible = false;
			//    if (!Page.IsPostBack)
			//        Cropper.SetState(Usr.Current.PicState);
			//    PanelUsrPic.Visible = true;
			//    PhotoSet photosMe = Usr.Current.PhotosMe(new ColumnSet(Photo.Columns.K, Photo.Columns.Crop, Photo.Columns.ContentDisabled, Photo.Columns.Master, Photo.Columns.Original), 2);
			//    if (photosMe.Count == 1)
			//    {
			//        if (UseMasterImageForCropping)
			//        {
			//            Cropper.ImageUrl = photosMe[0].MasterPath;
			//            Cropper.ImageGuid = photosMe[0].Master;
			//            Cropper.ImageStore = Storage.Stores.Master;
			//        }
			//        else
			//        {
			//            Cropper.ImageUrl = photosMe[0].CropPath;
			//            Cropper.ImageGuid = photosMe[0].Crop;
			//            Cropper.ImageStore = Storage.Stores.Pix;
			//        }
			//        this.ViewState["PhotoK"] = photosMe[0].K;
			//    }
			//    else
			//    {
			//        ChangePanel(PanelNoPhotosMe);
			//        Usr.Current.UpdatePhotosMeCount(true, null);
			//    }
			//}
			else
			{
				ChangePanel(PanelImages);
				BindImages();
			}

			if (Usr.Current.PicPhotoK == 0)
				ReCropButton.Visible = false;

			if (Usr.Current.ChatPicPhotoK == 0)
				ChatReCropButton.Visible = false;
		}

		public void BindImages()
		{
			int photosPerPage = 15;
			Query q = new Query();

			q.Paging.RecordsPerPage = photosPerPage;
			q.Paging.RequestedPage = AllPhotosPage;

			q.NoLock = true;
			q.Columns = new ColumnSet(
				Photo.Columns.Thumb,
				Photo.Columns.ThumbHeight,
				Photo.Columns.ThumbWidth,
				Photo.Columns.K,
				Photo.Columns.ContentDisabled,
				Photo.Columns.Web);
			q.TableElement = Photo.UsrMeJoin;
			q.QueryCondition = new Q(Usr.Columns.K, Usr.Current.K);
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
			q.TopRecords = (AllPhotosPage * photosPerPage) + 1;
			PhotoSet ps = new PhotoSet(q);

			if (AllPhotosPage != ps.Paging.ReturnedPage)
				AllPhotosPage = ps.Paging.ReturnedPage;

			if (ps.Paging.ShowNoLinks)
			{
				AllPhotosPageP1.Visible = false;
				AllPhotosPageP2.Visible = false;
			}
			else
			{
				AllPhotosPageP1.Visible = true;
				AllPhotosPageP2.Visible = true;

				string urlNextPage = ContainerPage.Url.CurrentUrl("page", ((int)(AllPhotosPage + 1)).ToString());
				string urlPrevPage = "";
				if (AllPhotosPage == 2)
					urlPrevPage = ContainerPage.Url.CurrentUrl("page", null);
				else
					urlPrevPage = ContainerPage.Url.CurrentUrl("page", ((int)(AllPhotosPage - 1)).ToString());

				AllPhotosNextPage1.Enabled = ps.Paging.ShowNextPageLink;
				AllPhotosNextPage2.Enabled = ps.Paging.ShowNextPageLink;
				AllPhotosPrevPage1.Enabled = ps.Paging.ShowPrevPageLink;
				AllPhotosPrevPage2.Enabled = ps.Paging.ShowPrevPageLink;

				AllPhotosNextPage1.NavigateUrl = urlNextPage;
				AllPhotosNextPage2.NavigateUrl = urlNextPage;
				AllPhotosPrevPage1.NavigateUrl = urlPrevPage;
				AllPhotosPrevPage2.NavigateUrl = urlPrevPage;
			}
			ImagesDataList.DataSource = ps;
			ImagesDataList.DataBind();
		}

		public void Cancel_Click(object o, System.EventArgs e)
		{
			Response.Redirect(ContainerPage.Url.CurrentUrl("k", null, "type", null));
		}
		public void ReCrop_Click(object o, System.EventArgs e)
		{
			Response.Redirect(ContainerPage.Url.CurrentUrl("type", "pic", "k", Usr.Current.PicPhotoK));
		}
		public void ChatReCrop_Click(object o, System.EventArgs e)
		{
			Response.Redirect(ContainerPage.Url.CurrentUrl("type", "chat", "k", Usr.Current.ChatPicPhotoK));
		}
		public void Save_Click(object o, System.EventArgs e)
		{
			try
			{
				UsrPhotoMe pm = new UsrPhotoMe(Usr.Current.K, (int)this.ViewState["PhotoK"]);
			}
			catch
			{
				throw new DsiUserFriendlyException("You're not in this photo!");
			}

			if (Cropper.ClientDataIsCorrupt)
			{
				Response.Redirect(ContainerPage.Url.CurrentUrl());
				return;
			}

			if (ContainerPage.Url["type"] == "pic")
			{
				
				bool hasOldPic = Usr.Current.HasPic;
				Guid oldPic = Usr.Current.HasPic ? Usr.Current.Pic : Guid.Empty;

				bool hasOldChatPic = Usr.Current.HasChatPic;
				Guid oldChatPic = Usr.Current.HasChatPic ? Usr.Current.ChatPic.Value : Guid.Empty;

				Usr.Current.Pic = Guid.NewGuid();
				Usr.Current.PicPhotoK = (int)this.ViewState["PhotoK"];
				Usr.Current.PicState = Cropper.GetState();

				Cropper.Store(Usr.Current.Pic, Usr.Current, "Pic");

				Cropper.CropHeight = 100;
				Cropper.CropWidth = 300;
				Cropper.ResetStateToEnsureImageIsWithinCropArea();

				Usr.Current.ChatPic = Guid.NewGuid();
				Usr.Current.ChatPicPhotoK = (int)this.ViewState["PhotoK"];
				Usr.Current.ChatPicState = Cropper.GetState();

				Cropper.Store(Usr.Current.ChatPic.Value, Usr.Current, "ChatPic");

				Usr.Current.Update();

				if (hasOldPic)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

				if (hasOldChatPic)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldChatPic, "jpg");

				Response.Redirect(ContainerPage.Url.CurrentUrl("type", "chat", "k", Usr.Current.ChatPicPhotoK));

			}
			else if (ContainerPage.Url["type"] == "chat")
			{
				if (Usr.Current.PicPhotoK != (int)this.ViewState["PhotoK"])
					throw new Exception("You must use the same photo for your profile picture as your chat picture.");

				bool hasOldChatPic = Usr.Current.HasChatPic;
				Guid oldChatPic = Usr.Current.HasChatPic ? Usr.Current.ChatPic.Value : Guid.Empty;

				Usr.Current.ChatPic = Guid.NewGuid();
				Usr.Current.ChatPicPhotoK = (int)this.ViewState["PhotoK"];
				Usr.Current.ChatPicState = Cropper.GetState();

				Cropper.Store(Usr.Current.ChatPic.Value, Usr.Current, "ChatPic");

				Usr.Current.Update();

				if (hasOldChatPic)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldChatPic, "jpg");

				Response.Redirect(ContainerPage.Url.CurrentUrl("k", null));
			}
			
		}
		public void Delete_Click(object o, System.EventArgs e)
		{
			DeletePic();
			Response.Redirect(ContainerPage.Url.CurrentUrl("k", null));
		}

		#region AllPhotosPage
		public int AllPhotosPage
		{
			get
			{
				if (allPhotosPage == -1)
				{
					if (ContainerPage.Url["page"].IsInt)
						return ContainerPage.Url["page"];
					else
						return 1;
				}
				return allPhotosPage;
			}
			set
			{
				allPhotosPage = value;
			}
		}
		private int allPhotosPage = -1;
		#endregion

		#region PanelImages
		protected Panel PanelImages, PanelCrop;
		protected DataList ImagesDataList;

		#region DeletePic
		void DeletePic()
		{
			Guid oldPic = Usr.Current.Pic;
			Guid oldChatPic = Usr.Current.ChatPic ?? Guid.Empty;

			Usr.Current.Pic = Guid.Empty;
			Usr.Current.PicPhotoK = 0;
			Usr.Current.PicState = "";

			Usr.Current.ChatPic = Guid.Empty;
			Usr.Current.ChatPicPhotoK = 0;
			Usr.Current.ChatPicState = "";

			Usr.Current.Update();

			try
			{
				if (oldPic != Guid.Empty)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");
			}
			catch { }

			try
			{
				if (oldChatPic != Guid.Empty)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldChatPic, "jpg");
			}
			catch { }
		}
		#endregion
		#endregion

		#region CurrentPhoto
		public Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null && ContainerPage.Url["K"].IsInt)
					currentPhoto = new Photo(ContainerPage.Url["K"]);
				return currentPhoto;
			}
			set
			{
				currentPhoto = value;
			}
		}
		private Photo currentPhoto;
		#endregion

		void ChangePanel(Panel p)
		{

			PanelImages.Visible = p.Equals(PanelImages);
			PanelNoPhotosMe.Visible = p.Equals(PanelNoPhotosMe);
			PanelCrop.Visible = p.Equals(PanelCrop);
		}

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

		}
		#endregion
	}
}

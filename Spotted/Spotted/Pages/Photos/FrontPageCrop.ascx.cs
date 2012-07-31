using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages.Photos
{
	public partial class FrontPageCrop : DsiUserControl
	{
		Photo CurrentPhoto { get { return ContainerPage.Url.ObjectFilterPhoto; } }
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotAdmin("");
			ContainerPage.UseLeftHandSideForContent = true;

			if (CurrentPhoto.ArticleK > 0)
				ParentName.Text = CurrentPhoto.Article.Name;
			else
				ParentName.Text = CurrentPhoto.Event.FriendlyName;

			if (!Page.IsPostBack)
			{
				Cropper.CropWidth = 600;
				Cropper.CropHeight = 250;

				Cropper.ImageUrl = ContainerPage.Url.ObjectFilterPhoto.CropPath;
				Cropper.ImageGuid = ContainerPage.Url.ObjectFilterPhoto.Crop;
				Cropper.ImageStore = Storage.Stores.Pix;

				if (!CurrentPhoto.FrontPagePic.HasValue || CurrentPhoto.FrontPagePic.Value == Guid.Empty)
				{
					//Cropper.ResetStateToEnsureImageIsWithinCropArea();
					//CurrentPhoto.FrontPagePic = Guid.NewGuid();
					//CurrentPhoto.FrontPagePicState = Cropper.GetState();
					//Cropper.Store(CurrentPhoto.FrontPagePic.Value, CurrentPhoto, "FrontPagePic");
					//CurrentPhoto.Update();

				}
				else
				{
					Cropper.SetState(CurrentPhoto.FrontPagePicState);
				}
				if (CurrentPhoto.FrontPagePic.HasValue)
					CurrentImage.Src = Storage.Path(CurrentPhoto.FrontPagePic.Value, Storage.Stores.Pix);

				CheckBox.Checked = CurrentPhoto.PhotoOfWeek;
				if (CurrentPhoto.PhotoOfWeek)
					Date.SelectedDate = CurrentPhoto.PhotoOfWeekDateTime;
				else
					Date.SelectedDate = DateTime.Today;
				Caption.Text = CurrentPhoto.PhotoOfWeekCaption;

				if (CurrentPhoto.FrontPageCaptionClass != null)
				{
					if (CurrentPhoto.FrontPageCaptionClass.Contains("Black"))
						ColourWhiteOnBlack.Checked = true;
					else if (CurrentPhoto.FrontPageCaptionClass.Contains("White"))
						ColourBlackOnWhite.Checked = true;

					CornerTopLeft.Checked = CurrentPhoto.FrontPageCaptionClass.Contains("Top") && CurrentPhoto.FrontPageCaptionClass.Contains("Left");
					CornerTopRight.Checked = CurrentPhoto.FrontPageCaptionClass.Contains("Top") && CurrentPhoto.FrontPageCaptionClass.Contains("Right");
					CornerBottomLeft.Checked = CurrentPhoto.FrontPageCaptionClass.Contains("Bottom") && CurrentPhoto.FrontPageCaptionClass.Contains("Left");
					CornerBottomRight.Checked = CurrentPhoto.FrontPageCaptionClass.Contains("Bottom") && CurrentPhoto.FrontPageCaptionClass.Contains("Right");
				}
			}


		}

		public void Save_Click(object o, System.EventArgs e)
		{
			if (Cropper.ClientDataIsCorrupt)
			{
				Response.Redirect(ContainerPage.Url.CurrentUrl());
				return;
			}

			CurrentPhoto.PhotoOfWeek = CheckBox.Checked;
			CurrentPhoto.PhotoOfWeekDateTime = new DateTime(Date.SelectedDate.Year, Date.SelectedDate.Month, Date.SelectedDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			CurrentPhoto.PhotoOfWeekCaption = Caption.Text;

			string colour = ColourWhiteOnBlack.Checked ? "Black" : "White";
			string corner = CornerTopLeft.Checked ? "Top Left" : CornerTopRight.Checked ? "Top Right" : CornerBottomLeft.Checked ? "Bottom Left" : "Bottom Right";

			CurrentPhoto.FrontPageCaptionClass = colour + " " + corner;

			bool hasOldPic = CurrentPhoto.HasFrontPagePic;
			Guid oldPic = hasOldPic ? CurrentPhoto.FrontPagePic.Value : Guid.Empty;

			CurrentPhoto.FrontPagePic = Guid.NewGuid();
			CurrentPhoto.FrontPagePicState = Cropper.GetState();

			Cropper.Store(CurrentPhoto.FrontPagePic.Value, CurrentPhoto, "FrontPagePic");

			CurrentPhoto.Update();

			if (hasOldPic)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			if (CheckBox.Checked)
			{
				try
				{
					FacebookPost.CreateFrontPagePhotoDsi(CurrentPhoto, Caption.Text);
				}
				catch { }

				if (CurrentPhoto.Usr.FacebookConnected && CurrentPhoto.Usr.FacebookStoryPhotoFeatured)
				{
					try
					{
						FacebookPost.CreateFrontPagePhoto(CurrentPhoto.Usr, CurrentPhoto);
					}
					catch { }
				}

			}

			
			Response.Redirect(CurrentPhoto.UrlApp("frontpagecrop") + "?" + Cambro.Misc.Utility.GenRandomChars(5));

		}
	}
}

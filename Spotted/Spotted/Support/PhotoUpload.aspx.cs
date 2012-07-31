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
using Bobs.JobProcessor;

namespace Spotted.Support
{
	public partial class PhotoUpload : System.Web.UI.Page
	{
		#region CurrentGallery
		public Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null)
				{
					if (GalleryK > 0)
					{
						currentGallery = new Gallery(GalleryK);
					}
					else if (EventK > 0)
					{
						//get oldest gallery for this event
						Query q = new Query();
						q.QueryCondition = new And(new Q(Gallery.Columns.OwnerUsrK, Usr.Current.K), new Q(Gallery.Columns.EventK, EventK));
						q.OrderBy = new OrderBy(Gallery.Columns.CreateDateTime, OrderBy.OrderDirection.Ascending);
						GallerySet gs = new GallerySet(q);
						if (gs.Count > 0)
							currentGallery = gs[0];
						else
						{
							Bobs.Event currentEvent = new Bobs.Event(EventK);
							//new gallery
							Gallery g = new Gallery();
							g.CreateDateTime = DateTime.Now;
							g.Name = Usr.Current.NickName + "'s photos";
							g.OwnerUsrK = Usr.Current.K;
							g.RunFinishedUploadingTask = false;

							if (currentEvent != null)
								g.EventK = EventK;
						//	else if (CurrentArticle != null)
						//		g.ArticleK = ArticleK;

							g.LivePhotos = 0;
							g.TotalPhotos = 0;
							g.UpdateUrlFragmentNoUpdate();
							g.Update();

							if (currentEvent != null)
							{
								Usr.Current.AttendEvent(currentEvent.K, true, Usr.Current.IsSpotter, null);
							}
							currentGallery = g;

						}
					}
				}
				return currentGallery;
			}
			set
			{
				currentGallery = value;
			}
		}
		private Gallery currentGallery;
		int EventK
		{
			get
			{
				if (!eventK.HasValue)
				{
					if (Request.QueryString["EventK"] != null)
						eventK = int.Parse(Request.QueryString["EventK"]);
					else
						eventK = 0;
				}
				return eventK.Value;
			}
		}
		int? eventK;
		int GalleryK
		{
			get
			{
				if (!galleryK.HasValue)
				{
					if (Request.QueryString["GalleryK"] != null)
						galleryK = int.Parse(Request.QueryString["GalleryK"]);
					else
						galleryK = 0;
				}
				return galleryK.Value;
			}
		}
		int? galleryK;
		#endregion
		#region CanUploadMore
		bool CanUploadMore
		{
			get
			{
				return true; // unlimited photos!
				//int totalPhotosIncludingProcessing = CurrentGallery.GetTotalPhotosIncludingProcessing();

				//if (Usr.Current != null && Usr.Current.IsSpotter && totalPhotosIncludingProcessing < 100)
				//    return true;

				//if (totalPhotosIncludingProcessing < 10)
				//    return true;

				//return false;
			}
		}
		#endregion
		private void Page_Init(object sender, System.EventArgs e)
		{
		//	if (Vars.DevEnv)
		//		System.Threading.Thread.Sleep(2000);

			//if (Usr.Current == null)
			//{
			//    string loginString = Request.QueryString["LoginString"];
			//    int usrK = int.Parse(Request.QueryString["UsrK"]);
			//    Usr u = new Usr(usrK);
			//    if (u.LoginString.ToLower().Equals(loginString.ToLower()))
			//    {
			//        u.LogInAsThisUserSilent(false);
			//    }
			//}
			Usr.Current.RegisterPageRequest(true, DateTime.Now);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			

		//	if (!CanUploadMore)
			//	throw new DsiUserFriendlyException("Sorry, you can't upload any more photos to this gallery - non-spotters are limited to 10 photos!");

		//	int PackageCount = int.Parse(Request.Form["PackageCount"]);
		//	int PackageIndex = int.Parse(Request.Form["PackageIndex"]);
		//	int Rotate = int.Parse(Request.Form["Angle_1"]);
		//	string Tags = Request.Form["Description_1"];

		//	HttpPostedFile SourceFile = Request.Files["SourceFile_1"];
			HttpPostedFile SourceFile = Request.Files[0];

			Random r = new Random();

			if (SourceFile.ContentLength <= Photo.MaxFileSizeInBytes &&
				(
					Photo.GetMediaType(SourceFile.FileName).Equals(Photo.MediaTypes.Image) ||
					Photo.GetMediaType(SourceFile.FileName).Equals(Photo.MediaTypes.Video)
				)
				)
			{
				//Photo.ProcessUploadedFile(SourceFile, CurrentGallery, r, Usr.Current, Rotate, Tags);
				Photo.ProcessUploadedFile(SourceFile, CurrentGallery, r, Usr.Current, 0, "");
			}
			if (Request.QueryString["source"] != null && Request.QueryString["source"] == "plupload")
			{
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.Write("File uploaded.");
				return;
			}
		}
	}
}

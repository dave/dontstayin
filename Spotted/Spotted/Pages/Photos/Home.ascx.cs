using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.ThreadControl;
using SpottedLibrary.Controls.LatestChat;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PagedRepeater;
 
namespace Spotted.Pages.Photos
{
	public partial class Home : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Photo p = null;
			if (ContainerPage.Url.HasPhotoObjectFilter)
			{
				p = ContainerPage.Url.ObjectFilterPhoto;
			}
			else if (ContainerPage.Url.HasGalleryObjectFilter)
			{
				try
				{
					if (ContainerPage.Url["photok"].IsInt)
						p = new Photo(ContainerPage.Url["photok"]);
				}
				catch { }

				if (p == null)
				{
					try
					{
						if (ContainerPage.Url.ObjectFilterGallery.MainPhotoK > 0 && ContainerPage.Url.ObjectFilterGallery.MainPhoto != null)
							p = ContainerPage.Url.ObjectFilterGallery.MainPhoto;
					}
					catch { }
				}

				if (p == null)
				{
					try
					{
						if (ContainerPage.Url.ObjectFilterGallery.LivePhotos > 0)
							p = ContainerPage.Url.ObjectFilterGallery.ChildPhotos(
								Photo.EnabledQueryCondition,
								Photo.DefaultOrder
							)[0];
					}
					catch { }
				}
			}
			string page = (1 + (p.GalleryTimeOrder / Spotted.Pages.Events.Photos.NumberOfPhotosPerPage)).ToString();
			if (page == "1") { page = null; }
			if (p.EventK > 0)
			{
				Response.Redirect(p.Event.UrlApp("photos", "gallery", p.GalleryK.ToString(), "photo", p.K.ToString(), "photopage", page));
			}
			else
			{
				Response.Redirect(p.Article.UrlApp("photos", "gallery", p.GalleryK.ToString(), "photo", p.K.ToString(), "photopage", page));
			}
		}


	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Bobs;
using Js.Controls.PhotoControl;
using System.Collections.Generic;

namespace Spotted.WebServices.Controls.PhotoBrowser
{
	/// <summary>
	/// Summary description for PhotoBrowserService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetRecentVideos(int pageNumber)
		{
			Common.IPagedDataService<Photo> photoPagedDataService = Photo.GetRecentVideos();
			Photo[] photoSet = photoPagedDataService.Page(pageNumber, Common.Properties.PhotoBrowser.IconsPerPage);

			return new PhotoResult()
			{
				photos = photoSet.ConvertAll(p => CreatePhotoStub(p, true)),
				lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage)
			};
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosByEventAndPage(int eventK, int pageNumber)
		{
			return GetPhotos(new Bobs.Event(eventK), pageNumber, false);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosByGalleryAndPage(int galleryK, int pageNumber)
		{
			Gallery g = new Gallery(galleryK);
			g.SetGalleryUsr(g.LivePhotos);
			return GetPhotos(g, pageNumber, false);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosByArticle(int articleK, int pageNumber)
		{
			return GetPhotos(new Bobs.Article(articleK), pageNumber, false);
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosOfUsr(int usrK, int pageNumber, int spottedByUsrK)
		{
			Common.IPagedDataService<Photo> photoPagedDataService =
				SpottedLibrary.Pages.Usrs.Photos.UsrPhotosController.GetUsrPhotosPagedDataService(new Bobs.Usr(usrK), spottedByUsrK);
			Photo[] photoSet = photoPagedDataService.Page(pageNumber, Common.Properties.PhotoBrowser.IconsPerPage);

			return new PhotoResult()
			{
				photos = photoSet.ConvertAll(p => CreatePhotoStub(p, true)),
				lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage)
			};
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetFavouritePhotosOfUsr(int usrK, int pageNumber)
		{
			Common.IPagedDataService<Photo> photoPagedDataService =
				SpottedLibrary.Pages.Usrs.FavouritePhotos.UsrFavouritePhotosController.GetUsrPhotosPagedDataService(new Bobs.Usr(usrK));
			Photo[] photoSet = photoPagedDataService.Page(pageNumber, Common.Properties.PhotoBrowser.IconsPerPage);

			return new PhotoResult()
			{
				photos = photoSet.ConvertAll(p => CreatePhotoStub(p, true)),
				lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage)
			};
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosByGroup(int groupK, int pageNumber)
		{
			Common.IPagedDataService<Photo> photoPagedDataService = 
				SpottedLibrary.Pages.Groups.Photos.GroupPhotosController.GetGroupPhotosPagedDataService(new Bobs.Group(groupK));
			Photo[] photoSet = photoPagedDataService.Page(pageNumber, Common.Properties.PhotoBrowser.IconsPerPage);

			return new PhotoResult()
			{
				photos = photoSet.ConvertAll(p => CreatePhotoStub(p, true)),
				lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage)
			};
		}

		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public PhotoResult GetPhotosByTag(int tagK, int pageNumber)
		{
			return GetPhotos(new Bobs.Tag(tagK), pageNumber, true, new Q(TagPhoto.Columns.Disabled, false),
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.DateTime, Bobs.OrderBy.OrderDirection.Descending));
		}


		private PhotoResult GetPhotos(Bobs.ChildInterfaces.IHasChildPhotos parentBob, int pageNumber, bool includeEventInfo)
		{
			return GetPhotos(parentBob, pageNumber, includeEventInfo, new Q(true),
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.ParentDateTime, OrderBy.OrderDirection.Ascending),
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.DateTime, OrderBy.OrderDirection.Ascending),
				new KeyValuePair<object, OrderBy.OrderDirection>(Photo.Columns.K, OrderBy.OrderDirection.Ascending));
		}

		private PhotoResult GetPhotos(Bobs.ChildInterfaces.IHasChildPhotos parentBob, int pageNumber, bool includeEventInfo, Q extraWhereConditions, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			Common.IPagedDataService<Photo> photoPagedDataService = parentBob.ChildPhotos(new And(Photo.EnabledQueryCondition, extraWhereConditions), orderBy);
			Photo[] photoSet = photoPagedDataService.Page(pageNumber, Common.Properties.PhotoBrowser.IconsPerPage);
			int lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage);
			
			return new PhotoResult()
			{
				photos = photoSet.ConvertAll(p => CreatePhotoStub(p, includeEventInfo)),
				lastPage = (int)Math.Ceiling((double)photoPagedDataService.Count / (double)Common.Properties.PhotoBrowser.IconsPerPage)
			};
		}

		private PhotoStub CreatePhotoStub(Bobs.Photo photo, bool includeEventInfo)
		{
			return new PhotoStub()
			{
				k = photo.K,
				url = photo.Url(),
				iconPath = photo.IconPath,
				webPath = photo.WebPath,
				thumbPath = photo.ThumbPath,
				thumbWidth = (int)(photo.ThumbWidth),
				thumbHeight = (int)(photo.ThumbHeight),
				width = (int)(photo.WebWidth),
				height = (int)(photo.WebHeight),
				takenByDetailsHtml = photo.TakenByDetailsHtml(includeEventInfo),
				usrLink = photo.Usr.Link(),
				isPhoto = photo.IsPhoto,
				isVideo = photo.IsVideo,
				usrsInPhotoHtml = photo.UsrsInPhotoHtml,
				usrIsInPhoto = Usr.Current != null ? photo.UsrsInPhoto.FindFirstIndex(u => u.K == Usr.Current.K).HasValue : false,
				rolloverMouseOverText = photo.RolloverMouseOverText,
				linkToPhotoUrl = photo.Link,
				embedThisPhotoHtml = photo.EmbedHtml,
				photoVideoLabel = photo.PhotoVideoLabel,
				quickBrowserUrl = photo.QuickBrowserUrl,
				downloadPhotoLinkHtml = photo.DownloadPhotoLinkHtml,
				videoMedWidth = photo.VideoMedWidth,
				videoMedHeight = photo.VideoMedHeight,
				videoMedPath = photo.VideoMedPath,

				isFavourite = photo.IsFavourite,
				isInCompetitionGroup = photo.IsInCompetitionGroup,
				canEnterCompetition = photo.CanEnterCompetition,

				photoUsageAdminString = Usr.Current != null && Usr.Current.IsAdmin ? photo.Usr.PhotoUsageAdminString : "",

				chatRoomGuid = new Chat.RoomSpec(SpottedScript.Controls.ChatClient.Shared.RoomType.Normal, Model.Entities.ObjectType.Photo, photo.K).Guid.Pack(),
				
				threadK = photo.ThreadK ?? 0,
				commentsCount = photo.ThreadK > 0 && photo.Thread != null ? photo.Thread.TotalComments : 0
			};
		}


		


		
		
		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public void SetAsPhotoOfWeek(int photoK, bool isPhotoOfWeek, string photoOfWeekCaption)
		{
			new Photo(photoK).SetAsPhotoOfWeek(isPhotoOfWeek, photoOfWeekCaption, true, true);
		}

		
		

		

		
	}
}

using System;
using System.Html;
using Js.Controls.PhotoControl;
using Js.Library;

namespace Js.Controls.PhotoBrowser
{
	public abstract class PhotoProvider
	{
		#region CurrentPhotoSet
		private PhotoResult currentPhotoSet;
		public PhotoResult CurrentPhotoSet
		{
			get
			{
				return currentPhotoSet;
			}
			protected set
			{
				currentPhotoSet = value;
			}
		}
		#endregion
		public EventHandler DoPostLoadPhotoSetActions;
		public EventHandler PhotoSetIsLoadingFromServer;
		protected abstract bool storePhotosInCache(PhotoResult photoSet, int pageNumber);
		protected abstract bool loadPhotosFromCache(int pageNumber);
		protected abstract void loadPhotosViaWebRequest(int pageNumber);

		protected void successCallback(PhotoResult result, object pageNumber, string methodName)
		{
			CurrentPhotoSet = result;
			storePhotosInCache(CurrentPhotoSet, (int)pageNumber);
			if (DoPostLoadPhotoSetActions != null)
				DoPostLoadPhotoSetActions(this, EventArgs.Empty);
		}
		
		public void LoadPhotos(int pageNumber)
		{
			if (loadPhotosFromCache(pageNumber))
			{
				if (DoPostLoadPhotoSetActions != null)
					DoPostLoadPhotoSetActions(this, EventArgs.Empty);
			}
			else
			{
				// this does doPostLoadPhotoSetActions too
				if (PhotoSetIsLoadingFromServer != null)
					PhotoSetIsLoadingFromServer(this, EventArgs.Empty);

				loadPhotosViaWebRequest(pageNumber);
			}
		}
	}

	public class EventPhotoProvider : PhotoProvider
	{
		Array photoSetsByGalleryK;
		Array photoSetsByEventK;

		int galleryK;
		int eventK;

		public EventPhotoProvider(int galleryK, int eventK)
		{
			this.galleryK = galleryK;
			this.eventK = eventK;
			this.photoSetsByGalleryK = new Array();
			this.photoSetsByEventK = new Array();
		}

		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			if (galleryK > 0)
			{
				Service.GetPhotosByGalleryAndPage(galleryK, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
			}
			else
			{
				Service.GetPhotosByEventAndPage(eventK, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
			}
		}

		#region Cache Load/Store
		protected override bool loadPhotosFromCache(int pageNumber)
		{
			// first check gallery
			if (galleryK > 0)
			{
				if ((photoSetsByGalleryK[galleryK] != null) && (((Array)photoSetsByGalleryK[galleryK])[pageNumber] != null))
				{
					CurrentPhotoSet = (PhotoResult)((Array)photoSetsByGalleryK[galleryK])[pageNumber];
					return true;
				}
			}
			// if gallery is -1, try for all photos from event
			else if (eventK > 0 && (photoSetsByEventK[eventK] != null) && (((Array)photoSetsByEventK[eventK])[pageNumber] != null))
			{
				CurrentPhotoSet = (PhotoResult)((Array)photoSetsByEventK[eventK])[pageNumber];
				return true;
			}
			return false;
		}
		protected override bool storePhotosInCache(PhotoResult photos, int pageNumber)
		{
			if (galleryK > 0)
			{
				if (photoSetsByGalleryK[galleryK] == null)
					photoSetsByGalleryK[galleryK] = new Array();
				((Array)photoSetsByGalleryK[galleryK])[pageNumber] = photos;
				return true;
			}
			if (eventK > 0)
			{
				if (photoSetsByEventK[eventK] == null)
					photoSetsByEventK[eventK] = new Array();
				((Array)photoSetsByEventK[eventK])[pageNumber] = photos;
				return true;
			}
			return false;
		}
		#endregion

		public void setGallery(int galleryK)
		{
			this.galleryK = galleryK;
			LoadPhotos(1);
		}

	}
	public abstract class SingleKeyPhotoProvider : PhotoProvider
	{
		Array photoSets;
		protected int key;

		public SingleKeyPhotoProvider(int key)
		{
			this.key = key;
			this.photoSets = new Array();
		}

		protected override bool storePhotosInCache(PhotoResult photoSet, int pageNumber)
		{
			if (this.key > 0)
			{
				if (photoSets[key] == null)
					photoSets[key] = new Array();
				((Array)photoSets[key])[pageNumber] = photoSet;
				return true;
			}
			return false;
		}

		protected override bool loadPhotosFromCache(int pageNumber)
		{
			if (this.key > 0 && photoSets[key] != null && ((Array)photoSets[key])[pageNumber] != null)
			{
				CurrentPhotoSet = (PhotoResult)((Array)photoSets[key])[pageNumber];
				return true;
			}
			return false;
		}
	}

	public class ArticlePhotoProvider : SingleKeyPhotoProvider
	{
		public ArticlePhotoProvider(int k) : base(k) { }
		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetPhotosByArticle(this.key, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}
	public class TagPhotoProvider : SingleKeyPhotoProvider
	{
		public TagPhotoProvider(int k) : base(k) { }
		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetPhotosByTag(this.key, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}
	public class GroupPhotoProvider : SingleKeyPhotoProvider
	{
		public GroupPhotoProvider(int k) : base(k) { }
		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetPhotosByGroup(this.key, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}
	public class PhotosOfUsrProvider : SingleKeyPhotoProvider
	{
		private readonly int spottedByUsrK;
		public PhotosOfUsrProvider(int k, int spottedByUsrK) : base(k) { this.spottedByUsrK = spottedByUsrK; }
		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetPhotosOfUsr(this.key, pageNumber, spottedByUsrK, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}
	public class FavouritePhotosOfUsrProvider : SingleKeyPhotoProvider
	{
		public FavouritePhotosOfUsrProvider(int k) : base(k) { }
		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetFavouritePhotosOfUsr(this.key, pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}

	public class VideoPhotoProvider : PhotoProvider
	{
		private Array videos;
		public VideoPhotoProvider()
		{
			this.videos = new Array();
		}

		protected override bool storePhotosInCache(PhotoResult photoSet, int pageNumber)
		{
			videos[pageNumber] = photoSet;
			return true;
		}

		protected override bool loadPhotosFromCache(int pageNumber)
		{
			if (videos[pageNumber] != null)
			{
				CurrentPhotoSet = (PhotoResult)videos[pageNumber];
				return true;
			}
			return false;
		}

		protected override void loadPhotosViaWebRequest(int pageNumber)
		{
			Service.GetRecentVideos(pageNumber, successCallback, Trace.WebServiceFailure, pageNumber, -1);
		}
	}
}

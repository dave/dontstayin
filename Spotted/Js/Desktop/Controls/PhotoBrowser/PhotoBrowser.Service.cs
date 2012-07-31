using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.PhotoControl;

namespace Js.Controls.PhotoBrowser
{
	public class Service
	{
		public static void GetRecentVideos(Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetRecentVideos",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetRecentVideos");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosByEventAndPage(Int32 eventK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["eventK"] = eventK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosByEventAndPage",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosByEventAndPage");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosByGalleryAndPage(Int32 galleryK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["galleryK"] = galleryK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosByGalleryAndPage",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosByGalleryAndPage");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosByArticle(Int32 articleK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["articleK"] = articleK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosByArticle",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosByArticle");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosOfUsr(Int32 usrK, Int32 pageNumber, Int32 spottedByUsrK, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["usrK"] = usrK;
			p["pageNumber"] = pageNumber;
			p["spottedByUsrK"] = spottedByUsrK;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosOfUsr",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosOfUsr");
				};
			jQuery.Ajax(o);
		}

		public static void GetFavouritePhotosOfUsr(Int32 usrK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["usrK"] = usrK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetFavouritePhotosOfUsr",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetFavouritePhotosOfUsr");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosByGroup(Int32 groupK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["groupK"] = groupK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosByGroup",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosByGroup");
				};
			jQuery.Ajax(o);
		}

		public static void GetPhotosByTag(Int32 tagK, Int32 pageNumber, ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["tagK"] = tagK;
			p["pageNumber"] = pageNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPhotosByTag",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PhotoResult)((Dictionary<string, object>)data)["d"], userContext, "GetPhotosByTag");
				};
			jQuery.Ajax(o);
		}

		public static void SetAsPhotoOfWeek(Int32 photoK, Boolean isPhotoOfWeek, String photoOfWeekCaption, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["photoK"] = photoK;
			p["isPhotoOfWeek"] = isPhotoOfWeek;
			p["photoOfWeekCaption"] = photoOfWeekCaption;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetAsPhotoOfWeek",
				"/WebServices/Controls/PhotoBrowser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "SetAsPhotoOfWeek");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServicePhotoResultWebServiceSuccessCallback(PhotoResult result, object userContext, string methodName);
	public delegate void ServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
}


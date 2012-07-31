using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.PhotoControl;

namespace Js.Controls.PhotoControl
{
	public class Service
	{
		public static void GetBanners(String placeholderClientID, ServiceBannerStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["placeholderClientID"] = placeholderClientID;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBanners",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((BannerStub[])((Dictionary<string, object>)data)["d"], userContext, "GetBanners");
				};
			jQuery.Ajax(o);
		}

		public static void RegisterBannerHit(Int32 bannerK, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["bannerK"] = bannerK;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"RegisterBannerHit",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "RegisterBannerHit");
				};
			jQuery.Ajax(o);
		}

		public static void IncrementViews(Int32 photoK, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["photoK"] = photoK;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"IncrementViews",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "IncrementViews");
				};
			jQuery.Ajax(o);
		}

		public static void SetIsFavouritePhoto(Int32 photoK, Boolean isFavourite, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["photoK"] = photoK;
			p["isFavourite"] = isFavourite;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetIsFavouritePhoto",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "SetIsFavouritePhoto");
				};
			jQuery.Ajax(o);
		}

		public static void SetCurrentUsrSpottedInPhoto(Int32 photoK, Boolean isInPhoto, ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["photoK"] = photoK;
			p["isInPhoto"] = isInPhoto;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetCurrentUsrSpottedInPhoto",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String[])((Dictionary<string, object>)data)["d"], userContext, "SetCurrentUsrSpottedInPhoto");
				};
			jQuery.Ajax(o);
		}

		public static void SetUsrSpottedInPhoto(Int32 spottedUsrK, Int32 photoK, Boolean isInPhoto, ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["spottedUsrK"] = spottedUsrK;
			p["photoK"] = photoK;
			p["isInPhoto"] = isInPhoto;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetUsrSpottedInPhoto",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String[])((Dictionary<string, object>)data)["d"], userContext, "SetUsrSpottedInPhoto");
				};
			jQuery.Ajax(o);
		}

		public static void SetAsCompetitionGroupPhoto(Int32 photoK, Boolean isCompetitionPhoto, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["photoK"] = photoK;
			p["isCompetitionPhoto"] = isCompetitionPhoto;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetAsCompetitionGroupPhoto",
				"/WebServices/Controls/PhotoControl/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "SetAsCompetitionGroupPhoto");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServiceBannerStubArrayWebServiceSuccessCallback(BannerStub[] result, object userContext, string methodName);
	public delegate void ServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
	public delegate void ServiceStringArrayWebServiceSuccessCallback(String[] result, object userContext, string methodName);
}


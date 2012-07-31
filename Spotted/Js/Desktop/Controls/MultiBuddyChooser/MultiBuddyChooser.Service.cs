using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.MultiBuddyChooser;

namespace Js.Controls.MultiBuddyChooser
{
	public class Service
	{
		public static void GetPlacesAndMusicTypes(ServiceGetMusicTypesAndPlacesResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			

			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPlacesAndMusicTypes",
				"/WebServices/Controls/MultiBuddyChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((GetMusicTypesAndPlacesResult)((Dictionary<string, object>)data)["d"], userContext, "GetPlacesAndMusicTypes");
				};
			jQuery.Ajax(o);
		}

		public static void ResolveUsrsFromMultiBuddyChooserValues(String[] values, ServiceDictionaryWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["values"] = values;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"ResolveUsrsFromMultiBuddyChooserValues",
				"/WebServices/Controls/MultiBuddyChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Dictionary<string, object>)((Dictionary<string, object>)data)["d"], userContext, "ResolveUsrsFromMultiBuddyChooserValues");
				};
			jQuery.Ajax(o);
		}

		public static void GetBuddiesSelectListHtml(ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			

			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBuddiesSelectListHtml",
				"/WebServices/Controls/MultiBuddyChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "GetBuddiesSelectListHtml");
				};
			jQuery.Ajax(o);
		}

		public static void CreateUsrFromEmailAndReturnK(String textEnteredByUser, ServiceInt32WebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["textEnteredByUser"] = textEnteredByUser;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreateUsrFromEmailAndReturnK",
				"/WebServices/Controls/MultiBuddyChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Int32)((Dictionary<string, object>)data)["d"], userContext, "CreateUsrFromEmailAndReturnK");
				};
			jQuery.Ajax(o);
		}

		public static void CreateUsrsFromEmails(String spaceSeparatedListOfEmailAddresses, Boolean addAsBuddies, ServiceInt32WebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["spaceSeparatedListOfEmailAddresses"] = spaceSeparatedListOfEmailAddresses;
			p["addAsBuddies"] = addAsBuddies;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreateUsrsFromEmails",
				"/WebServices/Controls/MultiBuddyChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Int32)((Dictionary<string, object>)data)["d"], userContext, "CreateUsrsFromEmails");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServiceGetMusicTypesAndPlacesResultWebServiceSuccessCallback(GetMusicTypesAndPlacesResult result, object userContext, string methodName);
	public delegate void ServiceDictionaryWebServiceSuccessCallback(Dictionary<string, object> result, object userContext, string methodName);
	public delegate void ServiceStringWebServiceSuccessCallback(String result, object userContext, string methodName);
	public delegate void ServiceInt32WebServiceSuccessCallback(Int32 result, object userContext, string methodName);
}


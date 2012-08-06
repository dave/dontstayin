using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.AutoCompleteLibrary;

namespace Spotted
{
	public class GenericPage
	{
		public static void ClientRequest(String typeName, String methodName, Object[] args, GenericPageObjectWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["typeName"] = typeName;
			p["methodName"] = methodName;
			p["args"] = args;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"ClientRequest",
				"/GenericPage.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Object)((Dictionary<string, object>)data)["d"], userContext, "ClientRequest");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void GenericPageObjectWebServiceSuccessCallback(Object result, object userContext, string methodName);
}


namespace Spotted.WebServices
{
	public class AutoComplete
	{
		public static void GetTags(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetTags",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetTags");
				};
			jQuery.Ajax(o);
		}

		public static void GetTagSearchString(String prefixText, Int32 count, AutoCompleteStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["prefixText"] = prefixText;
			p["count"] = count;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetTagSearchString",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String[])((Dictionary<string, object>)data)["d"], userContext, "GetTagSearchString");
				};
			jQuery.Ajax(o);
		}

		public static void GetGroupMembers(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetGroupMembers",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetGroupMembers");
				};
			jQuery.Ajax(o);
		}

		public static void GetUsrsPublic(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetUsrsPublic",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetUsrsPublic");
				};
			jQuery.Ajax(o);
		}

		public static void GetBuddiesThenUsrs(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBuddiesThenUsrs",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetBuddiesThenUsrs");
				};
			jQuery.Ajax(o);
		}

		public static void GetBuddies(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBuddies",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetBuddies");
				};
			jQuery.Ajax(o);
		}

		public static void GetBrands(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBrands",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetBrands");
				};
			jQuery.Ajax(o);
		}

		public static void GetPromotersWithK(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPromotersWithK",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetPromotersWithK");
				};
			jQuery.Ajax(o);
		}

		public static void GetUsersWithK(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetUsersWithK",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetUsersWithK");
				};
			jQuery.Ajax(o);
		}

		public static void GetPlacesEnabled(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPlacesEnabled",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetPlacesEnabled");
				};
			jQuery.Ajax(o);
		}

		public static void GetVenuesFull(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetVenuesFull",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetVenuesFull");
				};
			jQuery.Ajax(o);
		}

		public static void GetVenues(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetVenues",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetVenues");
				};
			jQuery.Ajax(o);
		}

		public static void GetEvents(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetEvents",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetEvents");
				};
			jQuery.Ajax(o);
		}

		public static void GetPlaces(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPlaces",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetPlaces");
				};
			jQuery.Ajax(o);
		}

		public static void GetSiteSearchResults(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetSiteSearchResults",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetSiteSearchResults");
				};
			jQuery.Ajax(o);
		}

		public static void GetGroups(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetGroups",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetGroups");
				};
			jQuery.Ajax(o);
		}

		public static void GetGroupsNoBrands(Int32 maxNumberOfItemsToGet, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetGroupsNoBrands",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetGroupsNoBrands");
				};
			jQuery.Ajax(o);
		}

		public static void GetObjects(String text, Int32 maxNumberOfItemsToGet, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["text"] = text;
			p["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetObjects",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetObjects");
				};
			jQuery.Ajax(o);
		}

		public static void GetCountries(Int32 get, String text, Dictionary<object, object> parameters, AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["get"] = get;
			p["text"] = text;
			p["parameters"] = parameters;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetCountries",
				"/WebServices/AutoComplete.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Suggestion[])((Dictionary<string, object>)data)["d"], userContext, "GetCountries");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void AutoCompleteSuggestionArrayWebServiceSuccessCallback(Suggestion[] result, object userContext, string methodName);
	public delegate void AutoCompleteStringArrayWebServiceSuccessCallback(String[] result, object userContext, string methodName);
}


using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.PlacesChooser;

namespace Js.Controls.PlacesChooser
{
	public class Service
	{
		public static void GetPlaces(Double north, Double south, Double east, Double west, Int32 maximumNumber, ServicePlaceStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["north"] = north;
			p["south"] = south;
			p["east"] = east;
			p["west"] = west;
			p["maximumNumber"] = maximumNumber;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPlaces",
				"/WebServices/Controls/PlacesChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PlaceStub[])((Dictionary<string, object>)data)["d"], userContext, "GetPlaces");
				};
			jQuery.Ajax(o);
		}

		public static void GetSurroundingPlaces(Int32 centredOnPlaceK, Int32 numberOfPlacesToGet, ServicePlaceStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["centredOnPlaceK"] = centredOnPlaceK;
			p["numberOfPlacesToGet"] = numberOfPlacesToGet;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetSurroundingPlaces",
				"/WebServices/Controls/PlacesChooser/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PlaceStub[])((Dictionary<string, object>)data)["d"], userContext, "GetSurroundingPlaces");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServicePlaceStubArrayWebServiceSuccessCallback(PlaceStub[] result, object userContext, string methodName);
}


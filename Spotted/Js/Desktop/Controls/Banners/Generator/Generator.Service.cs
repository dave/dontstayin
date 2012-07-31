using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.Banners.Generator;

namespace Js.Controls.Banners.Generator
{
	public class Service
	{
		public static void GetBanner(Int32 positionAsInt, String relevantPlacesCsv, String relevantMusicTypesCsv, ServiceBannerRenderInfoWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["positionAsInt"] = positionAsInt;
			p["relevantPlacesCsv"] = relevantPlacesCsv;
			p["relevantMusicTypesCsv"] = relevantMusicTypesCsv;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetBanner",
				"/WebServices/Controls/Banners/Generator/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((BannerRenderInfo)((Dictionary<string, object>)data)["d"], userContext, "GetBanner");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServiceBannerRenderInfoWebServiceSuccessCallback(BannerRenderInfo result, object userContext, string methodName);
}


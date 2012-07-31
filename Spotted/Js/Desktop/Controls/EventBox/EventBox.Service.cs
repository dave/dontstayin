using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.EventBox;

namespace Js.Controls.EventBox
{
	public class Service
	{
		public static void GetEventPage(String key, ServiceEventPageStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["key"] = key;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetEventPage",
				"/WebServices/Controls/EventBox/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((EventPageStub)((Dictionary<string, object>)data)["d"], userContext, "GetEventPage");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServiceEventPageStubWebServiceSuccessCallback(EventPageStub result, object userContext, string methodName);
}


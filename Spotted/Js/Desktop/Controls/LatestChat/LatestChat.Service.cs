using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.LatestChat;

namespace Js.Controls.LatestChat
{
	public class Service
	{
		public static void GetThreads(Int32 objectType, Int32 objectK, Int32 threadsCount, Boolean hasGroupObjectFilter, ServiceThreadStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["objectType"] = objectType;
			p["objectK"] = objectK;
			p["threadsCount"] = threadsCount;
			p["hasGroupObjectFilter"] = hasGroupObjectFilter;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetThreads",
				"/WebServices/Controls/LatestChat/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((ThreadStub[])((Dictionary<string, object>)data)["d"], userContext, "GetThreads");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ServiceThreadStubArrayWebServiceSuccessCallback(ThreadStub[] result, object userContext, string methodName);
}


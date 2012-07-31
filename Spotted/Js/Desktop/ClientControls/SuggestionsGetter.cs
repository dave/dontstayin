using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.AutoCompleteLibrary;

namespace Js.ClientControls
{

	public class SuggestionsGetter
	{
		public EventHandler SuggestionsGot = null;
		//WebRequest webRequest;
		jQueryXmlHttpRequest ajaxRequest;
		string webServiceUrl;
		string webServiceCommand;
		int maxNumberOfItemsToGet;
		public Suggestion[] Suggestions;
		public SuggestionsGetter(string webServiceUrl, string webServiceCommand, int maxNumberOfItemsToGet)
		{
			this.webServiceUrl = webServiceUrl;
			this.webServiceCommand = webServiceCommand;
			this.maxNumberOfItemsToGet = maxNumberOfItemsToGet;
		}
		public void RequestSuggestions(string textSoFar)
		{
			Dictionary<object, object> parameters = new Dictionary<object, object>();
			parameters["text"] = textSoFar;
			parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			CancelRequests();
		//	webRequest = WebServiceProxy.Invoke(webServiceUrl, webServiceCommand, true, parameters, this.SuccessCallback, this.FailureCallback, textSoFar, -1);

			jQueryAjaxOptions o = new jQueryAjaxOptions();
			o.Url = webServiceUrl + "/" + webServiceCommand;
			o.Timeout = 10000;
			o.Type = "POST";
			o.Async = true;
			o.Cache = false;
			o.ContentType = "application/json; charset=utf-8";
			o.Data = JSON.stringify(parameters);
			o.DataType = "json";
			o.Error = 
				delegate(jQueryXmlHttpRequest request, string error, System.Exception exception)
				{
					this.FailureCallback(
						new WebServiceError(
							exception.GetType().ToString(),
							error,
							exception.ToString(),
							request.Status,
							request.Status == 408),
						textSoFar,
						webServiceCommand);
				};
			o.Success =
			    delegate(object data, string textStatus, jQueryXmlHttpRequest request)
			    {
					this.SuccessCallback(((Dictionary<string, object>)data)["d"], textSoFar, webServiceCommand);
			    };
			ajaxRequest = jQuery.Ajax(o);
			
		}
		 

		public void CancelRequests()
		{
			if (ajaxRequest != null)
			{
				ajaxRequest.Abort();
				ajaxRequest = null;
			}
		}
		public void SuccessCallback(object result, object userContext, string methodName)
		{
			this.Suggestions = (Suggestion[])result;
			if (SuggestionsGot != null) { SuggestionsGot(this, EventArgs.Empty); }
		}
		public void FailureCallback(WebServiceError error, object userContext, string methodName) { }

	 
	}
}

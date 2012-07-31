using System;
using System.Collections.Generic;
using System.Html;
using Js.Library;
using jQueryApi;
using Js.AutoCompleteLibrary;

namespace Js.ClientControls.HtmlAutoComplete
{
	
	public class WebServiceRemoteSuggestionsGetter : RemoteSuggestionsGetter
	{
		string url;
		public string MethodName;
		//WebRequest webRequest;
		jQueryXmlHttpRequest ajaxRequest;
		internal WebServiceRemoteSuggestionsGetter(string url, string methodName)
		{
			this.url = url;
			this.MethodName = methodName;
		}

		protected override void MakeRequest(string text, Dictionary<object, object> requestParameters, int maxNumberOfItemsToGet)
		{
			Dictionary<object, object> parameters = new Dictionary<object, object>();
			parameters["text"] = text;
			parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			parameters["parameters"] = requestParameters;
			//if (webRequest != null && webRequest.Executor.Started) webRequest.Executor.Abort();
			//webRequest = WebServiceProxy.Invoke(
			//    url,
			//    this.MethodName,
			//    true,
			//    parameters, 
			//    this.SuccessCallback,
			//    Trace.WebServiceFailure, text, -1
			//);
			if (ajaxRequest != null)
			{
				try
				{
					ajaxRequest.Abort();
				}
				catch { }
			}

			jQueryAjaxOptions o = new jQueryAjaxOptions();
			o.Url = url + "/" + this.MethodName;
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
					//this.FailureCallback(
					//    new WebServiceError(
					//        exception.GetType().ToString(),
					//        error,
					//        exception.ToString(),
					//        request.Status,
					//        request.Status == 408),
					//    textSoFar,
					//    webServiceCommand);
				};
			o.Success =
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					this.SuccessCallback(((Dictionary<string, object>)data)["d"], text, this.MethodName);
				};
			ajaxRequest = jQuery.Ajax(o);

		}
		void SuccessCallback(object rawResult, object userContext, string methodName)
		{
			if (this.OnSuggestionReceived != null) OnSuggestionReceived((Suggestion[])rawResult);
			if (this.OnAllSuggestionsReceived != null) OnAllSuggestionsReceived();
		}
		 
		internal override bool IsMakingRequest
		{
			get
			{
				return ajaxRequest != null;
			}
		}
		internal override void DoAbortCurrentRequest()
		{
			if (ajaxRequest != null)
			{
				ajaxRequest.Abort();
				ajaxRequest = null;
			}
		}
	}
}

using System;
using System.DHTML;
using Sys.Net;
using Utils;

namespace ScriptSharpLibrary.HtmlAutoComplete
{
	
	public class WebServiceRemoteSuggestionsGetter : RemoteSuggestionsGetter
	{
		string url;
		public string MethodName;
		WebRequest webRequest;
		internal WebServiceRemoteSuggestionsGetter(string url, string methodName)
		{
			this.url = url;
			this.MethodName = methodName;
		}

		protected override void MakeRequest(string text, Dictionary requestParameters, int maxNumberOfItemsToGet)
		{
			Dictionary parameters = new Dictionary();
			parameters["text"] = text;
			parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			parameters["parameters"] = requestParameters;
			if (webRequest != null && webRequest.Executor.Started) webRequest.Executor.Abort();
			webRequest = WebServiceProxy.Invoke(
				url,
				this.MethodName,
				true,
				parameters, 
				this.SuccessCallback,
				Trace.WebServiceFailure, text, -1
			);
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
				return webRequest != null;
			}
		}
		internal override void DoAbortCurrentRequest()
		{
			if (webRequest != null)
			{
				webRequest.Executor.Abort();
				webRequest = null;
			}
		}
	}
}

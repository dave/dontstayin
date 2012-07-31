

using Sys.Net;
using System;
using Sys;
namespace ScriptSharpLibrary
{

	public class SuggestionsGetter
	{
		public EventHandler SuggestionsGot = null;
		WebRequest webRequest;
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
			Dictionary parameters = new Dictionary();
			parameters["text"] = textSoFar;
			parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			CancelRequests();
			webRequest = WebServiceProxy.Invoke(webServiceUrl, webServiceCommand, true, parameters, this.SuccessCallback, this.FailureCallback, textSoFar, -1);
		}
		 

		public void CancelRequests()
		{
			if (webRequest != null)
			{
				webRequest.Executor.Abort();
				webRequest = null;
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

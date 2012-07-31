using System;
using System.DHTML;
using Net.Comet;

namespace ScriptSharpLibrary.HtmlAutoComplete
{
	
	class CometRemoteSuggestionsGetter : RemoteSuggestionsGetter
	{
		string url;
		CometRequest cometRequest;
		internal CometRemoteSuggestionsGetter(string url)
		{
			this.url = url;
		}

		protected override void MakeRequest(string text, Dictionary parameters, int maxNumberOfItemsToGet)
		{
			string requestUrl = url + "?text=" + text.Escape() + "&maxNumberOfItemsToGet=" + maxNumberOfItemsToGet;

			foreach (DictionaryEntry entry in parameters)
			{
				requestUrl += "&" + entry.Key.Escape() + "=" + entry.Value.ToString().Escape();
			}
			this.cometRequest = CometProxy.Invoke(
				requestUrl,
				delegate(string message)
				{
					if (this.OnSuggestionReceived != null) OnSuggestionReceived(
						(Suggestion[])Script.Eval("(" + message + ")")
					);
				},
				delegate()
				{
					if (this.OnAllSuggestionsReceived != null) OnAllSuggestionsReceived();
				}
			);
		}
		internal override bool IsMakingRequest
		{
			get
			{
				return cometRequest != null;
			}
		}
		internal override void DoAbortCurrentRequest()
		{
			if (cometRequest != null)
			{
				cometRequest.Abort();
				cometRequest = null;
			}
		}
	}
}

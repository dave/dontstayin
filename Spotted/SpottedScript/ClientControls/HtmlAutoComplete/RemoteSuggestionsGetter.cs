using System;
using System.DHTML;
using Net.Comet;
using Utils;

namespace ScriptSharpLibrary.HtmlAutoComplete
{
	public delegate void SuggestionArrayAction(Suggestion[] suggestions);
	public abstract class RemoteSuggestionsGetter
	{
		
		public Action OnSuggestionsGot = null;
		public Action OnSuggestionsRequested = null;
		public Action OnAllSuggestionsReceived = null;
		public SuggestionArrayAction OnSuggestionReceived;
		public Action OnAbortCurrentRequest;
		internal void RequestSuggestions(string text, Dictionary parameters, int maxNumberOfItemsToGet)
		{
			
			if (IsMakingRequest)
			{
				AbortCurrentRequest();
			}
			if (text.Length < 50)
			{
				MakeRequest(text, parameters, maxNumberOfItemsToGet);
			}
			
			if (OnSuggestionsRequested != null) OnSuggestionsRequested();
		}

		protected abstract void MakeRequest(string text, Dictionary parameters, int maxNumberOfItemsToGet);
		internal abstract bool IsMakingRequest { get; }
		internal void AbortCurrentRequest()
		{
			DoAbortCurrentRequest();
			if (OnAbortCurrentRequest != null) OnAbortCurrentRequest();
		}
		internal abstract void DoAbortCurrentRequest();
	}
}

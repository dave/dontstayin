using System;
using System.Collections.Generic;
using System.Html;
//using Net.Comet;
using Js.Library;
using Js.AutoCompleteLibrary;

namespace Js.ClientControls.HtmlAutoComplete
{
	public delegate void SuggestionArrayAction(Suggestion[] suggestions);
	public abstract class RemoteSuggestionsGetter
	{
		
		public Action OnSuggestionsGot = null;
		public Action OnSuggestionsRequested = null;
		public Action OnAllSuggestionsReceived = null;
		public SuggestionArrayAction OnSuggestionReceived;
		public Action OnAbortCurrentRequest;
		internal void RequestSuggestions(string text, Dictionary<object, object> parameters, int maxNumberOfItemsToGet)
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

		protected abstract void MakeRequest(string text, Dictionary<object, object> parameters, int maxNumberOfItemsToGet);
		internal abstract bool IsMakingRequest { get; }
		internal void AbortCurrentRequest()
		{
			DoAbortCurrentRequest();
			if (OnAbortCurrentRequest != null) OnAbortCurrentRequest();
		}
		internal abstract void DoAbortCurrentRequest();
	}
}

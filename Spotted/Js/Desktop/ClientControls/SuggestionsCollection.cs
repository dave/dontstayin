using System;
using Js.Library;
using Js.AutoCompleteLibrary;

namespace Js.ClientControls
{
	public delegate void SuggestionsChanged();
	public class SuggestionsCollection
	{
		public SuggestionsChanged OnSuggestionsChanged = null;
		Array suggestions = new Array();
		public void Add(Suggestion newSuggestion)
		{
			AddWithoutSortOrNotify(newSuggestion);
			Sort();
			if (OnSuggestionsChanged != null) OnSuggestionsChanged();
		}

		private void AddWithoutSortOrNotify(Suggestion newSuggestion)
		{
			int i = 0;
			for (i = 0; i < suggestions.Length; i++)
			{
				Suggestion current = (Suggestion)suggestions[i];
				if (current.value == newSuggestion.value)
				{
					if (newSuggestion.priority < current.priority) { newSuggestion.priority = current.priority; }
					break;
				}
			}
			suggestions[i] = newSuggestion;
		}
		public void AddRange(Suggestion[] newSuggestions)
		{
			for (int i = 0; i < newSuggestions.Length; i++)
			{
				AddWithoutSortOrNotify(newSuggestions[i]);
			}
			Sort();
			if (OnSuggestionsChanged != null) OnSuggestionsChanged();
		}
		private void Sort()
		{
			suggestions.Sort
			 (
				 delegate(object a, object b)
				 {
					 return ((Suggestion)b).priority - ((Suggestion)a).priority;
				 }
			 );
		}
		public Suggestion this[int index]
		{
			get
			{
				return ((Suggestion)suggestions[index]);
			}
		}
		public void RemoveAt(int index)
		{
			int length = suggestions.Length;
			Array newSuggestions = new Array();
			for (int i = 0; i < suggestions.Length; i++)
			{
				if (i == index) { continue; }
				newSuggestions[newSuggestions.Length] = suggestions[i];
			}
			suggestions = newSuggestions;
			if (length != suggestions.Length && OnSuggestionsChanged != null) OnSuggestionsChanged();
			
		}
		public void Clear()
		{
			suggestions = new Array();
		}
		public int Count
		{
			get
			{
				return suggestions.Length;
			}
		}
	}
}

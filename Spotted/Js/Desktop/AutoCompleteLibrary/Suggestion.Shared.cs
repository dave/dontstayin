using System;
#if NOTSCRIPT
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Collections;
#endif

namespace Js.AutoCompleteLibrary
{
#if NOTSCRIPT
	[Serializable]
	public partial class Suggestion : IHasPriority
#else
	public partial class Suggestion
#endif
	{
		public string html;
		public string text;
		public string value;
		public int priority;
		public static string GetPicTitleDetailTemplateHtml(string imageSrc, string title, string detail)
		{
			return String.Format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 style='border-right:1px solid #999999;display:block;'/></td><td style='padding:2px 3px 2px 3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>",
				imageSrc,
				title,
				detail
			);
		}
		public static void AddSuggestion(Suggestion[] suggestions, Suggestion suggestion, int maxNumberOfItemsToGet)
		{
			suggestion.priority = (suggestions.Length > 0) ? suggestions[suggestions.Length - 1].priority : 1;
			suggestions[suggestions.Length < maxNumberOfItemsToGet ? suggestions.Length : suggestions.Length - 1] = suggestion;
		}
#if SCRIPT
		public static void AddSuggestionAtTop(Suggestion[] suggestions, Suggestion suggestion)
		{
			suggestion.priority = (suggestions.Length > 0) ? (suggestions[0].priority + 1): 1;
			for (int i = suggestions.Length; i > 0; i--)
			{
				Suggestion temp = suggestions[i - 1];
				suggestions[i - 1] = null;
				suggestions[i] = temp;
			}
			suggestions[0] = suggestion;
		}
#endif
#if NOTSCRIPT

		#region IHasPriority Members
		public int Priority
		{
			get { return this.priority; }
		}
		#endregion

		public class SuggestionsComparer : IEqualityComparer<Suggestion>
		{
			public bool Equals(Suggestion x, Suggestion y)
			{
				return (x.html.Equals(y.html, StringComparison.InvariantCultureIgnoreCase) || x.value.Equals(y.value, StringComparison.InvariantCultureIgnoreCase));
			}
			public int GetHashCode(Suggestion obj)
			{
				return obj.GetHashCode();
			}
		}
	
#endif
	}
}

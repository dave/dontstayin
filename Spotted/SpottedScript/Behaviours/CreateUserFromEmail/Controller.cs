using System;
using ScriptSharpLibrary;
using Spotted.WebServices.Controls.MultiBuddyChooser;
using Utils;
namespace SpottedScript.Behaviours.CreateUserFromEmail
{
	public class Controller
	{
		KeyStringPairAction oldItemChosen;
		HtmlAutoCompleteBehaviour selector;
		private Action oldOnSuggestionsRequested;

		internal Controller(HtmlAutoCompleteBehaviour selector)
		{
			this.selector = selector;
			this.oldOnSuggestionsRequested = selector.OnSuggestionsRequested;
			selector.OnSuggestionsRequested = CheckForEmailAddress;
			
			//oldItemChosen = selector.ItemChosen;
			//selector.ItemChosen = ItemChosen;
		}
		void ItemChosen(KeyStringPair item)
		{
			
			if (item.Value.StartsWith("{'email':"))
			{
				EmailSuggestionValue value = (EmailSuggestionValue)Script.Eval("(" + item.Value + ")");
				Service.CreateUsrFromEmailAndReturnK(
					value.email,
					CreateUsrFromEmailAndReturnKSuccessCallback,
					Trace.WebServiceFailure,
					item,
					3000
				);

			}
			else
			{
				if (oldItemChosen != null) { oldItemChosen(item); }
			}
		}
		void CreateUsrFromEmailAndReturnKSuccessCallback(Int32 result, object userContext, string methodName)
		{
			KeyStringPair pair = (KeyStringPair)userContext;
			pair.Value = result.ToString();
			this.selector.Value = pair.Value;
			if (oldItemChosen != null) { oldItemChosen(pair); }
		}
		const string EmailRegex = @"^[^\@\s]+\@[A-Za-z0-9\-]{1}[.A-Za-z0-9\-]+\.[.A-Za-z0-9\-]*[A-Za-z0-9]$";
		void CheckForEmailAddress()
		{
			Trace.Write("CheckingForEmailAddress");
			RegularExpression regex = new RegularExpression(EmailRegex);
			string email = selector.Text.Trim();
			if (regex.Test(email))
			{
				Suggestion suggestion = new Suggestion();
				suggestion.html = GetPicTitleDetailTemplateHtml(@"/gfx/icon40-inbox.png", "Add " + selector.Text.Trim() + " as a buddy", "When they join DontStayIn they will be added as one of your buddies.  If you type a name after the email address you can use that in future to find this person.");
				suggestion.text = selector.Text;
				suggestion.value = "{'email': '" + selector.Text.Escape() + "'}";
				suggestion.priority = selector.Text.Length * 100;
				for (int i = 0; i < selector.Suggestions.Count; i++)
				{
					if (selector.Suggestions[i].value.StartsWith("{'email': "))
					{
						selector.Suggestions.RemoveAt(i);
						break;
					}
				}
				selector.AddSuggestion(suggestion);
				selector.DisplaySuggestionsInPopupMenu();
			}
			if (oldOnSuggestionsRequested != null) oldOnSuggestionsRequested();
		}
		 
		static string GetPicTitleDetailTemplateHtml(string imageSrc, string title, string detail)
		{
			
			return String.Format("<table cellspacing='0' cellpadding='0'><tr><td width='40'><img src='{0}' border=0 width=40 hspace=0 height=40 /></td><td style='padding:3px;' valign='top'><b>{1}</b><br />{2}</td></tr></table>",
				imageSrc,
				title,
				detail
			);
		}
	}
}

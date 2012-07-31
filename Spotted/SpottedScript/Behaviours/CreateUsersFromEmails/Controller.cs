using System;
using ScriptSharpLibrary;
using Spotted.WebServices.Controls.MultiBuddyChooser;
using Utils;
namespace SpottedScript.Behaviours.CreateUsersFromEmails
{
	public class Controller
	{
		
		private Action oldOnSuggestionsRequested;
		HtmlAutoCompleteBehaviour selector;
		internal Controller(HtmlAutoCompleteBehaviour selector)
		{
			this.selector = selector;
			
			this.oldOnSuggestionsRequested = selector.OnSuggestionsRequested;
			selector.OnSuggestionsRequested = CheckForEmailAddresses;
			
		
			
		}
		
		private const string EmailsRegex = @"^( *([^\@\s]+\@[A-Za-z0-9\-]{1}[.A-Za-z0-9\-]+\.[.A-Za-z0-9\-]*[A-Za-z0-9]) *){2,}$";
	 
		void CheckForEmailAddresses()
		{
			Trace.Write("CheckingForEmailAddresses");
			RegularExpression regExp = new RegularExpression(@"\s|,|;| +", "g");
			string text = selector.Text.Replace(regExp, " ");
			while (text.IndexOf("  ") > -1)
			{
				text = text.Replace("  ", " ");
			}
			text = text.Trim();
			for (int i = selector.Suggestions.Count - 1; i > -1; i--)
			{
				Trace.Write(selector.Suggestions[i].value);
				if (selector.Suggestions[i].value.StartsWith("{'emails': "))
				{
					selector.Suggestions.RemoveAt(i);
					break;
				}
			}
			RegularExpression regex = new RegularExpression(EmailsRegex);
			if (regex.Test(text))
			{
				selector.Text = text;
				selector.Suggestions.Clear();
				string[] emails = text.Split(" ");
				Suggestion suggestion = new Suggestion();
				suggestion.html = GetPicTitleDetailTemplateHtml(@"/gfx/icon40-inbox.png", "Add " + emails.Length + " email addresses as buddies", "Next time you want to include these email addresses, just add all your buddies and they will be included.");
				suggestion.text = emails.Length + " email addresses as buddies";
				suggestion.value = "{'emails': '" + text.Escape() + "', 'buddies':'true'}";
				suggestion.priority = selector.Text.Length * 100;
				selector.AddSuggestion(suggestion);

				Suggestion suggestion2 = new Suggestion();
				suggestion2.html = GetPicTitleDetailTemplateHtml(@"/gfx/icon40-inbox.png", "Add " + emails.Length + " email addresses, but NOT as buddies", "Next time you want to include these email addresses you'll have to copy them in again");
				suggestion2.text = emails.Length + " email addresses";
				suggestion2.value = "{'emails': '" + text.Escape() + "', 'buddies':'false'}";
				suggestion2.priority = selector.Text.Length * 100;
				selector.AddSuggestion(suggestion2);
				
				
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
